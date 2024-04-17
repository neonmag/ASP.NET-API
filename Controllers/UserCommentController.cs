﻿using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Data.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCommentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserCommentDao _userCommentDao;

        public UserCommentController(DataContext dataContext, UserCommentDao userCommentDao)
        {
            _dataContext = dataContext;
            _userCommentDao = userCommentDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserCommentDao>>> GetAllUserComments()
        {
            var _userComments = await _userCommentDao.GetAllUserComments();

            var response = _userComments.Select(s => new UserComment(id: s.id,
                                                                     userId: s.userId,
                                                                     authorId: s.authorId,
                                                                     content: s.content,
                                                                     createdAt: s.createdAt)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserComment>> CreateUserComment([FromBody] UserCommentModel model)
        {
            var result = new UserComment(Guid.NewGuid(),
                model.userId,
                model.authorId,
                model.content,
                DateTime.Now);

            var response = _dataContext.dbUserComments.AddAsync(result);

            return Ok(response);
        }
    }
}
