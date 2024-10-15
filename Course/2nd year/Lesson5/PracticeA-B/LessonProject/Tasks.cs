using System;
using System.Linq;

namespace LessonProject
{
    public class Task1
    {
        static Task1()
        {
            Console.WriteLine("Задача 1: Найти в массиве целых чисел первый подмассив длиной N, сумма элементов которого максимальна. Вывести найденный подмассив.");
            Console.WriteLine("РЕШЕНИЕ:");

            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
            int Length = 3;
            Array.Sort(numbers);
            int[] answer = numbers[^Length..];

            Console.WriteLine(string.Join(' ', answer));
            

        }
    }

    public class Task2
    {
        static Task2()
        {
            Console.WriteLine("\n\n\nЗадача 2: Создать массив случайных чисел, сортировать его по возрастанию, затем найти количество уникальных чисел. Вывести отсортированный массив и количество уникальных чисел.");
            Console.WriteLine("РЕШЕНИЕ:");

            int[] array = new int[10];
            Random random = new Random();

            for (int i = 0; i < array.Length; i++) array[i] = random.Next(-100, 100);
            Array.Sort(array);

            HashSet<int> set = new HashSet<int>(array);

            Console.WriteLine("random massive:" + array.Length + " => [" + string.Join(' ', array) + ']');
            Console.WriteLine("set:" + set.Count + " => {" + string.Join(' ', set) + '}');
        }
    }

    public class Task3
    {
        static Task3()
        {
            Console.WriteLine("\n\n\nЗадача 3: Анализировать массив целых чисел и определить число, которое встречается чаще всего. Если таких чисел несколько, вывести их все и указать, сколько раз каждое из них встречается в массиве.");
            Console.WriteLine("РЕШЕНИЕ:");

            //int[] array = new int[10];
            //Random random = new Random();

            //for (int i = 0; i < array.Length; i++) array[i] = random.Next(-100, 100);


            int[] numbers = [1, 2, 3, 3, 1, 1];

            Dictionary<int, int> dict = [];
            List<int> result = [];


            foreach (int number in numbers)
            {
                dict.TryGetValue(number, out int total);
                dict[number] = total + 1;
            }

            Console.WriteLine("random massive:" + numbers.Length + " => [" + string.Join(' ', numbers) + ']');


            Console.WriteLine("Dictionary<int, int> = number: repeat-number {");
            foreach (var el in dict) Console.WriteLine("{0}:{1}", el.Key, el.Value);
            Console.WriteLine('}');

            Console.WriteLine();

            foreach (var el in dict)
            {
                if (el.Value == dict.Values.Max()) result.Add(el.Key);
            }
            Console.WriteLine("Answer:" + string.Join(" ", result));


        }
    }

    public class Task4
    {

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


        static Task4()
        {
            Console.WriteLine("\n\n\nЗадача 4: Выполнить циклическую ротацию массива на K позиций вправо. Вывести исходный и измененный массивы.");
            Console.WriteLine("РЕШЕНИЕ:");

            int[] array = [1, 2, 3, 4, 5, 6, 7];

            Console.WriteLine(string.Join(" ", array));
            array = CyclicRotation(array, 2);
            Console.WriteLine(string.Join(" ", array));
        }
    }

    public class Task5
    {
        static Task5()
        {
            Console.WriteLine("\n\n\nЗадача 5: Создать два отдельных массива целых чисел, объединить их в один и вывести числа, встречающиеся в обоих исходных массивах. В результирующем массиве каждое число должно встречаться только один раз.");
            Console.WriteLine("РЕШЕНИЕ:");

            int[] array1 = [1, 2, 3, 4];
            int[] array2 = [4, 5, 6, 7];

            HashSet<int> array = array1.Concat(array2).ToHashSet();
            Console.WriteLine(string.Join(" ", array) + "\nClones:");

            //foreach (int number in array1.Intersect(array2).ToArray()) Console.WriteLine(number); //можно вот так вывести встречающихся эл-ов

            // либо вот так
            foreach (int number in array)
            {
                if (array1.Contains(number) && array2.Contains(number))
                    Console.WriteLine(number);
            }

        }
    }
}
