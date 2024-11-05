using System;
using System.Threading.Channels;
using System.Transactions;


namespace HomeWork;


public enum Positions {
    noob,
    pro,
    master,
    gigachad,
    sigma,
    basiced
}

class Program
{

    public static void PracticeA()
    {
        Person person1 = new("Niko", 33);
        Person person2 = new("Roman", 25);
        Person person3 = new("Jacob", 30);

        Person[] people = { person1, person2, person3 };

        foreach (Person person in people) person.Introduce();
    }

    public static void PracticeB()
    {
        Person person1 = new("Anna", -15);
        Person person2 = new("Polina", -16);
        Person person3 = new("Anya", 12);


        Person[] people = { person1, person2, person3 };

        foreach (Person person in people) person.IntroduceMyAge();
    }

    static void PracticeC() 
    {
        Employee employee1 = new("Bob", 22, Positions.noob);
        Employee employee2 = new("Jon", 22, Positions.pro);
        Employee employee3 = new("Max", 32, Positions.master);
        Employee employee4 = new("George", 42, Positions.gigachad);
        Employee employee5 = new("Steve", 52, Positions.sigma);
        Employee employee6 = new("Basid", 62, Positions.basiced);

        Employee[] Employees = { employee1, employee2, employee3, employee4, employee5, employee6 };

        foreach (Employee person in Employees) person.IntroduceMyPosition();
    }

    static void Main(string[] args)
    {
        PracticeA();
        PracticeB();
        PracticeC();
    }
}



class Person
{
    public string Name { get; private set; }
    public int Age { get; private set; } = 0;

    public Person(string name, int age)
    {
        Name = name;
        Age = age > 0 ? age : Age;
    }

    public void Introduce() => Console.WriteLine($"Привет, мое имя {Name}!");

    public void IntroduceMyAge() => Console.WriteLine($"Мне, {Age} лет!");


}


class Employee : Person
{
    public Positions position;

    public Employee(string name, int age, Positions position) : base(name, age)
    {
        this.position = position;
    }

    public void IntroduceMyPosition() => Console.WriteLine($"Я являюсь: {position}");

}