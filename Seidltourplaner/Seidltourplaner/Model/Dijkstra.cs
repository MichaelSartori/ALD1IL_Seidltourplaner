using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner.Model
{
    internal class Dijkstra
    {
        // Membervariablen
        GMapOverlay m_markers = new GMapOverlay("markers");
        GMapOverlay m_routes = new GMapOverlay("routes");
        List<Vertex> m_allVertices = null;
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="allVertices">alle möglichen Knoten</param>
        public Dijkstra(List<Vertex> allVertices)
        {
            m_allVertices = allVertices;
        }

        /// <summary>
        /// Funktion berechnet von einem Startknoten aus den nächsten gewählten Knoten
        /// </summary>
        /// <param name="startNode">Startknoten</param>
        /// <param name="indicesOfClickedNodes">Liste der angehakten Knoten</param>
        /// <returns>Liste der Knoten von Startknoten bis zu nächstem gewählten Knoten</returns>
        public List<Vertex> CalculateNearestNode(Vertex startNode, List<int> indicesOfClickedNodes)
        {
            // Instanzieren 
            // Distanzen mit Anfangswerten initialisieren
            double[] distances = new double[m_allVertices.Count];
            for (int i = 0; i < distances.Length; i++) distances[i] = double.PositiveInfinity;
            distances[m_allVertices.IndexOf(startNode)] = 0;

            // Eleternknoten initialisieren
            int[] parents = new int[m_allVertices.Count];
            // Liste der bereits besuchten Knoten
            List<Vertex> visitedVertex = new List<Vertex>();

            // Knoten, der gerade untersucht wird
            Vertex actualVertexToCheck = null;

            // Alle Knoten die gecheckt werden müssen, Kopie von m_allVertices
            List<Vertex> vertexToCheck = new List<Vertex>();
            foreach (Vertex v in m_allVertices) vertexToCheck.Add(v);
            
            // Solange es Knoten gibt, die noch nicht untersucht wurden
            while (vertexToCheck.Count() > 0)
            {
                // Finden des Knotens mit der kleinsten Distanz zum Startknoten
                // (im ersten Durchlauf: actualVertexToCheck = startNode)
                actualVertexToCheck = FindNextVertex(distances, vertexToCheck);

                // Hinzufügen zur Liste der Besuchten Knoten
                visitedVertex.Add(actualVertexToCheck);
                // Löschen aus Liste der zu untersuchenden Knoten
                vertexToCheck.Remove(actualVertexToCheck);

                // Index des aktuellen Knotens
                int indexOfActualNode = m_allVertices.IndexOf(actualVertexToCheck);
                // Index des Nachbarknoten, wird in folgender foreach jeweils neu zugewiesen
                int indexOfNextNode;

                // Prüfen jedes Nachbarknotens von actualVertexToCheck
                foreach (Tuple<Vertex, MapRoute> v in actualVertexToCheck.m_neighborVertices)
                {
                    indexOfNextNode = m_allVertices.IndexOf(v.Item1);

                    // Falls aktuell gespeicherte Distanz von Startpunkt zu Nachbarknoten v gößer als
                    // Distanz von Startknoten zu actualVertexToCheck + Distanz von actualVertexToCheck zu Nachbarknoten v
                    if (distances[indexOfNextNode] > distances[indexOfActualNode] + v.Item2.Distance)
                    {
                        // Updaten der Distanzen und Elternknoten
                        distances[indexOfNextNode] = distances[indexOfActualNode] + v.Item2.Distance;
                        parents[indexOfNextNode] = indexOfActualNode;
                    }
                } 
            }

            // Kürzeste Distanzen zu allen anderen Knoten wurde ermittelt

            // Finden des nähesten Knotens, welcher ausgewählt wurde
            double dist = double.PositiveInfinity;
            Vertex nearestVertex = null;

            foreach (int i in indicesOfClickedNodes)
            {
                // Wenn Knoten mit kleinerer Distanz gefunden wurde, der nicht der Startknoten ist
                if (distances[i] < dist && m_allVertices.IndexOf(startNode) != i)
                {   
                    // dann ist dieser Knoten der aktuelle Kandidat für den nächsten Lösungsknoten
                    dist = distances[i];
                    nearestVertex = m_allVertices[i];
                }
            }

            // Pfad an Knoten von Startknoten zum nächsten Knoten aus gehackter Liste
            List<Vertex> path = new List<Vertex>();
            Vertex vertexToIterate = nearestVertex;
            // mit Dijkstra berechneter Knoten = letzter Knoten in Liste
            path.Insert(0, vertexToIterate);

            // Über Elternknoten den Pfad vervollständigen
            // bis Startknoten rückwärts iterieren
            while (parents[m_allVertices.IndexOf(vertexToIterate)] != m_allVertices.IndexOf(startNode))
            {
                vertexToIterate = m_allVertices[parents[m_allVertices.IndexOf(vertexToIterate)]];
                path.Insert(0, vertexToIterate);
            }
            // Startknoten = erster Knoten in Liste
            path.Insert(0, startNode);

            return path;
        }


        /// <summary>
        /// Finde den nächsten Knoten für den Dijkstra
        /// </summary>
        /// <param name="distances">double Array mit den aktuellen Distanzen der Knoten zum Startpunkt</param>
        /// <param name="VertexToCheck">Liste an Vertex die Kandidaten für den nächsten zu untersuchenden Knoten sind</param>
        /// <returns>nächsten zu untersuchenden Knoten</returns>
        private Vertex FindNextVertex(double[] distances, List<Vertex> VertexToCheck)
        {
            double dist = double.PositiveInfinity;
            Vertex nextVertex = null;

            foreach (Vertex v in VertexToCheck)
            {
                if (distances[m_allVertices.IndexOf(v)] < dist)
                {
                    dist = distances[m_allVertices.IndexOf(v)];
                    nextVertex = v;
                }
            }
            
            return nextVertex;
        }

        /// <summary>
        /// Funktion berechnet für einen Teilpfad des Lösungsweges die GMaps route
        /// </summary>
        /// <param name="path">Liste an Knoten die einen Teilpfad der Lösung repräsentiert</param>
        /// <param name="j">Index, gibt an wie vielter Teilpfad</param>
        /// <param name="dist">Out-Parameter, Distanz der Teilpfades des Lösungsweges</param>
        /// <returns>Liste an GMapRoute, welche in Visualisierung eingezeichnet werden</returns>
        public List<GMapRoute> GenerateRoutes(List<Vertex> path, int j, out int dist)
        {
            List<GMapRoute> routes = new List<GMapRoute>();
            dist = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                // aktueller Knoten im Pfad
                Vertex v1 = path[i];
                // nächter Knoten in Pfad
                Vertex v2 = path[i + 1];
                int k;

                // Finden des Indizes k, wo v1.m_neighborVertices[k].Item1 == v2
                for (k = 0; k < v1.m_neighborVertices.Count; k++)
                {
                    if (v1.m_neighborVertices[k].Item1 == v2) break;
                }

                // Teilroute zwischen zwei angehackten Knoten
                // Bsp. Teilroute zwischen Startknoten und kürzest entferntem Knoten aus der angehackten Liste
                MapRoute calculatedRoute = v1.m_neighborVertices[k].Item2;
                GMapRoute gMapRoute = new GMapRoute(calculatedRoute.Points, j.ToString());
                // Gesamtdistanz aktualisieren.
                dist = dist + Convert.ToInt32(calculatedRoute.Distance * 1000);
                // Teilroute hinzufügen 
                routes.Add(gMapRoute);
            }

            return routes;
        }
    }
}
