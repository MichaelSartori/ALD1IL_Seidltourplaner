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
        public PointLatLng m_coordinates { get; private set; }
        public string m_name { get; private set; }
        public List<Tuple<Vertex, MapRoute>> m_neighborVertices { get; set; }

        public Vertex m_nextVertex { get; set; }


        public Vertex(double latitude, double longitude, string name)
        {
            m_coordinates = new PointLatLng(latitude, longitude);
            m_name = name;
            m_neighborVertices = new List<Tuple<Vertex, MapRoute>>();

            // Initialisiere GMap
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // Setze den Bing Maps API-Schlüssel
            GMapProviders.BingMap.ClientKey = @"odSs6iYRjBisqTjOZeMV~X1lRoeYyJRrDQH6--qI7Rg~ArJ6lPStApT--whZWgV8ZwCZet2uRtMPANBkvwzkT-mFHR4wsDOF6sg2fZUuMB8R";
        }

        public void AddNeighborVertix(Vertex m_neighborVertic)
        {
            MapRoute calculatedRoute = BingMapProvider.Instance.GetRoute(m_coordinates, m_neighborVertic.m_coordinates, true, true, 15);
            m_neighborVertices.Add(new Tuple<Vertex, MapRoute>(m_neighborVertic, calculatedRoute));
        }
    }
}
