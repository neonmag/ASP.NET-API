﻿using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly PostDao _postDao;

        public PostController(DataContext dataContext, PostDao postDao)
        {
            _dataContext = dataContext;
            _postDao = postDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDao>>> GetAllPosts()
        {
            var _posts = await _postDao.GetAllPosts();

            var response = _posts.Select(p => new Post(p.id,
                                                           p.title,
                                                           p.description,
                                                           p.likesCount,
                                                           p.dislikesCount,
                                                           p.discussionId,
                                                           p.gameId,
                                                           p.authorId,
                                                           p.content,
                                                           p.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostModel model)
        {
            var result = new Post(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                0,
                model.discussionId,
                model.gameId,
               model.authorId,
               model.content,
                                            DateTime.Now
                                            );
            _dataContext.dbPosts.AddAsync(result);
            return Ok(result);
        }
    }
}
