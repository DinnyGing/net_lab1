using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Numerics;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White; // Установите белый фон
            Console.ForegroundColor = ConsoleColor.Black; // Установите черный цвет текста
            Console.Clear();
            Stopwatch stopwatch = new Stopwatch();
            BigInteger determinant = 0;
        
            Matrix matrix = new Matrix(50000);
            matrix.FillIn();
            //matrix.Print(matrix.Data);

            Console.WriteLine("---------------Matrix---------------");

            stopwatch.Start();
            determinant = matrix.Determinant();
            stopwatch.Stop();
            double t1 = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("------------------------------");

            ParallelMatrix parallelMatrix = new ParallelMatrix(matrix.Data, 12);
            //parallelMatrix.FillIn();
            //parallelMatrix.Print(parallelMatrix.Data);
            Console.WriteLine("--------------ParallelMatrix----------------");

            stopwatch = new Stopwatch();
            stopwatch.Start();
            determinant = parallelMatrix.Determinant();
            stopwatch.Stop();
            double t2 = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("------------------------------");

            ThreadMatrix threadMatrix = new ThreadMatrix(matrix.Data, 12);
            //parallelMatrix.FillIn();
            //parallelMatrix.Print(parallelMatrix.Data);
            Console.WriteLine("--------------ThreadMatrix----------------");

            stopwatch = new Stopwatch();
            stopwatch.Start();
            determinant = threadMatrix.Determinant();
            stopwatch.Stop();
            double t3 = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine(t1 /t2);
            Console.WriteLine(t1 /t3);

        }
    }
}