namespace code;

using System;
using System.Collections.Generic;

// Абстрактный класс — базовый для всех животных
public abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Species { get; set; }

    public Animal(string name, int age, string species)
    {
        Name = name;
        Age = age;
        Species = species;
    }

    // Абстрактный метод — должен быть переопределён в наследниках
    public abstract void MakeSound();

    // Виртуальный метод — можно переопределить
    public virtual void ShowInfo()
    {
        Console.WriteLine($"Животное: {Name}, Вид: {Species}, Возраст: {Age}");
    }
}

// Интерфейс для животных, которых можно кормить особым образом
public interface IFeedable
{
    void Feed(string food);
}

// Конкретные классы животных
public class Lion : Animal
{
    public Lion(string name, int age) : base(name, age, "Лев")
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} рычит: РРРРР!");
    }
}

public class Elephant : Animal, IFeedable
{
    public Elephant(string name, int age) : base(name, age, "Слон")
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} трубит: Ту-ду-ду!");
    }

    public void Feed(string food)
    {
        Console.WriteLine($"{Name} ест {food}. Спасибо!");
    }
}

public class Parrot : Animal
{
    public string Color { get; set; }

    public Parrot(string name, int age, string color) : base(name, age, "Попугай")
    {
        Color = color;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} кричит: Каррр! Красиво, да?");
    }

    public override void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Цвет оперения: {Color}");
    }
}

// Класс зоопарка — управляет коллекцией животных
public class Zoo
{
    private List<Animal> animals = new List<Animal>();

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
        Console.WriteLine($"{animal.Name} добавлен в зоопарк.");
    }

    public void MakeAllSounds()
    {
        Console.WriteLine("\nВсе животные издают звуки:");
        foreach (var animal in animals)
        {
            animal.MakeSound();
        }
    }

    public void ShowAllAnimals()
    {
        Console.WriteLine("\nСписок животных в зоопарке:");
        foreach (var animal in animals)
        {
            animal.ShowInfo();
        }
    }

    // Кормим только тех, кто реализует IFeedable
    public void FeedAnimal(string name, string food)
    {
        var animal = animals.Find(a => a.Name == name);
        if (animal is IFeedable feedable)
        {
            feedable.Feed(food);
        }
        else
        {
            Console.WriteLine($"{name} не хочет это есть или не умеет так кормиться.");
        }
    }
}

// Точка входа — демонстрация работы
class Program
{
    static void Main(string[] args)
    {
        Zoo zoo = new Zoo();

        zoo.AddAnimal(new Lion("Симба", 5));
        zoo.AddAnimal(new Elephant("Дамбо", 12));
        zoo.AddAnimal(new Parrot("Кеша", 3, "зелёный"));

        zoo.ShowAllAnimals();
        zoo.MakeAllSounds();

        zoo.FeedAnimal("Дамбо", "сено и бананы");
        zoo.FeedAnimal("Симба", "мясо"); // Лев не реализует IFeedable → сообщение

        Console.ReadKey();
    }
}