using System;
using System.Linq;


namespace Testy
{
    class Program
    {
        public static int Sum(int a, int b) => a + b;
        public static void Greet(string? name = null) => Console.WriteLine(name != null ? $"Hello, {name[0].ToString().ToUpper() + name[1..]}!" : "Oh, you didn't write your name...");

        // Мы уже показывали как найти max, так что я заюзал: Math.Max();
        public static int Max(int a, int b) => Math.Max(a, b);

        public static bool IsParity(int a) => a % 2 == 0;

        // Если есть ошибке в формуле, то извините, интернет подсказал формулу; а так просто непишите мне.
        public static double FromC2F(int temperature) => temperature * 1.8 + 32;

        public static string Reverse(string line) => new(line.Reverse().ToArray());

        public static int Count(string line, char find) => line.Count(x => x == find);
        public static int[] Count(string line, char[] finds)
        {
            int[] result = new int[finds.Length];

            for (int i = 0; i < finds.Length; i++) result[i] = line.Count(x => x == finds[i]);

            return result;
        }

        public static int Factorial(int a)
        {
            if (a == 0 || a == 1) return 1;
            return Factorial(a - 1) * a;
        }

        public static bool IsPrime(int a)
        {
            if (a == 1 || a == 0) return false;
            for (int i = 2; i <= Math.Sqrt(a); i++) if (a % i == 0) return false;
            return true;
        }


        public static int RandRange(int min, int max) => new Random().Next(min, max);


        static void Main(string[] args)
        {
            // Ну тут уже всё понятно по названиям функциям

            //Console.WriteLine(Sum(1, 3));

            //Greet();

            //Console.WriteLine(Max(1, 3));

            //Console.WriteLine("{0} | {1}", IsParity(1), IsParity(4));

            //Console.WriteLine(FromC2F(10));

            //Console.WriteLine(Reverse("12345"));

            //Console.WriteLine(string.Join(" ", Count("hello", 'l')));
            //Console.WriteLine(string.Join(" ", Count("hello", ['h', 'l'])));

            //Console.WriteLine(Factorial(5));

            //Console.WriteLine(IsPrime(3));

            //Console.WriteLine(RandRange(1, 4));

            Console.ReadLine(); // простая остановочка :)

        }
    }
}