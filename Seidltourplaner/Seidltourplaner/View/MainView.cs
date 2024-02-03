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
using System.Xml.Serialization;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;
using GMapRoute = GMap.NET.WindowsForms.GMapRoute;

namespace Seidltourplaner
{
    public partial class MainView : Form
    {
        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }

        // Events definieren
        public event EventHandler OnCalculateRouteRequested;
        public event EventHandler<List<int>> CheckedStationsChanged;
        public event EventHandler<int> StartStationChanged;

        /// <summary>
        /// Methode wird beim Laden der Visualisierung ausgeführt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Auswahl des Kartenmaterials
            map.MapProvider = GMapProviders.BingMap;

            // Festlegen der Standardeinstellung für die Map
            map.DragButton = MouseButtons.Left;
            map.Position = new PointLatLng(47.7989424, 13.0477231); // Startup Location
            map.ShowCenter = false;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 15;
        }

        /// <summary>
        /// Button "Route berechnen" wurde gedrückt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalculateRoute_Click(object sender, EventArgs e)
        {
            int numberCheckedIndices = ClbStations.CheckedIndices.Count;
            // Überprüfen, ob ausreichend Stationen und ein Startpunkt ausgewählt wurde
            if (CbStart.Text != "" && numberCheckedIndices > 1)
            {
                // Integer-Liste mit ausgewählten Lokalen erzeugen und Startlokal bestimmen
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

                // Überprüfen, ob der Startpunkt existiert
                if (indexStartVertex == -1)
                {
                    DialogResult result = MessageBox.Show(
                        "Der Startpunkt muss auch in den zu besuchenden Stationen liegen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Die ausgewählten Stationen im MainPresenter aktualisieren
                CheckedStationsChanged(this, indexCheckedVertices);
                // Den Startpunkt im MainPresenter aktualisieren
                StartStationChanged(this, indexStartVertex);
                // Die Stationsreihenfolge berechnen
                OnCalculateRouteRequested(this, e);
            }
            else
            {
                // MessageBox, wenn nicht ausreichend Stationen oder ein Startpunkt ausgewählt wurde
                DialogResult result = MessageBox.Show(
                    "Zu besuchende Stationen und Startpunkt auswählen!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// DropDown für Startpunkt wurde ausgewählt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbStart_DropDown(object sender, EventArgs e)
        {
            // In der List befinden sich jene Stationen, die in der Liste "Zu besuchende Pubs" ausgewählt wurden 
            CbStart.Items.Clear();
            foreach (var item in ClbStations.CheckedItems)
            { 
                CbStart.Items.Add(item.ToString());
            }
        }

        /// <summary>
        /// Aktualisieren der Visualisierung
        /// </summary>
        /// <param name="markers">Markierungen der Lokale</param>
        /// <param name="routes">Zu absolvierende Routen</param>
        /// <param name="distance">Insgesamte Distanz</param>
        /// <param name="path">Reihenfolge der Lokale</param>
        /// <param name="error"></param>
        internal void UpdateView(GMapOverlay markers, GMapOverlay routes, int distance, List<string> path, bool error)
        {
            if (error)
            {
                // MessageBox, wenn die Berechnung fehlgeschlagen ist
                DialogResult result = MessageBox.Show(
                    "Berechnung fehlgeschlagen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LblDistance.Text = distance.ToString() + " m";

            // Vorherige Routen und Markierungen in der Visualisierung löschen
            map.Overlays.Clear();

            // Aktualisierte Routen und Markierungen hinzufügen
            map.Overlays.Add(markers);
            map.Overlays.Add(routes);
            // Map zentrieren
            map.ZoomAndCenterRoutes("routes");

            // Reihenfolge der Stationen aktualisieren
            UpdatePathTarget(path);
        }

        /// <summary>
        /// Aufgaben, die intial ausgeführt werden
        /// </summary>
        /// <param name="markers"></param>
        /// <param name="allStations"></param>
        internal void InitView(GMapOverlay markers, List<string>allStations)
        {
            // Markierungen aller Lokale zeichnen
            map.Overlays.Add(markers);
            // Map zentrieren
            map.ZoomAndCenterMarkers("markers");
            // Liste mit allen zur Verfügung stehenden Lokalen aktualisieren
            foreach (var item in allStations)
            {
                ClbStations.Items.Add(item);
            }
        }

        /// <summary>
        /// Applikation beenden, wenn das Fenster geschlossen wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Aktualieren der Reihenfolge der zu besuchenden Lokale
        /// </summary>
        /// <param name="stationsToUpdate"></param>
        private void UpdatePathTarget(List<string> stationsToUpdate)
        {
            // Liste mit den vorherigen Stationen löschen und neu anlegen
            LB_StationsSequence.Items.Clear();
            int i = 1;
            foreach (var station in stationsToUpdate)
            {
                LB_StationsSequence.Items.Add(i.ToString() + ". " + station);
                i++;
            }
        }
    }
}
