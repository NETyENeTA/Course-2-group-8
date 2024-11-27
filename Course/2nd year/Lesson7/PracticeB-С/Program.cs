using System.Collections;

namespace Testy;

public enum Directions
{
    Left,
    Right,
    Top,
    Bottom,
}

public class Vector2
{
    public int x;
    public int y;

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public (int, char) ToMax() => Math.Abs(x) > Math.Abs(y) ? (x, 'x') : (y, 'y');

    public static Vector2Additional operator -(Vector2 left, Vector2 right) => new(left.x - right.x, left.y - right.y);
}


public class Vector2Additional : Vector2
{


    public int steps = -1;

    public Vector2Additional(int x, int y, int? steps = null) : base(x, y)
    {
        if (steps.HasValue) this.steps = steps.Value;
    }

    public override string ToString() => $"{x} {y}";

    public static bool operator !=(Vector2Additional left, Vector2 right) => (left.x, left.y) != (right.x, right.y);
    public static bool operator ==(Vector2Additional left, Vector2 right) => (left.x, left.y) == (right.x, right.y);
    public static Vector2Additional operator +(Vector2Additional left, Vector2 right) => new(left.x + right.x, left.y + right.y, left.steps);
    public static Vector2Additional operator -(Vector2Additional left, Vector2 right) => new(left.x - right.x, left.y - right.y, left.steps);
}

class Program
{
    public static int EmptyPlaces;


    public static string Searching(char[,] matrix, Vector2Additional position, Vector2 theEnd, Directions direction, Directions current)
    {
        if (position.steps > EmptyPlaces) return "Похоже, я заблудился :(";
        matrix[position.y, position.x] = '@';
        position.steps++;


        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine(position.ToString());
        Console.WriteLine(position.steps);
        Console.WriteLine();

        if (position == theEnd) return "Выход, всегда есть!";
        switch (direction)
        {
            case Directions.Bottom:
                if (matrix[position.y + 1, position.x] == ' ' || matrix[position.y + 1, position.x] == 'x') return Searching(matrix, position + new Vector2(0, 1), theEnd, direction, current);
                if (current == Directions.Right)
                {
                    if (matrix[position.y, position.x + 1] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Left);
                    return Searching(matrix, position + new Vector2(1, 0), theEnd, direction, current);
                }
                else if (current == Directions.Left)
                {
                    if (matrix[position.y, position.x - 1] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Right);
                    return Searching(matrix, position + new Vector2(-1, 0), theEnd, direction, current);
                }
                break;
            case Directions.Top:
                if (matrix[position.y - 1, position.x] == ' ' || matrix[position.y - 1, position.x] == 'x') return Searching(matrix, position + new Vector2(0, -1), theEnd, direction, current);
                if (current == Directions.Right)
                {
                    if (matrix[position.y, position.x + 1] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Left);
                    return Searching(matrix, position + new Vector2(1, 0), theEnd, direction, current);
                }
                else if (current == Directions.Left)
                {
                    if (matrix[position.y, position.x - 1] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Right);
                    return Searching(matrix, position + new Vector2(-1, 0), theEnd, direction, current);
                }
                break;
            case Directions.Left:
                if (matrix[position.y, position.x - 1] == ' ' || matrix[position.y, position.x - 1] == 'x') return Searching(matrix, position + new Vector2(-1, 0), theEnd, direction, current);
                if (current == Directions.Bottom)
                {
                    if (matrix[position.y + 1, position.x] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Top);
                    return Searching(matrix, position + new Vector2(0, 1), theEnd, direction, current);
                }
                else if (current == Directions.Top)
                {
                    if (matrix[position.y - 1, position.x] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Bottom);
                    return Searching(matrix, position + new Vector2(0, -1), theEnd, direction, current);
                }
                break;

            case Directions.Right:
                if (matrix[position.y, position.x + 1] == ' ' || matrix[position.y, position.x + 1] == 'x') return Searching(matrix, position + new Vector2(1, 0), theEnd, direction, current);
                if (current == Directions.Bottom)
                {
                    if (matrix[position.y + 1, position.x] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Top);
                    return Searching(matrix, position + new Vector2(0, 1), theEnd, direction, current);
                }
                else if (current == Directions.Top)
                {
                    if (matrix[position.y - 1, position.x] == '■') return Searching(matrix, position + new Vector2(0, 0), theEnd, direction, Directions.Bottom);
                    return Searching(matrix, position + new Vector2(0, -1), theEnd, direction, current);
                }
                break;

        }
        return "или мне кажеться, но выхода нету";
    }

    public static void Labyrinth()
    {
        char[,] Labyrinth =
        {
                {'■', '■', '■', '■', '■'},
                {'■', ' ', ' ', ' ', '■'},
                {'■', ' ', '■', ' ', '■'},
                {'■', '@', '■', ' ', 'x'},
                {'■', ' ', '■', ' ', '■'},
                {'■', ' ', ' ', ' ', '■'},
                {'■', '■', '■', '■', '■'},
            };

        Vector2Additional StartPos = new(1, 1);
        Vector2 EndPos = new(1, 1);

        for (int i = 0; i < Labyrinth.GetLength(0); i++)
            for (int j = 0; j < Labyrinth.GetLength(1); j++)
            {
                if (Labyrinth[i, j] == ' ') EmptyPlaces++;
                else if (Labyrinth[i, j] == '@') StartPos = new(j, i);
                else if (Labyrinth[i, j] == 'x') EndPos = new(j, i);
            }

        Vector2 offset = EndPos - StartPos;

        Console.WriteLine(EndPos - StartPos);
        int value;
        char directionXY;

        Directions direction;
        Directions helpDirection;

        (value, directionXY) = offset.ToMax();

        if (directionXY == 'x')
        {
            direction = value > 0 ? Directions.Right : Directions.Left;
            helpDirection = Directions.Top;
        }
        else
        {
            direction = value > 0 ? Directions.Bottom : Directions.Top;
            helpDirection = Directions.Left;
        }

        Console.WriteLine(Searching(Labyrinth, StartPos, EndPos, direction, helpDirection));
    }


    public static int Partition(int[] numbers, int left, int right)
    {
        int pivot = numbers[left];
        while (true)
        {
            while (numbers[left] < pivot)
                left++;

            while (numbers[right] > pivot)
                right--;

            if (left < right) (numbers[right], numbers[left]) = (numbers[left], numbers[right]);
            else return right;
        }
    }
    public static void QuickSort(int[] array, int left, int right)
    {
        if (left > right) return;

        int pivot = Partition(array, left, right);

        if (pivot > 1) QuickSort(array, left, pivot - 1);
        if (pivot + 1 < right) QuickSort(array, pivot + 1, right);

    }


    public static void QuickSort(int[] array)
    {

        int lenIndex = array.Length - 1;

        if (lenIndex < 0) return;

        int pivot = Partition(array, 0, lenIndex);

        if (pivot > 1) QuickSort(array, 0, pivot - 1);
        if (pivot + 1 < lenIndex) QuickSort(array, pivot + 1, lenIndex);
    }


    public static void Sorty()
    {
        int[] numbers = [7, 2, 1, 6, 4, 8, 9, 3, 5, 0];

        //QuickSort(numbers, 0, numbers.Length - 1);
        QuickSort(numbers);
        foreach (int number in numbers) Console.Write($"{number} ");
    }


    public static void PracticeB()
    {
        //Labyrinth();

        //Sorty();
    }

    public static int BinarySearch<T>(T[] array, int left, int right, T desiredValue) where T : IComparable<T>
    {

        // Проверка правильности границ
        if (left > right)
            return -1;

        // Сравнение значения с нижней границей
        switch (desiredValue.CompareTo(array[left]))
        {
            // Если меньше
            case -1: return -1;
            // Если равно
            case 0: return left;
            // Если больше - продолжить
            case 1: break;
            // Остальные значения - ошибка
            default: throw new Exception("Метод CompareTo вернул недопустимое значение");
        }


        // Сравнение значения с верхней границей
        switch (desiredValue.CompareTo(array[right]))
        {
            // Если меньше - продолжить
            case -1: break;
            // Если равно
            case 0: return right;
            // Если больше 
            case 1: return -1;
            // Остальные значения - ошибка
            default: throw new Exception("Метод CompareTo вернул недопустимое значение");
        }


        int middle = left + (right - left) / 2;

        // Сравнение значения с серидиной
        switch (desiredValue.CompareTo(array[middle]))
        {
            // Если меньше - продолжить
            case -1: return BinarySearch(array, left + 1, middle - 1, desiredValue);
            // Если равно
            case 0: return middle;
            // Если больше 
            case 1: return BinarySearch(array, left + 1, middle - 1, desiredValue);
            // Остальные значения - ошибка
            default: throw new Exception("Метод CompareTo вернул недопустимое значение");
        }
    }


    private static void BinaryTree()
    {
        Tree<DataClass> Data = new Tree<DataClass>(new DataClass(50));
        Data.Insert(new(45));
        Data.Insert(new(59));
        Data.Insert(new(15));
        Data.Insert(new(83));
        Data.Insert(new(33));
        Data.Insert(new(53));
        Data.Insert(new(22));
        Data.Insert(new(1));
        Console.WriteLine(Data.Search(new(53)).GetValue.GetValue.ToString());
    }

    private static void BinarySearch()
    {
        Console.WriteLine("Массив чисел от 1 до 21 ");

        int[] array = new int[21];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i + 1;
            Console.Write(" " + array[i]);
        }

        Console.WriteLine();
        Console.Write("Введите число от 1 до 21: ");
        int desiredValue = Convert.ToInt32(Console.ReadLine());

        int ErrorValue1 = 23;

        Console.WriteLine(Environment.NewLine + "Индекс искомого значения {0} в массиве: {1} ", desiredValue, BinarySearch(array, 0, array.Length - 1, desiredValue));
        Console.WriteLine("Индекс искомого значения {0} в массиве: {1} \n", ErrorValue1, BinarySearch(array, 0, array.Length - 1, ErrorValue1));
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

    public static void PracticeC()
    {
        //BinaryTree();
        //BinarySearch();
        //ExampleDeepCopy();
    }

    static void Main(string[] args)
    {
        // Ну тут уже всё понятно по названиям функциям.

        //PracticeB();

        PracticeC();

        Console.ReadLine(); // простая остановочка :)
    }
}

public class Tree
{
    public int somthingInt { get; set; }
    public string? somthingString { get; set; }
    public double somthingDouble { get; set; }
    public bool somthingBool { get; set; }

    public override string ToString() => $"Tree:\nint:{somthingInt}\nstring:{somthingString}\ndouble:{somthingDouble}\nbool:{somthingBool}";
}

class Tree<T> where T : IComparable
{
    private T Value;
    private Tree<T> Up;
    private Tree<T> Left;
    private Tree<T> Right;
    public T GetValue
    {
        get
        {
            return this.Value;
        }
    }
    public void Insert(T Value)
    {
        if (this.Value == null) this.Value = Value;
        else
        {
            if (this.Value.CompareTo(Value) < 0)
            {
                if (this.Right == null) this.Right = new Tree<T>(Value);
                else this.Right.Insert(Value);
            }
            else if (this.Value.CompareTo(Value) > 0)
            {
                if (this.Left == null) this.Left = new Tree<T>(Value);
                else this.Left.Insert(Value);
            }
            else if (this.Value.CompareTo(Value) == 0) Console.WriteLine("Невозможно добавить одинаковый элемент");
        }
    }
    public Tree<T>? Search(T Value)
    {
        if (this != null)
        {
            if (this.Value.CompareTo(Value) == 0) return this;
            else if (this.Value.CompareTo(Value) > 0) return this.Left.Search(Value);
            else if (this.Value.CompareTo(Value) < 0) return this.Right.Search(Value);
        }
        return null;
    }
    public Tree()
    {
        this.Up = null;
        this.Left = null;
        this.Right = null;
    }
    public Tree(T Value)
    {
        this.Up = null;
        this.Left = null;
        this.Right = null;
        this.Value = Value;
    }
}
class DataClass : IComparable
{
    private Int32 Value = new Int32();
    public Int32 GetValue
    {
        get
        {
            return this.Value;
        }
    }
    public Int32 CompareTo(object Data)
    {
        if (this.Value == (Data as DataClass).Value) return 0;
        else if (this.Value > (Data as DataClass).Value) return 1;
        else return -1;
    }
    public DataClass()
    {
        this.Value = new Int32();
    }
    public DataClass(Int32 Value)
    {
        this.Value = Value;
    }
}