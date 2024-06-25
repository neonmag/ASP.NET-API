﻿using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.LanguageRepository;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageInGameController : Controller
    {
        private readonly LanguageInGameRepository _languageInGameDao;

        public LanguageInGameController(LanguageInGameRepository languageInGameDao)
        {
            _languageInGameDao = languageInGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageInGameRepository>>> GetAllLanguagesInGame()
        {
            var _languages = await _languageInGameDao.GetAllLanguageInGames();

            return Ok(_languages);
        }


        [HttpPost]
        public async Task<ActionResult<LanguageInGame>> CreateLanguageInGame([FromBody] LanguageInGame model)
        {
            var result = new LanguageInGame(Guid.NewGuid(),
                                            model.gameId,
                                            model.languageId,
                                            DateTime.Now
                                            );
            await _languageInGameDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageInGame>> GetLanguageInGame(Guid id)
        {
            var response = await _languageInGameDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguageInGame(Guid id)
        {
            await _languageInGameDao.DeleteLanguageInGame(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateLanguageInGame(Guid id, [FromBody] LanguageInGameModel language)
        {
            var result = await _languageInGameDao.UpdateLanguageInGame(new LanguageInGame(id, language.gameId, language.languageId, language.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<LanguageInGame>>> GetAllLanguagesInGameByIds([FromBody] List<Guid> guidList)
        {
            var response = await _languageInGameDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
