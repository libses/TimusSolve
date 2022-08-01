using System;
using System.Collections.Generic;
using System.Linq;

namespace TimusSolve
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
    }
    
    class Program
    {
        private List<Func<int, int, int>> Arithmetics = new List<Func<int, int, int>>()
        {
            (x, y) => x + y,
            (x, y) => x - y,
            (x, y) => x / y,
            (x, y) => x * y
        };

        private static List<Vector2> HorseMoves = new List<Vector2>()
        {
            new Vector2(1, 2),
            new Vector2(2, 1),
            new Vector2(-1, 2),
            new Vector2(1, -2),
            new Vector2(-1, -2),
            new Vector2(-2, 1),
            new Vector2(-2, -1),
            new Vector2(2, -1)
        };

        private static bool VectorIsInside(Vector2 vector, Vector2 lower, Vector2 higher)
        {
            var lDif = vector - lower;
            var rDif = higher - vector;
            return lDif.X >= 0 && lDif.Y >= 0 && rDif.X >= 0 && rDif.Y >= 0;
        }

        private static Vector2 ParseFromChessNotation(string note)
        {
            //97
            var x = note[0] - 97;
            var y = note[1] - 49;
            return new Vector2(x, y);
        }

        static void Main(string[] args)
        {
            A1910();
        }

        public static void A1910()
        {
            Console.ReadLine();
            var array = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            var max = int.MinValue;
            var maxIndex = -1;
            var queue = new Queue<int>(3);
            for (var i = 0; i < array.Length; i++)
            {
                queue.Enqueue(array[i]);
                if (queue.Count == 3)
                {
                    var sth = queue.Dequeue();
                    var sum = queue.Sum() + sth;
                    if (sum > max)
                    {
                        max = sum;
                        maxIndex = i;
                    }
                }
            }

            Console.WriteLine(max + " " + (maxIndex));
        }

        public static void A1639()
        {
            var array = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            var numba = array[0] * array[1];
            Console.WriteLine(numba % 2 == 0 ? "[:=[first]" : "[second]=:]");
        }

        public static void A1880()
        {
            var sets = new List<HashSet<int>>();
            for (var i = 0; i < 3; i++)
            {
                Console.ReadLine();
                var numbers = Console.ReadLine().Split().Select(x => int.Parse(x)).ToHashSet();
                sets.Add(numbers);
            }
            
            sets[0].IntersectWith(sets[1]);
            sets[0].IntersectWith(sets[2]);
            Console.WriteLine(sets[0].Count);
        }

        public static void A2100()
        {
            var n = int.Parse(Console.ReadLine());
            var currentNumber = 2;
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                if (input.Contains("+one"))
                {
                    currentNumber++;
                }

                currentNumber++;
            }
            
            Console.WriteLine(currentNumber == 13 ? 1400 : currentNumber * 100);
        }

        public static void A1197()
        {
            var n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                var lower = new Vector2(0, 0);
                var higher = new Vector2(7, 7);
                var result = 0;
                var node = ParseFromChessNotation(Console.ReadLine());
                foreach (var horseMove in HorseMoves)
                {
                    if (VectorIsInside(node + horseMove, lower, higher))
                    {
                        result++;
                    }
                }

                Console.WriteLine(result);
            }
        }

        public static int A2066()
        {
            var a = int.Parse(Console.ReadLine());
            var b = int.Parse(Console.ReadLine());
            var c = int.Parse(Console.ReadLine());

            return Math.Min(a - b - c, a - (b * c));
        }

        private static IEnumerable<List<int>> GetPermutations(List<int> list, int l, int r)
        {
            if (l == r)
            {
                yield return list;
            }

            for (var i = l; i <= r; i++)
            {
                Swap(list, l, i);
                foreach (var e in GetPermutations(list, l + 1, r))
                {
                    yield return e;
                }

                Swap(list, l, i);
            }
        }

        private static void Swap(List<int> list, int l, int r)
        {
            (list[l], list[r]) = (list[r], list[l]);
        }
    }
}