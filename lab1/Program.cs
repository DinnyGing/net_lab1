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
            Stopwatch stopwatch = new Stopwatch();
            BigInteger determinant = 0;
        
            Matrix matrix = new Matrix(20);
            matrix.FillIn();
            matrix.Print(matrix.Data);

            Console.WriteLine("------------------------------");

            stopwatch.Start();
            determinant = matrix.Determinant();
            stopwatch.Stop();

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("------------------------------");

            ParallelMatrix parallelMatrix = new ParallelMatrix(matrix.Data, 3);
            //parallelMatrix.FillIn();
            //parallelMatrix.Print(parallelMatrix.Data);
            Console.WriteLine("------------------------------");


            stopwatch.Start();
            determinant = parallelMatrix.Determinant();
            stopwatch.Stop();

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("------------------------------");

            ThreadMatrix threadMatrix = new ThreadMatrix(matrix.Data, 11);
            //parallelMatrix.FillIn();
            //parallelMatrix.Print(parallelMatrix.Data);
            Console.WriteLine("------------------------------");


            stopwatch.Start();
            determinant = threadMatrix.Determinant();
            stopwatch.Stop();

            Console.WriteLine($"Determinant: {determinant}");
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");

        }
    }
}