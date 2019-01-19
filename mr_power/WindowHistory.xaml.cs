using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;

namespace mr_power
{
    /// <summary>
    /// Interaction logic for WindowHistory.xaml
    /// </summary>
    public partial class WindowHistory : Window
    {
        public static int margin =80;


        public WindowHistory()
        {
            InitializeComponent();
            
            

            

            Window win = winH;
            

    


            Point[] data = new Point[] { 
            new Point(0,100),
            new Point (100,100),
            new Point(200,0),
            new Point(0,0)};


            
            CreateGraph(win, canvasX);

            /*
            CreateBezierSegment(canvasX, inGraph(win, new Point(0, 0)),
                inGraph(win, 96,0),
                inGraph(win, 96,10),
                inGraph(win, 96,20));

            */


            //CreateLine(win, canvasX, inGraph(win, 50, 5), inGraph(win, 90, 7));




            /*
            
            CreateBezierSegment(canvasX,inGraph(win,new Point(0,0)),
                inGraph(win, new Point(0, 0)),
                inGraph(win, new Point(100, 350)),
                inGraph(win, new Point(200, 5)));
            */


        }

        private void CreateBezierSegment(Canvas canvas,Point startingPoint,Point p1,Point p2,Point p3)
        {
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = startingPoint;

            Point Point1 = p1;
            Point Point2 = p2;
            Point Point3 = p3;

            BezierSegment bzSeg = new BezierSegment();
            bzSeg.Point1 = Point1;
            bzSeg.Point2 = Point2;
            bzSeg.Point3 = Point3;


            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(bzSeg);

            pthFigure.Segments = myPathSegmentCollection;

            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;

            Path arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Black);
            arcPath.StrokeThickness = 1;
            arcPath.Data = pthGeometry;

            canvas.Children.Add(arcPath);
        }



        private static void CreteLineGraph(Window win, Canvas canvas,Battery[] data,int width)
        {

        }


        private static void CreateGraph(Window win ,Canvas canvas)
        {

            int widthMargin = (int)win.Width - 50;
            CreateAxis(win, canvas);

            

            int t = 7 * 24 + 1;
            for(int i=0;i<t;++i)
            {
                if(i%24!=0)
                    CreateVerticalLine(win, canvas, new Point((i * widthMargin) / (t-1), 0), 3, 1);
                else
                    CreateVerticalLine(win, canvas, new Point((i * widthMargin) / (t - 1), 0), 7, 1);


            }


            int heightMargin = (int)win.Height - 80;

            for(int i=0;i<11;++i)
            {
                CreateHorizontalLine(win, canvas, new Point(0,(i*heightMargin)/10), 7, 1);
            }


        }

        private static void CreateAxis(Window win, Canvas canvasX)
        {

            CreateVerticalLine(win, canvasX, new Point(0, 0),1);
            CreateHorizontalLine(win, canvasX, new Point(0, 0),1);
        }


        private static void CreateHorizontalLine(Window win, Canvas canvas, Point a,int dis, int width)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)(a.X + dis), (int)a.Y, width);
        }

        private static void CreateHorizontalLine(Window win, Canvas canvas, Point a,int width)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)(a.X+win.Width), (int)a.Y ,width);
        }
        private static void CreateVerticalLine(Window win, Canvas canvas, Point a, int dis, int width)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)a.X, (int)(a.Y + dis), width);
        }

        private static void CreateVerticalLine(Window win, Canvas canvas, Point a,int width)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)a.X, (int)(a.Y+win.Height),width);
        }

        private static void CreateLine(Window win, Canvas canvas, Point a, Point b, int width,Color color)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)b.X, (int)b.Y, width,color);
        }

        private static void CreateLine(Window win, Canvas canvas, Point a, Point b,int width)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)b.X, (int)b.Y,width);
        }

        private static void CreateLine(Window win, Canvas canvas, Point a, Point b)
        {
            CreateLine(win, canvas, (int)a.X, (int)a.Y, (int)b.X, (int)b.Y);
        }


        private static void CreateLine(Window win, Canvas canvas, int x1,int y1,int x2,int y2)
        {
            Line line = new Line();
            line.X1 = x1 + margin / 4;
            line.X2 = x2 + margin / 4;
            line.Y1 = win.Height - y1 - margin;
            line.Y2 = win.Height - y2 - margin;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Color.FromRgb(0, 0, 180);
            line.Stroke = blackBrush;
            line.StrokeThickness = 4;
            canvas.Children.Add(line);
        }

        private static void CreateLine(Window win, Canvas canvas, int x1, int y1, int x2, int y2,int width)
        {
            Line line = new Line();
            line.X1 = x1 + margin / 4;
            line.X2 = x2 + margin / 4;
            line.Y1 = win.Height - y1 - margin;
            line.Y2 = win.Height - y2 - margin;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Color.FromRgb(0, 0, 180);
            line.Stroke = blackBrush;
            line.StrokeThickness = width;
            canvas.Children.Add(line);
        }

        private static void CreateLine(Window win, Canvas canvas, int x1, int y1, int x2, int y2, int width,Color color)
        {
            Line line = new Line();
            line.X1 = x1 + margin / 4;
            line.X2 = x2 + margin / 4;
            line.Y1 = win.Height - y1 - margin;
            line.Y2 = win.Height - y2 - margin;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = color;
            line.Stroke = blackBrush;
            line.StrokeThickness = width;
            canvas.Children.Add(line);
        }



        private static void CreateAPolygon(Window win,Canvas canvas,Point[] points,int width)
        {
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Color.FromRgb(200,0,0);
            Polygon yellowPolygon = new Polygon();
            yellowPolygon.Stroke = blackBrush;
            yellowPolygon.StrokeThickness = width;
            PointCollection polygonPoints = new PointCollection();
            foreach(Point p in points)
            {
                Point pt = p;
                pt.Y = win.Height - margin - pt.Y;
                pt.X += margin/4;
                polygonPoints.Add(pt);
            }
            
            yellowPolygon.Points = polygonPoints;
            canvas.Children.Add(yellowPolygon);
        }


        private static Point inGraph(Window win,Point point)
        {
            point.X = point.X + margin / 4;
            point.Y = win.Height - point.Y - margin;
            return point;
        }

        private static Point inGraph(Window win, int value,int nth)
        {
            Point point = new Point(0,0);
            int w =(int) (win.Width - 50 - margin);
            point.X = (nth * w) / (7 * 24);
            int h = (int)(win.Height - 80 - margin);
            point.Y = (int)((value * h) / 100);
            return point;
        }

    }
}