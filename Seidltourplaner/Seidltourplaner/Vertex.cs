using GMap.NET;
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
        public List<Vertex> m_vertices;
        public Vertex m_nextVertex;

        public Vertex(double latitude, double longitude, string name)
        {
            m_coordinates = new PointLatLng(latitude, longitude);
            m_name = name;
        }
    }
}
