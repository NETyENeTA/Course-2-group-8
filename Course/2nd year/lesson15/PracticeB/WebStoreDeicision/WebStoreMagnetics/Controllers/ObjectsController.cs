using Microsoft.AspNetCore.Mvc;
using WebStoreMagnetics.Modules;

namespace WebStoreMagnetics.Controllers;

[ApiController]
public class ObjectsController : Controller
{
    private static List<Product> products = [];

    [HttpPut]
    [Route("/store/update/price")]
    public IActionResult UpdatePrice(string name, double newPrice)
    {
        Product? product = products.FirstOrDefault(product => product.Name == name);
        if (product == null) return NotFound($"Продукт {name} Не найден!");
        product.Price = newPrice;
        return Ok($"Продукт {name} был обновлен с новой ценой:{newPrice};");
    }

    [HttpPut]
    [Route("/store/update/name")]
    public IActionResult UpdateName(string name, string newName)
    {
        Product? product = products.FirstOrDefault(product => product.Name == name);
        if (product == null) return NotFound($"Продукт {name} Не найден!");
        product.Name = newName;
        return Ok($"Продукт {name} был обновлен с новой ценой:{newName};");
    }

    [HttpPost]
    [Route("/store/add/product")]
    public IActionResult AddProduct(Product product)
    {
        products.Add(product);
        return Ok($"Продукт {product.Name} был добавлен.");
    }

    [HttpPost]
    [Route("/store/add")]
    public IActionResult AddProduct(string name, double price, int stock)
    {
        products.Add(new(name, price, stock));
        return Ok($"Продукт {name} был добавлен.");
    }

    [HttpDelete]
    [Route("/store/remove")]
    public IActionResult RemoveProduct(string name)
    {
        Product? product = products.FirstOrDefault(product => product.Name == name);
        if (product == null) return NotFound($"Продукт {name} Не найден!");
        return Ok($"Продукт {name} был удалён.");
    }


    [HttpGet]
    [Route("/store/get")]
    public IActionResult GetProduct(string name)
    {
        Product? product = products.FirstOrDefault(product => product.Name == name);
        if (product == null) return NotFound($"Продукт {name} Не найден!");
        return Json(product);
    }

    [HttpGet]
    [Route("/store/gets")]
    public IActionResult GetProducts()
    {
        if (products.Any()) return Json(products);
        return BadRequest("В продуктах пусто!");
    }

    [HttpGet]
    [Route("/store/get/oos")]
    public IActionResult OutOfStock()
    {
        List<Product> products = ObjectsController.products.Where(product => product.Stock == 0).ToList();
        if (products.Any()) return Json(products);
        return BadRequest("На складе пусто!");
    }


}

