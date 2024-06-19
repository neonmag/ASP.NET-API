﻿using Microsoft.AspNetCore.Mvc;
using Slush.DAO;
using Slush.Entity;
using Slush.Models;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionController : Controller
    {
        private readonly DiscussionDao _discussionDao;

        public DiscussionController( DiscussionDao discussionDao)
        {
            _discussionDao = discussionDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiscussionDao>>> GetAllDiscussions()
        {
            var _discussions = await _discussionDao.GetAllDiscussions();

            return Ok(_discussions);
        }

        [HttpPost]
        public async Task<ActionResult<Discussion>> CreateDiscussion([FromBody] DiscussionModel model)
        {
            var result = new Discussion(Guid.NewGuid(),
                model.authordId,
                model.attachedId,
                model.content,
                model.likesCount,
                DateTime.Now);

            var response = _discussionDao.UpdateDiscussion(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discussion>> GetDiscussion(Guid id)
        {
            var response = await _discussionDao.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byattachedid/{id}")]
        public async Task<ActionResult<List<Discussion>>> GetDiscussionByAttachedId(Guid id)
        {
            var response = await _discussionDao.GetByAttachedId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscussion(Guid id)
        {
            await _discussionDao.DeleteDiscussion(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiscussion(Guid id, [FromBody] DiscussionModel model)
        {
            var result = await _discussionDao.UpdateDiscussion(new Discussion(id, model.authordId, model.attachedId, model.content, model.likesCount, model.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Discussion>>> GetAllDiscussionsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _discussionDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
