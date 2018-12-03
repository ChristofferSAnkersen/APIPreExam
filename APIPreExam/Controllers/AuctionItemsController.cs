using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreExam.Data;
using APIPreExam.Models;

namespace APIPreExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionItemsController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public AuctionItemsController(AuctionDbContext context)
        {
            _context = context;
        }

        // GET: api/AuctionItems
        [HttpGet]
        public IEnumerable<AuctionItem> GetAuctionItems()
        {
            return _context.AuctionItems;
        }

        // GET: api/AuctionItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auctionItem = await _context.AuctionItems.FindAsync(id);

            if (auctionItem == null)
            {
                return NotFound();
            }

            return Ok(auctionItem);
        }

        // PUT: api/AuctionItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionItem([FromRoute] int id, [FromBody] AuctionItem auctionItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auctionItem.ItemNumber)
            {
                return BadRequest();
            }

            _context.Entry(auctionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AuctionItems
        [HttpPost]
        public async Task<IActionResult> PostAuctionItem([FromBody] AuctionItem auctionItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AuctionItems.Add(auctionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuctionItem", new { id = auctionItem.ItemNumber }, auctionItem);
        }

        // DELETE: api/AuctionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auctionItem = await _context.AuctionItems.FindAsync(id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            _context.AuctionItems.Remove(auctionItem);
            await _context.SaveChangesAsync();

            return Ok(auctionItem);
        }

        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItems.Any(e => e.ItemNumber == id);
        }
    }
}