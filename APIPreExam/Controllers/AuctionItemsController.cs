using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreExam.Data;
using APIPreExam.Models;
using System.Net;
using System.Net.Http;

namespace APIPreExam.Controllers
{
    [Produces("application/json")]
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
        [HttpGet("{itemnumber}")]
        public async Task<IActionResult> GetAuctionItem([FromRoute] int itemnumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auctionItem = await _context.AuctionItems.FindAsync(itemnumber);

            if (auctionItem == null)
            {
                return NotFound();
            }

            return Ok(auctionItem);
        }

        [HttpPut("{auctionItem.ItemNumber}")]
        public HttpResponseMessage ProvideBid(AuctionItem auctionItem)
        {
            if (auctionItem == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            auctionItem.BidTimeStamp = DateTime.Now;

            Bid bid = new Bid
            {
                ItemNumber = auctionItem.ItemNumber,
                CustomName = auctionItem.BidCustomName,
                CustomPhone = auctionItem.BidCustomePhone,
                Price = auctionItem.BidPrice
            };

            using (var db = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Bids ON");
                    if (!BidIsHigherThanPrevious(auctionItem) == true)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                    if (!BidExists(bid.ItemNumber))
                    {
                        _context.Bids.Add(bid);
                    }
                    if (BidIsHigherThanPrevious(auctionItem) == true)
                    {
                        _context.Entry(bid).State = EntityState.Modified;
                    }

                    _context.Entry(auctionItem).State = EntityState.Modified;
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Bids OFF");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!AuctionExists(auctionItem.ItemNumber))
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                db.Commit();
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private bool AuctionExists(int id)
        {
            return _context.AuctionItems.Any(e => e.ItemNumber == id);
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.ItemNumber == id);
        }

        private bool BidIsHigherThanPrevious(AuctionItem auctionItem)
        {
            var oldBid = _context.Bids.AsNoTracking().FirstOrDefault(c => c.ItemNumber == auctionItem.ItemNumber);
            if (oldBid.Price < auctionItem.BidPrice && auctionItem.RatingPrice < auctionItem.BidPrice)
            {
                return true;
            }
            return false;
        }

        //// PUT: api/AuctionItems/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAuctionItem([FromRoute] int id, [FromBody] AuctionItem auctionItem)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != auctionItem.ItemNumber)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(auctionItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuctionItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //        // POST: api/AuctionItems
        //        [HttpPost]
        //        public async Task<IActionResult> PostAuctionItem([FromBody] AuctionItem auctionItem)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            _context.AuctionItems.Add(auctionItem);
        //            await _context.SaveChangesAsync();

        //            return CreatedAtAction("GetAuctionItems", new { id = auctionItem.ItemNumber }, auctionItem);
        //        }

        //        [HttpPost]
        //        public async Task<IActionResult> ProvideBid([FromBody] Bid bid)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            var auctionItem = await _context.AuctionItems.FindAsync(bid.ItemNumber);
        //            if (auctionItem != null)
        //            {
        //                auctionItem.BidCustomePhone = bid.CustomPhone;
        //                auctionItem.BidCustomName = bid.CustomName;
        //                auctionItem.BidPrice = bid.Price;
        //                auctionItem.BidTimeStamp = DateTime.Now;
        //            }

        //            _context.Bids.Add(bid);
        //            _context.AuctionItems.Add(auctionItem);
        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException e)
        //            {
        //                BadRequest(e);
        //            }

        //            return CreatedAtAction("GetAuctionItems", new { id = auctionItem.ItemNumber }, auctionItem);
        //        }





        //        // DELETE: api/AuctionItems/5
        //        [HttpDelete("{id}")]
        //        public async Task<IActionResult> DeleteAuctionItem([FromRoute] int id)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            var auctionItem = await _context.AuctionItems.FindAsync(id);
        //            if (auctionItem == null)
        //            {
        //                return NotFound();
        //            }

        //            _context.AuctionItems.Remove(auctionItem);
        //            await _context.SaveChangesAsync();

        //            return Ok(auctionItem);
        //        }

        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItems.Any(e => e.ItemNumber == id);
        }
    }
}