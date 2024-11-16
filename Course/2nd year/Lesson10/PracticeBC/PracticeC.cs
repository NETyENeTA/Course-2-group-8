using System;
using System.Collections.Generic;
using System.IO;


public class FileManager
{
    private readonly string filePath;

    public FileManager(string filePath) => this.filePath = filePath;

    public void WriteToFile(string text, bool append = false)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append)) writer.Write(text);
        }
        catch (Exception ex) { Console.WriteLine($"Write [Error]! cause:{ex}."); }
    }

    public string ReadFromFile()
    {
        try
        {
            if (File.Exists(filePath)) using (StreamReader reader = new StreamReader(filePath)) return reader.ReadToEnd();
        }
        catch (Exception ex) { Console.WriteLine($"Read [Error]! cause:{ex}."); }
        Console.WriteLine("File hasn't found!");
        return string.Empty;
    }

    public void RemoveFile() => File.Delete(filePath);

}


public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }


    public Person(string name, int age)
    {
        Name = name;
        SetAge(age);
    }


    public void Introduce()
    {
        Console.WriteLine("Hello, my name is " + Name);
    }


    public void SetAge(int newAge)
    {
        if (newAge >= 0)
        {
            Age = newAge;
        }
        else
        {
            Console.WriteLine("Age cannot be negative.");
        }
    }

    public override string ToString() => $"class:Person, Name:{Name}, Age:{Age};\n";

    public virtual string ToClass(char divide) => $"{divide}Person{divide}{Name}{divide}{Age}\n";


}


public class Employee : Person
{
    public string Position { get; set; }

    public Employee(string name, int age, string position) : base(name, age) => Position = position;

    public override string ToString() => $"class:Person>Employee, Name:{Name}, Age:{Age}, Position:{Position};\n";

    public override string ToClass(char divide) => $"{divide}Person{divide}{Name}{divide}{Age}{divide}{Position}\n";

}


public class PersonFileService
{

    FileManager peopleFile;
    FileManager DataFile = new("data.md");

    private char divide;


    public PersonFileService(string path, char divide='|')
    {
        peopleFile = new(path);
        this.divide = divide;
        peopleFile.RemoveFile();
        DataFile.RemoveFile();
    }


    public void WritePeopleToFile(List<Person> people)
    {
        foreach (var person in people) peopleFile.WriteToFile(person.ToString(), true);
    }

    public string[] ReadPeopleFromFile() => peopleFile.ReadFromFile().Split(divide);


    public void SavePeople(List<Person> people)
    {
        foreach (var person in people) DataFile.WriteToFile(person.ToClass(divide), true);
    }

    public List<Person> LoadPeople()
    {
        List<Person> people = new();


        foreach (string line in DataFile.ReadFromFile().Split("\n")[..^1])
        {
            string[] data = line[1..].Split(divide);

            switch (data[0].ToLower())
            {
                case "person":
                    people.Add(new Person(data[1], int.Parse(data[2])));
                    break;
                case "employee":
                    people.Add(new Employee(data[1], int.Parse(data[2]), data[3]));
                    break;
            }
        }

        return people;
    }
}


public class Program
{
    public static void Main()
    {
        // List of people to write to and read from the file
        var people = new List<Person>
        {
            new Person("Alice", 28),
            new Person("Bob", 35),
            new Employee("Charlie", 42, "Manager")
        };


        PersonFileService personFileService = new("People.md");

        // for people-viewing write 
        personFileService.WritePeopleToFile(people);


        // for people-viewing read
        foreach (string line in personFileService.ReadPeopleFromFile()) Console.WriteLine(line);


        // Writing people to the file
        personFileService.SavePeople(people);


        // Reading people from the file
        foreach (var person in personFileService.LoadPeople()) person.Introduce();

    }
}
