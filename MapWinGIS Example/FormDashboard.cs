using GenerealClass;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapWindow_HeatMap
{
    public partial class FormMain : Form
    {

        string app_ver = " V0.1.0  13-03-2021";
        PolygonAreaClass temp_polygon;
        MapView mFrmMapView ;

        public FormMain()
        {
            InitializeComponent();
            //Load AxMap OCX after to initial it as a component
            loadGisOCXonRuntime();

            bool exists = System.IO.Directory.Exists("ScreenShots");
            if (!exists)
                System.IO.Directory.CreateDirectory("ScreenShots");

        }


        //Load axMap ocx manually because the x64 ocx version has problem in loading on VisualStudio
        private void loadGisOCXonRuntime()
        {
            try
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
                this.axMap1 = new AxMapWinGIS.AxMap();
                ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
                // 
                // axMap1
                // 
                this.axMap1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.axMap1.Enabled = true;
                this.axMap1.Location = new System.Drawing.Point(0, 0);
                this.axMap1.Name = "axMap1";
                this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
                this.axMap1.Size = new System.Drawing.Size(834, 470);
                this.axMap1.TabIndex = 0;
                this.axMap1.MouseMoveEvent += new AxMapWinGIS._DMapEvents_MouseMoveEventHandler(this.axMap1_MouseMoveEvent);
                this.axMap1.MeasuringChanged += new AxMapWinGIS._DMapEvents_MeasuringChangedEventHandler(this.axMap1_MeasuringChanged);
                ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();

                this.panel_GIS.Controls.Add(this.axMap1);//M.H.H
            }
            catch (Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
            }
        }

        void initMapView()
        {
            try
            {
                //Create ne instance of MapView class
                mFrmMapView = new MapView();
                // Initialize AxMap component with a defualt configuration
                mFrmMapView.initGISControl(axMap1);
                //Initialize layers
                mFrmMapView.initLayers(axMap1);
            }
            catch (Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));

                MessageBox.Show(ex.Message);
            }
        }


        //Function to handle mouse event on the AxMap 
        private void axMap1_MouseMoveEvent(object sender, AxMapWinGIS._DMapEvents_MouseMoveEvent e)
        {
            double projX = 0.0;
            double projY = 0.0;
            axMap1.PixelToProj(e.x, e.y, ref projX, ref projY);
            string lat_str = "E ";
            string lon_str = "N ";
            if (projX < 0)
            {
                lat_str = "W ";
                projX = Math.Abs(projX);
            }
            if (projY < 0)
            {
                lon_str = "S ";
                projY = Math.Abs(projY);
            }
            string _pos = "Cursor : " + lat_str +  " - " + lon_str;
            toolStripStatusLabelMouse.Text = _pos;
            statusBarlatlon.Text = _pos;
        }


        //Function to handle measurement on the AxMap 

        private void axMap1_MeasuringChanged(object sender, AxMapWinGIS._DMapEvents_MeasuringChangedEvent e)
        {
            try
            {
                //Check if new point is added by the user 
                if (e.action == tkMeasuringAction.PointAdded)
                {
                    if (axMap1.Measuring.IsUsingEllipsoid)
                    {
                        Console.WriteLine("Calculations on ellipsoid.");
                        Console.WriteLine("Area: " + axMap1.Measuring.Area + "sq.m");
                        Console.WriteLine("Distance: " + axMap1.Measuring.Length + "m");
                    }
                    else
                    {
                        Console.WriteLine("Calculations on plane.");
                        Console.WriteLine("Area: " + axMap1.Measuring.Area + "map units");
                        Console.WriteLine("Distance: " + axMap1.Measuring.Length + "map units");
                    }

                    if (temp_polygon != null)
                    {
                        temp_polygon.Points.Clear();
                        double x, y;
                        Console.WriteLine("Measured points (in map units.): " + axMap1.Measuring.PointCount);
                        for (int i = 0; i < axMap1.Measuring.PointCount; i++)
                        {
                            if (axMap1.Measuring.get_PointXY(i, out x, out y))
                            {
                                Console.WriteLine("x={0}; y={1}", x, y);
                                temp_polygon.Points.Add(new PointClass(x, y));
                            }
                        }
                        temp_polygon.IsEllipsoid = axMap1.Measuring.IsUsingEllipsoid;
                        temp_polygon.Distance = axMap1.Measuring.Length;
                        temp_polygon.Area = axMap1.Measuring.Area;
                    }


                }
            }
            catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text += app_ver;
            //Initialize an instance of the MapView class
            initMapView();

        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmNone;
        }

        private void tnZoomIn_Click(object sender, EventArgs e)
        {
            mFrmMapView.zoomInClicked();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mFrmMapView.zoomOutClicked();
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            mFrmMapView.panClicked();
        }

        private void btnZoomExist_Click(object sender, EventArgs e)
        {
            axMap1.ZoomToMaxExtents();
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            mFrmMapView.polyLineClicked();
            temp_polygon = new PolygonAreaClass();
        }

        private void btnAddIcon_Click(object sender, EventArgs e)
        {
            mFrmMapView.pinClicked();
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
             
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            takeScreenShot();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mFrmMapView.Referesh();
        }

        private void btnRuler_Click(object sender, EventArgs e)
        {
            mFrmMapView.rulerClicked();
        }


        //Take and save an screen-shot of the application form
        private void takeScreenShot()
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    bmp.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ScreenShots\\" +
                       
                        DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".png");  // saves the image
                }
            }
            catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); }

        }


    }
}
