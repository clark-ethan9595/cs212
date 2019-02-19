using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FernNamespace
{
    /*
     * this class draws a fractal fern when the constructor is called.
     * Written as sample C# code for a CS 212 assignment -- October 2011.
     * Bugs: WPF and shape objects are the wrong tool for the task 
     */   
    class Fern
    {
        private static int TENDRILMIN = 5;      //Minimum length that a tendril can be
        private static double SEGLENGTH = 3.0;      //Number of pixels that the line draws at one time
        private Random random_num = new Random();       //Create an instance of the Random Class to be used later
        
        /* 
         * Fern constructor erases screen and draws a fern
         * 
         * Size: number of 3-pixel segments of tendrils
         * Redux: how much smaller children clusters are compared to parents
         * Branches: number of branches for each recursive call of the fractal
         * canvas: the canvas that the fern will be drawn on
         */
        public Fern(double size, double redux, double branches, Canvas canvas)
        {
            canvas.Children.Clear();        // delete old canvas contents
            int num_branches = (int)Math.Round(branches);       //Set the number of branches to an integer value
            tendril((int) (canvas.Width / 2), 480, size, redux, num_branches, 0, canvas);       //Call tendril to begin drawing the fractal fern

            DrawFlower(canvas, 50, 480);        //Draw flowers on the left of the fractal fern
            DrawFlower(canvas, 200, 480);
            DrawFlower(canvas, 600, 480);       //Draw flowers on the right of the fractal fern
            DrawFlower(canvas, 450, 480);
        }

        /*
         * tendril draws a tendril (a randomly-wavy line) in the given direction, for the given length, 
         * and draws a cluster at the other end if the line is big enough.
         */
        private void tendril(int x1, int y1, double size, double redux, int branches, double angle, Canvas canvas)
        {
            int x2=x1, y2=y1;       //Pull the starting values out of the argument list

            int starting_y = y1;        //Store the start x for the line to be used in the Branches Function
            int starting_x = x1;        //Store the start y for the line to be used in the Branches Function

            for (int i = 0; i < size; i++)      //Draw the entire line with 3 pixel-long segements, each segment with a different shade of a green/yellow/red tint
            {
                x1 = x2; y1 = y2;       //Set the starting x1 and y1 values for where the line to start drawing

                x2 = x1 + (int)(SEGLENGTH * Math.Sin(angle));       //Set the ending x2 value for where the line to draw to
                y2 = y1 - (int)(SEGLENGTH * Math.Cos(angle));       //Set the ending y2 value for where the line to draw to

                byte red = Convert.ToByte(random_num.Next(100, 220));       //First aspect of randomness in my project - random color for the trendril
                byte green = Convert.ToByte(random_num.Next(120, 255));     // First aspect of randomness in my project - random color for the tendril

                line(x1, y1, x2, y2, red, green, 0, 1+size/80, canvas);     //Draw the tendril
            }

            int star = random_num.Next(0, 7);       //Third and final randomness aspect - each tendril has a 1 in 7 chance of having a star at its end

            if (star == 0)      //Determine if the tendril is going to have a star drawn at its end
            {
                DrawStar(x2, y2, canvas);
            }

            if (size > TENDRILMIN)      //Check if the size is less than our determined/set minimum length a tendril can be (10)
                Branches(x2, y2, starting_y, starting_x, size / redux, redux, branches, canvas, angle);     //If the size is still greater, call Branches function
        }

        /*
         * Function that is called that creates the branches off of the tendril line
         * Calculates where on the tendril the certain number of branches should start
         * Calculates the angle at which the resulting branches should come off
         * Calls the function tendril twice, once for the left side branches and once for the right side branches
         */

        private void Branches(int x1, int y1, int starting_y, int starting_x, double size, double redux, int branches, Canvas canvas, double direction)
        {
            int x2 = x1, y2 = y1;       //Pull out the points where the Branch is going to start

            int factor_y = (y1 - starting_y) / (branches + 1);      //Split up the y value of the line into evenly spaced sections
            int factor_x = (x1 - starting_x) / (branches + 1);      //Split up the x value of the line into evenly spaced sections

            for (int i = 0; i < branches; i++)      //Loop through however many branches the user wants, determined by the Branches Slider
            {
                x2 = x2 - factor_x;     //Subtract by the factor to get the x-value next location for the next branch
                y2 = y2 - factor_y;     //Subtract by the factor to get the y-value for the next location for the branch

                double direction_left = direction;      //Create two direction variables for the left and for the right branches
                double direction_right = direction;

                direction_left -= 45;       //Subtract 45 degrees from the current angle for the left branches
                direction_right += 45;      //Add 45 degrees from the current angle for the right branches

                //Call the function tendril to create a tendril on the left side of the current tendril
                tendril(x2, y2, size, redux, branches, direction_left, canvas);

                //Call the function tendril to create a tendril on the right side of the current tendril
                tendril(x2, y2, size, redux, branches, direction_right, canvas);

            }
        }

        /*
         * draw a line segment (x1,y1) to (x2,y2) with given color, thickness on canvas
         */
        private void line(int x1, int y1, int x2, int y2, byte r, byte g, byte b, double thickness, Canvas canvas)
        {
            Line myLine = new Line();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, r, g, b);
            myLine.X1 = x1;
            myLine.Y1 = y1;
            myLine.X2 = x2;
            myLine.Y2 = y2;
            myLine.Stroke = mySolidColorBrush;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.StrokeThickness = thickness;
            canvas.Children.Add(myLine);
        }

        //Function to draw a flower on the canvas at a particular starting x and y coordinate point
        private void DrawFlower(Canvas canvas, int start_x, int start_y)
        {
            int flower_height = random_num.Next(30, 60);        //Second element of randomness, height of the drawn flowers is randomly selected
            line(start_x, start_y, start_x, start_y - flower_height, 44, 143, 58, 3, canvas);       //Draw line for the stem of the flower

            int circle_centerX = start_x;       //Get the center points for the circle to use for drawing the petals
            int circle_centerY = start_y - flower_height;

            Ellipse myEllipse = new Ellipse();      //Create a new ellipse for the top of the flower
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 200, 0, 0);
            myEllipse.Fill = mySolidColorBrush;        //Fill the circle of the flower to be red
            myEllipse.StrokeThickness = 2;      //Set the thickness of the circle's lines
            myEllipse.HorizontalAlignment = HorizontalAlignment.Center;
            myEllipse.VerticalAlignment = VerticalAlignment.Center;
            myEllipse.Width = 20;       //Set the height and the width of the circle of the flower
            myEllipse.Height = 20;
            myEllipse.SetCenter(start_x, start_y - flower_height);
            DrawPetals(circle_centerX, circle_centerY, canvas, flower_height);      //Call DrawPetals to draw two petals on the flower
            canvas.Children.Add(myEllipse);     //Add the flower to the canvas
        }

        private void DrawPetals(int centerX, int centerY, Canvas canvas, int flower_height)
        {
            Ellipse myEllipse = new Ellipse();      //Create a new ellipse for the flower's petal
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 200, 0, 215);
            myEllipse.Fill = mySolidColorBrush;     //Fill in the elipse color to be purple
            myEllipse.StrokeThickness = 2;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Center;
            myEllipse.VerticalAlignment = VerticalAlignment.Center;
            myEllipse.Width = 50;       //Set the height and the width of the petal to make it very oblong
            myEllipse.Height = 10;
            myEllipse.SetCenter(centerX, centerY);
            canvas.Children.Add(myEllipse);     //Add petal to the canvas

            Ellipse myEllipseOne = new Ellipse();       //Create a new ellipse for the flower's petal
            SolidColorBrush mySolidColorBrushOne = new SolidColorBrush();
            mySolidColorBrushOne.Color = Color.FromArgb(255, 200, 0, 215);
            myEllipseOne.Fill = mySolidColorBrushOne;       //Fill in the elipse color to be purple
            myEllipseOne.StrokeThickness = 2;
            myEllipseOne.HorizontalAlignment = HorizontalAlignment.Center;
            myEllipseOne.VerticalAlignment = VerticalAlignment.Center;
            myEllipseOne.Width = 10;        //Set the height and the width of the petal to make it very oblong
            myEllipseOne.Height = 50;
            myEllipseOne.SetCenter(centerX, centerY);
            canvas.Children.Add(myEllipseOne);      //Add petal to the canvas
        }

        //Function to draw a star on a canvas based on an inital x and y coordinate pair.
        private void DrawStar(int x, int y, Canvas canvas)
        {
            Polygon myPolygon = new Polygon();      //Create an instance of a polygon
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myPolygon.Fill = mySolidColorBrush;     //Fill in the polygon to be yellow in color
            myPolygon.StrokeThickness = 1.5;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Center;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;

            //Set the points needed to draw the lines to outline a star
            myPolygon.Points = new PointCollection() { new Point(x, y), new Point(x+1, y+1), new Point(x+3, y+1), new Point(x+2, y+2), new Point(x+3, y+4),
                                                        new Point(x, y+3), new Point(x-3, y+4), new Point(x-2, y+2), new Point(x-3, y+1), new Point(x-1, y+1) };

            canvas.Children.Add(myPolygon);     //Add the polygon star to the canvas
        }
    }

    /*
    * this class is needed to enable us to set the center for an ellipse (not built in?!)
    */
    public static class EllipseX
    {
        public static void SetCenter(this Ellipse ellipse, double X, double Y)
        {
            Canvas.SetTop(ellipse, Y - ellipse.Height / 2);
            Canvas.SetLeft(ellipse, X - ellipse.Width / 2);
        }
    }
}

