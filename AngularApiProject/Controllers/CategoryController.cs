using AngularApiProject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        public CategoryController(Context context)
        {
            this.context = context;
        }
        private readonly Context context;
        [HttpGet]
        //[Authorize]
        public IActionResult getAll()
        {
            List<Category> CategoryList = context.Category.Include(c => c.Products).ToList();
            if (CategoryList == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(CategoryList);
        }
      
        [HttpGet("{id:int}", Name = "getOneRoute")]
      
        public IActionResult getByID(int id)
        {
            Category CategoryList = context.Category.Include(c=>c.Products).FirstOrDefault(C => C.id == id);
            if (CategoryList == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(CategoryList);
        }
        [HttpGet("{Name:alpha}")]
        public IActionResult getByNAme(string Name)
        {
            Category CategoryList =
                 context.Category.FirstOrDefault(C => C.name.Contains(Name));
            if (CategoryList == null)
            {
                return BadRequest("NO Matches");
            }
            return Ok(CategoryList);

        }
        [HttpPost]
        public IActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Category.Add(category);
                    context.SaveChanges();
                    string url = Url.Link("getOneRoute", new { id = category.id });
                    return Created(url, category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPatch("{id:int}")]
        public IActionResult Edit(int id, Category Cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category CatModel =
                        context.Category.FirstOrDefault(d => d.id == id);
                    CatModel.name = Cat.name;
                    context.SaveChanges();

                    return StatusCode(204, "Data Saved");// Created(url, dep);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}
