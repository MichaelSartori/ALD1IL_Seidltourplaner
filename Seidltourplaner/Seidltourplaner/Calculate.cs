using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner
{
    internal class Calculate
    {
        static public bool CalculateRoute(ref List<Vertex> allVertices, List<int> checkedVertices, ref Vertex startVertex, out int distance)
        {

            // only for testing
            for (int i = 0; i < checkedVertices.Count - 1; i++)
            {
                allVertices[checkedVertices[i]].m_nextVertex = allVertices[checkedVertices[i + 1]];
            }
            startVertex = allVertices[checkedVertices[0]];
            //

            distance = 951;

            return true;
        }
        // Listen-Index wird einer Funktion in dieser Klasse übergeben übergeben 

        // return: Kilometer und geordnete Liste von Stationen wird zurückgegeben
    }
}
