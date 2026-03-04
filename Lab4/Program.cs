using System;

namespace Lab4
{
    // Класс для демонстрации перегрузки методов, операторов, конструкторов и деструктора
    class Vector
    {
        // Поля класса
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Счетчик созданных объектов (для демонстрации работы конструкторов)
        private static int objectCount = 0;

        // СТАТИЧЕСКИЙ КОНСТРУКТОР
        static Vector()
        {
            Console.WriteLine("Статический конструктор Vector вызван (вызывается 1 раз)");
        }

        // КОНСТРУКТОР ПО УМОЛЧАНИЮ (без параметров)
        public Vector()
        {
            X = 0;
            Y = 0;
            Z = 0;
            objectCount++;
            Console.WriteLine($"Конструктор по умолчанию. Создан вектор (0,0,0). Всего объектов: {objectCount}");
        }

        // КОНСТРУКТОР С ОДНИМ ПАРАМЕТРОМ
        public Vector(double value)
        {
            X = value;
            Y = value;
            Z = value;
            objectCount++;
            Console.WriteLine($"Конструктор с 1 параметром. Создан вектор ({value},{value},{value}). Всего объектов: {objectCount}");
        }

        // КОНСТРУКТОР С ТРЕМЯ ПАРАМЕТРАМИ
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            objectCount++;
            Console.WriteLine($"Конструктор с 3 параметрами. Создан вектор ({x},{y},{z}). Всего объектов: {objectCount}");
        }

        // КОНСТРУКТОР КОПИРОВАНИЯ
        public Vector(Vector other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
            objectCount++;
            Console.WriteLine($"Конструктор копирования. Создана копия вектора ({X},{Y},{Z}). Всего объектов: {objectCount}");
        }

        // ДЕСТРУКТОР
        ~Vector()
        {
            objectCount--;
            Console.WriteLine($"Деструктор вызван для вектора ({X},{Y},{Z}). Осталось объектов: {objectCount}");
        }

        // ПЕРЕГРУЗКА МЕТОДОВ

        // Метод для вычисления длины вектора (без параметров)
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        // Перегруженный метод: скалярное произведение с другим вектором
        public double Length(Vector other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        // Перегруженный метод: длина после умножения на скаляр
        public double Length(double scalar)
        {
            return Math.Sqrt((X * scalar) * (X * scalar) + 
                            (Y * scalar) * (Y * scalar) + 
                            (Z * scalar) * (Z * scalar));
        }

        // Перегруженный метод: вывод информации
        public void Print()
        {
            Console.WriteLine($"Вектор ({X}, {Y}, {Z})");
        }

        // Перегруженный метод: вывод с дополнительным сообщением
        public void Print(string message)
        {
            Console.WriteLine($"{message}: ({X}, {Y}, {Z})");
        }

        // ПЕРЕГРУЗКА ОПЕРАТОРОВ

        // Унарный оператор -
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        // Бинарный оператор + (сложение двух векторов)
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        // Бинарный оператор - (вычитание векторов)
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // Умножение вектора на скаляр
        public static Vector operator *(Vector v, double scalar)
        {
            return new Vector(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        // Умножение скаляра на вектор (для симметрии)
        public static Vector operator *(double scalar, Vector v)
        {
            return v * scalar; // Используем уже определенный оператор
        }

        // Оператор сравнения ==
        public static bool operator ==(Vector v1, Vector v2)
        {
            // Проверка на null
            if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null))
                return ReferenceEquals(v1, v2);
            
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        // Оператор сравнения != (всегда нужно перегружать вместе с ==)
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        // Переопределение Equals (рекомендуется при перегрузке ==)
        public override bool Equals(object obj)
        {
            if (obj is Vector other)
                return this == other;
            return false;
        }

        // Переопределение GetHashCode (рекомендуется при перегрузке Equals)
        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
        }

        // Перегрузка ToString
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    // Второй класс для демонстрации перегрузки методов с разными типами
    class Calculator
    {
        // Перегрузка метода Add для разных типов

        public int Add(int a, int b)
        {
            Console.Write("Метод Add(int, int): ");
            return a + b;
        }

        public double Add(double a, double b)
        {
            Console.Write("Метод Add(double, double): ");
            return a + b;
        }

        public int Add(int a, int b, int c)
        {
            Console.Write("Метод Add(int, int, int): ");
            return a + b + c;
        }

        public string Add(string a, string b)
        {
            Console.Write("Метод Add(string, string): ");
            return a + b;
        }

        // Перегрузка с разным количеством параметров
        public double Add(params double[] numbers)
        {
            Console.Write("Метод Add(params double[]): ");
            double sum = 0;
            foreach (var num in numbers)
                sum += num;
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа №4 ===\n");
            Console.WriteLine("Тема: Перегрузка методов и операторов, конструкторы и деструкторы\n");

            // 1. ДЕМОНСТРАЦИЯ КОНСТРУКТОРОВ
            Console.WriteLine("--- 1. Демонстрация конструкторов ---");
            
            Console.WriteLine("Создание объектов класса Vector:\n");
            
            // Конструктор по умолчанию
            Vector v1 = new Vector();
            
            // Конструктор с 1 параметром
            Vector v2 = new Vector(5);
            
            // Конструктор с 3 параметрами
            Vector v3 = new Vector(1, 2, 3);
            
            // Конструктор копирования
            Vector v4 = new Vector(v3);
            
            Console.WriteLine();

            // 2. ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ МЕТОДОВ
            Console.WriteLine("--- 2. Демонстрация перегрузки методов ---");
            
            // Класс Vector
            Console.WriteLine("Методы класса Vector:");
            v3.Print();
            v3.Print("Вектор v3");
            
            Console.WriteLine($"Длина v3 (без параметров): {v3.Length():F2}");
            Console.WriteLine($"Скалярное произведение v3 * v2: {v3.Length(v2)}");
            Console.WriteLine($"Длина v3 после умножения на 2: {v3.Length(2):F2}");
            
            Console.WriteLine();
            
            // Класс Calculator - автоматически считает присваемое значение и выводит строковый результат
            Console.WriteLine("Методы класса Calculator:");
            Calculator calc = new Calculator();
            
            Console.WriteLine(calc.Add(5, 10));
            Console.WriteLine(calc.Add(5.5, 10.3));
            Console.WriteLine(calc.Add(1, 2, 3));
            Console.WriteLine(calc.Add("Hello", " World"));
            Console.WriteLine(calc.Add(1.1, 2.2, 3.3, 4.4, 5.5));
            
            Console.WriteLine();

            // 3. ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ ОПЕРАТОРОВ
            Console.WriteLine("--- 3. Демонстрация перегрузки операторов ---");
            
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine($"v3 = {v3}");
            
            // Унарный минус
            Vector v2neg = -v2;
            Console.WriteLine($"-v2 = {v2neg}");
            
            // Сложение
            Vector sum = v2 + v3;
            Console.WriteLine($"v2 + v3 = {sum}");
            
            // Вычитание
            Vector diff = v3 - v2;
            Console.WriteLine($"v3 - v2 = {diff}");
            
            // Умножение на скаляр
            Vector scaled = v3 * 2;
            Console.WriteLine($"v3 * 2 = {scaled}");
            
            // Умножение скаляра на вектор (симметричная версия)
            Vector scaled2 = 3 * v3;
            Console.WriteLine($"3 * v3 = {scaled2}");
            
            // Операторы сравнения
            Vector v5 = new Vector(1, 2, 3);
            Console.WriteLine($"\nv3 = {v3}, v5 = {v5}");
            Console.WriteLine($"v3 == v5: {v3 == v5}");
            Console.WriteLine($"v3 != v5: {v3 != v5}");
            
            Vector v6 = new Vector(7, 8, 9);
            Console.WriteLine($"v3 == v6: {v3 == v6}");
            
            Console.WriteLine();

            // 4. ДЕМОНСТРАЦИЯ РАБОТЫ ДЕСТРУКТОРА
            Console.WriteLine("--- 4. Демонстрация работы деструктора ---");
            Console.WriteLine("(Деструктор вызывается автоматически при сборке мусора)");
            Console.WriteLine("Создаем временный объект в блоке:");
            
            // Создаем вложенный блок, чтобы объект вышел из области видимости
            {
                Vector temp = new Vector(99, 99, 99);
                Console.WriteLine($"temp = {temp}");
                Console.WriteLine("Выходим из блока - temp становится доступен для сборки мусора");
            }
            
            // Принудительно вызываем сборку мусора (в учебных целях)
            Console.WriteLine("\nПринудительный вызов сборщика мусора:");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Console.WriteLine("\nПрограмма завершена. Остальные деструкторы будут вызваны при завершении программы.");
            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}