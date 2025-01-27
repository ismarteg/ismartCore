using DbCore.Entities.Clients;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblClinicData), Schema = "Clinic")]
    public class tblClinicData:GuidBaseEntity
    {
        public string Tilte { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public string Phones { get; set; }
        public string Emial { get; set; }
        public bool IsMain { get; set; }
    }
}
