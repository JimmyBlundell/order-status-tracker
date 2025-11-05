using OrderTrackerAPI.Models;

namespace OrderTrackerAPI.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}