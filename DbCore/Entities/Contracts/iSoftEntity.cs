using DbCore.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Contracts
{
    public interface iSoftEntity<Tkey>
    {
        public Tkey Id { get; set; }
        public bool IsDeleted { get; set; }


        public string? CreatorId { get; set; }
        public AppUser? Creator { get; set; }

        public string? LastEditorId { get; set; }
        public AppUser? LastEditor { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }
    }
}
