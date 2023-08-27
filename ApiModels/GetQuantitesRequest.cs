namespace CircuitBreaker.ApiModels
{
    public class GetQuantityRequest
    {
        public List<string> Items { get; set; }

        public GetQuantityRequest()
        {
            this.Items = new List<string>();
        }
    }
}