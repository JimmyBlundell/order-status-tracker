using OrderTrackerAPI.Models;

namespace OrderTrackerAPI.Services
{
    public class OrderService
    {
        private readonly List<Order> _orders;
        private readonly List<OrderStatusHistory> _orderHistory;

        public OrderService()
        {
            _orders = new List<Order>
    {
        new Order
        {
            Id = 1,
            OrderNumber = "ORD-10001",
            CustomerId = "CUST-001",
            Status = OrderStatus.Delivered,
            OrderDate = DateTime.Now.AddDays(-10),
            TotalAmount = 299.99m,
            Description = "The four pillars: Encapsulation, Inheritance, Polymorphism, Abstraction"
        },
        new Order
        {
            Id = 2,
            OrderNumber = "ORD-10002",
            CustomerId = "CUST-002",
            Status = OrderStatus.Shipped,
            OrderDate = DateTime.Now.AddDays(-3),
            TotalAmount = 149.50m,
            Description = "Express shipping requested"
        },
        new Order
        {
            Id = 3,
            OrderNumber = "ORD-10003",
            CustomerId = "CUST-001",
            Status = OrderStatus.Processing,
            OrderDate = DateTime.Now.AddDays(-1),
            TotalAmount = 89.99m,
            Description = "Gift wrap included"
        },
        new Order
        {
            Id = 4,
            OrderNumber = "ORD-10004",
            CustomerId = "CUST-003",
            Status = OrderStatus.Pending,
            OrderDate = DateTime.Now,
            TotalAmount = 450.00m,
            Description = "Awaiting payment confirmation"
        }
    };

            // Initialize order status history
            _orderHistory = new List<OrderStatusHistory>
    {
        // Order 1 (ORD-10001) - Delivered
        new OrderStatusHistory { Id = 1, OrderId = 1, Status = OrderStatus.Pending, Timestamp = DateTime.Now.AddDays(-10), Notes = "Order received" },
        new OrderStatusHistory { Id = 2, OrderId = 1, Status = OrderStatus.Processing, Timestamp = DateTime.Now.AddDays(-9), Notes = "Payment confirmed, preparing shipment" },
        new OrderStatusHistory { Id = 3, OrderId = 1, Status = OrderStatus.Shipped, Timestamp = DateTime.Now.AddDays(-8), Notes = "Package shipped via UPS" },
        new OrderStatusHistory { Id = 4, OrderId = 1, Status = OrderStatus.Delivered, Timestamp = DateTime.Now.AddDays(-7), Notes = "Delivered to front door" },

        // Order 2 (ORD-10002) - Shipped
        new OrderStatusHistory { Id = 5, OrderId = 2, Status = OrderStatus.Pending, Timestamp = DateTime.Now.AddDays(-3), Notes = "Order received" },
        new OrderStatusHistory { Id = 6, OrderId = 2, Status = OrderStatus.Processing, Timestamp = DateTime.Now.AddDays(-2), Notes = "Processing express order" },
        new OrderStatusHistory { Id = 7, OrderId = 2, Status = OrderStatus.Shipped, Timestamp = DateTime.Now.AddDays(-1), Notes = "Out for delivery" },

        // Order 3 (ORD-10003) - Processing
        new OrderStatusHistory { Id = 8, OrderId = 3, Status = OrderStatus.Pending, Timestamp = DateTime.Now.AddDays(-1), Notes = "Order received" },
        new OrderStatusHistory { Id = 9, OrderId = 3, Status = OrderStatus.Processing, Timestamp = DateTime.Now.AddHours(-12), Notes = "Gift wrapping in progress" },

        // Order 4 (ORD-10004) - Pending
        new OrderStatusHistory { Id = 10, OrderId = 4, Status = OrderStatus.Pending, Timestamp = DateTime.Now, Notes = "Awaiting payment" }
    };
        }

        public Order GetOrderByOrderNumber(string orderNumber)
        {
            return _orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }

        public List<OrderStatusHistory> GetOrderHistory(int orderId)
        {
            return _orderHistory.Where(h => h.OrderId == orderId)
            .OrderBy(h => h.Timestamp)
            .ToList();
        }

        public void AdvanceOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                order.Status = newStatus;

                // Add history entry
                var historyEntry = new OrderStatusHistory
                {
                    Id = _orderHistory.Count + 1,
                    OrderId = orderId,
                    Status = newStatus,
                    Timestamp = DateTime.Now,
                    Notes = $"Status updated to {newStatus}"
                };
                _orderHistory.Add(historyEntry);
            }
        }
    }
}