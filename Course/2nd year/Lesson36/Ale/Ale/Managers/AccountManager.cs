using Ale.Contexts;
using Ale.Interfaces;
using Ale.Models;

namespace Ale.Managers;

class AccountManager : IAccountManager
{

    DateBaseContext context;

    public User? GetAccount(string accountName) => context.Users.FirstOrDefault(u => u.Name == accountName);

    public List<User> GetAccounts() => context.Users.ToList();

    public void RegisterAccount(User account)
    {
        if (!VerifyAccount(account)) return;
        
        context.Users.Add(account);
        context.SaveChanges();
    }

    public void RegisterAccount(string name)
    {
        if (!VerifyAccount(name)) return;

        context.Users.Add(new(name));
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        User? user = context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return;

        context.Users.Remove(user);
        context.SaveChanges();
    }


    public void AddBook(User account, Book book)
    {
        context.Connections.Add(new(account.Id, book.Id));
        context.SaveChanges();
    }

    public void AddBook(string name, Book book)
    {

        User? user = GetAccount(name);
        if (user == null) return;

        AddBook(user, book);
    }

    public void RemoveBook(string name, Book book)
    {
        User? user = GetAccount(name);
        if (user == null) return;

        RemoveBook(user, book);
    }

    public void RemoveBook(User account, Book book)
    {
        context.Connections.Remove(new(account.Id, book.Id));
        context.SaveChanges();
    }

    public bool VerifyAccount(User account) => VerifyAccount(account.Name);
    public bool VerifyAccount(string name) => GetAccount(name) == null;
}
