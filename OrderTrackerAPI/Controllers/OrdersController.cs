using Microsoft.AspNetCore.Mvc;
using OrderTrackerAPI.Models;
using OrderTrackerAPI.Services;

namespace OrderTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order/{orderId}
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrder(string orderId)
        {
            // TODO: Add [Authorize] and validate user owns this order
            // Would check: order.CustomerId == User.Identity.UserId

            var order = _orderService.GetOrderByOrderNumber(orderId);

            if (order == null)
            {
                return NotFound(new { message = $"Order {orderId} not found" });
            }

            return Ok(order);
        }

        // GET: api/orders/{orderId}/history
        [HttpGet("{orderId}/history")]
        public ActionResult<List<OrderStatusHistory>> GetOrderHistory(string orderId)
        {
            // TODO: Add [Authorize] and validate user owns this order

            var order = _orderService.GetOrderByOrderNumber(orderId);

            if (order == null)
            {
                return NotFound(new { message = $"Order {orderId} not found" });
            }

            var history = _orderService.GetOrderHistory(order.Id);
            return Ok(history);
        }

        // POST: api/order/{orderId}/advance-status
        [HttpPost("{orderId}/advance-status")]
        public ActionResult<Order> AdvanceOrderStatus(string orderId)
        {
            var order = _orderService.GetOrderByOrderNumber(orderId);

            if (order == null)
            {
                return NotFound(new { message = $"Order {orderId} not found" });
            }

            // Advance status w/ valdation to prevent invalid transitions / skipping
            var newASAtatus = order.Status switch
            {
                OrderStatus.Pending => OrderStatus.Processing,
                OrderStatus.Processing => OrderStatus.Shipped,
                OrderStatus.Shipped => OrderStatus.Delivered,
                OrderStatus.Delivered => OrderStatus.Delivered, // Already delivered
                OrderStatus.Cancelled => OrderStatus.Cancelled, // Can't advance cancelled orders
                _ => order.Status
            };

            if (newASAtatus == order.Status)
            {
                return BadRequest(new { message = $"Order is already {order.Status} and cannot be advanced further." });
            }

            _orderService.AdvanceOrderStatus(order.Id, newASAtatus);

            return Ok(order);
        }
    }
}
