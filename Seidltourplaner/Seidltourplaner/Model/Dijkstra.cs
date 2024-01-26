using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner.Model
{
    internal class Dijkstra
    {
        GMapOverlay m_markers = new GMapOverlay("markers");
        GMapOverlay m_routes = new GMapOverlay("routes");
        List<Vertex> m_allVertices = null;
        //Vertex m_startVertex = null;
        //List<int> m_indexCheckedVertices = null;
        

        public Dijkstra(List<Vertex> allVertices)
        {
            m_allVertices = allVertices;
            //m_startVertex = m_allVertices[startVertex];
            //m_indexCheckedVertices = indexCheckedVertices;
        }

        public event EventHandler<List<int>> UpdatePathTarget;

        public List<Vertex> calculateNearestNode(Vertex startNode, List<int> indicesOfClickedNodes)
        {
            // Instanzieren 
            double[] distances = new double[m_allVertices.Count];
            for (int i = 0; i < distances.Length; i++) { distances[i] = double.PositiveInfinity; }
            distances[m_allVertices.IndexOf(startNode)] = 0;

            int[] parents = new int[m_allVertices.Count];
            List<Vertex> visitedVertex = new List<Vertex>();

            Vertex actualVertexToCheck = null;

            // Alle Knoten die gecheckt werden müssen, Kopie von m_allVertices
            List<Vertex> VertexToCheck = new List<Vertex>();
            foreach (Vertex v in m_allVertices)
            {
                VertexToCheck.Add(v);
            }
                        
            while (VertexToCheck.Count() > 0)
            {
                // Finden des Knotens mit der kleinsten Distanz zum Startknoten
                actualVertexToCheck = FindNextVertex(distances, VertexToCheck);

                // Hinzufügen zur Liste der Besuchten Knoten
                visitedVertex.Add(actualVertexToCheck);
                VertexToCheck.Remove(actualVertexToCheck);

                foreach (Tuple<Vertex, MapRoute> v in actualVertexToCheck.m_neighborVertices)
                {
                    int indexOfNextNode = m_allVertices.IndexOf(v.Item1);
                    int indexOfActualNode = m_allVertices.IndexOf(actualVertexToCheck);
                    if (distances[indexOfNextNode] > distances[indexOfActualNode] + v.Item2.Distance)
                    {
                        // Updaten
                        distances[indexOfNextNode] = distances[indexOfActualNode] + v.Item2.Distance;
                        parents[indexOfNextNode] = indexOfActualNode;
                    }
                } 
            }

            // Finden des nähesten Knotens, welcher ausgewählt wurde
            double dist = double.PositiveInfinity;
            Vertex nearestVertex = null;

            foreach (int i in indicesOfClickedNodes)
            {
                if (distances[i] < dist && m_allVertices.IndexOf(startNode) != i)
                {
                    dist = distances[i];
                    nearestVertex = m_allVertices[i];
                }
            }

            List<Vertex> path = new List<Vertex>();
            Vertex vertexToIterate = nearestVertex;
            path.Insert(0, vertexToIterate);

            while (parents[m_allVertices.IndexOf(vertexToIterate)] != m_allVertices.IndexOf(startNode))
            {
                vertexToIterate = m_allVertices[parents[m_allVertices.IndexOf(vertexToIterate)]];
                path.Insert(0, vertexToIterate);
            }
            path.Insert(0, startNode);

            return path;

        }


        // Finde den nächsten Knoten für den Dijkstra
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
    }
}
