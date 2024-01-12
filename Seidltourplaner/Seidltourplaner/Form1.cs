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
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;
using GMapRoute = GMap.NET.WindowsForms.GMapRoute;

namespace Seidltourplaner
{
    public partial class Form1 : Form
    {
        List<Vertex> allVertices = new List<Vertex>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialisiere GMapControl
            map.MapProvider = GMapProviders.BingMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // Setze den Bing Maps API-Schlüssel
            GMapProviders.BingMap.ClientKey = @"odSs6iYRjBisqTjOZeMV~X1lRoeYyJRrDQH6--qI7Rg~ArJ6lPStApT--whZWgV8ZwCZet2uRtMPANBkvwzkT-mFHR4wsDOF6sg2fZUuMB8R";

            // Startup-Einstellungen
            map.DragButton = MouseButtons.Left;
            map.Position = new PointLatLng(47.7989424, 13.0477231); // Startup Location
            map.ShowCenter = false;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 15;

            // Anlegen der Knoten
            Vertex monkeys = new Vertex(47.80064507790365, 13.048103825465349, "Monkey cafe.bar");
            Vertex segabar = new Vertex(47.80004719908009, 13.046236505471862, "Segabar");
            Vertex steinlechners = new Vertex(47.798889396189196, 13.063847967838894, "Steinlechners");

            // Kanten anlegen
            // ToDo

            // Liste aller Knoten generieren
            allVertices.Add(monkeys);
            allVertices.Add(segabar);
            allVertices.Add(steinlechners);


            // Visualisierung aktualisieren
            GMapMarker gMapMarkers;
            GMapOverlay markers = new GMapOverlay("markers");
            foreach (var item in allVertices)
            {
                ClbStations.Items.Add(item.m_name);
                gMapMarkers = new GMarkerGoogle(item.m_coordinates, GMarkerGoogleType.red_dot);
                markers.Markers.Add(gMapMarkers);
            }
            map.Overlays.Add(markers);
            map.ZoomAndCenterMarkers("markers");


            //PointLatLng point1 = new PointLatLng(47.80004719908009, 13.046236505471862); // Segabar
            //PointLatLng point2 = new PointLatLng(47.80064507790365, 13.048103825465349); // Monkey
            //PointLatLng point3 = new PointLatLng(47.798889396189196, 13.063847967838894); // Steinlechners

            //GMapMarker marker1 = new GMarkerGoogle(point1, GMarkerGoogleType.red_dot);
            //GMapOverlay markers = new GMapOverlay("markers");
            //markers.Markers.Add(marker1);
            //marker1 = new GMarkerGoogle(point2, GMarkerGoogleType.red_dot);
            //markers.Markers.Add(marker1);
            //marker1 = new GMarkerGoogle(point3, GMarkerGoogleType.red_dot);
            //markers.Markers.Add(marker1);
            //map.Overlays.Add(markers);


            //var route1 = BingMapProvider.Instance.GetRoute(point1, point2, true, true, 15);
            //var route2 = BingMapProvider.Instance.GetRoute(point2, point3, true, true, 15);
            ////var route3 = GoogleMapProvider.Instance.GetRoute(point3, point4, false, true, 15);

            //var r1 = new GMapRoute(route1.Points, "My Route1");
            //var r2 = new GMapRoute(route2.Points, "My Route2");
            ////var r3 = new GMapRoute(route3.Points, "My Route3");

            //var routes = new GMapOverlay("routes");
            //routes.Routes.Add(r1);
            //routes.Routes.Add(r2);
            ////routes.Routes.Add(r3);

            //map.Overlays.Add(routes);
            //map.Zoom = 14;
            //map.Zoom = 15;
        }

        private void BtnCalculateRoute_Click(object sender, EventArgs e)
        {
            int numberCheckedIndices = ClbStations.CheckedIndices.Count;
            if (CbStart.Text != "" && numberCheckedIndices > 1)
            {
                // Liste mit angehackten Knoten erzeugen und Startknoten bestimmen
                List<Vertex> checkedVertices = new List<Vertex>();
                int indexStartVertex = 0;
                for (int i = 0; i < numberCheckedIndices; i++)
                {
                    checkedVertices.Add(allVertices[ClbStations.CheckedIndices[i]]);
                    if (ClbStations.CheckedItems[i].ToString() == CbStart.Text)
                    {
                        indexStartVertex = i;
                    }

                }
                Vertex startVertex = allVertices[indexStartVertex];

                // only for testing
                for (int i = 0; i < numberCheckedIndices-1; i++)
                {
                    checkedVertices[i].m_nextVertex = checkedVertices[i+1];
                }

                // Routen in der Visualisierung einzeichnen
                var routes = new GMapOverlay("routes");
                Vertex actualVertex = checkedVertices[0]; // Später auf "startVertex"
                for (int i = 0; i < numberCheckedIndices-1; i++)
                {
                    var calculatedRoute = BingMapProvider.Instance.GetRoute(actualVertex.m_coordinates, actualVertex.m_nextVertex.m_coordinates, true, true, 15);
                    var GMapRoute = new GMapRoute(calculatedRoute.Points, i.ToString());
                    routes.Routes.Add(GMapRoute);
                    actualVertex = actualVertex.m_nextVertex;
                }
                map.Overlays.Add(routes);
                map.ZoomAndCenterRoutes("routes");

            }
            else
            {
                // show message box
                DialogResult result = MessageBox.Show(
                    "Zu besuchende Stationen und Startpunk auswählen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
