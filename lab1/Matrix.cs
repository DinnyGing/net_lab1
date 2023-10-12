using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace lab1
{
    public class Matrix
    {
        public int[,] Data;
        int n;
        Random random = new Random();
        public Matrix(int n)
        {
            Data = new int[n, n];
            this.n = n;
        }
        public Matrix(int[,] matrix)
        {
            this.Data = matrix;
            this.n = matrix.GetLength(0);
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
                BigInteger determ = 0;
                int length = Data.GetLength(0);
                for (int i = 0; i < length; i++)
                {
                    BigInteger r1 = 1;
                    BigInteger r2 = 1;
                    for (int j = 0; j < length; j++)
                    {
                        if (j + i >= length)
                        {
                            r1 *= Data[j, j + i - length];
                            r2 *= Data[j, length * 2 - i - j - 1];
                        }
                        else
                        {
                            r1 *= Data[j, j + i];
                            r2 *= Data[j, length - i - j - 1];
                        }
                    }
                    determ += r1;
                    determ -= r2;
                }
                return determ;

            }
        }
    }
}
