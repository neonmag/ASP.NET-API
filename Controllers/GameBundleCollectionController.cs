﻿using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopRepository;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleCollectionController : Controller
    {
        private readonly GameBundleCollectionRepository _dao;

        public GameBundleCollectionController(GameBundleCollectionRepository dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleCollectionRepository>>> GetAllCollections()
        {
            var _bundles = await _dao.GetAll();

            return Ok(_bundles);
        }

        [HttpPost]
        public async Task<ActionResult<GameBundleCollection>> CreateCollection([FromBody] GameBundleCollectionModel model)
        {
            var result = new GameBundleCollection(Guid.NewGuid(),
                model.gameId,
                model.dlcId,
                model.bundleId,
                DateTime.Now);

            var response = _dao.Add(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBundleCollection>> GetCollection(Guid id)
        {
            var response = await _dao.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameBundleCollection>>> GetCollectionByGameId(Guid id)
        {
            var response = await _dao.GetByGameId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _dao.DeleteGameBundleCollection(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GameBundleCollectionModel model)
        {
            var result = await _dao.UpdateGameBundleCollection(new GameBundleCollection(id, model.gameId, model.dlcId, model.bundleId, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameBundleCollection>>> GetAllBundleCollectionsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _dao.GetByGameIds(guidList);

            return Ok(response);
        }
    }
}
