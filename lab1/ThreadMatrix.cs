using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace lab1
{
    internal class ThreadMatrix
    {
        public int[,] Data;
        int n;
        Random random = new Random();

        object objLock = new object();

        BigInteger determ = 0;
        int countOfThreads = 0;
        public ThreadMatrix(int n, int countOfThreads)
        {
            Data = new int[n, n];
            this.n = n;
            this.countOfThreads = countOfThreads > Environment.ProcessorCount ? Environment.ProcessorCount : countOfThreads;

        }
        public ThreadMatrix(int[,] matrix, int countOfThreads)
        {
            this.Data = matrix;
            this.n = matrix.GetLength(0);
            this.countOfThreads = countOfThreads > Environment.ProcessorCount ? Environment.ProcessorCount : countOfThreads;
        }
        public void FillIn()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Data[i, j] = random.Next(-2, 3);
                }
            }
        }
        public void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Write(matrix[i, j] + "\t");
                }
                WriteLine();
            }
        }
        private void InSide(int k)
        {
            BigInteger r1 = 1;
            BigInteger r2 = 1;
            for (int j = 0; j < n; j++)
            {
                if (j + k >= n)
                {
                    r1 *= Data[j, j + k - n];
                    r2 *= Data[j, n * 2 - k - j - 1];
                }
                else
                {
                    r1 *= Data[j, j + k];
                    r2 *= Data[j, n - k - j - 1];
                }
            }
            lock (objLock)
            {
                determ += r1;
                determ -= r2;
            }
        }
        public BigInteger Determinant()
        {
            if (Data.GetLength(0) == 1)
            {
                return Data[0, 0];
            }
            else if (Data.GetLength(0) == 2)
            {
                return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
            }
            else
            {
                int length = Data.GetLength(0);
                Queue<Thread> queueInRun = new Queue<Thread>();
                int step = (int)Math.Ceiling((double)length / countOfThreads);
                for (int i = 0; i < step * countOfThreads; i+= step)
                {
                    int k = i;
                    Thread thread = new Thread(() =>
                    {
                        WriteLine("Thread" + k + " come in");
                        for (int j = 0; j < step && length>0; j++)
                        {
                            lock (objLock)
                            {
                                length--;
                            }
                            InSide(k + j);
                        }
                        Thread.Sleep(1000);
                        WriteLine("Thread" + k + " come out");

                    });
                    thread.Start();
                    queueInRun.Enqueue(thread);
                }
                while (queueInRun.Count > 0)
                {
                    Thread thread = queueInRun.Peek();
                    if (!thread.IsAlive)
                        queueInRun.Dequeue();
                }
                return determ;

            }
        }
    }
}
