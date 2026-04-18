namespace ComputerService.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string LangCode { get; set; }
        public IEnumerable<string> ImageUrls { get; set; }
        public bool Visible { get; set; }
    }
}
