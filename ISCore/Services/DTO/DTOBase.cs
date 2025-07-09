using ISCore.Services.DTO.Users;



namespace ISCore.Services.DTO
{
    public class DTOBase<tkey>
    {
        public tkey Id { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatorId { get; set; }
        public DtoUser? Creator { get; set; }
        public string? LastEditorId { get; set; }
        public DtoUser? LastEditor { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastEditDate { get; set; } = DateTime.Now;
    }
}
