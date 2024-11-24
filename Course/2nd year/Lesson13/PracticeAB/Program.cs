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

    public string? Read()
    {
        try
        {
            if (File.Exists(Path)) using (StreamReader reader = new StreamReader(Path)) return reader.ReadToEnd();
        }
        catch (Exception ex) { Console.WriteLine($"Read [Error]! cause:{ex}."); }
        Console.WriteLine("File hasn't found!");
        return string.Empty;
    }

    public void RemoveFile()
    {
        if (File.Exists(Path)) File.Delete(Path);
    }


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

    public static void RemoveFile(string path)
    {
        if (File.Exists(path)) File.Delete(path);
    }


}

public class MyJSON<T>
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

    public T? Read()
    {
        string? json = fileManager.Read();
        return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json);
    }

    public static T? Read(string path) => JsonSerializer.Deserialize<T>(FileManager.Read(path));

    public void Write(T obj) => fileManager.Write(JsonSerializer.Serialize(obj, options));

    public static void Write(T obj, string path) => FileManager.Write(path, JsonSerializer.Serialize(obj, options));

}


class Person
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public bool isVeridied { get; set; }

    public string ToString(string type) => type.Replace("%0%", id.ToString()).Replace("%1%", name).Replace("%2%", email).Replace("3", isVeridied.ToString());

}

class PersonAdditional
{
    public string? orderId { get; set; }
    public string? customerName { get; set; }
    public double totalPrice { get; set; }
    public List<string>? items { get; set; }
    public string ToString(string type) => type.Replace("%0%", orderId).Replace("%1%", customerName).Replace("%2%", totalPrice.ToString()).Replace("%3%", GetItems());

    private string GetItems()
    {
        string total = "\n";
        foreach (string item in items!) total += item + "\n";
        return total;
    }
}


class Library
{
    public string? libraryName { get; set; }
    public List<Book>? books { get; set; }

}

class Book
{
    public string? title { get; set; }
    public string? author { get; set; }
    public int year { get; set; }
}


public class Program
{
    public static void PracticeAB()
    {
        // PracticeA
        MyJSON<PersonAdditional> AnnaJson = new("2.json");

        Person Ivan = MyJSON<Person>.Read("1.json")!;
        PersonAdditional Anna = AnnaJson.Read()!;

        Console.WriteLine(Ivan.ToString("Email:%2%, ID:%0%"));
        Console.WriteLine(Anna.ToString("items:%3%"));

        //PracticeB
        Anna.items!.Add("Салфетки для мониторв");
        Anna.totalPrice += 1555;
        Anna.totalPrice -= Anna.totalPrice * 0.02d;

        AnnaJson.Write(Anna);

        MyJSON<Library> LibraryJson = new("3.json");

        Library library = LibraryJson.Read()!;

        library.books!.Add(new()
        {
            title = "Капитанская дочка",
            author = "Александр Сергеевич Пушкин",
            year = 1836
        });

        LibraryJson.Write(library);

    }

    public static void Main()
    {
        PracticeAB();
    }

}
