using AngularApiProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace AngularApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(Context context)
        {
            this.context = context;
        }
        private readonly Context context;
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> ProductList = context.Product.ToList();
            if (ProductList == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(ProductList);
        }
        [HttpGet("{id:int}", Name = "getOneRoutee")]
        public IActionResult getByID(int id)
        {
            Product Product = context.Product.FirstOrDefault(P=> P.id == id);
            if (Product == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(Product);
        }
        [HttpGet("Catid")]
        public IActionResult getproductsByCatID([FromQuery] int catID)
        {
            List<Product> ProductList = context.Product.Where(P => P.catID == catID).ToList();
            if (ProductList == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(ProductList);
        }
        [HttpGet("{Name:alpha}")]
        public IActionResult getByNAme(string Name)
        {
            Product product =
                 context.Product.FirstOrDefault(C => C.name.Contains(Name));
            if (product == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(product);

        }
        [HttpPost]
        public IActionResult New(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Product.Add(product);
                    context.SaveChanges();
                    return Ok(product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPatch("{id:int}")]
        public IActionResult Edit(int id, Product Pro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product ProModel =
                        context.Product.FirstOrDefault(P => P.id == id);
                    ProModel.name = Pro.name;
                    ProModel.price = Pro.price;
                    ProModel.quantity = Pro.quantity;
                    ProModel.catID = Pro.catID;
                    ProModel.img = Pro.img;
                    ProModel.date = Pro.date;

                    context.SaveChanges();

                    return StatusCode(204, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product ProModel =
                        context.Product.FirstOrDefault(d => d.id == id);

                    context.Remove(ProModel);
                    context.SaveChanges();

                    return StatusCode(200, "Data Deleted");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPost, DisableRequestSizeLimit]
        [Route("Images")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
