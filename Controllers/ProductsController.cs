using Microsoft.AspNetCore.Mvc;

namespace MyRestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99M },
        new Product { Id = 2, Name = "Mouse", Price = 29.99M },
        new Product { Id = 3, Name = "Keyboard", Price = 79.99M }
    };

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(products);
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = products.Max(p => p.Id) + 1;
        products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // PUT: api/products/1
    [HttpPut("{id}")]
    public ActionResult Update(int id, Product product)
    {
        var existing = products.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            return NotFound();
        
        existing.Name = product.Name;
        existing.Price = product.Price;
        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();
        
        products.Remove(product);
        return NoContent();
    }
}

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

