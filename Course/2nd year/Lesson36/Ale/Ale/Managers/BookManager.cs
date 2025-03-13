using Ale.Contexts;
using Ale.Interfaces;
using Ale.Models;
using Microsoft.EntityFrameworkCore;


namespace Ale.Managers;
public class BookManager : IBookManager
{

    DateBaseContext context;

    public Book[]? GetBooks(string title)
    {
        Book[] result = context.Books.Where(b => b.Title == title).ToArray();
        if (result.Length == 0) return null;
        return result;
    }

    public List<Book>? GetBooks(int tag)
    {
        Tag[] tags = context.Tags.Where(t => t.Id == tag).ToArray();
        if (tags.Length == 0) return null;

        List<Book> result = [];

        foreach (Tag Tag in tags)
        {
            Book[] books = context.Books.Where(b => b.Id == Tag.IdBook).ToArray();
            result.AddRange(books);
        }

        return result;
    }

    public Book? GetBook(int id) => context.Books.FirstOrDefault(b => b.Id == id);

    public List<Book> GetBooks() => context.Books.ToList();

    public void Create(Book book)
    {
        if (VerifyBook(book)) context.Books.Add(book);
    }

    public void Delete(Book book)
    {
        if (!VerifyBook(book)) return;
        context.Books.Remove(book);
    }

    public void Delete(int id)
    {
        Book[] books = context.Books.Where(b => b.Id == id).ToArray();

        foreach (Book book in books) Delete(book);
    }


    public void AddTag(string name, Book book)
    {
        context.Tags.Add(new(name, book.Id));
        context.SaveChanges();
    }

    public void AddTag(string name, int book)
    {
        context.Tags.Add(new(name, book));
        context.SaveChanges();
    }


    public void RemoveTag(int id)
    {
        Tag[] tags = context.Tags.Where(t => t.Id == id).ToArray();

        foreach (Tag tag in tags) RemoveTag(tag);
    }

    public void RemoveTag(Tag tag)
    {
        context.Tags.Remove(tag);
        context.SaveChanges();
    }

    public bool VerifyBook(Book book) => GetBook(book.Id) == null;
}

