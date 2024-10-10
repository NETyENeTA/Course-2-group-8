using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;


public class Lesson3
{
   public void PracticeA()
    {
        int[] a = [1, 2, 3, 4, 5];
        string[] b = [ "Hello, World!", "How are you?", "Luck and change" ];
        Console.WriteLine(string.Join(" ", a));
        Console.WriteLine(string.Join(" ", b));
    }


    public void PracticeB() 
    {
        int[] a = [1, 2, 3, 4, 5];
        Console.WriteLine(a[1]);
        a[2] = 100;
        Console.WriteLine(a.Length);
    }
    public void PracticeC()
    {
        int[] a = new int[4]; // не задан тип массива в Т/З, так что выбрал int
        int[] b = new int[5] { 1, 2, 3, 4, 5 };
        char[] c = ['a', 'c', 'b'];
        //char[] c = new char[3]; // либо так.

        int[] ac = new int[5];

        ac[0] = 1;
        ac[1] = 2;
        ac[2] = 3;
        ac[3] = 4;
        ac[4] = 5;

        //for (int i = 0; i < ac.Length; i++) ac[i] = i + 1; можно заполнить массив вот так.


        //int[] ac = [1, 2, 3, 4, 5]; // либо так.
        //int[] ac = new int[5] { 1, 2, 3, 4, 5 }; // либо вот так.
    }
}





namespace Testy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lesson = new Lesson3();

            //lesson.PracticeA(); // Объявил массив целых чисел длины 5. Инициализировал массив из трех строк. Вывел их.
            //lesson.PracticeB(); // Создал массив чисел и вывел второй элемент. Изменил третий элемент массива на 100. Вывел длину массива.
            //lesson.PracticeC(); // Объявил пустой массив длины 4. Создал массив из пяти чисел, используя оператор new. Инициализировал массив из трёх символов.
            // Создал массив из пяти элементов и вручную заполнил его значениями.


            Console.ReadLine(); // просто стопает прогу.

        }
    }
}