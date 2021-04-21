using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using AxMapWinGIS;
using MapWinGIS;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using GenerealClass;


namespace MapWindow_HeatMap
{
    public class MapView
    {
        bool bDrawLineInTrackMode = false;
        public bool FirstTime = false;
        #region class private members

        private string m_mapDocumentName = string.Empty;
        string[] pic = new string[10];


        #region Target Color

        public int[] RedColor = { 0, 255, 0, 0, 255, 255, 0, 80, 100, 40, 0, 100, 255, 100, 64, 0, 250, 0 };
        public int[] GreenColor = { 0, 0, 255, 0, 255, 0, 255, 80, 0, 85, 0, 0, 90, 0, 0, 100, 15, 0 };
        public int[] BlueColor = { 0, 0, 0, 255, 0, 255, 255, 0, 100, 85, 100, 0, 15, 200, 0, 160, 200, 64 };

        #endregion

        #endregion

 
        public MapView()
        {

        }

        public void Referesh()
        {
            axMap1.Refresh();
            axMap1.Redraw();
            axMap1.SendMouseMove = true;     
            axMap1.SendMouseDown = true;
        }


        public void DeleteAllLayerIcon( )
        {
 
             try
             {

                Shapefile sf = axMap1.get_Shapefile(m_userVehicleLayerHandler);

                if (sf == null)
                    return;

                for(int i=0;i< sf.NumShapes; i++)
                    sf.Shape[i].Clear();

                for (int i = (sf.Labels.Count-1);i>=0 ; i--)
                    sf.Labels.RemoveLabel(i);

                sf = axMap1.get_Shapefile(m_userObstaclesLayerHandler); ;
                for (int i = 0; i < sf.NumShapes; i++)
                    sf.Shape[i].Clear();

               for(int i = (sf.Labels.Count - 1); i >= 0; i--)
                    sf.Labels.RemoveLabel(i);

                Console.WriteLine("Layers Num :" + axMap1.NumLayers);

            }
             catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); }

        }

      
        public void DeleteAllIcon( )
        {
            try
            {
                //axMap1.Clear();
            }
            catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); }

        }


   
        ///////// Map Function
        AxMap axMap1;
        public enum GISLAYER { USER_VEHICLE, OBSTACLES, OTHERS };

 


        ClickMode clickMode = ClickMode.PAN;
        int m_userVehicleLayerHandler = -1;
        int m_userObstaclesLayerHandler = -1;
        int m_userOthersLayerHandler = -1;

        MapWinGIS.Image selectedPictureOthers= new MapWinGIS.Image();
        MapWinGIS.Image selectedPictureHuman = new MapWinGIS.Image();
        MapWinGIS.Image selectedPictureObstacle = new MapWinGIS.Image();
        MapWinGIS.Image selectedPictureVehicle = new MapWinGIS.Image();
        

        
        //List<string> imageList = new List<string>();

        public class LinePointsClass
        {
            public double lat1;
            public double long1;
            public double lat2;
            public double long2;
            public double angle;
            public int distance;

            public LinePointsClass(double _lat1, double _long1, double _lat2, double _long2)
            {
                lat1 = _lat1;
                long1 = _long1;
                lat2 = _lat2;
                long2 = _long2;
            }

            public LinePointsClass(double _lat1, double _long1, double _angle, int _distance)
            {

                lat1 = _lat1;
                long1 = _long1;
                angle = _angle;
                distance = _distance;

              /*  double utmx = 0.0, utmy = 0.0;
                double mapx2, mapy2;
                 string zone = "";
                utmll.LLToUTM(_lat1, _long1, ref utmy, ref utmx, ref zone);

                if (angle < 0)
                    angle = angle + 2 * Math.PI;
                double d = distance *9;// 700;

                try
                {
                    mapx2 = utmx + d * Math.Cos(angle);
                    mapy2 = utmy + d * Math.Sin(angle);
                    //utmll.UTMtoLL(mapy2, mapx2, UserInterface.FrmMainPF.ZoneSurround, ref mapy2, ref mapx2);
                    utmll.UTMtoLL(mapy2, mapx2, zone, ref mapy2, ref mapx2);
                    long2 = Math.Round(mapx2, 4);
                    lat2 = Math.Round(mapy2, 4);
                  }
                catch { }*/


             }
        }

        List<LinePointsClass> liness = new List<LinePointsClass>();
        public enum ClickMode { POINT, PAN, ZOOM_IN, ZOOM_OUT, RULER, ADD_LAYER, PIN, POLY_LINE,NONE };

        // the handle of the layer with markers
        private int m_layerHandle = -1;
        public bool addLayers(AxMap axMap1, string dataPath)
        {
            axMap1.RemoveAllLayers();
            m_userVehicleLayerHandler = -1;
            axMap1.LockWindow(tkLockMode.lmLock);

            try
            {
                string[] files = Directory.GetFiles(dataPath);
                foreach (string file in files)
                {

                    if (file.ToLower().EndsWith(".shp"))
                    {
                        Shapefile sf = new Shapefile();
                        if (sf.Open(file, null))
                        {
                            m_layerHandle = axMap1.AddLayer(sf, true);
                        }
                        else
                            MessageBox.Show(sf.ErrorMsg[sf.LastErrorCode]);
                    }
                    else if (file.ToLower().EndsWith(".tif") ||
                             file.ToLower().EndsWith(".png"))
                    {
                        MapWinGIS.Image img = new MapWinGIS.Image();
                        if (img.Open(file, ImageType.TIFF_FILE, false, null))
                        {
                            m_layerHandle = axMap1.AddLayer(img, true);
                        }
                        else
                            MessageBox.Show(img.ErrorMsg[img.LastErrorCode]);
                    }

                    if (m_layerHandle != -1)
                        axMap1.set_LayerName(m_layerHandle, Path.GetFileName(file));
                }
            }
            finally
            {
                axMap1.LockWindow(tkLockMode.lmUnlock);
                Debug.Print("Layers added to the map: " + axMap1.NumLayers);
            }
            initLayers(axMap1);
            return axMap1.NumLayers > 0;
        }

        private int getLayerHandle(GISLAYER layerType)
        {
            switch (layerType)
            {
                case GISLAYER.USER_VEHICLE:
                    return m_userVehicleLayerHandler;
                case GISLAYER.OBSTACLES:
                    return m_userObstaclesLayerHandler;
                case GISLAYER.OTHERS:
                    return m_userOthersLayerHandler;
                default:
                    return -1;
            }
        }

        private MapWinGIS.Image getLayerIcon(GISLAYER layerType)
        {
            switch (layerType)
            {
                case GISLAYER.USER_VEHICLE:
                    return selectedPictureVehicle;
                case GISLAYER.OTHERS:
                    return selectedPictureOthers;
                case GISLAYER.OBSTACLES:
                    return selectedPictureObstacle;
                default:
                    return null;
            }
        }


        //Initialize some handles to set default  desired configuration
        public bool initGISControl(AxMap _axMap)
        {
            try
            {
                    m_userVehicleLayerHandler = -1;
                    m_userOthersLayerHandler = -1;
                    m_userObstaclesLayerHandler = -1;

                    axMap1 = _axMap;
                    axMap1.SendMouseDown = true;
                    axMap1.SendMouseMove = true;
                    axMap1.CursorMode = tkCursorMode.cmNone;
                    axMap1.MouseDownEvent +=new AxMapWinGIS._DMapEvents_MouseDownEventHandler( axMap1MouseDownEvent);   // change MapEvents to axMap1
                    axMap1.UseWaitCursor = false;
                    axMap1.DisableWaitCursor = true;
                    axMap1.TileProvider = tkTileProvider.ProviderNone;

                    return true;
            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
                return false;
            }
        }

        //Initialize some layers on map to work on them

        public bool initLayers(AxMap _axMap)
        {
            try
            {
                    axMap1.Clear();
                    axMap1.RemoveAllLayers();
                    Shapefile sf = new Shapefile();
                    axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
                    
                    //http://www.mapwindow.org/documentation/mapwingis4.9/class_file_manager.html#details

                    if (Properties.Settings.Default.LastGISFilePath != "")
                    {

                        string[] files = Properties.Settings.Default.LastGISFilePath.Split(';');

                        string[] ext = { ".asc", ".bt", ".bil", ".bmp", ".dem", ".ecw", ".img", ".gif", ".map", ".jp2", ".jpg", ".sid", ".pgm", ".pnm", ".png", ".ppm", ".LF2", ".kap", ".tif " };
                        foreach (string s in files)
                        {
                            if (ext.Any(s.Contains))
                            {
                                var fm = new FileManager();
                                var sff2 = fm.Open(s, tkFileOpenStrategy.fosAutoDetect, null);
                                axMap1.AddLayer(sff2, true);
                            }
                        }

                        foreach (string s in files)
                        {
                            if (s.Contains(".shp"))
                            {
                                Shapefile sff = new Shapefile();
                                if (sff.Open(s, null))
                                {
                                    axMap1.AddLayer(sff, true);
                                }
                            }
                        }
                    }

                    //Display the layer on the map control
                    m_layerHandle = axMap1.AddLayer(sf, true);

                    if (m_userOthersLayerHandler == -1)
                    {
                        sf = new Shapefile();
                        if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                        {
                            MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                            return false;
                        }

                        m_userOthersLayerHandler = axMap1.AddLayer(sf, true);
                    }
                    else
                        sf = axMap1.get_Shapefile(m_userOthersLayerHandler);


                    if (m_userObstaclesLayerHandler == -1)
                    {
                        sf = new Shapefile();
                        if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POLYLINE))
                        {
                            MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                            return false;
                        }

                        m_userObstaclesLayerHandler = axMap1.AddLayer(sf, true);
                    }
                    else
                        sf = axMap1.get_Shapefile(m_userObstaclesLayerHandler);

                    if (m_userVehicleLayerHandler == -1)
                        {
                            sf = new Shapefile();
                            if (!sf.CreateNewWithShapeID("", ShpfileType.SHP_POINT))
                            {
                                MessageBox.Show("Failed to create shapefile: " + sf.ErrorMsg[sf.LastErrorCode]);
                                return false;
                            }

                            m_userVehicleLayerHandler = axMap1.AddLayer(sf, true);
                        }
                        else
                            sf = axMap1.get_Shapefile(m_userVehicleLayerHandler);          

                    selectedPictureOthers = this.openMarker(Path.GetDirectoryName(Application.ExecutablePath) + @"\images\none.png");
                    selectedPictureVehicle = this.openMarker(Path.GetDirectoryName(Application.ExecutablePath) + @"\images\vehicle.png");
                    selectedPictureObstacle = this.openMarker(Path.GetDirectoryName(Application.ExecutablePath) + @"\images\ship.png");
                    selectedPictureHuman = this.openMarker(Path.GetDirectoryName(Application.ExecutablePath) + @"\images\human.png");

                    axMap1.SendMouseMove = true;
                    axMap1.CursorMode = tkCursorMode.cmNone;
                    axMap1.ShowCoordinatesFormat = tkAngleFormat.afSeconds;

                    // river pattern
                    var utils = new Utils();
                    LinePattern pattern = new LinePattern();
                    pattern = new LinePattern();
                    pattern.AddLine(utils.ColorByName(tkMapColor.DarkBlue), 2.0f, tkDashStyle.dsSolid);
                    pattern.AddLine(utils.ColorByName(tkMapColor.DarkBlue), 1.0f, tkDashStyle.dsSolid);

                    sf = axMap1.get_Shapefile(getLayerHandle(GISLAYER.OBSTACLES));
                    CategoryRiver = sf.Categories.Add("River");
                    CategoryRiver.DrawingOptions.LinePattern = pattern;
                    CategoryRiver.DrawingOptions.UseLinePattern = true;

                    return true;
            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
                return false;
            }
        }


        public bool insertPointByScreenXY(int ex, int ey, string title)
        {

             if (m_userVehicleLayerHandler != -1)
            {

                Shapefile sf = axMap1.get_Shapefile(getLayerHandle(GISLAYER.USER_VEHICLE));
                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POINT);

                MapWinGIS.Point pnt = new MapWinGIS.Point();
                double x = 0.0;
                double y = 0.0;
                axMap1.PixelToProj(ex, ey, ref x, ref y);
                pnt.x = x;
                pnt.y = y;
                int index = shp.numPoints;
                sf.Labels.AddLabel(title, pnt.x, pnt.y, 0);
                shp.InsertPoint(pnt, ref index);

                index = sf.NumShapes;
                sf.StartEditingShapes();
                if (!sf.EditInsertShape(shp, ref index))
                {
                    MessageBox.Show("Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode]);
                    return false;
                }
                sf.StopEditingShapes();
                axMap1.Redraw();
            }
            return true;
        }




        // <summary>
        // Adds randomly positioned labels to the image layer.
        // </summary>
        public void ImageLabels(AxMap axMap1, double x, double y)
        {
            try
            { 

                axMap1.Projection = tkMapProjection.PROJECTION_WGS84;
                axMap1.GrabProjectionFromData = true;

                MapWinGIS.Image img = new MapWinGIS.Image();

                img.Open(Path.GetDirectoryName(Application.ExecutablePath) + @"\tank_wagon.png", ImageType.USE_FILE_EXTENSION, false, null);

                Labels lbl = new Labels();// img.Labels;
                lbl.FontSize = 12;
                lbl.FontBold = true;

                lbl.FontOutlineVisible = true;
                lbl.FontOutlineColor = (255 << 16) + (255 << 8) + 255;  //white
                lbl.FontOutlineWidth = 4;

                LabelCategory cat = lbl.AddCategory("Red");
                cat.FontColor = 255;

                cat = lbl.AddCategory("Blue");
                cat.FontColor = 255 << 16;

                cat = lbl.AddCategory("Yellow");
                cat.FontColor = 255 + 255 << 8;

                Extents ext = img.Extents;

                Shapefile sf = axMap1.get_Shapefile(m_userVehicleLayerHandler);
                Shape shp = new Shape();
                int i = shp.numPoints;

                int categoryIndex = -1;// i % 3;
                lbl.AddLabel("label", x, y, 0, i);//categoryIndex);

                axMap1.Redraw();
                axMap1.Refresh();
                //}
            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
        }



        public void insertPointByLatLong(AxMap axMap1, double lat, double longt, GISLAYER layerType, string title)
        {
            try
            {
                Shapefile sf = axMap1.get_Shapefile(getLayerHandle(layerType));

                Shape shp = new Shape();
                shp.Create(ShpfileType.SHP_POINT);

                MapWinGIS.Point pnt = new MapWinGIS.Point();
                double x = 0.0;
                double y = 0.0;
                double labelOffsety = 0;
                double labelOffsetx = 0;// 0.000001;

                switch(axMap1.CurrentZoom)
                {
                    case 12:
                        labelOffsety = 0.0005;
                        break;
                    case 13:
                        labelOffsety = 0.0002;
                        break;
                    case 14:
                        labelOffsety = 0.0001;
                        break;
                    case 15:
                        labelOffsety = 0.00005;
                        break;
                    case 16:
                        labelOffsety = 0.00002;
                        break;
                    case 17:
                        labelOffsety = 0.00001;
                        break;
                    case 18:
                        labelOffsety = 0.000005;
                        break;
                    case 19:
                        labelOffsety = 0.000002;
                        break;
                }

                axMap1.PixelToProj(lat, longt, ref x , ref y);
                pnt.y = lat;// x;
                pnt.x = longt;// y;

                sf.Labels.AddLabel(title, (pnt.x - labelOffsetx), (pnt.y - labelOffsety), 0);

                sf.StartEditingShapes(true);
                int index = shp.numPoints;

                ShapeDrawingOptions options = sf.DefaultDrawingOptions;
                options.PointType = tkPointSymbolType.ptSymbolPicture;

                options.Picture = getLayerIcon(layerType);

                pnt.y += labelOffsety;
                shp.InsertPoint(pnt, ref index);

                index = sf.NumShapes;
                if (!sf.EditInsertShape(shp, ref index))
                {
                    string str = "Failed to insert shape: " + sf.ErrorMsg[sf.LastErrorCode];
                    MessageBox.Show(str);
                    Console.WriteLine(str);
                    return;
                }
                sf.StartEditingShapes(false);

                axMap1.Redraw();
                axMap1.Refresh();
            }
            catch (Exception e1)
            {

            }
        }


        public void zoomToPrevious()
        {
            try
            {
                axMap1.ZoomToPrev();
            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
        }

        public void zoomToNext()
        {
            try
            {
                axMap1.ZoomToNext();
            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
        } 
        //////////////Line Pattern///////////////
        // <summary>
        // Creates and displayes custom line patterns
        // </summary>
        public void linePattern(AxMap axMap1, List<LinePointsClass> liness, GISLAYER layerType)
        {
            try
            { 

                axMap1.Projection = tkMapProjection.PROJECTION_WGS84;

                var sf = this.drawLines(liness, layerType);
                //axMap1.AddLayer(sf, true);
                axMap1.Redraw();
                var utils = new Utils();

                // railroad pattern
                LinePattern pattern = new LinePattern();

                ShapefileCategory ct;

                // river pattern
                for (int i = 0; i < liness.Count; i++)
                {                
                    sf.set_ShapeCategory(i, i);
                }

            }
            catch (System.Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
             }
        }




        // <summary>
        // This function creates a number of parallel polylines (segments)
        // </summary>
        ShapefileCategory CategoryRiver;

        public Shapefile drawLines(List<LinePointsClass> liness, GISLAYER layerType)
        {
            Shapefile sf = axMap1.get_Shapefile(getLayerHandle(layerType));
            foreach (var lin in liness)
            {


                for (int i = 0; i < 1; i++)
                {
                    Shape shp = new Shape();
                    shp.Create(ShpfileType.SHP_POLYLINE);

                    MapWinGIS.Point pnt = new MapWinGIS.Point();
                    pnt.y = lin.lat1;
                    pnt.x = lin.long1;// +i * step;
                    int index = shp.numPoints;
                    shp.InsertPoint(pnt, ref index);

                    pnt = new MapWinGIS.Point();
                    pnt.y = lin.lat2;// +width;
                    pnt.x = lin.long2;// +i * step;
                    index = shp.numPoints;
                    shp.InsertPoint(pnt, ref index);

                    index = sf.NumShapes;
                    sf.EditInsertShape(shp, ref index);
                    sf.set_ShapeCategory3(index, CategoryRiver);
                }
            }
            return sf;
        }




        // <summary>
        // Opens a marker from the file
        // </summary>
        public MapWinGIS.Image openMarker(string dataPath)
        {
            string path = dataPath;// Path.GetDirectoryName(Application.ExecutablePath) + @"\tank_wagon.png";
            if (!File.Exists(path))
            {
                MessageBox.Show("Can't find the file: " + path);
            }
            else
            {
                MapWinGIS.Image img = new MapWinGIS.Image();
                if (!img.Open(path, ImageType.USE_FILE_EXTENSION, true, null))
                {
                    MessageBox.Show(img.ErrorMsg[img.LastErrorCode]);
                    img.Close();
                }
                else
                    return img;
            }
            return null;
        }


        // <summary>
        // Handles mouse down event and adds the marker
        // </summary>
        public void axMap1MouseDownEvent(object sender, _DMapEvents_MouseDownEvent e)
        {
            if (e.button == 1)          // left button
            {
                switch (clickMode)
                {
                    case ClickMode.POINT:
                        insertPointByScreenXY(e.x, e.y, "");
                        break;
                    case ClickMode.ZOOM_IN:
                        axMap1.ZoomIn(20);
                        break;
                    case ClickMode.ZOOM_OUT:
                        axMap1.ZoomOut(20);
                        break;
                }
            }
        }


        public void axMap1MouseMoved(object sender, _DMapEvents_MouseMoveEvent e)
        {
            Console.WriteLine(e.x + " " + e.y);
        }


        public void arrowClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmNone;
            clickMode = ClickMode.NONE;
        }

        public void panClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
            clickMode = ClickMode.PAN;
        }

        public void zoomInClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmZoomIn;
            clickMode = ClickMode.ZOOM_IN;
        }

        public void zoomOutClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmZoomOut;
            clickMode = ClickMode.ZOOM_OUT;
        }

        public void rulerClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmMeasure;
            axMap1.Measuring.AreaUnits = tkAreaDisplayMode.admMetric;
            axMap1.Measuring.LengthUnits = tkLengthDisplayMode.ldmMetric;
            axMap1.Measuring.ShowBearing = true;
            axMap1.Measuring.ShowLength = true;
            clickMode = ClickMode.RULER;
        }

        public void pinClicked()
        {
            axMap1.CursorMode = tkCursorMode.cmAddShape;
            clickMode = ClickMode.PIN;
        }

        public void polyLineClicked()
        {
            //axMap1.Measuring.Area
            axMap1.CursorMode = tkCursorMode.cmMeasure;
            clickMode = ClickMode.POLY_LINE;
        }


             //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //:::                                                                         :::
            //:::  This routine calculates the distance between two points (given the     :::
            //:::  latitude/longitude of those points). It is being used to calculate     :::
            //:::  the distance between two locations using GeoDataSource(TM) products    :::
            //:::                                                                         :::
            //:::  Definitions:                                                           :::
            //:::    South latitudes are negative, east longitudes are positive           :::
            //:::                                                                         :::
            //:::  Passed to function:                                                    :::
            //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
            //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
            //:::    unit = the unit you desire for results                               :::
            //:::           where: 'M' is statute miles (default)                         :::
            //:::                  'K' is kilometers                                      :::
            //:::                  'N' is nautical miles                                  :::
            //:::                                                                         :::
            //:::  Worldwide cities and other features databases with latitude longitude  :::
            //:::  are available at http://www.geodatasource.com                          :::
            //:::                                                                         :::
            //:::  For enquiries, please contact sales@geodatasource.com                  :::
            //:::                                                                         :::
            //:::  Official Web site: http://www.geodatasource.com                        :::
            //:::                                                                         :::
            //:::           GeoDataSource.com (C) All Rights Reserved 2017                :::
            //:::                                                                         :::
            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

            public double distance(double lat1, double lon1, double lat2, double lon2, char unit) {
              double theta = lon1 - lon2;
              double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
              dist = Math.Acos(dist);
              dist = rad2deg(dist);
              dist = dist * 60 * 1.1515;
              if (unit == 'K') {
                dist = dist * 1.609344;
              } else if (unit == 'N') {
  	            dist = dist * 0.8684;
                }
              return (dist);
            }

            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //::  This function converts decimal degrees to radians             :::
            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            private double deg2rad(double deg) {
              return (deg * Math.PI / 180.0);
            }

            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //::  This function converts radians to decimal degrees             :::
            //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            private double rad2deg(double rad) {
              return (rad / Math.PI * 180.0);
            }
    }
}
