﻿using FullStackBrist.Server.Models.Profile;
using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly FriendsDao _friendsDao;

        public FriendsController(DataContext dataContext, FriendsDao friendsDao)
        {
            _dataContext = dataContext;
            _friendsDao = friendsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendsDao>>> GetAllFriends()
        {
            var _friends = await _friendsDao.GetAllFriends();

            var response = _friends.Select(f => new Friends(id: f.id,
                                                            userId:       f.userId,
                                                            friendId:       f.friendId,
                                                            createdAt:       f.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Friends>> CreateFriend([FromBody] FriendsModel model)
        {
            var result = new Friends(Guid.NewGuid(),
                                            model.userId,
                                            model.friendId,
                                            DateTime.Now
                                            );
            var response = await _dataContext.dbFriends.AddAsync(result);

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Friends>> GetFriends(Guid id)
        {
            var response = await _friendsDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFriends(Guid id)
        {
            await _friendsDao.DeleteFriends(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFriends(Guid id, [FromBody] FriendsModel model)
        {
            var result = new Friends(id, model.userId, model.friendId, model.createdAt);
            await _friendsDao.UpdateFriends(result);
            return NoContent();
        }
    }
}
