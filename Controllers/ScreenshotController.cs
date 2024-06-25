﻿using FullStackBrist.Server.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data.Entity.Profile;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreenshotController : Controller
    {
        private readonly ScreenshotDao _screenshotDao;
        private readonly MinioService _minioService;

        public ScreenshotController(ScreenshotDao screenshotDao, MinioService minioService)
        {
            _screenshotDao = screenshotDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScreenshotDao>>> GetAllScreenshots()
        {
            var _screenshots = await _screenshotDao.GetAllScreenshots();

            return Ok(_screenshots);
        }


        [HttpPost]
        public async Task<ActionResult<Screenshot>> CreateScreenshot([FromBody] ScreenshotModel model, IFormFile? file)
        {
            var result = new Screenshot(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.gameId,
                model.authorId,
                model.contentUrl,
                DateTime.Now);

            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            await _screenshotDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Screenshot>> GetScreenshot(Guid id)
        {
            var response = await _screenshotDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<Screenshot>>> GetScreenshotByGame(Guid id)
        {
            var response = await _screenshotDao.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScreenshot(Guid id)
        {
            await _screenshotDao.DeleteScreenshot(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateScreenshot(Guid id, [FromBody] ScreenshotModel screenshot, IFormFile? file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        screenshot.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            var result = await _screenshotDao.UpdateScreenshot(new Screenshot(id, screenshot.title, screenshot.description, screenshot.likesCount, screenshot.gameId, screenshot.authorId, screenshot.contentUrl, screenshot.createdAt));
            return Ok(result);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<Screenshot>>> GetByUserId(Guid id)
        {
            var response = await _screenshotDao.GetByUserId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Screenshot>>> GetAllScreenshotsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _screenshotDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
