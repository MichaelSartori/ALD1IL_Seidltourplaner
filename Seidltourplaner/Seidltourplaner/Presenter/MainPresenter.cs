using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Seidltourplaner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seidltourplaner
{
    internal class MainPresenter
    {
        // member variables
        private MainModel _model;
        private MainView _mainView;

        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay routes = new GMapOverlay("routes");

        List<int> m_indicesCheckedVertices;
        int m_indexStartStation;

        public MainPresenter(MainModel model, MainView view)
        {
            _model = model;
            _mainView = view;

            // connecting the subscribers to the events
            SetupLinks();

            m_indicesCheckedVertices = new List<int>();

        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public void Run()
        {
            // List of Stationnames for CheckedListBox in Mainview
            List<string> clbStations = new List<string>();

            // Visualisierung aktualisieren
            GMapMarker gMapMarkers;
            foreach (var item in _model.m_allVertices)
            {
                clbStations.Add(item.m_name);
                gMapMarkers = new GMarkerGoogle(item.m_coordinates, GMarkerGoogleType.red_dot);
                markers.Markers.Add(gMapMarkers);
            }

            _mainView.Show();
            _mainView.InitView(markers, clbStations);
            Application.Run();
        }

        /// <summary>
        /// connects the subscribers to the events
        /// </summary>
        private void SetupLinks()
        {
            // From GUI -> MainPresenter
            _mainView.OnCalculateRouteRequested += OnCalculateRoute;
            _mainView.CheckedStationsChanged += ChangeCheckedStations;
            _mainView.StartStationChanged += ChangeStartStation;

            ////von Markus
            //_mainView.UpdatePathTarget += UpdatePathTarget;

        }

        private void ChangeStartStation(object sender, int startStation)
        {
            m_indexStartStation = startStation;
        }

        private void ChangeCheckedStations(object sender, List<int> stations)
        {
            m_indicesCheckedVertices = stations;
        }

        private void OnCalculateRoute(object sender, EventArgs e)
        {
            Vertex startVertex = _model.m_allVertices[m_indexStartStation];
            routes.Clear();


            /// only for testing // ToDo Markus
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
            for (int i = 0; i < m_indicesCheckedVertices.Count; i++) { clickedVertices.Add(_model.m_allVertices[m_indicesCheckedVertices[i]]); }

            Dijkstra dijkstra = new Dijkstra(_model.m_allVertices);
            List<Vertex> path = null;
            List<string> pathToTarget = new List<string>();
            // Erster Startknoten
            Vertex actualStartNode = _model.m_allVertices[m_indexStartStation];
            pathToTarget.Add(actualStartNode.m_name);
            int j = 1;

            // Solange nicht alle gewünschten Knoten besucht wurden
            while (clickedVertices.Count > 1)
            {
                path = dijkstra.calculateNearestNode(actualStartNode, m_indicesCheckedVertices);
                for (int i = 0; i < path.Count-1; i++)
                {
                    int indexOfPathElement = _model.m_allVertices.IndexOf(path[i]);

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
                m_indicesCheckedVertices.Remove(_model.m_allVertices.IndexOf(actualStartNode));
                actualStartNode = path[path.Count - 1];
                pathToTarget.Add(actualStartNode.m_name);
            }      

            _mainView.UpdateView(markers, routes, distance, pathToTarget, error);
        }
    }
}
