namespace EgyByte.Models
{
    public class BasePage<T> where T : class
    {
        public T Item { get; set; }
        public List<T> Items { get; set; }

        public int Pageindex { get; set; } = 1;
        public int Pagesize { get; set; } = 50;
        public int TotalCount { get; set; } = 0;
        public string? SearchText { get; set; }
        public Guid? SelectedId { get; set; }
        public string? ErrorMessage { get; set; }
        public int LangId { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
