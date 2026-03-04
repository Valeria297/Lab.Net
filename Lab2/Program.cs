using System;
using System.Collections.Generic; 

namespace Lab2
{
    // Базовый класс (демонстрация наследования)
    class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine("Животное издает звук");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}, Возраст: {Age}");
        }
    }

    // Производный класс (наследование от Animal)
    class Dog : Animal
    {
        public string Breed { get; set; }

        public Dog(string name, int age, string breed) : base(name, age)
        {
            Breed = breed;
        }

        // Переопределение метода (полиморфизм)
        public override void MakeSound()
        {
            Console.WriteLine("Гав-гав!");
        }

        public void Fetch()
        {
            Console.WriteLine($"{Name} приносит палку");
        }
    }

    // Еще один производный класс
    class Cat : Animal
    {
        public bool IsLazy { get; set; }

        public Cat(string name, int age, bool isLazy) : base(name, age)
        {
            IsLazy = isLazy;
        }

        public override void MakeSound()
        {
            Console.WriteLine("Мяу-мяу!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа №2 ===\n");

            // 1. ДЕМОНСТРАЦИЯ НАСЛЕДОВАНИЯ
            Console.WriteLine("--- Наследование ---");
            
            Animal genericAnimal = new Animal(name: "Животное", age: 5);
            Dog dog = new Dog(name: "Бобик", age: 3, breed: "Такса");
            Cat cat = new Cat(name: "Мурка", age: 2, isLazy: true);

            // Полиморфизм
            Animal[] animals = { genericAnimal, dog, cat };

            foreach (var animal in animals)
            {
                animal.DisplayInfo();
                animal.MakeSound(); // Вызывается правильная версия метода
                Console.WriteLine();
            }

            // 2. ДЕМОНСТРАЦИЯ КОЛЛЕКЦИИ
            Console.WriteLine("--- Коллекция List<T> ---");
        
            List<Animal> animalList = new List<Animal>();
        
            animalList.Add(new Dog(name: "Рекс", age: 4, breed: "Овчарка"));
            animalList.Add(new Cat(name: "Барсик", age: 1, isLazy: false));
            animalList.Add(new Dog(name: "Шарик", age: 5, breed: "Дворняга"));
           
            foreach (var animal in animalList)
            {
                Console.Write($"{animal.Name}: ");
                animal.MakeSound();
            }
            
            Console.WriteLine($"\nВсего животных в коллекции: {animalList.Count}");
            
            Console.WriteLine($"Первое животное: {animalList[0].Name}");

            // 3. ДЕМОНСТРАЦИЯ ОБРАБОТКИ ИСКЛЮЧЕНИЙ
            Console.WriteLine("\n--- Обработка исключений ---");

            // Пример 1: Деление на ноль
            try
            {
                Console.Write("Введите число для деления 10: ");
                string input = Console.ReadLine();
                int divisor = int.Parse(input);
                
                int result = 10 / divisor;
                Console.WriteLine($"Результат: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введено не число!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally выполняется всегда");
            }

            // Пример 2: Выход за границы массива
            try
            {
                int[] numbers = { 1, 2, 3 };
                Console.WriteLine("Массив: [1, 2, 3]");
                Console.Write("Введите индекс (0-2): ");
                int index = int.Parse(Console.ReadLine());
                
                Console.WriteLine($"Элемент [{index}] = {numbers[index]}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка: индекс вне границ массива!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите целое число!");
            }

            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}