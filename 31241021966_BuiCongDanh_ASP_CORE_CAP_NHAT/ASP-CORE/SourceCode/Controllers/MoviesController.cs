using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _31241021966_BuiCongDanh.Data;
using _31241021966_BuiCongDanh.Models;
using _31241021966_BuiCongDanh.Extension;
using System.Text;
using System.Net;
namespace _31241021966_BuiCongDanh.Controllers;
public class MoviesController(ApplicationDbContext db,IWebHostEnvironment env,IConfiguration config) : Controller {
    private readonly ApplicationDbContext _context=db;
    private const string Key="HoaDon";
    private HoaDon Cart()=>HttpContext.Session.Get<HoaDon>(Key);
    private void Store(HoaDon cart){cart.TongTien=cart.ChiTietHoaDons.Sum(x=>x.SoLuong*x.DonGia);HttpContext.Session.Set(Key,cart);}
    public async Task<IActionResult> Index(string q,int? genre) {
        var query=_context.Movies.Include(x=>x.GenreObj).AsQueryable();
        if(!string.IsNullOrWhiteSpace(q)) query=query.Where(x=>x.Title.Contains(q));
        if(genre.HasValue) query=query.Where(x=>x.GenreID==genre);
        ViewBag.Genres=new SelectList(await _context.Genres.OrderBy(x=>x.Name).ToListAsync(),"ID","Name",genre); ViewBag.Q=q;
        return View(await query.OrderByDescending(x=>x.ID).ToListAsync());
    }
    [Authorize] public async Task<IActionResult> Manage()=>View(await _context.Movies.Include(x=>x.GenreObj).ToListAsync());
    public async Task<IActionResult> Details(int id){var m=await _context.Movies.Include(x=>x.GenreObj).FirstOrDefaultAsync(x=>x.ID==id);return m==null?NotFound():View(m);}
    private async Task Genres(int? id=null)=>ViewData["GenreID"]=new SelectList(await _context.Genres.OrderBy(x=>x.Name).ToListAsync(),"ID","Name",id);
    [Authorize] public async Task<IActionResult> Create(){await Genres();return View(new Movie{ReleaseDate=DateTime.Today.AddDays(30),Price=59000,Rated=4});}
    private async Task<string> SaveImageAsync(IFormFile file){
        if(file==null || file.Length==0)return null;
        var ext=Path.GetExtension(file.FileName).ToLowerInvariant();
        if(file.Length>5*1024*1024 || !new[]{".jpg",".jpeg",".png",".gif",".webp"}.Contains(ext)){ModelState.AddModelError("PictureUpload","Ảnh JPG/PNG/GIF/WEBP, tối đa 5 MB.");return null;}
        using var input=file.OpenReadStream(); var head=new byte[12]; var read=await input.ReadAtLeastAsync(head,12,throwIfEndOfStream:false);
        bool valid=read>=12 && (ext is ".jpg" or ".jpeg" ? head[0]==255&&head[1]==216&&head[2]==255 : ext==".png" ? head.AsSpan(0,8).SequenceEqual(new byte[]{137,80,78,71,13,10,26,10}) : ext==".gif" ? Encoding.ASCII.GetString(head,0,3)=="GIF" : Encoding.ASCII.GetString(head,0,4)=="RIFF"&&Encoding.ASCII.GetString(head,8,4)=="WEBP");
        if(!valid){ModelState.AddModelError("PictureUpload","Nội dung tệp không đúng định dạng ảnh.");return null;}
        var folder=Path.Combine(env.WebRootPath,"images");Directory.CreateDirectory(folder);var name=Guid.NewGuid().ToString("N")+ext;
        using var output=System.IO.File.Create(Path.Combine(folder,name));await file.CopyToAsync(output);return "/images/"+name;
    }
    [Authorize,HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create([Bind("Title,Summary,ReleaseDate,Genre,GenreID,Price,Rated,PictureUpload")] Movie movie){
        if(ModelState.IsValid){movie.PicturePath=await SaveImageAsync(movie.PictureUpload)??"/images/no-image.jpg";if(ModelState.IsValid){_context.Add(movie);await _context.SaveChangesAsync();return RedirectToAction(nameof(Manage));}}
        await Genres(movie.GenreID);return View(movie);
    }
    [Authorize] public async Task<IActionResult> Edit(int id){var m=await _context.Movies.FindAsync(id);if(m==null)return NotFound();await Genres(m.GenreID);return View(m);}
    [Authorize,HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,[Bind("ID,Title,Summary,ReleaseDate,Genre,GenreID,Price,Rated,PictureUpload")] Movie movie){
        if(id!=movie.ID)return NotFound();var old=await _context.Movies.FindAsync(id);if(old==null)return NotFound();movie.PicturePath=old.PicturePath;
        if(ModelState.IsValid){var path=await SaveImageAsync(movie.PictureUpload);if(ModelState.IsValid){old.Title=movie.Title;old.Summary=movie.Summary;old.ReleaseDate=movie.ReleaseDate;old.Genre=movie.Genre;old.GenreID=movie.GenreID;old.Price=movie.Price;old.Rated=movie.Rated;old.PicturePath=path??old.PicturePath??"/images/no-image.jpg";await _context.SaveChangesAsync();return RedirectToAction(nameof(Manage));}}
        await Genres(movie.GenreID);return View(movie);
    }
    [Authorize] public async Task<IActionResult> Delete(int id){var m=await _context.Movies.Include(x=>x.GenreObj).FirstOrDefaultAsync(x=>x.ID==id);return m==null?NotFound():View(m);}
    [Authorize,HttpPost,ActionName("Delete"),ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var m=await _context.Movies.FindAsync(id);if(m==null)return NotFound();if(await _context.ChiTietHoaDons.AnyAsync(x=>x.MaMovie==id)){TempData["Message"]="Không thể xóa phim đã có trong đơn hàng.";return RedirectToAction(nameof(Manage));}_context.Remove(m);await _context.SaveChangesAsync();return RedirectToAction(nameof(Manage));}
    public IActionResult CartView()=>View("AddToCart",Cart());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> AddToCart(int id){var m=await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x=>x.ID==id);if(m==null)return NotFound();var c=Cart();var line=c.ChiTietHoaDons.FirstOrDefault(x=>x.MaMovie==id);if(line==null)c.ChiTietHoaDons.Add(new ChiTietHoaDon{MaMovie=id,MovieObj=m,SoLuong=1,DonGia=m.Price});else line.SoLuong=Math.Min(99,line.SoLuong+1);Store(c);return RedirectToAction(nameof(CartView));}
    [HttpPost,ValidateAntiForgeryToken] public IActionResult UpdateCart(int id,int quantity){var c=Cart();var line=c.ChiTietHoaDons.FirstOrDefault(x=>x.MaMovie==id);if(line!=null)line.SoLuong=Math.Clamp(quantity,1,99);Store(c);return RedirectToAction(nameof(CartView));}
    [HttpPost,ValidateAntiForgeryToken] public IActionResult RemoveFromCart(int maMovies){var c=Cart();var line=c.ChiTietHoaDons.FirstOrDefault(x=>x.MaMovie==maMovies);if(line!=null)c.ChiTietHoaDons.Remove(line);Store(c);return RedirectToAction(nameof(CartView));}
    public IActionResult Checkout(){if(Cart().ChiTietHoaDons.Count==0)return RedirectToAction(nameof(CartView));return View(new ShippingDetail{ReleaseDate=DateTime.Today.AddDays(3)});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Checkout([Bind("Name,Email,Address,Mobile,ReleaseDate")] ShippingDetail detail){
        var c=Cart();if(c.ChiTietHoaDons.Count==0)ModelState.AddModelError("","Giỏ hàng trống.");if(detail.ReleaseDate.Date<DateTime.Today)ModelState.AddModelError("ReleaseDate","Ngày giao không được ở quá khứ.");
        if(!ModelState.IsValid)return View(detail);
        var order=new HoaDon{NgayLap=DateTime.Now,ShippingDetail=detail};var body=new StringBuilder("<h2>CineBox - Xác nhận đơn hàng</h2><ul>");
        foreach(var line in c.ChiTietHoaDons){var m=await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x=>x.ID==line.MaMovie);if(m==null){ModelState.AddModelError("","Phim không còn tồn tại. Hãy xóa khỏi giỏ hàng.");return View(detail);}if(m.Price!=line.DonGia){line.DonGia=m.Price;line.MovieObj=m;Store(c);ModelState.AddModelError("","Giá đã thay đổi. Vui lòng kiểm tra lại giỏ hàng.");return View(detail);}order.ChiTietHoaDons.Add(new ChiTietHoaDon{MaMovie=m.ID,SoLuong=line.SoLuong,DonGia=m.Price});order.TongTien+=m.Price*line.SoLuong;body.AppendLine($"<li>{line.SoLuong} x {WebUtility.HtmlEncode(m.Title)}: {m.Price*line.SoLuong:N0} đ</li>");}
        _context.HoaDons.Add(order);await _context.SaveChangesAsync();HttpContext.Session.Remove(Key);
        body.Append($"</ul><p>Đơn #{order.MaHoaDon} – Tổng: {order.TongTien:N0} đ</p><p>{WebUtility.HtmlEncode(detail.Name)}<br>{WebUtility.HtmlEncode(detail.Address)}<br>{WebUtility.HtmlEncode(detail.Mobile)}</p>");
        var gmail=config["Email:Address"];ViewBag.EmailSent=await EmailServiceNew.SendMailGoogleSmtp(gmail,detail.Email,"CineBox - Xác nhận đơn hàng",body.ToString(),gmail,config["Email:AppPassword"]);return View("CheckoutCompleted",order);
    }
}
