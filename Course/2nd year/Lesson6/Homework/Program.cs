using System;
using System.Collections.Generic;
using System.Linq;


namespace Testy
{
    class Program
    {

        public static int? GetMaxClone(int[] numbers)
        {
            Dictionary<int, int> dict = [];

            foreach (int number in numbers)
            {
                dict.TryGetValue(number, out int total);
                dict[number] = total + 1;
            }

            foreach (var el in dict)
            {
                if (el.Value == dict.Values.Max()) return el.Key;
            }
            return null;
        }

        public static string? GetMaxClone(string[] numbers)
        {
            Dictionary<string, int> dict = [];

            foreach (string number in numbers)
            {
                dict.TryGetValue(number, out int total);
                dict[number] = total + 1;
            }

            foreach (var el in dict)
            {
                if (el.Value == dict.Values.Max()) return el.Key.ToString();
            }
            return null;
        }


        public static int[,] TransposeMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[,] result = new int[cols, rows];

            for (int i = 0; i < cols; ++i) for (int j = 0; j < rows; ++j) result[i, j] = matrix[j, i];

            return result;
        }


        public static void Transpose()
        {
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            int rows = 2;
            int cols = 3;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++) Console.Write($"{matrix[i, j]}\t");
                Console.WriteLine();
            }

            Console.WriteLine();

            matrix = TransposeMatrix(matrix);

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++) Console.Write($"{matrix[i, j]}\t");
                Console.WriteLine();
            }

        }


        static void Main(string[] args)
        {
            // Ну тут уже всё понятно по названиям функциям


            //Console.WriteLine(GetMaxClone([1, 2, 3, 3, 1, 1]));
            //Console.WriteLine(GetMaxClone(["one", "one", "two"]));

            //Transpose();

            Console.ReadLine(); // простая остановочка :)
        }
    }
}