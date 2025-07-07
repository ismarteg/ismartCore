using ISCore.DTO.Users;



namespace ISCore.DTO
{
    public class DTOBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatorId { get; set; }
        public DtoUser? Creator { get; set; }
        public string? LastEditorId { get; set; }
        public DtoUser? LastEditor { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastEditDate { get; set; } = DateTime.Now;
    }
}
