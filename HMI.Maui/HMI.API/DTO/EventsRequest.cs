namespace HMI.API.DTO
{
    public class EventsRequest
    {
        public EventFilters? Filters { get; set; }
        public Order? Order { get; set; }
        public Pager? Pager { get; set; }
    }

    public class EventFilters
    {
        public string? User { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Allarm { get; set; }
    }

    public class Order
    {
        public string? Property { get; set; }
        public string? Direction { get; set; }
    }

    public class Pager
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
