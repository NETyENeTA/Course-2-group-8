using System;
using System.Collections.Generic;


namespace Testy
{
    class Program
    {
        public static int[] PracticeB(int[] numbers)
        {
            Array.Sort(numbers);
            return numbers[1..];

            // Если подумать над этой задачи, то она изи-пизи, ток не понял смысл задачи.
            // Нам нужно вывестм подмассив чисел с max-sum, то нужно юзнуть цикл,
            // НО!!! что такое подмассив - это некоторая часть массива (даже если эта часть это 1 элемент массива), то есть:
            // int[] array = [1, 2, 3, 4, 5]; => это массив, а иначе: int[] array1 = [2, 3, 4, 5] => это подмассив массива array.
            // НО... подмассив != массив, то есть нету подмассива, который равен массиву: [1, 2, 3, 4, 5] == [1, 2, 3, 4, 5] это уже 2 одинаковых МАССИВА.
            // + в том что если посмотреть на отсортированный массив,
            // пусть, например, это будет [1, 2, 3].
            // подмассивы [1, 2], [2, 3], [1, 3] и их вариации (но сумма не меняется).
            // видно что 1-ый элемент самый малый (так как он уже отсортирован), следовательно суммв остальных эл-ов это и будет max-sum без 1-ого элемента.
            // так как его невозможно включить в подмассив, иначе это будет массив.
            // ч. т. д.

            // P.S: Если я ошибся, то пожалуйста сообщите. я может на уроке у Вас спрошу об этой задаче и об алгоритме.
        }

        public static List<int[]> PracticeC(List<int> list)
        {
            List<int[]> result = [];
            List<int> temp = [];

            double count = Math.Pow(2, list.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                for (int j = 0; j < str.Length; j++) if (str[j] == '1') temp.Add(list[j]);
                result.Add(temp.ToArray());
                temp.Clear();
            }

            return result;
        }



        static void Main(string[] args)
        {
            // Ну тут уже всё понятно по названиям функциям


            //Console.WriteLine(string.Join(' ', PracticeB([4, 2, 1, 3])));

            //foreach (int[] numbers in PracticeC([1, 2, 3, 4])) Console.WriteLine(string.Join(' ', numbers));

            Console.ReadLine(); // простая остановочка :)

        }
    }
}