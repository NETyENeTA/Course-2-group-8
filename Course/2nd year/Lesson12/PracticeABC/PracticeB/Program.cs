using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


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


public class Product
{
    public string? Name { get; set; }
    public int Price { get; set; }
    public string? Description { get; set; }
}


public class Products
{
    public Product[]? products { get; set; }
}

public class Shop
{
    public string? category { get; set; }
    public string[]? products { get; set; }
}


public class Order
{
    public OrderElements? order { get; set; }
}

public class OrderElements
{
    public int id { get; set; }
    public string[]? items { get; set; }
    public int total { get; set; }
}

public class User
{
    public UserElements? user { get; set; }
}

public class UserElements
{
    public string? name { get; set; }
    public string? email  { get; set; }
    public int purchases { get; set; }
}


public class Cart
{
    public string[]? cart { get; set; }
}


public class Shopping
{
    public ShoppingElements? shopping { get; set; }
}

public class ShoppingElements
{
    public string? method { get; set; }
    public int price { get; set; }
    public int estimatedDays { get; set; }
}


public class Payment
{
    public PaymentElements? payment { get; set; }
}

public class PaymentElements
{
    public string? method { get; set; }
    public string? status { get; set; }
}


public class Reviews
{
    public List<Review>? reviews { get; set; }
}

/*
 Don't working :/
public class ReviewNoComments
{
    public string? product { get; set; }
    public int rating { get; set; }

}

public class Review : ReviewNoComments
{
    public string? comment { get; set; }
}
*/


public class Review
{
    public string? product { get; set; }
    public int rating { get; set; }

    public string? comment { get; set; }

}


public class Discounts
{
    public Discount[]? discounts { get; set; }
}


public class Discount
{
    public string? product { get; set; }
    public string? discount { get; set; }
}


public class Addresses
{
    public Address[]? addresses { get; set; }
}

public class Address
{
    public string? type { get; set; }
    public string? address { get; set; }
}



public class Program
{

    public static void First()
    {
        Product product = new Product
        {
            Name = "Laptop",
            Price = 1200,
            Description = "High performance laptop"
        };

        Products products = new Products
        {
            products = [product]
        };


        string json = JsonSerializer.Serialize(products);
        Console.WriteLine(json);

    }


    public static void Second() 
    {

        Shop shop = new Shop
        {
            category = "Electronics",
            products = ["TV", "Radio", "Camera"]
        };


        string json = JsonSerializer.Serialize(shop);
        Console.WriteLine(json);
    }

    public static void Third()
    {
        OrderElements orderElements = new OrderElements
        {
            id = 12345,
            items = ["Laptop", "Camera"],
            total = 1700
        };

        Order order = new Order
        {
            order = orderElements
        };

        string json = JsonSerializer.Serialize(order);
        Console.WriteLine(json);
    }


    public static void Fourth()
    {
        UserElements userElements = new UserElements
        {
            name = "John Doe",
            email = "john@example.com",
            purchases = 5
        };

        User user = new User
        {
            user = userElements
        };

        string json = JsonSerializer.Serialize(user);
        Console.WriteLine(json);

    }

    public static void Fith()
    {
        Cart cart = new Cart
        {
            cart = ["Product1", "Product2", "Product3", "Product4"]
        };

        string json = JsonSerializer.Serialize(cart);
        Console.WriteLine(json);
    }

    public static void Sixth()
    {
        ShoppingElements shoppingElements = new ShoppingElements
        {
            method = "Standart",
            price = 20,
            estimatedDays = 3
        };

        Shopping shopping = new Shopping
        {
            shopping = shoppingElements
        };

        string json = JsonSerializer.Serialize(shopping);
        Console.WriteLine(json);
    }
    public static void Seventh() 
    {

        PaymentElements paymentElements = new PaymentElements
        {
            method = "Credit Card",
            status = "Pending"
        };

        Payment payment = new Payment
        {
            payment = paymentElements
        };


        string json = JsonSerializer.Serialize(payment);
        Console.WriteLine(json);
    }
    public static void Eighth() 
    {
        Review review1 = new Review
        {
            product = "Laptop",
            rating = 5,
            comment = "Great!"
        };

        Review review2 = new Review
        {
            product = "Camera",
            rating = 4
        };

        List<Review> reviewList = [review1, review2];

        Reviews reviews = new Reviews
        {
            reviews = reviewList
        };

        string json = JsonSerializer.Serialize(reviews);
        Console.WriteLine(json);
    }
    public static void Ninth()
    {

        Discount discount1 = new Discount
        {
            product = "TV",
            discount = "15%"
        };

        Discount discount2 = new Discount
        {
            product = "Camera",
            discount = "10%"
        };

        Discounts discounts = new Discounts
        {
            discounts = [discount1, discount2]
        };


        string json = JsonSerializer.Serialize(discounts);
        Console.WriteLine(json);
    }
    public static void Tenth()
    {
        Address address1 = new Address
        {
            type = "Billing",
            address = "23 Main St"
        };

        Address address2 = new Address
        {
            type = "Shipping",
            address = "456 Elm St"
        };

        Addresses addresses = new Addresses
        {
            addresses = [address1, address2]
        };

        string json = JsonSerializer.Serialize(addresses);
        Console.WriteLine(json);
    }

    public static void Main()
    {

        // Every each json class has written in here

        //First();
        //Second();
        //Third();
        //Fourth();
        //Fith();
        //Sixth();
        //Seventh();
        //Eighth();
        //Ninth();
        //Tenth();

        Console.ReadLine();

    }
}
