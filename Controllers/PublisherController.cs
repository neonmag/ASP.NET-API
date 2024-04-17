﻿using FullStackBrist.Server.Models.Creators;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.CreatorsDao;
using Slush.Data;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly PublisherDao _publisherDao;

        public PublisherController(DataContext dataContext, PublisherDao publisherDao)
        {
            _dataContext = dataContext;
            _publisherDao = publisherDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDao>>> GetAllPublishers()
        {
            var publishers = await _publisherDao.GetAllPublishers();

            var response = publishers.Select(p => new Publisher(id: p.id,
                                                                subscribersCount: p.subscribersCount,
                                                                name: p.name,
                                                                description: p.description,
                                                                avatar: p.avatar,
                                                                backgroundImage: p.backgroundImage,
                                                                urlForNewsPage: p.urlForNewsPage,
                                                                createdAt: p.createdAt)).ToList();

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher([FromBody] PublisherModel model)
        {
            var result = new Publisher(Guid.NewGuid(),
                0,
                model.name,
                model.description,
                model.avatar,
                model.backgroundImage,
                null,
                DateTime.Now);

            var response = _dataContext.dbPublishers.AddAsync(result);

            return Ok(response);
        }
    }
}
