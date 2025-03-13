using Ale.Models;

namespace Ale.Interfaces;

public interface IBookManager
{
    void Create(Book book);
    Book[]? GetBooks(string title);
    Book? GetBook(int id);
    List<Book> GetBooks();
    bool VerifyBook(Book book);
}