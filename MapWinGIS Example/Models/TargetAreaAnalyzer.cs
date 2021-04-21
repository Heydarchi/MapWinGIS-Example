using GenerealClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapWindow_HeatMap
{
    public enum TARGET_TYPE { HUMAN = 0, VEHICLE = 1, AIRCRAFT = 2, SHIP = 3, HELICOPTER = 4, NONE = 5, DISABLE = 6 };

    public enum FREQ_TYPE { FIX = 0, HOPPING = 1, BURST = 2, NONE = 3 };
    public enum AREA_PRIORITY
    {
        HIGH = 0,
        MEDIUM = 1,
        LOW = 2,
    }

    public enum AREA_TYPE
    {
        ALARM = 0,
        FORBIDEN = 1,
        GATE_UPPER = 2,
        GATE_LOWER = 3,
        GATE_RIGHT = 4,
        GATE_LEFT = 5,
    }

    public class POINT
    {
        public double X;
        public double Y;
        public POINT(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }
    }


    public class PolygonArea
    {
        public double Area;
        public double Distance;
        public bool IsEllipsoid;
        public AREA_TYPE AreaType;
        public AREA_PRIORITY AreaPriority;
        public string AreaName = "";
        public bool Active = true;
        public List<POINT> Points = new List<POINT>();
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


    public class AreaAlarms
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public DateTime AlarmsTime { get; set; }
        public double targetLat { get; set; }
        public double targetLong { get; set; }
        public double targetFreq { get; set; }
        public AreaAlarms(int _id, string _areaname, DateTime _time, double _lat, double _longt, double _freq)
        {
            Id = _id;
            AreaName = _areaname;
            targetFreq = _freq;
            targetLat = _lat;
            targetLong = _longt;
            AlarmsTime = _time;
        }
    }
    public class TargetAreaAnalyze
    {
        public static List<PolygonArea> CriticalArea = new List<PolygonArea>();

        public List<PolygonArea> CriticalAreaNonStatic = new List<PolygonArea>();

        public static List<AreaAlarms> CheckAreas(double x, double y, double _freq = -1)
        {
            bool inArea = false;
            List<AreaAlarms> areaAlarms = new List<AreaAlarms>();
            try
            {
                foreach (var area in CriticalArea)
                {
                    //Point p = { 20, 20 };
                    bool ret = isInside(area.Points, area.Points.Count, new POINT(x, y));// ? cout << "Yes \n" : cout << "No \n";
                    if (ret)
                    {
                        inArea = true;
                        areaAlarms.Add(new AreaAlarms(0, area.AreaName, DateTime.Now, y, x, _freq));
                    }
                    //bool ret=isPointInPath(x, y, area.Points);
                    Console.WriteLine("CheckArea : " + ret.ToString());


                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
            return areaAlarms;


        }

        public bool CheckAreasNonStatic(double x, double y)
        {
            bool inArea = false;
            try
            {
                foreach (var area in CriticalAreaNonStatic)
                {
                    //Point p = { 20, 20 };
                    bool ret = isInside(area.Points, area.Points.Count, new POINT(x, y));// ? cout << "Yes \n" : cout << "No \n";
                    if (ret)
                        inArea = true;
                    //bool ret=isPointInPath(x, y, area.Points);
                    Console.WriteLine("CheckArea : " + ret.ToString());

                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
            return inArea;

        }

        private static bool isPointInPath(double x, double y, List<POINT> poly)
        {
            try
            {
                /*
                x, y -- x and y coordinates of point
                poly -- a list of tuples [(x, y), (x, y), ...]
                */

                int num = poly.Count;
                int i = 0;
                int j = num - 1;
                bool c = false;

                double minX = poly[0].X;
                double maxX = poly[0].X;
                double minY = poly[0].Y;
                double maxY = poly[0].Y;
                for (i = 1; i < poly.Count; i++)
                {
                    POINT q = poly[i];
                    minX = Math.Min(q.X, minX);
                    maxX = Math.Max(q.X, maxX);
                    minY = Math.Min(q.Y, minY);
                    maxY = Math.Max(q.Y, maxY);
                }

                if (x < minX || x > maxX || y < minY || y > maxY)
                {
                    return false;
                }

                for (i = 0; i < num; i++)
                {
                    if (((poly[i].Y > y) != (poly[j].Y > y)) &&
                            (x < poly[i].X + (poly[j].X - poly[i].X) * (y - poly[i].Y) / (poly[j].Y - poly[i].Y)))
                    {
                        c = true;
                    }
                    j = i;
                }
                return c;
            }
            catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); return false; }

        }



        // Define Infinite (Using INT_MAX caused overflow problems)
        static int INF = 10000;

        static bool onSegment(POINT p, POINT q, POINT r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }


        // To find orientation of ordered tr    iplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        static int orientation(POINT p, POINT q, POINT r)
        {
            double val = ((q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y));

            if (val == 0) return 0; // colinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        static bool doIntersect(POINT p1, POINT q1, POINT p2, POINT q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        // Returns true if the point p lies inside the polygon[] with n vertices
        static bool isInside(List<POINT> polygon, int n, POINT p)
        {
            // There must be at least 3 vertices in polygon[]
            if (n < 3) return false;

            // Create a point for line segment from p to infinite
            POINT extreme = new POINT(INF, p.Y);

            // Count intersections of the above line with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 'extreme' intersects
                // with the line segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i], polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line segment 'i-next',
                    // then check if it lies on segment. If it lies, return true,
                    // otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                        return onSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return ((count & 1) == 1 ? true : false); // Same as (count%2 == 1)
        }


    }
}
