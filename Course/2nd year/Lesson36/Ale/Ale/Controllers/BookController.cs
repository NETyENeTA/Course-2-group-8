using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ale.Models;
using Ale.Managers;

namespace Ale.Controllers;


[ApiController]
public class BookController : Controller
{

    BookManager _bookManager;

    [HttpPost("api/book/verify")]
    public ActionResult Verify(Book book) => Ok(_bookManager.VerifyBook(book));

    [HttpDelete("api/book/delete/{id}")]
    public ActionResult Delete(int id)
    {
        _bookManager.Delete(id);
        return Ok("Ok");
    }

    [HttpDelete("api/book/delete")]
    public ActionResult Delete(Book book)
    {
        _bookManager.Delete(book);
        return Ok("Ok");
    }


    [HttpPost("/api/book/create")]
    public ActionResult Create(Book book)
    {
        _bookManager.Create(book);
        return Ok("All good!");
    }

    [HttpGet("/api/book/get/{id}")]
    public ActionResult GetBook(int id)
    {
        Book? user = _bookManager.GetBook(id);
        if (user == null) return BadRequest("Account undefined");
        return Ok(user);
    }

    [HttpPost("/api/book/add/Tag")]
    public ActionResult AddTag(int book, string name)
    {
        Book? Book = _bookManager.GetBook(book);

        if (Book == null) return BadRequest("Book undefined");

        _bookManager.AddTag(name, book);
        return Ok("added");
    }

    [HttpDelete("/api/book/delete/Tag")]
    public ActionResult DeleteTag(int tag)
    {
        _bookManager.RemoveTag(tag);
        return Ok("added");
    }


    [HttpGet("/api/book/getall")]
    public ActionResult GetUsers() => Ok(_bookManager.GetBooks());

}

