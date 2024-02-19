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
using static GMap.NET.Entity.OpenStreetMapRouteEntity;

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

            // Dijkstra initialisieren
            Dijkstra dijkstra = new Dijkstra(m_model.m_allVertices);

            // Startwerte
            int distance = 0;
            int fullDistance = 0;
            bool error = false;

            // Erstelle Liste mit allen Knoten die angehackt wurden
            List<Vertex> clickedVertices = new List<Vertex>();
            for (int i = 0; i < m_indicesCheckedVertices.Count; i++) clickedVertices.Add(m_model.m_allVertices[m_indicesCheckedVertices[i]]);

            List<Vertex> path;
            // Lösungspfad, wie er in der Visualisierung später ausgegeben wird
            List<string> pathToTarget = new List<string>();

            // Erster Startknoten
            Vertex actualStartNode = m_model.m_allVertices[m_indexStartStation];
            // Erster Startknoten = erster Lösungsknoten
            pathToTarget.Add(actualStartNode.m_name);
            // Index der Teilrouten
            int j = 1;

            // Solange nicht alle gewünschten Knoten besucht wurden
            while (clickedVertices.Count > 1)
            {
                // Pfad an Knoten von Startknoten zum nächsten Knoten aus gehackter Liste
                path = dijkstra.CalculateNearestNode(actualStartNode, m_indicesCheckedVertices);

                List<GMapRoute> generatedRoutes = dijkstra.GenerateRoutes(path, j, out distance);
                foreach (GMapRoute route in generatedRoutes) routes.Routes.Add(route);            
                // Gesamtdistanz aktualisieren
                fullDistance = fullDistance + distance;
                // Index der Teilrouten erhöhen
                j++;

                // aktuellen Knoten aus Liste der angehackten Knoten entfernen
                clickedVertices.Remove(actualStartNode);
                m_indicesCheckedVertices.Remove(m_model.m_allVertices.IndexOf(actualStartNode));
                // nächster aktueller Knoten = letzer Knoten des berechneten Pfades
                actualStartNode = path[path.Count - 1];
                // nächster Knoten des Lösungspfades gefunden
                pathToTarget.Add(actualStartNode.m_name);
            }


            // Markierungen, Routen und Reihenfolge der Lokale in der MainView aktualisieren
            m_mainView.UpdateView(markers, routes, fullDistance, pathToTarget, error);
        }
    }
}
