using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblServicesCashTransactions), Schema = "Clinic")]
    public class tblServicesCashTransactions:BaseEntity
    {
		[ForeignKey(nameof(tblServicesReservation))]
		public int ServicesReservationId { get; set; }

		public DateTime TransactionDate {  get; set; }=DateTime.Now;
        public decimal Ammount { get; set; } = 0;
        public bool IsTransactionIn { get; set; } = true;
        public string TransactionDescription { get; set; } = "";

        //[ForeignKey(nameof(tblServicesCashTransactions))]
        //public Guid VersionParentId { get; set; }

        //public virtual List<tblServicesCashTransactions> Versions {  get; set; }
        
	}
}
