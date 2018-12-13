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
    public class BidsController : ControllerBase
    {
        private readonly AuctionDbContext _context;

        public BidsController(AuctionDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ProvideBid([FromBody] Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auctionItem = await _context.AuctionItems.FindAsync(bid.ItemNumber);
            if (auctionItem != null)
            {
                auctionItem.BidCustomePhone = bid.CustomPhone;
                auctionItem.BidCustomName = bid.CustomName;
                auctionItem.BidPrice = bid.Price;
                auctionItem.BidTimeStamp = DateTime.Now;
            }

            _context.Bids.Add(bid);
            _context.AuctionItems.Add(auctionItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBids", new { id = bid.ItemNumber }, bid);
        }


        // GET: api/Bids
        [HttpGet]
        public IEnumerable<Bid> GetBids()
        {
            return _context.Bids;
        }

        // GET: api/Bids/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBid([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bid = await _context.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return Ok(bid);
        }

        // PUT: api/Bids/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBid([FromRoute] int id, [FromBody] Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bid.ItemNumber)
            {
                return BadRequest();
            }

            _context.Entry(bid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
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

        // POST: api/Bids
        [HttpPost]
        public async Task<IActionResult> PostBid([FromBody] Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bid.ItemNumber }, bid);
        }

        // DELETE: api/Bids/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return Ok(bid);
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.ItemNumber == id);
        }
    }
}