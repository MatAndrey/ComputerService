namespace ComputerService.Areas.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanDelete { get; set; }
        public string LangCode { get; set; }
    }
}
