using System;
using System.Linq;

namespace PracticeAB;

class Program
{


    // Творим тут


    //1
    private static int Add(int a, int b) => a + b;

    //2
    private static bool IsEven(int number) => number % 2 == 0;

    //3
    private static string ReversString(string s) => new(s.Reverse().ToArray());

    //4
    private static int FindMax(int[] arr) => arr.Max();

    //5
    private static int factorial(int sallary) => sallary * 12;

    //6
    private static double CelsiusToFahrenheit(int celsius) => celsius * 1.8 + 32;

    //7
    private static int CountVowels(string s, string vowels = "аиоуеёяюэ") => s.ToLower().Count(c => vowels.Contains(c));

    //8
    private static int? GeneratePassword(string passToHack)
    {
        if (passToHack.Length != 4 || !int.TryParse(passToHack, out _)) throw new ArgumentException("Пароль должен состоять из 4 цифр.");

        int count = 0;
        for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                for (int z = 0; z < 10; z++)
                    for (int h = 0; h < 10; h++)
                    {
                        count++;
                        if (x.ToString() + y.ToString() + z.ToString() + h.ToString() == passToHack)
                        {
                            Console.WriteLine("Ура! Вы взломали пароль теперь вы хакер");
                            return count;
                        }
                    }
        return null;
    }


    static void Main(string[] args)
    {
        // это функция мейн которая вызывает все отсальные функции для практики А и Б
        //вызов первой функци.... и т.п.
    }
}
