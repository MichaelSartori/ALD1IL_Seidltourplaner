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
