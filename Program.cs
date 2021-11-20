using System;
using System.Numerics;
using Raylib_cs;

namespace Mandelbrot_Set
{
    class Program
    {
        const int WIDTH = 1200;
        const int HEIGHT = 800;
        const int SPACING = 1;

        const int MAX_ITERATIONS = 50;

        static double minX = -3;
        static double maxX = 1.5;
        static double minY = -1.5;
        static double maxY = 1.5;

        static double juliaX = -0.8;
        static double juliaY = 0.156;

        const double ZOOM_AMOUNT = 2;

        static void Main(string[] args)
        {
            Raylib.InitWindow(WIDTH, HEIGHT, "Mandelbrot Set");
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) Zoom();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);
                for (int x = 0; x < WIDTH; x += SPACING)
                {
                    for (int y = 0; y < HEIGHT; y += SPACING)
                    {
                        double a = Map(x, 0, WIDTH, minX, maxX);
                        double b = Map(y, 0, HEIGHT, minY, maxY);
                        Display(Mandelbrot(a, b), x, y);
                    }
                }
                Raylib.EndDrawing();
            }
        }

        static void Zoom()
        {
            // TODO
        }

        static int Mandelbrot(double x, double y)
        {
            double origX = x;
            double origY = y;

            int iterations = 0;
            while (x*x + y*y <= 16 && iterations < MAX_ITERATIONS)
            {
                double xtemp = x * x - y * y + origX;
                y = 2 * x * y + origY;
                x = xtemp;
                iterations++;
            }
            return iterations;
        }

        static void Display(int mandelbrotIterations, int x, int y)
        {
            float hue = (int) ( 360 * mandelbrotIterations / MAX_ITERATIONS );
            float value = mandelbrotIterations < MAX_ITERATIONS ? 1 : 0;

            //hue = (float) mandelbrotIterations;
            //value = 1;

            if (SPACING == 1) Raylib.DrawPixel(x, y, Raylib.ColorFromHSV(hue, 1, value));
            else Raylib.DrawRectangle(x, y, SPACING, SPACING, Raylib.ColorFromHSV(hue, 1, value));
        }

        static double Map(double value, double minValue, double maxValue, double minTarget, double maxTarget)
        {
            return (value - minValue) / (maxValue - minValue) * (maxTarget - minTarget) + minTarget;
        }
    }
}
