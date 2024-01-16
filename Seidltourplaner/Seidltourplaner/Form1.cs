﻿using GMap.NET;
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
        GMapOverlay markers = new GMapOverlay("markers");
        Calculate calculateRoute = new Calculate();

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
            Vertex augustinerbraeu = new Vertex(47.80568, 13.03377, "Augustiner Bräu Kloster Mülln");
            Vertex watzmann = new Vertex(47.80101, 13.04686, "Watzmann Cultbar");
            Vertex partymaus = new Vertex(47.79197, 12.987, "Partymaus");
            Vertex schnaitlpub = new Vertex(47.80299, 13.04572, "Schnaitl Pub");
            Vertex rockhouse = new Vertex(47.80705, 13.05895, "Rockhouse Salzburg");
            Vertex weisse = new Vertex(47.80689, 13.05172, "Die Weisse");
            Vertex stiegelkeller = new Vertex(47.79645, 13.04801, "Stiegelkeller");
            Vertex urbankeller = new Vertex(47.80722, 13.06083, "Urbankeller Salzburg");
            Vertex cityalm = new Vertex(47.79979, 13.04678, "City Alm");
            Vertex omalleys = new Vertex(47.80012, 13.04607, "O'Malley's Irish Pub");
            Vertex flip = new Vertex(47.80114, 13.0389, "Flip");
            Vertex stiegelbrauwelt = new Vertex(47.79368, 13.02143, "Stiegl-Brauwelt");
            // Kanten anlegen
            // ToDo

            // Liste aller Knoten generieren
            allVertices.Add(monkeys);
            allVertices.Add(segabar);
            allVertices.Add(steinlechners);
            allVertices.Add(augustinerbraeu);
            allVertices.Add(watzmann);
            allVertices.Add(partymaus);
            allVertices.Add(schnaitlpub);
            allVertices.Add(rockhouse);
            allVertices.Add(weisse);
            allVertices.Add(urbankeller);
            allVertices.Add(stiegelkeller);
            allVertices.Add(cityalm);
            allVertices.Add(omalleys);
            allVertices.Add(flip);
            allVertices.Add(stiegelbrauwelt);


            // Visualisierung aktualisieren
            GMapMarker gMapMarkers;
            foreach (var item in allVertices)
            {
                ClbStations.Items.Add(item.m_name);
                gMapMarkers = new GMarkerGoogle(item.m_coordinates, GMarkerGoogleType.red_dot);
                markers.Markers.Add(gMapMarkers);
            }
            map.Overlays.Add(markers);
            map.ZoomAndCenterMarkers("markers");
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
                        indexStartVertex = i;
                    }
                }
                if(indexStartVertex == -1)
                {
                    DialogResult result = MessageBox.Show(
                        "Der Startpunkt muss auch in den zu besuchenden Stationen liegen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Vertex startVertex = allVertices[indexStartVertex];

                int distance = 0;
                if(!Calculate.CalculateRoute(ref allVertices, indexCheckedVertices, ref startVertex, out distance))
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

                // Routen in die Visualisierung einzeichnen
                var routes = new GMapOverlay("routes");
                Vertex actualVertex = startVertex;
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
    }
}
