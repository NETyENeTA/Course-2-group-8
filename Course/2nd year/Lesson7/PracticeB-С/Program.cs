

public enum Directions
{
    Left,
    Right,
    Top,
    Bottom,
}


namespace Testy
{

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

            if (direction == Directions.Bottom)
            {

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

        static void Main(string[] args)
        {
            // Ну тут уже всё понятно по названиям функциям.

            //Labyrinth();

            //Sorty();

            Console.ReadLine(); // простая остановочка :)
        }
    }
}