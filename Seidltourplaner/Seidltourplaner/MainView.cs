using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;
using GMapRoute = GMap.NET.WindowsForms.GMapRoute;

namespace Seidltourplaner
{
    public partial class MainView : Form
    {
        //List<Vertex> allVertices = new List<Vertex>();
        //GMapOverlay markers = new GMapOverlay("markers");
        //MainPresenter calculateRoute = new MainPresenter();

        public MainView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event when a person should be added
        /// </summary>
        public event EventHandler OnCalculateRouteRequested;
        public event EventHandler<List<int>> CheckedStationsChanged;
        public event EventHandler<int> StartStationChanged;

        private void Form1_Load(object sender, EventArgs e)
        {
            map.MapProvider = GMapProviders.BingMap;

            // Startup-Settings
            map.DragButton = MouseButtons.Left;
            map.Position = new PointLatLng(47.7989424, 13.0477231); // Startup Location
            map.ShowCenter = false;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 15;

            // Anlegen der Knoten
            //Vertex monkeys = new Vertex(47.80064507790365, 13.048103825465349, "Monkey cafe.bar");
            //Vertex segabar = new Vertex(47.80004719908009, 13.046236505471862, "Segabar");
            //Vertex steinlechners = new Vertex(47.798889396189196, 13.063847967838894, "Steinlechners");

            // Kanten anlegen
            // ToDo

            // Liste aller Knoten generieren
            //allVertices.Add(monkeys);
            //allVertices.Add(segabar);
            //allVertices.Add(steinlechners);


            //// Visualisierung aktualisieren
            //GMapMarker gMapMarkers;
            //foreach (var item in allVertices)
            //{
            //    ClbStations.Items.Add(item.m_name);
            //    gMapMarkers = new GMarkerGoogle(item.m_coordinates, GMarkerGoogleType.red_dot);
            //    markers.Markers.Add(gMapMarkers);
            //}
            //map.Overlays.Add(markers);
            //map.ZoomAndCenterMarkers("markers");
        }

        private void BtnCalculateRoute_Click(object sender, EventArgs e)
        {
            int numberCheckedIndices = ClbStations.CheckedIndices.Count;
            if (CbStart.Text != "" && numberCheckedIndices > 1)
            {
                // Liste mit angehackten Knoten erzeugen und Startknoten bestimmen
                List<int> indexCheckedVertices = new List<int>();
                int indexStartVertex = -1;
                for (int i = 0; i < numberCheckedIndices; i++)
                {
                    indexCheckedVertices.Add(ClbStations.CheckedIndices[i]);
                    if (ClbStations.CheckedItems[i].ToString() == CbStart.Text)
                    {
                        indexStartVertex = ClbStations.CheckedIndices[i];
                    }
                }

                if (indexStartVertex == -1)
                {
                    DialogResult result = MessageBox.Show(
                        "Der Startpunkt muss auch in den zu besuchenden Stationen liegen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CheckedStationsChanged(this, indexCheckedVertices);
                StartStationChanged(this, indexStartVertex);
                OnCalculateRouteRequested(this, e);



                // ToDo
                //Vertex startVertex = allVertices[indexStartVertex];

                //int distance = 0;
                //if (!MainPresenter.CalculateRoute(ref allVertices, indexCheckedVertices, ref startVertex, out distance))
                //{
                //    // show message box
                //    DialogResult result = MessageBox.Show(
                //        "Berechnung fehlgeschlagen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //LblDistance.Text = distance.ToString() + " m";

                //// Routen und Marker in der Visualisierung löschen
                //map.Overlays.Clear();
                //map.Overlays.Add(markers);

                // Routen in die Visualisierung einzeichnen
                //var routes = new GMapOverlay("routes");
                //Vertex actualVertex = startVertex;
                //for (int i = 0; i < numberCheckedIndices-1; i++)
                //{
                //    var calculatedRoute = BingMapProvider.Instance.GetRoute(actualVertex.m_coordinates, actualVertex.m_nextVertex.m_coordinates, true, true, 15);
                //    var GMapRoute = new GMapRoute(calculatedRoute.Points, i.ToString());
                //    routes.Routes.Add(GMapRoute);
                //    actualVertex = actualVertex.m_nextVertex;
                //}
            }
            else
            {
                // show message box
                DialogResult result = MessageBox.Show(
                    "Zu besuchende Stationen und Startpunkt auswählen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CbStart_DropDown(object sender, EventArgs e)
        {
            CbStart.Items.Clear();
            foreach (var item in ClbStations.CheckedItems)
            { 
                CbStart.Items.Add(item.ToString());
            }
        }

        internal void UpdateView(GMapOverlay markers, GMapOverlay routes, int distance, bool error)
        {
            if (error)
            {
                // show message box
                DialogResult result = MessageBox.Show(
                    "Berechnung fehlgeschlagen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LblDistance.Text = distance.ToString() + " m";

            // Routen und Marker in der Visualisierung löschen
            map.Overlays.Clear();

            map.Overlays.Add(markers);

            map.Overlays.Add(routes);
            map.ZoomAndCenterRoutes("routes");
        }

        internal void InitView(GMapOverlay markers, List<string>allStations)
        {
            map.Overlays.Add(markers);
            map.ZoomAndCenterMarkers("markers");
            foreach (var item in allStations)
            {
                ClbStations.Items.Add(item);
            }
        }
    }
}
