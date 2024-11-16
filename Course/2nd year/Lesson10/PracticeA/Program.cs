using System;
using System.IO;

namespace PracticeA;

public class FileManager
{
    private readonly string filePath;

    public FileManager(string filePath) => this.filePath = filePath;

    public void WriteToFile(string text)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false)) writer.Write(text);
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
}


class FileOperations
{
    static void Main(string[] args)
    {

        // чтение запись в файл test.txt

        string? text = Console.ReadLine();
        if (text == null) return;

        FileManager file = new("test.txt");
        file.WriteToFile(text);
        Console.WriteLine(file.ReadFromFile());
    }
}