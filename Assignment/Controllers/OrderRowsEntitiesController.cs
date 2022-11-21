using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Context;
using Assignment.Models.Entities;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderRowsEntitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderRowsEntitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrderRowsEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRowsEntity>>> GetOrderRows()
        {
          if (_context.OrderRows == null)
          {
              return NotFound();
          }
            return await _context.OrderRows.ToListAsync();
        }

        // GET: api/OrderRowsEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRowsEntity>> GetOrderRowsEntity(int id)
        {
          if (_context.OrderRows == null)
          {
              return NotFound();
          }
            var orderRowsEntity = await _context.OrderRows.FindAsync(id);

            if (orderRowsEntity == null)
            {
                return NotFound();
            }

            return orderRowsEntity;
        }

        // PUT: api/OrderRowsEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderRowsEntity(int id, OrderRowsEntity orderRowsEntity)
        {
            if (id != orderRowsEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderRowsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRowsEntityExists(id))
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

        // POST: api/OrderRowsEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderRowsEntity>> PostOrderRowsEntity(OrderRowsEntity orderRowsEntity)
        {
          if (_context.OrderRows == null)
          {
              return Problem("Entity set 'DataContext.OrderRows'  is null.");
          }
            _context.OrderRows.Add(orderRowsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderRowsEntity", new { id = orderRowsEntity.Id }, orderRowsEntity);
        }

        // DELETE: api/OrderRowsEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderRowsEntity(int id)
        {
            if (_context.OrderRows == null)
            {
                return NotFound();
            }
            var orderRowsEntity = await _context.OrderRows.FindAsync(id);
            if (orderRowsEntity == null)
            {
                return NotFound();
            }

            _context.OrderRows.Remove(orderRowsEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderRowsEntityExists(int id)
        {
            return (_context.OrderRows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
