using Microsoft.Win32;
using System;
using System.Threading.Channels;


public class Lesson2
{
    private string DefaultName = "user";
    private int CurrentYear = DateTime.Now.Year;

    private string? name;
    private string? phoneNumber;

    public int num1;
    public int num2;


    public void PracticeA1(string name)
    {
        if (name.Contains('?'))
            Console.WriteLine($"Hello, my dear '{(name == "?" ? DefaultName : name.Remove(name.Length - 1))}'!");
        else
            Console.WriteLine($"Hello, my dear '{name}'!");
    }


    public void PracticeA1() => PracticeA1(Console.ReadLine() + '?');


    public void PracticeA2(int year) => Console.WriteLine(CurrentYear - year);

    public void PracticeA2()
    {
        try
        {
            PracticeA2(Convert.ToInt32(Console.ReadLine()));
        }
        catch (FormatException)
        {
            Console.WriteLine("Please write a year of your birth...");
        }
    }


    public void PracticeB1(string name, string number)
    {
        if (name == "?" || number == "?")
        {
            Console.WriteLine("Type a symbols...");
            return;
        }
        else if (name.Contains('?')) name = name.Remove(name.Length - 1);
        if (number.Contains('?')) number = number.Remove(number.Length - 1);

        this.name = name;
        phoneNumber = number;

    }

    public void PracticeB1(bool cout)
    {
        if (cout)
        {
            Console.WriteLine(name);
            Console.WriteLine(phoneNumber);
        }
        else
        {
            PracticeB1(Console.ReadLine() + '?', Console.ReadLine() + '?');
        }
    }


    public void PracticeB2()
    {
        PracticeB2(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
    }



    public void PracticeB2(int number1, int number2)
    {
        if (number1 > number2)
        {
            Console.WriteLine(number1);
            Console.WriteLine(number2);
        }
        else
        {
            Console.WriteLine(number2);
            Console.WriteLine(number1);
        }
    }

    public void PracticeB2(float number1, float number2)
    {
        if (number1 > number2)
        {
            Console.WriteLine(number1);
            Console.WriteLine(number2);
        }
        else
        {
            Console.WriteLine(number2);
            Console.WriteLine(number1);
        }
    }

    public void PracticeB2(double number1, double number2)
    {
        if (number1 > number2)
        {
            Console.WriteLine(number1);
            Console.WriteLine(number2);
        }
        else
        {
            Console.WriteLine(number2);
            Console.WriteLine(number1);
        }
    }


    public void PracticeB2(int[] nums)
    {
        Console.WriteLine(nums.Max());
        Console.WriteLine(nums.Min());
    }

    public void PracticeB2(float[] nums)
    {
        Console.WriteLine(nums.Max());
        Console.WriteLine(nums.Min());
    }

    public void PracticeB2(double[] nums)
    {
        Console.WriteLine(nums.Max());
        Console.WriteLine(nums.Min());
    }

    public void PracticeC(int number)
    {
        string result = number.ToString();
        for (int i = 0; i < result.Length; i++)
        {
            Console.WriteLine(result[i]);
        }

    }

    public void PracticeC()
    {
        try
        {
            PracticeC(Convert.ToInt32(Console.ReadLine()));
        }
        catch (FormatException)
        {
            Console.WriteLine("Type a number...");
        }
    }


    public void HomeWork1() => HomeWork1(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));

    public void HomeWork1(int number1, int number2)
    {
        num1 = number1;
        num2 = number2;
        Console.WriteLine(number2 + number1);
    }


    public void HomeWork2() => HomeWork2(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));

    public void HomeWork2(int min, int max)
    {
        Random rnd = new Random();
        int number = rnd.Next(min, max);
        int? answer = null;

        Console.WriteLine($"Введите число от:{min} до:{max} / Enter a number from:{min} to:{max}");

        while (number != answer)
        {
            answer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(answer != number ? (answer > number ? "Меньше / Smaller" : "Больше / Bigger") : "Поздравляем! вы попедили! / Congratilations! you won!");
        }
    }
}




namespace Testy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lesson = new Lesson2();
            //lesson.PracticeA1(); // правктика А1; надо ввести имя.
            //lesson.PracticeA1("lala"); //правктика А1; имя уже в аргументе.


            //lesson.PracticeA2(); // правктика А2; надо ввести год.
            //lesson.PracticeA2(2000);  // правктика А2; год введён.


            //lesson.PracticeB1(false); // правктика B1; принимает аргумент bool cout "выводить или нет", если cout == true, то попросит ввести имя, а потом номер телефона.
            //Если cout == true, просто выводит имя и номер телефона. PS:все данные храняться в объекте(lesson) класса(Lesson2).
            //lesson.PracticeB1(true);  // правктика B1, год введён.

            //lesson.PracticeB1("artem", "+7(000)-000-22-00"); // тоже самое как "lesson.PracticeB1(false)", но тут не надо вводить данные.
            //lesson.PracticeB1(true); // выведет данные.


            //lesson.PracticeB2(1, 4); // практика B2; принимает 2 аргумента int int, float float, double double, или их массвы, выводит большое, потом маленькое.
            //lesson.PracticeB2(1, 4); // практика B2; без аргументов, попросит пользователя ввести 2 int-а.

            //lesson.PracticeB2(new int[] { 1, 2, 3 }); //пример с массивом int.


            // lesson.PracticeC(123); // практика C; принимает 1 аргумент десятичное int-вое число, выводит все разряды этого числа.
            // lesson.PracticeC() // практика C; без аргумента, попросит пользователя ввести десятичное int-вое число, выводит тоже самое.

            // Д/З:

            //lesson.HomeWork1(1, 1); // домашнее задание 1; принимает 2 аргумента int int, выводит их сумму и сохраняет их в объекте(lesson) классе(Lesson2).
            //Console.WriteLine("number1:" + lesson.num1 + "  number2:" + lesson.num2); // вывод этих самых чисел.

            //lesson.HomeWork1(); // без аргументов - попросит их ввести)
            //Console.WriteLine("number1:" + lesson.num1 + "  number2:" + lesson.num2); // вывод этих самых чисел.


            //lesson.HomeWork2(); // домашнее задание 2; без аргумента - попросит Вас ввести 2 числа min и max
            //lesson.HomeWork2(1, 100); // домашнее задание 2; аргументы введены, просто играйте!

            Console.ReadLine(); // просто стопает прогу.

        }
    }
}