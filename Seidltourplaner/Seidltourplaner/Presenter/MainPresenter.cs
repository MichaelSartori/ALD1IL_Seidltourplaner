using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Seidltourplaner.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seidltourplaner
{
    internal class MainPresenter
    {
        // Model und Presenter als Membervariablen
        private MainModel m_model;
        private MainView m_mainView;

        // Eingezeichnete Markierungen der Lokale
        GMapOverlay markers = new GMapOverlay("markers");
        // Eingezeichnete Routen zwischen den Lokalen
        GMapOverlay routes = new GMapOverlay("routes");

        // Liste mit Indizes aller ausgewählten Lokalen
        List<int> m_indicesCheckedVertices;
        // Index des Startlokals
        int m_indexStartStation;

        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public MainPresenter(MainModel model, MainView view)
        {
            m_model = model;
            m_mainView = view;

            // Die Subscribers mit den Events verknüpfen
            SetupLinks();

            m_indicesCheckedVertices = new List<int>();
        }

        /// <summary>
        /// Die Applikation starten
        /// </summary>
        public void Run()
        {
            // Liste der Lokalnamen für die CheckBox in der MainView
            List<string> clbStations = new List<string>();

            // Visualisierung aktualisieren
            GMapMarker gMapMarker;
            foreach (var item in m_model.m_allVertices)
            {
                // Die Namen aller Lokale hinzufügen
                clbStations.Add(item.m_name);
                // Die Standorte aller Lokale zu den Makierungen hinzufügen, die eingezeichnet werden sollten
                gMapMarker = new GMarkerGoogle(item.m_coordinates, GMarkerGoogleType.red_dot);
                // Hoovert man über eine Markierung auf der Karte wird Lokalname angezeigt
                gMapMarker.ToolTipText = item.m_name;
                markers.Markers.Add(gMapMarker);
            }

            // MainView initialisieren und Applikation starten
            m_mainView.Show();
            m_mainView.InitView(markers, clbStations);
            Application.Run();
        }

        /// <summary>
        /// Die Subscribers mit den Events verknüpfen
        /// </summary>
        private void SetupLinks()
        {
            // Von GUI -> MainPresenter
            m_mainView.OnCalculateRouteRequested += OnCalculateRoute;
            m_mainView.CheckedStationsChanged += ChangeCheckedStations;
            m_mainView.StartStationChanged += ChangeStartStation;

            ////von Markus
            //_mainView.UpdatePathTarget += UpdatePathTarget;

        }

        /// <summary>
        /// Index des Startlokals aktualisieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="startStation"></param>
        private void ChangeStartStation(object sender, int startStation)
        {
            m_indexStartStation = startStation;
        }

        /// <summary>
        /// Indizes der ausgewählten Lokale aktualisieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="stations"></param>
        private void ChangeCheckedStations(object sender, List<int> stations)
        {
            m_indicesCheckedVertices = stations;
        }

        /// <summary>
        /// Die kürzeste Abfolge an Stationen berechnen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCalculateRoute(object sender, EventArgs e)
        {
            // Startlokal als Knoten festlegen
            Vertex startVertex = m_model.m_allVertices[m_indexStartStation];

            // Alle eingezeichneten Routen löschen
            routes.Clear();


            /// only for testing
            int distance = 0;
            bool error = false;

            //for (int i = 0; i < m_indexCheckedVertices.Count - 1; i++)
            //{
            //    Vertex actualVertex = _model.m_allVertices[m_indexCheckedVertices[i]];
            //    MapRoute calculatedRoute = actualVertex.m_neighborVertices[1].Item2;
            //    var GMapRoute = new GMapRoute(calculatedRoute.Points, i.ToString());
            //    distance = distance + Convert.ToInt32(calculatedRoute.Distance * 1000);
            //    routes.Routes.Add(GMapRoute);
            //}
            /// End - only for testing
            /// 

            // Erstelle Liste mit allen Knoten die angehackt wurden
            List<Vertex> clickedVertices = new List<Vertex>();
            for (int i = 0; i < m_indicesCheckedVertices.Count; i++) clickedVertices.Add(m_model.m_allVertices[m_indicesCheckedVertices[i]]);

            // Dijkstra initialisieren
            Dijkstra dijkstra = new Dijkstra(m_model.m_allVertices);


            List<Vertex> path;
            List<string> pathToTarget = new List<string>();
            // Erster Startknoten
            Vertex actualStartNode = m_model.m_allVertices[m_indexStartStation];
            pathToTarget.Add(actualStartNode.m_name);
            int j = 1;

            // Solange nicht alle gewünschten Knoten besucht wurden
            while (clickedVertices.Count > 1)
            {
                path = dijkstra.calculateNearestNode(actualStartNode, m_indicesCheckedVertices);
                for (int i = 0; i < path.Count-1; i++)
                {
                    int indexOfPathElement = m_model.m_allVertices.IndexOf(path[i]);

                    Vertex v1 = path[i];
                    Vertex v2 = path[i+1];
                    int k;
                    for (k = 0; k < v1.m_neighborVertices.Count; k++)
                    {
                        if (v1.m_neighborVertices[k].Item1 == v2) break;                        
                    }

                    MapRoute calculatedRoute = v1.m_neighborVertices[k].Item2;
                    GMapRoute gMapRoute = new GMapRoute(calculatedRoute.Points, j.ToString());
                    distance = distance + Convert.ToInt32(calculatedRoute.Distance * 1000);
                    routes.Routes.Add(gMapRoute);
                }
                

                j++;
                
                clickedVertices.Remove(actualStartNode);
                m_indicesCheckedVertices.Remove(m_model.m_allVertices.IndexOf(actualStartNode));
                actualStartNode = path[path.Count - 1];
                pathToTarget.Add(actualStartNode.m_name);
            }      

            // Markierungen, Routen und Reihenfolge der Lokale in der MainView aktualisieren
            m_mainView.UpdateView(markers, routes, distance, pathToTarget, error);
        }
    }
}
