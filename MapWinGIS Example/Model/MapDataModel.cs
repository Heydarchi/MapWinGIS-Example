using GenerealClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapWindow_HeatMap
{

    public class PointClass
    {
        public double X;
        public double Y;
        public PointClass(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }
    }


    public class PolygonAreaClass
    {
        public double Area;
        public double Distance;
        public bool IsEllipsoid;
        public bool Active = true;
        public List<PointClass> Points = new List<PointClass>();
        public double[,] getPointsArray()
        {
            double[,] points = new double[Points.Count, 2];
            for (int i = 0; i < Points.Count; i++)
            {
                points[i, 0] = Points[i].X;
                points[i, 1] = Points[i].Y;
            }
            return points;
        }

    }

}
