using System;
using System.Collections.Generic;
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


class FunFactAboutCat
{
    private static int id;
    public string? fact { get; set; }
    public int length { get; set; }

    public override string ToString()
    {
        id++;
        return $"Fact {id}:{fact}\n";
    }
}

class Joke
{
    public string? type { get; set; }
    public string? setup { get; set; }
    public string? punchline { get; set; }
    public int id { get; set; }

    public override string ToString() => $"Setup:{setup}\nPunchline:{punchline}\nType:{type}\nID:{id}\n";

    public string ToSaveFile() => ToString() + '\n';
}


class University
{
    public string? name { get; set; }

    public string? alpha_two_code { get; set; }
    public string? country { get; set; }
    public string? stateProvince { get; set; }
    public string[]? domains { get; set; }
    public string[]? web_pages { get; set; }

    public override string ToString() => $"Name:{name};\nAlpha code:{alpha_two_code};\nState province:{stateProvince};\ndomains:{string.Join('\n', domains!)}\nWeb pages:{string.Join('\n', web_pages!)}\n";

}

class PersonResponse
{
    public Person[]? results { get; set; }
}

class TotalResponse
{
    public string? gender { get; set; }
}

class Person
{
    public Name? name { get; set; }
    public string? gender { get; set; }
}

class Name
{
    public string? first { get; set; }
}


class PostalCode
{
    public string? postal { get; set; }

    public string? country { get; set; }
}

class PostalLocation
{
    public Location[]? places { get; set; }
}

class Location
{
    [JsonPropertyName("place name")]
    public string? PlaceName { get; set; }
    public string? longitude { get; set; }
    public string? latitude { get; set; }
    public override string ToString() => $"Place name: {PlaceName},\nLongitude: {longitude},\nLatitude: {latitude};\n";

}


public class Program
{

    public static string TempPath = "Temp.txt";
    
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



    public static void PracticeA1()
    {
        CoindeskResponse coindesk = JsonSerializer.Deserialize<CoindeskResponse>(Request("https://api.coindesk.com/v1/bpi/currentprice.json"))!;
        Console.WriteLine(coindesk.bpi.USD.rate_float * 102.58d);
    }

    public static void PracticeA2()
    {
        Console.WriteLine("Get Fun Fact About Cat, Version:0.1a\nType r or R in console.\nType nothing to exit!\n");

        while (true)
        {
            Console.Write('>');
            string input = Console.ReadLine().ToLower();
            if (input == "r")
            {
                FunFactAboutCat info = JsonSerializer.Deserialize<FunFactAboutCat>(Request("https://catfact.ninja/fact"))!;
                Console.WriteLine(info.ToString());
            }
            else if (input == "") break;
        }

        Console.WriteLine("Good bye! Thanks for using my Console's Apps");

    }

    public static void PracticeA3()
    {
        FileManager.RemoveFile(TempPath, false);
        Console.WriteLine("Get Fun Jokes, Version:0.1a\nType r or R in console!\nType nothing to exit.\n");

        while (true)
        {
            Console.Write('>');
            string input = Console.ReadLine()!.ToLower();
            if (input == "r")
            {
                Joke joke = JsonSerializer.Deserialize<Joke>(Request("https://official-joke-api.appspot.com/random_joke"))!;
                FileManager.Write(TempPath, joke.ToSaveFile(), true);
                Console.WriteLine(joke.ToString());
            }
            else if (input == "") break;
        }

        if (File.Exists(TempPath)) Ask();

        Console.WriteLine("\nGood bye! Thanks for using my Console's Apps");
    }

    private static void Ask()
    {
        string input;
        Console.WriteLine("\nPlease name your data-save about jokes.\nYes or No?\n");
        Console.Write('>');

        if (Console.ReadLine()!.ToLower() == "yes")
        {
            Console.WriteLine("\nType a new name for your save file:\n");
            while (true)
            {
                Console.Write('>');
                input = Console.ReadLine()!;

                if (input.ToLower() == "exit") break;
                else if (input != "" && input.Contains('.'))
                {
                    if (FileManager.ReName(TempPath, input))
                    {
                        Console.WriteLine("File was saved on the path :)");
                        break;
                    }
                }

                if (input.Contains('.')) Console.WriteLine("Empty Name!\nRetry, or\ntype a Exit\n"); 
                else Console.WriteLine("Missing file's TYPE!\nRetry, or\nwrite a Exit\n");
            }
        }
    }


    public static void PracticeA4()
    {
        Console.WriteLine("Write a country, to get her data base of universities");
        Console.Write(">");
        string country = Console.ReadLine();

        if (country == "")
        {
            Console.WriteLine("You wrote a null name country");
            return;
        }

        University[] universities = JsonSerializer.Deserialize<University[]>(Request($"http://universities.hipolabs.com/search?country={country}&limit=3"))!;
        if (universities.Length == 0)
        {
            Console.WriteLine("wrong country name name");
            return;
        }

        foreach (University university in universities) Console.WriteLine(university.ToString());
    }
    

    public static void PracticeB()
    {
        Person person = JsonSerializer.Deserialize<PersonResponse>(Request("https://randomuser.me/api/"))!.results![0];
        if (JsonSerializer.Deserialize<TotalResponse>(Request($"https://api.genderize.io/?name={person.name!.first}"))!.gender == person.gender) Console.WriteLine("YES");
        else Console.WriteLine("NO");
    }

    public static void PracticeC()
    {
        PostalCode code = JsonSerializer.Deserialize<PostalCode>(Request($"https://ipinfo.io/{Request("https://api.ipify.org")}/geo"))!;
        PostalLocation postal = JsonSerializer.Deserialize<PostalLocation>(Request($"https://api.zippopotam.us/{code.country}/{code.postal}"))!;
        foreach (Location location in postal.places!) Console.WriteLine(location.ToString());
    }

    public static void Main()
    {
        //PracticeA1();
        //PracticeA2();
        //PracticeA3();
        //PracticeA4();
        //PracticeB();
        //PracticeC();

        Console.ReadLine();

    }

}


