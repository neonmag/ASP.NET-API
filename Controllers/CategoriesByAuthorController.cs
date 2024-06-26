﻿using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CategoriesDao;
using Slush.Data.Entity;
using Slush.Data;
using FullStackBrist.Server.Models.Creators;
using Slush.Entity.Store.Product.Creators;
using FullStackBrist.Server.Models.Categories;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesByAuthorController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CategoriesByAuthorDao _categoriesByAuthorDao;

        public CategoriesByAuthorController(DataContext dataContext, CategoriesByAuthorDao categoriesByAuthorDao)
        {
            _dataContext = dataContext;
            _categoriesByAuthorDao = categoriesByAuthorDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesByAuthorDao>>> GetAllCategoriesByAuthor()
        {
            var _categoriesByAuthor = await _categoriesByAuthorDao.GetAllCategoriesByAuthor();

            var response = _categoriesByAuthor.Select(c => new CategoryByAuthor(id: c.id,
                                                                                authorId: c.authorId,
                                                                                name: c.name,
                                                                                description: c.description,
                                                                                image: c.image,
                                                                                createdAt: c.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryByAuthor>> CreateCategoryByAuthor([FromBody] CategoryByAuthorModel model)
        {
            var result = new CategoryByAuthor(Guid.NewGuid(),
                                        model.authorId,
                                        model.name,
                                        model.description,
                                        model.image,
                                        DateTime.Now);
            var response = await _dataContext.dbCategoriesByAuthors.AddAsync(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryByAuthor>> GetCategoryByAuthor(Guid id)
        {
            var response = await _categoriesByAuthorDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryByAuthor(Guid id)
        {
            await _categoriesByAuthorDao.DeleteCategoryByAuthor(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryByAuthor(Guid id, [FromBody] CategoryByAuthorModel model)
        {
            var result = new CategoryByAuthor(id, model.authorId, model.name, model.description, model.image, DateTime.Now);
            await _categoriesByAuthorDao.UpdateCategoriesByAuthor(result);
            return NoContent();
        }
    }
}
