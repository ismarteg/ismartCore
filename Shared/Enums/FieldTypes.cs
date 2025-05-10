
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Shared.Enums
{
    public enum FieldTypes
    {
        [Description("نص قصير")]
        [Display(Name ="نص قصير")]
        Text = 1,
        [Description("اختيارات متعدده")]
        [Display(Name = "اختيارات متعدده")]
        CheckBox = 2,
        [Description("اختيار واحد")]
        [Display(Name = "اختيار واحد")]
        RadioButton = 3,

        [Description("قائمة منسدلة")]
        [Display(Name = "قائمة منسدلة")]
        ItemChoice = 4,

        [Description("رقم")]
        [Display(Name = "رقم")]
        Number = 5,
        [Description("تاريخ")]
        [Display(Name = "تاريخ")]
        Date = 6,
        [Description("وقت")]
        [Display(Name = "وقت")]
        Time = 7,
        [Description("نص كبير")]
        [Display(Name = "نص كبير")]
        TextArea = 8,
       
        [Description("ملف")]
        [Display(Name = "ملف")]
        FileUpload = 9,
    }
}
