using Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblFileProperty), Schema = "Clinic")]
    public class tblFileProperty:GuidBaseEntity
    {
        [MaxLength(200)]
        public string Title{ get; set; }


        /// <summary>
        /// DisplayOrder will be aplayed on section and if now section will be aplayed on file
        /// </summary>
        public int DisplayOrder { get; set; }

        public FieldTypes FieldType { get; set; }

        /// <summary>
        /// Coma Seprated Data
        /// </summary>
        public string? PreSelectedData { get; set; }

        /// <summary>
        /// to Set it in Ui as bootstrab column from 1 to 12
        /// Defult is 6 to make to item in row in md view
        /// </summary>
        public int ColumnWidth { get; set; } = 6;


        [ForeignKey(nameof(tblFiles))]
        public Guid FileId { get; set; }

        [ForeignKey(nameof(tblFilesGroup))]
        public Guid FilePropertiesSectionId { get; set; }
        public tblFilesGroup FilePropertiesSection { get; set; }
    }
}
