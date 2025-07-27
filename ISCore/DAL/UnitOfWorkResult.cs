namespace ISCore.DAL
{
    public class UnitOfWorkResult
    {
        public bool IsCompleted { get; set; }
        public string Message { get; set; }

        public object innerObject{ get; set; }
    }
}
