using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner
{
    internal class Vertex
    {
        // Koordinate des Lokals (Knotens)
        public PointLatLng m_coordinates { get; private set; }

        // Name des Lokals (Knotens)
        public string m_name { get; private set; }

        // Liste alle Nachbarlokale mit Route dorthin
        public List<Tuple<Vertex, MapRoute>> m_neighborVertices { get; set; }

        // Das nächste zu besuchende Lokal
        //public Vertex m_nextVertex { get; set; }

        /// <summary>
        /// Standard Konstruktor
        /// </summary>
        /// <param name="latitude">Breitengrad des Lokals</param>
        /// <param name="longitude">Längengrad des Lokals</param>
        /// <param name="name">Name des Lokals</param>
        public Vertex(double latitude, double longitude, string name)
        {
            // Membervariablen initialisieren
            m_coordinates = new PointLatLng(latitude, longitude);
            m_name = name;
            m_neighborVertices = new List<Tuple<Vertex, MapRoute>>();

            // Initialisiere GMap
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // Setze des API-Schlüssels für Bing-Maps
            GMapProviders.BingMap.ClientKey = @"odSs6iYRjBisqTjOZeMV~X1lRoeYyJRrDQH6--qI7Rg~ArJ6lPStApT--whZWgV8ZwCZet2uRtMPANBkvwzkT-mFHR4wsDOF6sg2fZUuMB8R";
        }

        /// <summary>
        /// Methode zum hinzufügen eines Nachbarlokals
        /// </summary>
        /// <param name="m_neighborVertex">Nachbarlokal</param>
        public void AddNeighborVertex(Vertex m_neighborVertex)
        {
            MapRoute calculatedRoute = BingMapProvider.Instance.GetRoute(m_coordinates, m_neighborVertex.m_coordinates, true, true, 15);
            m_neighborVertices.Add(new Tuple<Vertex, MapRoute>(m_neighborVertex, calculatedRoute));
        }
    }
}
