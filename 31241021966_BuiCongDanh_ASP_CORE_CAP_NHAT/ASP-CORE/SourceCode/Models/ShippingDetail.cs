using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _31241021966_BuiCongDanh.Models;
public class ShippingDetail {
    public int ID {get;set;}
    [Required,Display(Name="Họ và tên"),StringLength(150)] public string Name {get;set;}
    [Required,Display(Name="Địa chỉ"),StringLength(500)] public string Address {get;set;}
    [Required,EmailAddress,StringLength(254)] public string Email {get;set;}
    [Required,Phone,StringLength(30),Display(Name="Số điện thoại")] public string Mobile {get;set;}
    [DataType(DataType.Date),Display(Name="Ngày giao")] public DateTime ReleaseDate {get;set;}
    [ForeignKey(nameof(HoaDonObj))] public int MaHoaDon {get;set;}
    public HoaDon HoaDonObj {get;set;}
}
