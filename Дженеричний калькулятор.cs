using System;

public class Calculator<T>
{
    public delegate T BinaryOperation(T a, T b);

    public BinaryOperation Add { get; set; }
    public BinaryOperation Subtract { get; set; }
    public BinaryOperation Multiply { get; set; }
    public BinaryOperation Divide { get; set; }

    public Calculator()
    {
        // Ініціалізуємо делегати за замовчуванням
        Add = (a, b) => (dynamic)a + b;
        Subtract = (a, b) => (dynamic)a - b;
        Multiply = (a, b) => (dynamic)a * b;
        Divide = (a, b) => (dynamic)a / b;
    }

    public T PerformOperation(T a, T b, BinaryOperation operation)
    {
        return operation(a, b);
    }
}

class Program
{
    static void Main()
    {
        // Створюємо екземпляр класу Calculator для цілих чисел (int)
        Calculator<int> intCalculator = new Calculator<int>();

        int result1 = intCalculator.PerformOperation(10, 5, intCalculator.Add);       // Додавання
        int result2 = intCalculator.PerformOperation(10, 5, intCalculator.Subtract);  // Віднімання

        Console.WriteLine("Integer Calculator:");
        Console.WriteLine("Addition: " + result1);    // Виведе 15
        Console.WriteLine("Subtraction: " + result2); // Виведе 5

        // Створюємо екземпляр класу Calculator для чисел з рухомою комою (double)
        Calculator<double> doubleCalculator = new Calculator<double>();

        double result3 = doubleCalculator.PerformOperation(10.5, 3.2, doubleCalculator.Multiply); // Множення
        double result4 = doubleCalculator.PerformOperation(7.0, 2.0, doubleCalculator.Divide);    // Ділення

        Console.WriteLine("\nDouble Calculator:");
        Console.WriteLine("Multiplication: " + result3); // Виведе 33.6
        Console.WriteLine("Division: " + result4);       // Виведе 3.5
    }
}
