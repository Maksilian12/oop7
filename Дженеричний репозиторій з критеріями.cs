using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T>
{
    private List<T> data;

    public Repository()
    {
        data = new List<T>();
    }

    public void Add(T item)
    {
        data.Add(item);
    }

    public IEnumerable<T> Find(Criteria<T> criteria)
    {
        return data.Where(item => criteria(item));
    }
}

public delegate bool Criteria<T>(T item);

class Program
{
    static void Main()
    {
        Repository<int> intRepository = new Repository<int>();

        // Додавання елементів до репозиторію
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);
        intRepository.Add(4);
        intRepository.Add(5);

        // Приклад критерію для пошуку парних чисел
        Criteria<int> isEven = x => x % 2 == 0;

        // Знаходження елементів, що задовольняють критерій
        IEnumerable<int> evenNumbers = intRepository.Find(isEven);

        Console.WriteLine("Even numbers:");
        foreach (var number in evenNumbers)
        {
            Console.WriteLine(number);
        }
    }
}
