﻿using FullStackBrist.Server.Models.GameGroup;
using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameGroupDao;
using Slush.Data;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameTopicController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameTopicDao _gameTopicDao;

        public GameTopicController(DataContext dataContext, GameTopicDao gameTopicDao)
        {
            _dataContext = dataContext;
            _gameTopicDao = gameTopicDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameTopicDao>>> GetAllGameTopics()
        {
            var gameTopics = await _gameTopicDao.GetAllGameTopics();

            var response = gameTopics.Select(g => new GameTopic(id: g.id,
                                                                attachedId: g.attachedId,
                                                                name: g.name,
                                                                description: g.description,
                                                                createdAt: g.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<GameTopic>> CreateGameTopic([FromBody] GameTopicModel model)
        {
            var result = new GameTopic(Guid.NewGuid(),
                                            model.attachedId,
                                            model.name,
                                            model.description,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbGameTopics.AddAsync(result);

            return Ok(response);
        }
    }
}
