using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ale.Models;
using Ale.Managers;

namespace Ale.Controllers;

[ApiController]
public class AccountController : Controller
{
    AccountManager _accountManager;
    BookManager _bookManager;

    [HttpPost("api/account/verify")]
    public ActionResult Verify(User user) => Ok(_accountManager.VerifyAccount(user));


    [HttpPost("/api/account/register")]
    public ActionResult Register(string name)
    {
        _accountManager.RegisterAccount(name);
        return Ok("All good!");
    }

    [HttpPut("/api/account/books/add")]
    public ActionResult AddBook(string name, int id)
    {
        User? user = _accountManager.GetAccount(name);
        if (user == null) return BadRequest("Account undefined");

        Book? book = _bookManager.GetBook(id);
        if (book == null) return BadRequest("Book undefined");

        _accountManager.AddBook(user, book);

        return Ok("added");
    }


    [HttpPut("/api/account/books/remove")]
    public ActionResult removeBook(string name, int id)
    {
        User? user = _accountManager.GetAccount(name);
        if (user == null) return BadRequest("Account undefined");

        Book? book = _bookManager.GetBook(id);
        if (book == null) return BadRequest("Book undefined");

        _accountManager.RemoveBook(user, book);

        return Ok("removed");
    }


    [HttpGet("/api/account/get/{name}")]
    public ActionResult GetUser(string name)
    {
        User? user = _accountManager.GetAccount(name);
        if (user == null) return BadRequest("Account undefined");
        return Ok(user);

    }

    [HttpGet("/api/account/getall")]
    public ActionResult GetUsers() => Ok(_accountManager.GetAccounts());

}

