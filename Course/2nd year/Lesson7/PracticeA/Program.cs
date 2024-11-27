using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Testy;


public class Tree
{
    public int somthingInt { get; set; }
    public string somthingString { get; set; }
    public double somthingDouble { get; set; }
    public bool somthingBool { get; set; }

    public override string ToString() => $"Tree:\nint:{somthingInt}\nstring:{somthingString}\ndouble:{somthingDouble}\nbool:{somthingBool}";

}



class Program
{
    public static int Factorial(int a)
    {
        if (a == 0 || a == 1) return 1;
        return Factorial(a - 1) * a;
    }

    public static int Fibonacci(int a)
    {
        if (a <= 1) return a;
        return Fibonacci(a - 1) + Fibonacci(a - 2);
    }

    public static string Reverse(string s)
    {
        if (s.Length <= 1) return s;
        return Reverse(s.Substring(1)) + s[0];
    }

    public static int Sum(Stack<int> numbers)
    {
        if (numbers.Count == 1) return numbers.Pop();
        return numbers.Pop() + Sum(numbers);
    }


    public static int NOD(int a, int b)
    {
        return b != 0 ? NOD(b, a % b) : a;
    }

    public static int Prime(int a) => a % 2 != 0 ? 1 : 0;


    public static bool Palindrom(string s)
    {
        s = s.ToLower();
        return s[(s.Length / 2 + Prime(s.Length))..] == new string(s[..(s.Length / 2)].Reverse().ToArray());
    }

    public static void Exchange(int[] array1, int[] array2, int index)
    {
        (array1[index], array2[index]) = (array2[index], array1[index]);
    }

    public static int[] HanoiTowers(int[] tower, int[] subTower, int[] result, int index = 0)
    {
        if (index == tower.Length) return result;
        if (tower.Length > 0) Exchange(tower, subTower, index);
        if (subTower.Length > 0) Exchange(subTower, result, index);
        index++;
        return HanoiTowers(tower, subTower, result, index);
    }


    public static void GenerateSubsets(List<int> set, List<int> subset, int index = 0)
    {
        if (index == set.Count)
        {
            Console.Write("{ ");
            foreach (var item in subset)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("}");
            return;
        }

        GenerateSubsets(set, subset, index + 1);

        subset.Add(set[index]);
        GenerateSubsets(set, subset, index + 1);

        subset.RemoveAt(subset.Count - 1);
    }

    public static void Combinations<T>(IList<T> array, string current = "")
    {
        if (array.Count == 0)
        {
            Console.WriteLine(current);
            return;
        }

        for (int i = 0; i < array.Count; i++)
        {
            List<T> list = new List<T>(array);
            list.RemoveAt(i);
            Combinations(list, current + array[i].ToString());
        }
    }

    public static T DeepCopy<T>(T obj)
    {
        if (obj == null)
            return default(T);

        var result = Activator.CreateInstance<T>();

        foreach (var property in typeof(T).GetProperties())
        {
            var propertyValue = property.GetValue(obj);

            if (propertyValue == null)
                continue;

            if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
            {
                property.SetValue(result, propertyValue);
            }
            else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var list = (IList)property.GetValue(obj);
                var newList = new List<object>();

                foreach (var item in list)
                {
                    newList.Add(DeepCopy(item));
                }

                property.SetValue(result, newList);
            }
            else if (property.PropertyType.IsClass)
            {
                var nestedObj = DeepCopy((object)property.GetValue(obj));
                property.SetValue(result, nestedObj);
            }
        }

        return (T)result;
    }

    public static void ExampleDeepCopy()
    {
        Tree tree = new()
        {
            somthingInt = 1,
            somthingString = "Hello, World!",
            somthingDouble = 2.1d,
            somthingBool = true
        };

        Tree CopyTree = DeepCopy<Tree>(tree);
        Console.WriteLine(CopyTree.ToString());
    }

    public static void ActivateHTowers(int quanity = 6)
    {
        int[] array = new int[quanity];
        int[] support = new int[quanity];
        int[] result = new int[quanity];

        for (int i = 0; i < array.Length; i++) array[i] = i + 1;


        HanoiTowers(array, support, result);

        Console.WriteLine("array: " + string.Join(' ', array));
        Console.WriteLine("support: " + string.Join(' ', support));
        Console.WriteLine("result: " + string.Join(' ', result));
    }

    static void Main(string[] args)
    {
        // Ну тут уже всё понятно по названиям функциям.

        //Console.WriteLine(Factorial(5));
        //Console.WriteLine(Fibonacci(10));
        //Console.WriteLine(Reverse("12345"));
        //Console.WriteLine(Sum(new([1, 2])));
        //Console.WriteLine(NOD(32, 6));
        //Console.WriteLine($"{Palindrom("Доход")} {Palindrom("огго")} {Palindrom("Работа")}");

        //ActivateHTowers(5);

        //GenerateSubsets([1,2,3,4], []);

        //ExampleDeepCopy();

        //Combinations([1, 2, '1']);


        Console.ReadLine(); // простая остановочка :)

    }
}
