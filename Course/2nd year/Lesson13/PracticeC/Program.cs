using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Testy;

public class FileManager
{
    private readonly string Path;

    public FileManager(string filePath) => this.Path = filePath;

    public void Write(string text, bool append = false)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(Path, append)) writer.Write(text);
        }
        catch (Exception ex) { Console.WriteLine($"Write [Error]! cause:{ex}."); }
    }

    public string Read()
    {
        try
        {
            if (File.Exists(Path)) using (StreamReader reader = new StreamReader(Path)) return reader.ReadToEnd();
        }
        catch (Exception ex) { Console.WriteLine($"Read [Error]! cause:{ex}."); }
        Console.WriteLine("File hasn't found!");
        return string.Empty;
    }

    public void RemoveFile() => File.Delete(Path);


    public static void Write(string path, string text, bool append = false)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(path, append)) writer.Write(text);
        }
        catch (Exception ex) { Console.WriteLine($"Write [Error]! cause:{ex}."); }
    }

    public static string Read(string path)
    {
        try
        {
            if (File.Exists(path)) using (StreamReader reader = new StreamReader(path)) return reader.ReadToEnd();
        }
        catch (Exception ex) { Console.WriteLine($"Read [Error]! cause:{ex}."); }
        Console.WriteLine("File hasn't found!");
        return string.Empty;
    }

    public static void RemoveFile(string path) => File.Delete(path);


}

public class MyJSON <T>
{
    private FileManager fileManager { get; set; }

    private static JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    };

    public MyJSON(string path)
    {
        fileManager = new FileManager(path);
    }

    public T? Read() => JsonSerializer.Deserialize<T>(fileManager.Read());

    public static T? Read(string path) => JsonSerializer.Deserialize<T>(FileManager.Read(path));

    public void Write(T obj) => fileManager.Write(JsonSerializer.Serialize(obj, options));

    public static void Write(T obj, string path) => FileManager.Write(path, JsonSerializer.Serialize(obj, options));

}


class Company
{
    public string? companyName { get; set; }
    public List<Employee>? employees { get; set; }
    public Dictionary<string, string>? location { get; set; }

    public void AddEmployee(Employee employee) => employees?.Add(employee);

    public Employee? GetEmployee(int id)
    {
        foreach (Employee employee in employees!) if (employee.id == id) return (Employee?)employee.Clone();
        return null;
    }

    private bool CheckId(int id, string message)
    {
        foreach (Employee employee in employees!)
        {
            if (employee.id == id)
            {
                Console.WriteLine($"Found Simular ID: {message}, cause {id} == {id}, Место занято {employee.name}");
                return true;
            }
        }
        return false;
    }

    public void SetEmployee(int index, Employee employee)
    {
        if (CheckId(employee.id, $"Set {employee.name} Canceled")) return;
        employees![index] = employee;
    }

    public void InsertEmplyee(int index, Employee employee)
    {
        if (CheckId(employee.id, $"Insert {employee.name} Canceled")) return;
        employees!.Insert(index, employee);
    }

    public int GetAverageSalary()
    {
        int total = 0;
        foreach (Employee employee in employees!) total += employee.salary;
        return total / employees.Count();
    }


}

class Employee : ICloneable
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? position { get; set; }
    public string[]? skills { get; set; }
    public int salary { get; set; }


    public object Clone() => this.MemberwiseClone();

}


class Cinema
{
    public List<Film>? movies { get; set; }
    public List<string>? genres { get; set; }
    public Rating? ratings { get; set; }

    public void AddMovie(Film movie) => movies?.Add(movie);
    public void AddMovies(Film[] movies)
    {
        foreach (Film movie in movies) AddMovie(movie);
    }

    public void AddGenre(string genre) => genres?.Add(genre);

}

class Rating
{
    public int IMDb { get; set; }
    public string? RottenTomatoes { get; set; }
}


class Director
{
    public string? name { get; set; }
    public string? born { get; set; }
}

class Cast
{
    public string? name { get; set; }
    public string? role { get; set; }
}

class Film
{
    public string? tittle { get; set; }
    public int year { get; set; }
    public Director? director { get; set; }
    public Cast[]? cast { get; set; }
}


public class Program
{
    public static void PracticeC()
    {
        MyJSON<Cinema> CinemaJson = new("1.json");
        Cinema cinema = CinemaJson.Read()!;

        Director[] directors = [
            new() { name="Jone Ban", born="1999-12-22" },
            new() { name="Lilia Ann", born="1952-5-12" },
            new() { name="VanGo Lan", born="1983-3-30" },
            ];

        Cast[] casts =
        {
            new() { name = "LoRence Mi", role = "Steve" },
            new() { name = "Afgam Ni", role = "Freddy" },
            new() { name = "Realf gotofon", role = "Ralf" }
        };


        Film[] films = [
            new() { tittle = "Ralf", director = directors[0], cast = casts, year = 2017 },
            new() { tittle = "Gigachad", director = directors[0], cast = casts, year = 1980 },
            new() { tittle = "Meon", director = directors[0], cast = casts, year = 2001 },
        ];

        cinema.AddMovies(films);
        CinemaJson.Write(cinema);

        MyJSON<Company> RogsAndCamps = new("2.json");
        Company company = RogsAndCamps.Read()!;

        company.AddEmployee(new()
        {
            id = 100,
            name = "Александр",
            position = "Программист",
            skills = ["Back-end", "Front-end", "Full-stack", "Soft-Skills"],
            salary = 250_000
        });

        Employee employee = company.GetEmployee(1001)!;
        employee.position = "Front-End";
        //employee.id = 1002; // For error!
        employee.id = 1004;
        company.InsertEmplyee(2, employee);
        //company.SetEmployee(0, employee); // or tak :)

        RogsAndCamps.Write(company);
    }


    public static void Main()
    {
        PracticeC();
    }

}


