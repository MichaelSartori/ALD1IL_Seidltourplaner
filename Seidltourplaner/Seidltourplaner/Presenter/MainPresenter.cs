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

        List<int> m_indexCheckedVertices;
        int m_indexStartStation;

        public MainPresenter(MainModel model, MainView view)
        {
            _model = model;
            _mainView = view;

            // connecting the subscribers to the events
            SetupLinks();

            m_indexCheckedVertices = new List<int>();

            //// Initialisiere GMap
            //GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //// Setze den Bing Maps API-Schlüssel
            //GMapProviders.BingMap.ClientKey = @"odSs6iYRjBisqTjOZeMV~X1lRoeYyJRrDQH6--qI7Rg~ArJ6lPStApT--whZWgV8ZwCZet2uRtMPANBkvwzkT-mFHR4wsDOF6sg2fZUuMB8R";
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

        }

        private void ChangeStartStation(object sender, int startStation)
        {
            m_indexStartStation = startStation;
        }

        private void ChangeCheckedStations(object sender, List<int> stations)
        {
            m_indexCheckedVertices = stations;
        }

        private void OnCalculateRoute(object sender, EventArgs e)
        {
            Vertex startVertex = _model.m_allVertices[m_indexStartStation];
            routes.Clear();


            /// only for testing // ToDo Markus
            int distance = 1098;
            bool error = false;

            for (int i = 0; i < m_indexCheckedVertices.Count - 1; i++)
            {
                Vertex actualVertex = _model.m_allVertices[m_indexCheckedVertices[i]];
                MapRoute calculatedRoute = actualVertex.m_neighborVertices[1].Item2;
                var GMapRoute = new GMapRoute(calculatedRoute.Points, i.ToString());
                routes.Routes.Add(GMapRoute);
            }
            /// End - only for testing


            _mainView.UpdateView(markers, routes, distance, error);
        }
        // Listen-Index wird einer Funktion in dieser Klasse übergeben übergeben 

        // return: Kilometer und geordnete Liste von Stationen wird zurückgegeben
    }
}
