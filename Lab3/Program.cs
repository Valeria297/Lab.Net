using System;

namespace Lab3
{
    //Перечисления
    enum DaysOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    class ArrayProcessor
    {
         // Свойство класса
        public string ProcessorName { get; set; }
        private int _processedCount;

        // Свойство с проверкой значения
        public int ProcessedCount
        {
            get { return _processedCount; }
            private set
            {
                if (value >= 0)
                    _processedCount = value;
                else
                    throw new ArgumentException("Count cannot be negative");
            }
        }

        public ArrayProcessor(string name)
        {
            ProcessorName = name;
            ProcessedCount = 0;
        }

        // Объявление события
        public event EventHandler<string> ArrayProcessed;

        // Метод для вызова события
        protected virtual void OnArrayProcessed(string message)
        {
            ArrayProcessed?.Invoke(this, message);
        }

        // Метод класса: работа с одномерным массивом
        public void ProcessOneDimensionalArray(int[] array)
        {
            Console.WriteLine($"\n--- Обработка одномерного массива (метод класса) ---");
            Console.WriteLine($"Процессор: {ProcessorName}");
            
            Console.Write("Исходный массив: ");
            PrintArray(array);
            
            Array.Sort(array);
            
            Console.Write("Отсортированный массив: ");
            PrintArray(array);
            
            ProcessedCount++;
            OnArrayProcessed($"Одномерный массив обработан. Количество элементов: {array.Length}");
        }

        // Метод класса: работа с прямоугольным (двумерным) массивом
        public void ProcessRectangularArray(int[,] array)
        {
            Console.WriteLine($"\n--- Обработка прямоугольного массива {array.GetLength(0)}x{array.GetLength(1)} ---");
            
            Console.WriteLine("Исходный массив:");
            PrintRectangularArray(array);
            
            int sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }
            
            Console.WriteLine($"Сумма всех элементов: {sum}");
            
            ProcessedCount++;
            OnArrayProcessed($"Прямоугольный массив обработан. Размер: {array.GetLength(0)}x{array.GetLength(1)}");
        }

        // Метод класса: работа с зубчатым (ступенчатым) массивом
        public void ProcessJaggedArray(int[][] jaggedArray)
        {
            Console.WriteLine($"\n--- Обработка зубчатого массива ---");
            
            Console.WriteLine("Исходный зубчатый массив:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write($"Строка {i}: ");
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write($"{jaggedArray[i][j]} ");
                }
                Console.WriteLine();
            }
            
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    rowSum += jaggedArray[i][j];
                }
                Console.WriteLine($"Сумма в строке {i}: {rowSum}");
            }
            
            ProcessedCount++;
            OnArrayProcessed($"Зубчатый массив обработан. Количество строк: {jaggedArray.Length}");
        }

        // Вспомогательные методы для вывода
        private void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        private void PrintRectangularArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }

    // 3. КЛАСС-ОБРАБОТЧИК СОБЫТИЙ
    class EventListener
    {
        public void OnArrayProcessedHandler(object sender, string message)
        {
            Console.WriteLine($"[СОБЫТИЕ ПОЛУЧЕНО] {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа №3 ===\n");

            // 1. ДЕМОНСТРАЦИЯ ПЕРЕЧИСЛЕНИЯ (enum)
            Console.WriteLine("--- Перечисление DaysOfWeek ---");
            DaysOfWeek today = DaysOfWeek.Wednesday;
            Console.WriteLine($"Сегодня: {today} (значение: {(int)today})");
            
            // Перебор всех значений перечисления
            Console.WriteLine("Все дни недели:");
            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                Console.WriteLine($"  {day} = {(int)day}");
            }
            Console.WriteLine();

            // 2. ИНИЦИАЛИЗАЦИЯ РАЗЛИЧНЫХ ТИПОВ МАССИВОВ
            Console.WriteLine("--- Инициализация массивов ---");

            // Одномерный массив
            int[] oneDArray = new int[] { 5, 2, 8, 1, 9, 3 };
            Console.WriteLine("Одномерный массив инициализирован");

            // Прямоугольный (двумерный) массив
            int[,] rectangularArray = new int[3, 4] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };
            Console.WriteLine("Прямоугольный массив 3x4 инициализирован");

            // Зубчатый (ступенчатый) массив
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2, 3 };
            jaggedArray[1] = new int[] { 4, 5 };
            jaggedArray[2] = new int[] { 6, 7, 8, 9 };
            Console.WriteLine("Зубчатый массив инициализирован\n");

            // 3. РАБОТА С КЛАССОМ, МЕТОДАМИ, СВОЙСТВАМИ И СОБЫТИЯМИ
            Console.WriteLine("--- Демонстрация класса, методов, свойств и событий ---");

            // Создание экземпляра класса (использование конструктора)
            ArrayProcessor processor = new ArrayProcessor("Lab3 Processor");
            
            // Использование свойства (set/get)
            Console.WriteLine($"Имя процессора: {processor.ProcessorName}");
            
            // Подписка на событие
            EventListener listener = new EventListener();
            processor.ArrayProcessed += listener.OnArrayProcessedHandler;

            // Вызов методов класса для обработки массивов
            processor.ProcessOneDimensionalArray(oneDArray);
            processor.ProcessRectangularArray(rectangularArray);
            processor.ProcessJaggedArray(jaggedArray);

            // Использование свойства ProcessedCount (get)
            Console.WriteLine($"\n--- Итог ---");
            Console.WriteLine($"Всего обработано массивов: {processor.ProcessedCount}");

            // 4. ДОПОЛНИТЕЛЬНАЯ ДЕМОНСТРАЦИЯ СВОЙСТВ
            Console.WriteLine($"\n--- Демонстрация свойства с проверкой ---");
            try
            {
                // Попытка установить отрицательное значение (приватный set, поэтому напрямую не получится)
                // processor.ProcessedCount = -5; // Ошибка компиляции - set private
                
                // Но можно показать, что свойство защищено от некорректных значений в конструкторе
                Console.WriteLine("Свойство ProcessedCount защищено от отрицательных значений (private set)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}