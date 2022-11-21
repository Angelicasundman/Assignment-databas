using Assignment.Context;
using Assignment.Models.Entities;
using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(OrderRequest req)
        {
            try
            {

                var orderEntity = new OrderEntity()
                {
                    TotalPrice = req.TotalPrice,
                    CustomerName = req.CustomerName,
                    CustomersId = req.CustomersId,
                    OrderDate = DateTime.Now,
                   
                };

                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();

                foreach (var product in req.Products)
                {
                    _context.OrderRows.Add(new OrderRowsEntity
                    {
                        OrderId = orderEntity.Id,
                        ProductId = product.Id
                    });
                }
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetAllOrderAsync()
        {
            var orderModel = new List<OrderModel>();

            try
            {
                foreach (var item in await _context.Orders.ToListAsync())
                    orderModel.Add(new OrderModel
                    {
                        Id = item.Id,
                        OrderDate = item.OrderDate,
                        TotalPrice = item.TotalPrice,
                        CustomerName = item.CustomerName,
                        CustomersId = item.CustomersId
                    });

                return orderModel;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return orderModel;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOneOrderAsync(int id)
        {
            try
            {
                var orderModel = await _context.Orders.FindAsync(id);
                if (orderModel == null)
                    return new NotFoundResult();

                return new OkObjectResult(new OrderModel
                {
                    Id = orderModel.Id,
                    OrderDate = orderModel.OrderDate,
                    TotalPrice = orderModel.TotalPrice,
                    CustomerName = orderModel.CustomerName,
                    CustomersId = orderModel.CustomersId
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderAsync(int id, OrderModel orderModel)
        {
            try
            {
                if (id != orderModel.Id)
                    return new BadRequestResult();

                var orderEntity = await _context.Orders.FindAsync(id);
                if (orderEntity != null)
                {
                    orderEntity.Id = orderModel.Id;
                    orderEntity.OrderDate = orderModel.OrderDate;
                    orderEntity.TotalPrice = orderModel.TotalPrice;
                    orderEntity.CustomerName = orderModel.CustomerName;
                    orderEntity.CustomersId = orderModel.CustomersId;
                    _context.Entry(orderEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return new OkResult();
                }
                return new NotFoundResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            try
            {
                var orderEntity = await _context.Orders.FindAsync(id);
                if (orderEntity == null)
                    return new NotFoundResult();
                _context.Orders.Remove(orderEntity);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}

