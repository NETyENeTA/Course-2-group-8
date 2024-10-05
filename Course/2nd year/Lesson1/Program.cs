using System;


public class Lesson1
{
    public void PracticeA()
    {
        int a = 0;
        float b = 0.1f;
        string c = "Hello, World!";
        bool d = true;

        Console.WriteLine(a);
        Console.WriteLine(b);
        Console.WriteLine(c);
        Console.WriteLine(d);
    }

    public void PracticeB(int number1, int number2)
    {
        Console.WriteLine("sum:" + (number1 + number2));
        Console.WriteLine("sub:" + (number1 - number2));
        Console.WriteLine("div:" + (number1 / number2));
        Console.WriteLine("mul:" + (number1 * number2));
    }

    public void PracticeB()
    {
        try
        {
            PracticeB(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
        }
        catch (FormatException)
        {
            PracticeB(0, 0);
        }
  
    }


    public void PracticeC(string? line)
    {
        if (line != null)
        {
            try
            {
                Console.WriteLine(int.Parse(line) * 5);
            }
            catch (FormatException)
            {
                Console.WriteLine("Written NULL!!!");
            }
            
        }
        else
        {
            Console.WriteLine("Written NULL!!!");
        }
    }

    public void PracticeC() => PracticeC(Console.ReadLine());

}





namespace Testy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lesson = new Lesson1(); // раскоментируйте комманды :>
            //lesson.PracticeA(); // просто практика А.


            //lesson.PracticeB(); // практика Б; без аргументов, просит ввести пользователя числа.
            //lesson.PracticeB(1, 2); // тоже самое ток с аргументами.


            //lesson.PracticeC();// практика С; без аргументов, просит ввести пользователя строку.
            //lesson.PracticeC("3");// практика С, уже с аргументом string.

            Console.ReadLine(); // просто стопает прогу.

        }
    }
}