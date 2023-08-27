namespace CircuitBreaker.ApiModels
{
    public class GetQuantityResponse
    {
        public string WarehouseId { get; set; }
        public string ItemId { get; set; }
        public int ATP_Stock { get; set; }
        public string UOM { get; set; }
    }
}