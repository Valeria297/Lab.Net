using System;
using System.IO; 
using System.Collections.Generic; 

namespace Lab5
{
    // Класс для хранения параметра оборудования
    class EquipmentParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public EquipmentParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Лабораторная работа №5 ===\n");
            Console.WriteLine("Тема: Доступ к данным из файла и вывод в командную строку\n");

            // Путь к файлу с данными
            string filePath = "equipment_data.txt";

            // 1. ПРОВЕРКА СУЩЕСТВОВАНИЯ ФАЙЛА
            Console.WriteLine("--- Проверка наличия файла с данными ---");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл {filePath} не найден!");
                Console.WriteLine("Создаю пример файла с тестовыми данными...");
                CreateSampleFile(filePath);
            }
            else
            {
                Console.WriteLine($"Файл {filePath} найден.");
            }
            Console.WriteLine();

            // 2. ЧТЕНИЕ ДАННЫХ ИЗ ФАЙЛА
            Console.WriteLine("--- Чтение данных из файла ---");
            List<EquipmentParameter> parameters = ReadParametersFromFile(filePath);

            // 3. ВЫВОД ДАННЫХ В КОМАНДНУЮ СТРОКУ
            Console.WriteLine("\n--- Параметры оборудования ---");
            
            if (parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    Console.WriteLine(param);
                }
            }
            else
            {
                Console.WriteLine("Нет данных для отображения.");
            }
            
            Console.WriteLine($"Всего параметров: {parameters.Count}");

            // 4. ДОПОЛНИТЕЛЬНО: ПОИСК ПАРАМЕТРА ПО ИМЕНИ
            Console.WriteLine("\n--- Поиск параметра ---");
            Console.Write("Введите имя параметра для поиска: ");
            string searchName = Console.ReadLine();

            SearchParameter(parameters, searchName);

            // 5. ДОПОЛНИТЕЛЬНО: СОХРАНЕНИЕ ОТЧЕТА В НОВЫЙ ФАЙЛ
            Console.WriteLine("\n--- Сохранение отчета ---");
            string reportFile = "equipment_report.txt";
            SaveReportToFile(parameters, reportFile);
            Console.WriteLine($"Отчет сохранен в файл: {reportFile}");

            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода...");
            Console.ReadLine();
        }

        // Метод для создания примера файла с данными
        static void CreateSampleFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("# Параметры оборудования (пример)");
                    writer.WriteLine("# Имя параметра = Значение");
                    writer.WriteLine();
                    writer.WriteLine("Temperature = 75.5");
                    writer.WriteLine("Pressure = 1.2");
                    writer.WriteLine("Voltage = 220.0");
                    writer.WriteLine("Current = 5.3");
                    writer.WriteLine("Power = 1166.0");
                    writer.WriteLine("Frequency = 50.0");
                    writer.WriteLine("Humidity = 45.2");
                    writer.WriteLine("Speed = 1500");
                    writer.WriteLine("Status = Running");
                    writer.WriteLine("Mode = Automatic");
                }
                Console.WriteLine($"Файл {filePath} успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
            }
        }

        // Метод для чтения параметров из файла
        static List<EquipmentParameter> ReadParametersFromFile(string filePath)
        {
            List<EquipmentParameter> parameters = new List<EquipmentParameter>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        line = line.Trim();

                        // Пропускаем пустые строки и комментарии
                        if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                        {
                            continue;
                        }

                        // Парсим строку формата "Имя = Значение"
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string name = parts[0].Trim();
                            string value = parts[1].Trim();
                            parameters.Add(new EquipmentParameter(name, value));
                            Console.WriteLine($"Строка {lineNumber}: Прочитан параметр {name} = {value}");
                        }
                        else
                        {
                            Console.WriteLine($"Строка {lineNumber}: Неверный формат, пропущено");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Ошибка: Файл {filePath} не найден.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            }

            return parameters;
        }

        // Метод для поиска параметра по имени
        static void SearchParameter(List<EquipmentParameter> parameters, string name)
        {
            bool found = false;
            foreach (var param in parameters)
            {
                if (param.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Найден: {param}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Параметр с именем '{name}' не найден.");
            }
        }

        // Метод для сохранения отчета в файл
        static void SaveReportToFile(List<EquipmentParameter> parameters, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("ОТЧЕТ О ПАРАМЕТРАХ ОБОРУДОВАНИЯ");
                    writer.WriteLine($"Дата: {DateTime.Now}");
                    writer.WriteLine();

                    foreach (var param in parameters)
                    {
                        writer.WriteLine(param);
                    }

                    writer.WriteLine();
                    writer.WriteLine($"Всего параметров: {parameters.Count}");
                }
                Console.WriteLine($"Отчет успешно сохранен в {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении отчета: {ex.Message}");
            }
        }
    }
}