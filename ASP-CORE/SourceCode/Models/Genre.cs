using System.ComponentModel.DataAnnotations;
namespace _31241021966_BuiCongDanh.Models;
public class Genre {
    [Key] public int ID {get;set;}
    [Required,StringLength(200),Display(Name="Tên thể loại")] public string Name {get;set;}
    public virtual IList<Movie> Movies {get;set;} = new List<Movie>();
}
