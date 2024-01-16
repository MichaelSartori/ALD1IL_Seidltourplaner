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
        public List<Tuple<Vertex,int>> m_neighborVertices { get; set; }

        public Vertex m_nextVertex { get; set; }

        public Vertex(double latitude, double longitude, string name)
        {
            m_coordinates = new PointLatLng(latitude, longitude);
            m_name = name;
            m_neighborVertices = new List<Tuple<Vertex,int>>();
        }

        public void AddNeighborVertix(Vertex m_neighborVertic, int distance = 10)
        {
            m_neighborVertices.Add(new Tuple<Vertex,int>(m_neighborVertic, distance));
        }
    }
}
