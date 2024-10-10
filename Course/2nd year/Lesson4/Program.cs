using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;





public class Lesson4
{
    public void Practice1(int[] numbers)
    {
        int total = 0;
        foreach(int number in numbers)
        {
            total += number;
        }
        Console.WriteLine(total);
    }


    public void Practice2(int[] numbers)
    {
        int max = int.MinValue;
        foreach (int number in numbers)
            if (number > max) max = number;
        Console.WriteLine(max);

        // Можно и юзнуть обычную функцию
        //Console.WriteLine(numbers.Max());
    }

    public void Practice3(int[] numbers)
    {

        //foreach (int number in numbers) Console.WriteLine(number); //либо так вывести
        Console.WriteLine(string.Join(" ", numbers));

        //Array.Reverse(numbers); //можно юзнуть статический метод
        for (int i = 0; i < numbers.Length / 2; i++)
        {
            (numbers[i], numbers[^(i + 1)]) = (numbers[^(i + 1)], numbers[i]);
        }
        Console.WriteLine(string.Join(" ", numbers));
    }

    public void Practice4(int[] numbers)
    {
        foreach (int number in numbers) if (number % 2 == 0) Console.WriteLine(number);
    }

    public void Practice5(int[] numbers)
    {
        int total = 0;
        foreach (int number in numbers) if (number < 0) total++;
        Console.WriteLine(total);
    }

    public void Practice6(int[] numbers, int find)
    {
        // PS: вообще есть разные методы нахождение чего-то либо в массива или даже string (так как string тоже "считаеться" как массив char-ов или симв-ов).
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == find)
            {
                Console.WriteLine(i);
                break;
            }
        }
    }

    public void Practice7(int[] numbers)
    {
        for (int i = 1; i < numbers.Length; i += 2) Console.WriteLine(numbers[i]);
    }


    public void Practice8(int[] numbers)
    {
        Practice2(numbers); // уже описана функция: нахождение мин. знач.

        int min = int.MaxValue;
        foreach (int number in numbers)
            if (number < min) min = number;
        Console.WriteLine(min);


        // можно и юзнуть обычные фунции
        //Console.WriteLine(numbers.Max());
        //Console.WriteLine(numbers.Min());
    }

    public void Practice9(int[] numbers)
    {
        for (int counter = 0; counter < numbers.Length; counter++)
        {
            for (int sort = 0; sort < numbers.Length - 1; sort++)
            {
                if (numbers[sort] > numbers[sort + 1])
                {
                    (numbers[sort], numbers[sort + 1]) = (numbers[sort + 1], numbers[sort]);
                }
            }
        }


        Console.WriteLine(string.Join(" ", numbers));


    }


    public void Practice10(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 0) numbers[i] = 0;
        }

        Console.WriteLine(string.Join(" ", numbers));

    }


    public void HomeWork1()
    {
        // не понял задачу, где "Элементы второго массива должны следовать за элементами первого массива." 
        // если нужно чтобы c = a потом b, то оставте, иначе (при c = b потом a), просто надо поменять 2 массива при их инициализации либо инвертировать код
        int[] a = [1, 2, 3];
        int[] b = [4, 5, 6];


        // либо так
        //int[] c = new int[a.Length + b.Length];
        //a.CopyTo(c, 0);
        //b.CopyTo(c, b.Length);

        // либо вот так
        //int[] c = a.Union(b).ToArray();

        // либо же вот так
        int[] c = a.Concat(b).ToArray();

        Console.WriteLine(string.Join(" ", c));
    }

    public static int[] CyclicRotation(int[] A, int K)
    {

        if (A.Length == 0 || A.Length == 1)
        {
            return A;
        }
        int lastElement;
        int[] newArray = new int[A.Length];

        List<int> listOfNumbers = new List<int>();

        for (int i = 1; i < K + 1; i++)
        {

            lastElement = A[A.Length - 1];
            newArray = A.Take(A.Length - 1).ToArray();
            listOfNumbers = newArray.ToList<int>();
            listOfNumbers.Insert(0, lastElement);

            A = listOfNumbers.ToArray();
            newArray = A;

        }
        return newArray;
    }

    public void HomeWork2(int[] numbers, int positionOffset)
    {
        Console.WriteLine(string.Join(" ", CyclicRotation(numbers, positionOffset)));
    }


}




namespace Testy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lesson = new Lesson4();

            // аргумент - массив int чисел;

            //lesson.Practice1([1, 2, 3]); // выводит сумму эл-ов массива.
            //lesson.Practice2([1, 2, 4, 3]); // выводит maximal int-value из int-массива.
            //lesson.Practice3([1, 2, 3, 4, 5]); // выводит изначальный массив, потом переворачивает его и опять-же выводит.
            //lesson.Practice4([1, 2, 3, 4, 5, 6]); // выводит чётные числа из массива int-чисел.
            //lesson.Practice5([1, 2, 3, 4, 5, 6, -1]); // считает и выводит кол-во отрицательных значений int-чисел.
            //lesson.Practice6([1, 2, 3, 4, 5, 6, -1], -1); // выводит индекс (позицию) нахождения данного значения в этом массиве; аргументы: массив int-чисел и int-число (то что надо найти).
            //lesson.Practice7([1, 2, 3, 4, 5, 6, 7]); // выводит числа, стоящие на нечётных позициях ({1,3,5, ...} % 2 != 0, start-index=0).
            //lesson.Practice8([1, 2, 3, 4, 5, 6, -7]); // выводит сначало max int-число, а потом min int-число.
            //lesson.Practice9([3, 1, 4, 2, 0, -2, -1, -3]); // выводит уже сортированный список.
            //lesson.Practice10([1,2,3,45,0, -1, -20]); // заменяет отрицательные числа на int-число:0, и выводит сам список.


            //lesson.HomeWork1(); // объединяет 2 массива; аргументов нету.
            //lesson.HomeWork2([1,2,3,4,5], 2); // делает сдвиг вправо в массиве на заданное число и выводит сам массив; аргументы: массив и кол-во сдвигов вправо.

            Console.ReadLine(); // просто стопает прогу.

        }
    }
}