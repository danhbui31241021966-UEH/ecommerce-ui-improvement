using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _31241021966_BuiCongDanh.Validation;
namespace _31241021966_BuiCongDanh.Models;
public class Movie {
    public int ID {get;set;}
    [Display(Name="Tiêu đề"),Required,StringLength(200,MinimumLength=3)] public string Title {get;set;}
    [Display(Name="Tóm tắt"),StringLength(2000,MinimumLength=3)] public string Summary {get;set;}
    [Display(Name="Ngày phát hành"),DataType(DataType.Date),DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true),CheckDateGreaterThanToday] public DateTime ReleaseDate {get;set;}
    [Display(Name="Mã thể loại"),StringLength(10),GenreAttribute] public string Genre {get;set;}
    [Display(Name="Thể loại"),ForeignKey(nameof(GenreObj)),Required] public int? GenreID {get;set;}
    public virtual Genre GenreObj {get;set;}
    [Display(Name="Giá"),Range(5000,1000000000),Column(TypeName="decimal(18,2)")] public decimal Price {get;set;}
    [Display(Name="Xếp hạng"),Range(1,5)] public double Rated {get;set;}
    public string PicturePath {get;set;}
    [NotMapped,Display(Name="Hình ảnh")] public IFormFile PictureUpload {get;set;}
}
