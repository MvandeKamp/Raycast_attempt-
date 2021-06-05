using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raycasting
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Ray> rays = new List<Ray>();
        public List<Wall> walls = new List<Wall>();
       // public Ray ray = new Ray(250, 250, 0, 2);
      //  public Wall wall = new Wall(800, 100, new RotateTransform(0, 0, 0), 200);
        public MainWindow()
        {
            Random rnd = new Random();
            InitializeComponent();
            /* for (int i = 0; i < 8; i++)
             {

                 Wall wall = new Wall(rnd.Next(10, 990), rnd.Next(5, 490), new RotateTransform(rnd.Next(0, 360)), rnd.Next(0, 250));
                 walls.Add(wall);

             } */
            Wall wall = new Wall(800, 100, new RotateTransform(0, 0, 0), 200);
            walls.Add(wall);
           // Ray ray = new Ray(0, 0, -180, 1);
           // rays.Add(ray);
            for (int i = 0; i < 3; i++)
            {
                Ray ray = new Ray(200, 200, i * 10, 1);
                rays.Add(ray);
                
            }
            // rootGrid.Children.Add(wall.WallConstruct());
            //  Console.WriteLine("Start x " + wall.X1Cords + " End x " + wall.X2Cords + "Start y " + wall.Y1Cords + " End y " + wall.Y2Cords);

            //  rootGrid.Children.Add(ray.RayConstruct());
            // Console.WriteLine("Start x " + ray.X3Cords + " End x " + ray.X4Cords + "Start y " + ray.Y3Cords + " End y " + ray.Y4Cords);

        }
        public class Wall
        {
            private double x1;
            private double y1;
            private RotateTransform rotation;
            private double length;
            private Rectangle wall = new Rectangle();
            public Wall(double XCords, double YCords, RotateTransform rotateTransform, double RLenght)
            {
                x1 = XCords;
                y1 = YCords;
                rotation = rotateTransform;
                length = RLenght;
            }
            public double X1Cords
            {
                get { return x1; }
                set { }
            }
            public double Y1Cords
            {
                get { return y1; }
                set { }
            }
            public double X2Cords
            {
                get { return (x1 + wall.Width); }
                set { }
            }
            public double Y2Cords
            {
                get { return (y1 + wall.Height); }
                set { }
            }

            public Rectangle WallConstruct()
            {
                wall.Width = 2;
                wall.Height = length;
                wall.HorizontalAlignment = 0;
                wall.VerticalAlignment = 0;
                wall.LayoutTransform = rotation;
                wall.Margin = new Thickness(x1, y1, 0, 0);
                wall.Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
                return wall;
            }
        }
            public class Ray
        {
            private double x3;
            private double y3;
            private double rotation;
            private double length;
            private double width;
            private Rectangle ray = new Rectangle();
            public Ray(double XCords, double YCords, double rotateTransform, double RLenght)
            {
                x3 = XCords;
                y3 = YCords;
                rotation = rotateTransform;
                length = RLenght;
            }
            public double X3Cords
            {
                get { return x3; }
                set { x3 = value; RayUpdate(); }
            }
            public double Y3Cords
            {
                get { return y3; }
                set { y3 = value; RayUpdate(); }
            }
            public double X4Cords
            {
                get { return (x3 + (Math.Cos(ToRadiant(rotation)))); }
                set { }
            }
            public double Y4Cords
            {
                get { return (y3 + (Math.Sin(ToRadiant(rotation)))); }
                set {  }
            }
            public double Length
            {
                get { return length; }
                set { length = value; RayUpdate(); }
            }
            public double Width
            {
                get { return width; }
                set { width = value; RayUpdate(); }
            }
            public double Rotation
            {
                get { return rotation; }
                set { rotation = value; RayUpdate(); }
            }
            public Rectangle RayConstruct()
            {
                ray.Width = 0;
                ray.Height = length;
                ray.HorizontalAlignment = HorizontalAlignment.Left;
                ray.VerticalAlignment = VerticalAlignment.Top;
                ray.LayoutTransform = new RotateTransform(rotation, 0, 0);
                ray.RenderTransform = new RotateTransform(rotation, 0, 0);
                ray.Margin = new Thickness(x3, y3, 0, 0);
                ray.Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
                return ray;
            }
            private void RayUpdate()
            {
                ray.Margin = new Thickness(x3, y3, 0, 0);
                ray.Height = length;
                ray.LayoutTransform = new RotateTransform(rotation, 0, 0);
                ray.Width = width;
            }
        }
        public (double, double) GetPosition(Shape shape)
        {
            return ((shape.Margin.Left), (shape.Margin.Top));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(Mouse.GetPosition(this));
            // ray.Rotation = ray.Rotation + 10;
            // Console.WriteLine(Math.Cos(ToRadiant(ray.Rotation)));
            foreach (Wall wall in walls)
            {
                rootGrid.Children.Add(wall.WallConstruct());
            }
            foreach (Ray ray in rays)
            {
                rootGrid.Children.Add(ray.RayConstruct());
            }
            
        }
        public static double ToRadiant(double degree)
        {
            return degree * Math.PI / 180;
        }
        public double ToDegree(double radiant)
        {
            return radiant * 180 / Math.PI;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Ray ray in rays)
            {
                ray.X3Cords = Mouse.GetPosition(this).X;
                ray.Y3Cords = Mouse.GetPosition(this).Y;
                //ray.Rotation = ToDegree(Math.Atan(Math.Tan(Mouse.GetPosition(this).X - ray.X3Cords / Mouse.GetPosition(this).Y - ray.Y3Cords)));
                // ray.Rotation = ToDegree(Math.Asin(Math.Sin(Mouse.GetPosition(this).X - ray.X3Cords / Mouse.GetPosition(this).Y - ray.Y3Cords)));
                foreach (Wall wall in walls)
                {
                    double den = ((wall.X1Cords - wall.X2Cords) * (ray.Y3Cords - ray.Y4Cords) - (wall.Y1Cords - wall.Y2Cords) * (ray.X3Cords - ray.X4Cords));
                    if (den == 0)
                    {
                        ray.Width = 150;
                        return;
                        
                    }
                    else
                    {
                        double t = (((wall.X1Cords - ray.X3Cords) * (ray.Y3Cords - ray.Y4Cords) - (wall.Y1Cords - ray.Y3Cords) * (ray.X3Cords - ray.X4Cords)) / den);
                        double u = (-((wall.X1Cords - wall.X2Cords) * (wall.Y1Cords - ray.Y3Cords) - (wall.Y1Cords - wall.Y2Cords) * (wall.X1Cords - ray.X3Cords)) / den);
                        //Console.WriteLine(t);
                        //Console.WriteLine(u);
                        if (t > 0 && t < 1 && u > 0)
                        {
                            
                            Vector pt = new Vector();
                            pt.X = wall.X1Cords + t * (wall.X2Cords - wall.X1Cords);
                            pt.Y = wall.Y1Cords + t * (wall.Y2Cords - wall.Y1Cords);
                            ray.Width = Math.Sqrt(Math.Pow(pt.X - ray.X3Cords, 2) + Math.Pow(pt.Y - ray.Y3Cords, 2));
                        }
                        else
                        {

                            
                            ray.Width = 150;
                        }
                    }
                }
            }
        }
    }
}
