//using System.Text.RegularExpressions;

//1
//using System;

//class Program
//{
//    public static bool IsPerfect(long n)
//    {
//        if (n <= 1) return false;

//        long sum = 1; 

//        for (long i = 2; i * i <= n; i++)
//        {
//            if (n % i == 0)
//            {
//                sum += i;
//                if (i != n / i && n / i != n) 
//                {
//                    sum += n / i;
//                }
//            }
//        }

//        return sum == n;
//    }

//    public static void FindFirstPerfectNumbers(int count)
//    {
//        Console.WriteLine($"Первые {count} совершенных числа:");

//        int found = 0;
//        long num = 2; 

//        while (found < count)
//        {
//            if (IsPerfect(num))
//            {
//                Console.WriteLine($"{found + 1}-е: {num}");
//                found++;
//            }
//            num++;
//        }
//    }

//    static void Main(string[] args)
//    {
//        long test = 8128;
//        Console.WriteLine($"{test} — совершенное? {IsPerfect(test)}");

//        FindFirstPerfectNumbers(4);

//        Console.ReadKey();
//    }
//}

//2
//using System;

//class Program
//{
//    public static bool IsPermutation(string s1, string s2)
//    {
//        if (s1 == null || s2 == null)
//            return false;

//        if (s1.Length != s2.Length)
//            return false;

//        int[] charCount = new int[256];

//        foreach (char c in s1)
//        {
//            charCount[c]++;
//        }

//        foreach (char c in s2)
//        {
//            charCount[c]--;
//            if (charCount[c] < 0)
//            {
//                return false; 
//            }
//        }


//        return true;
//    }

//    static void Main(string[] args)
//    {
//        string[,] tests =
//        {
//            { "abc", "cab", "true" },
//            { "hello", "olelh", "true" },
//            { "test", "tes", "false" },
//            { "abc", "abd", "false" },
//            { "а роза упала на лапу Азора", "Азора лапу на упала роза а", "true" }, 
//            { "", "", "true" }
//        };

//        Console.WriteLine("Тестирование функции IsPermutation:\n");

//        for (int i = 0; i < tests.GetLength(0); i++)
//        {
//            string a = tests[i, 0];
//            string b = tests[i, 1];
//            bool expected = bool.Parse(tests[i, 2]);

//            bool result = IsPermutation(a, b);

//            Console.WriteLine($"\"{a}\" и \"{b}\" → {result} (ожидалось: {expected})");
//            Console.WriteLine(result == expected ? "   ✓ Успех" : "   ✗ Ошибка");
//            Console.WriteLine();
//        }

//        Console.ReadKey();
//    }
//}

//3
//using System;

//public class Matrix
//{
//    private double[,] data;
//    public int Rows { get; }
//    public int Columns { get; }

//    public Matrix(int rows, int columns)
//    {
//        if (rows <= 0 || columns <= 0)
//            throw new ArgumentException("Размеры матрицы должны быть положительными.");

//        Rows = rows;
//        Columns = columns;
//        data = new double[rows, columns];
//    }

//    public Matrix(double[,] array)
//    {
//        if (array == null)
//            throw new ArgumentNullException(nameof(array));

//        Rows = array.GetLength(0);
//        Columns = array.GetLength(1);

//        if (Rows == 0 || Columns == 0)
//            throw new ArgumentException("Матрица не может быть пустой.");

//        data = (double[,])array.Clone();
//    }

//    public double this[int row, int col]
//    {
//        get
//        {
//            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
//                throw new IndexOutOfRangeException("Индекс вне границ матрицы.");
//            return data[row, col];
//        }
//        set
//        {
//            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
//                throw new IndexOutOfRangeException("Индекс вне границ матрицы.");
//            data[row, col] = value;
//        }
//    }

//    public static Matrix operator +(Matrix a, Matrix b)
//    {
//        if (a.Rows != b.Rows || a.Columns != b.Columns)
//            throw new InvalidOperationException(
//                $"Нельзя сложить матрицы: размеры не совпадают ({a.Rows}x{a.Columns} и {b.Rows}x{b.Columns}).");

//        Matrix result = new Matrix(a.Rows, a.Columns);
//        for (int i = 0; i < a.Rows; i++)
//        {
//            for (int j = 0; j < a.Columns; j++)
//            {
//                result[i, j] = a[i, j] + b[i, j];
//            }
//        }
//        return result;
//    }

//    public static Matrix operator -(Matrix a, Matrix b)
//    {
//        if (a.Rows != b.Rows || a.Columns != b.Columns)
//            throw new InvalidOperationException(
//                $"Нельзя вычесть матрицы: размеры не совпадают ({a.Rows}x{a.Columns} и {b.Rows}x{b.Columns}).");

//        Matrix result = new Matrix(a.Rows, a.Columns);
//        for (int i = 0; i < a.Rows; i++)
//        {
//            for (int j = 0; j < a.Columns; j++)
//            {
//                result[i, j] = a[i, j] - b[i, j];
//            }
//        }
//        return result;
//    }

//    public static Matrix operator *(Matrix a, Matrix b)
//    {
//        if (a.Columns != b.Rows)
//            throw new InvalidOperationException(
//                $"Нельзя умножить матрицы: количество столбцов первой ({a.Columns}) не равно количеству строк второй ({b.Rows}).");

//        Matrix result = new Matrix(a.Rows, b.Columns);
//        for (int i = 0; i < a.Rows; i++)
//        {
//            for (int j = 0; j < b.Columns; j++)
//            {
//                double sum = 0;
//                for (int k = 0; k < a.Columns; k++)
//                {
//                    sum += a[i, k] * b[k, j];
//                }
//                result[i, j] = sum;
//            }
//        }
//        return result;
//    }

//    public override string ToString()
//    {
//        string result = "";
//        for (int i = 0; i < Rows; i++)
//        {
//            for (int j = 0; j < Columns; j++)
//            {
//                result += $"{data[i, j],8:F2} ";
//            }
//            result += "\n";
//        }
//        return result.Trim();
//    }
//}

//4
//using System;
//using System.Collections.Generic;

//public class LongestIncreasingSubsequence
//{
//    public static (int Length, List<int> Sequence) FindLIS(int[] arr)
//    {
//        if (arr == null || arr.Length == 0)
//            return (0, new List<int>());

//        int n = arr.Length;
//        int[] dp = new int[n];       
//        int[] prev = new int[n];     

//        Array.Fill(dp, 1);
//        Array.Fill(prev, -1);

//        int maxLength = 1;
//        int maxIndex = 0;

//        for (int i = 1; i < n; i++)
//        {
//            for (int j = 0; j < i; j++)
//            {
//                if (arr[j] < arr[i] && dp[j] + 1 > dp[i])
//                {
//                    dp[i] = dp[j] + 1;
//                    prev[i] = j;
//                }
//            }

//            if (dp[i] > maxLength)
//            {
//                maxLength = dp[i];
//                maxIndex = i;
//            }
//        }

//        List<int> sequence = new List<int>();
//        int current = maxIndex;
//        while (current != -1)
//        {
//            sequence.Add(arr[current]);
//            current = prev[current];
//        }
//        sequence.Reverse(); 

//        return (maxLength, sequence);
//    }

//    static void Main(string[] args)
//    {
//        Console.OutputEncoding = System.Text.Encoding.UTF8;

//        int[][] tests =
//        {
//            new int[] {10, 9, 2, 5, 3, 7, 101, 18},
//            new int[] {0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15},
//            new int[] {5, 4, 3, 2, 1},
//            new int[] {1, 2, 3, 4, 5},
//            new int[] {1},
//            new int[] {}
//        };

//        foreach (var test in tests)
//        {
//            var (length, seq) = FindLIS(test);

//            Console.WriteLine($"Массив: [{string.Join(", ", test)}]");
//            Console.WriteLine($"Длина LIS: {length}");
//            if (length > 0)
//            {
//                Console.WriteLine($"Одна из подпоследовательностей: [{string.Join(", ", seq)}]");
//            }
//            Console.WriteLine();
//        }

//        Console.ReadKey();
//    }
//}

//5
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.RegularExpressions;

//class LogAnalyzer
//{
//    // Функция для анализа логов
//    public static Dictionary<string, int> AnalyzeLogs(string filePath)
//    {
//        var errorCounts = new Dictionary<string, int>();

//        if (!File.Exists(filePath))
//        {
//            throw new FileNotFoundException($"Файл логов не найден: {filePath}");
//        }

//        var regex = new Regex(@"\[(ERROR|WARNING|INFO|FATAL|DEBUG)\]");

//        using (var reader = new StreamReader(filePath))
//        {
//            string line;
//            while ((line = reader.ReadLine()) != null)
//            {
//                var match = regex.Match(line);
//                if (match.Success)
//                {
//                    string errorType = match.Groups[1].Value;
//                    if (errorCounts.ContainsKey(errorType))
//                    {
//                        errorCounts[errorType]++;
//                    }
//                    else
//                    {
//                        errorCounts[errorType] = 1;
//                    }
//                }
//            }
//        }

//        return errorCounts;
//    }

//    public static void PrintStatistics(Dictionary<string, int> stats)
//    {
//        Console.WriteLine("Статистика по типам ошибок:");
//        foreach (var kvp in stats)
//        {
//            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
//        }

//        int total = 0;
//        foreach (var count in stats.Values) total += count;
//        Console.WriteLine($"Всего записей с типами: {total}");
//    }

//    static void Main(string[] args)
//    {
//        Console.OutputEncoding = System.Text.Encoding.UTF8;

//        string filePath = "logs.txt"; /

//        try
//        {
//            var stats = AnalyzeLogs(filePath);
//            PrintStatistics(stats);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        Console.ReadKey();
//    }
//}