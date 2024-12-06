using System.ComponentModel.DataAnnotations;

namespace BaiTapLon.Models
{
    public class BookType
    {
        [Key]
        [Display(Name = "Mã thể loại")]
        public int BookTypeId { get; set; }
        [Display(Name = "Tên thể loại")]
        public string BookTypeName { get; set; }
        public ICollection<Books>? Books { get; set; }
    }
}