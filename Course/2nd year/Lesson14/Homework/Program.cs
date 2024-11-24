using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Example;

public class FileManager
{
    private readonly string Path;

    public FileManager(string filePath)
    {
        RemoveFile(filePath);
        this.Path = filePath;
    }

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

    public void RemoveFile(bool needSystemMessage = true)
    {
        if (File.Exists(Path)) { File.Delete(Path); return; }
        if (needSystemMessage) Console.WriteLine("File hasn't found!");
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

    public static void RemoveFile(string path, bool needSystemMessage = true)
    {
        if (File.Exists(path)) { File.Delete(path); return; }
        if (needSystemMessage) Console.WriteLine("File hasn't found!");
    }

    public static bool ReName(string path, string newName, bool needSystemMessage = true)
    {
        try
        {
            File.Move(path, newName);
        }
        catch
        {
            if (needSystemMessage) Console.WriteLine("File with simular name has been exist!");
            return false;
        }
        return true;
    }

    public static bool Exists(string path) => File.Exists(path);

}

class Post
{
    public int userId { get; set; }
    public int id { get; set; }
    public string? title { get; set; }
    public string? body { get; set; }
}

public class Program
{
    public static string Request(string url)
    {
        WebRequest request = WebRequest.Create(url);

        WebResponse response = request.GetResponse();
        Stream DataStream = response.GetResponseStream();
        StreamReader reader = new(DataStream);
        string TotalData = reader.ReadToEnd();

        reader.Close();
        response.Close();

        return TotalData;
    }

    public static void Main()
    {
        Post[] posts = JsonSerializer.Deserialize<Post[]>(Request("https://jsonplaceholder.typicode.com/posts"))!;

        FileManager file = new("Posts.txt");
        FileManager fileUser = new("Posts_User1.txt");

        foreach (Post post in posts)
        {
            file.Write($"{post.title}\n{post.body}\n\n", true);
            if (post.userId == 1) fileUser.Write($"{post.title}\n{post.body}\n\n", true);
        }

        Console.ReadLine();
    }

}


