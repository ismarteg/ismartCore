using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Services
{
    public class tbServicesFields:BaseEntity
    {
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public FieldTypes DataType { get; set; }= FieldTypes.Text;
        public int DisplayOrder { get; set; } = 0;
        public bool IsRequired { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        public int ColumnWidth { get; set; } = 12;

    }
}
