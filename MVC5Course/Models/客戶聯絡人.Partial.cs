namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {//執行至此時，public partial class 客戶資料MetaData屬性均已驗證完畢，整個模型的驗證

            var db = new 客戶資料Entities();

            if (this.Id == 0)//新增前Id欄位不會有值
            {
                //Create
                if (db.客戶聯絡人.Where(m => m.客戶Id == this.客戶Id && m.Email == this.Email).Any())
                {
                    yield return new ValidationResult("此Email已被使用!",new string[] {"Email" });
                }
            }
            else
            {
                //Edit
                if (db.客戶聯絡人.Where(m => m.客戶Id == this.客戶Id && m.Id != this.Id && m.Email == this.Email).Any())
                {
                    yield return new ValidationResult("此Email已被使用!", new string[] { "Email" });
                }
            }
            yield return ValidationResult.Success;
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
