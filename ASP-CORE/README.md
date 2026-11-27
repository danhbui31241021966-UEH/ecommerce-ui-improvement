# CineBox – Bài thực hành ASP.NET Core MVC

**Bùi Công Danh – 31241021966** | Phát triển ứng dụng TMĐT | UEH

CineBox là cửa hàng phim giả định, có 6 mẫu phim tự thiết kế thuộc 3 thể loại. Dữ liệu mẫu nằm trong `DU-LIEU-MAU.md`, nhập qua giao diện để có minh chứng thật. Đây là dự án học tập, không thu tiền trực tuyến.

## 1. Công nghệ và chức năng
- .NET 10, ASP.NET Core MVC, Razor, EF Core 10 + SQL Server, ASP.NET Core Identity, Session, Newtonsoft.Json, Gmail SMTP.
- CRUD phim/thể loại, đăng ký/đăng nhập, tìm kiếm/lọc, upload JPG/PNG/GIF/WEBP tối đa 5 MB, ảnh mặc định và giữ ảnh cũ khi sửa.
- Thêm/cập nhật/xóa giỏ hàng, tính tổng tiền; Checkout kiểm tra dữ liệu, lưu hóa đơn, chi tiết, thông tin giao hàng; xác nhận qua Gmail nếu cấu hình thành công.
- Giao diện responsive; quản lý yêu cầu đăng nhập (bản học tập: mọi tài khoản đăng nhập đều có quyền quản lý; không dùng dữ liệu thật, cần phân quyền Admin khi dùng thực tế).

## 2. Cấu trúc
```
ASP-CORE/
  README.md
  DU-LIEU-MAU.md
  SourceCode/
    31241021966_BuiCongDanh.csproj
    Program.cs
    Models/ Controllers/ Views/ Data/ Validation/ Extension/
    wwwroot/css/ wwwroot/images/
    Properties/launchSettings.json
    appsettings.json, appsettings.example.json
  Report/
    BaiTap_CaNhan_ASP_CORE_LMS.docx
    images/
```
Namespace C# có dấu `_` ở đầu vì định danh không được bắt đầu bằng số. Source đã viết sẵn controller/view, không chạy scaffold đè lên. Migration cần tạo thực tế trên máy bạn rồi commit mã migration (không commit secrets). Chưa kiểm thử build trong môi trường tạo gói vì không có SDK .NET và mạng NuGet/Somee.

## 3. Chạy trên Windows / VS Code
1. Cài VS Code, C# Dev Kit, [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), SSMS. Giải nén rồi mở `ASP-CORE/SourceCode` bằng VS Code. Mở Terminal PowerShell. Chạy:
```powershell
dotnet --version
dotnet --list-sdks
dotnet restore
dotnet build
dotnet dev-certs https --trust
```
Nếu SDK mặc định là 11, dùng `dotnet new globaljson --sdk-version 10.0.100 --roll-forward latestFeature` (cần có SDK 10 phù hợp).

2. Vào https://somee.com → đăng ký Free .NET Hosting → Managed products → MS SQL → Databases → CREATE → tạo DB, ví dụ `PTUDW_31241021966` nếu tên còn trống. Copy **Connection string** từ Connection details; thêm `;TrustServerCertificate=True` nếu chưa có. Không gửi mật khẩu cho người khác. Dùng User Secrets thay vì lưu mật khẩu trong Git:
```powershell
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' 'DAN_CHUOI_KET_NOI_SOMEE_VAO_DAY;TrustServerCertificate=True'
```
Thay toàn bộ chuỗi giả trên bằng chuỗi thật; không lặp thuộc tính nếu đã có. Nếu muốn chạy SQL Server LocalDB trên Windows trước khi có Somee (cần cài LocalDB):
```powershell
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' 'Server=(localdb)\MSSQLLocalDB;Database=CineBox31241021966;Trusted_Connection=True;TrustServerCertificate=True'
```
Chuyển lại Somee và chạy database update để đáp ứng yêu cầu hosting; dữ liệu LocalDB không tự chuyển sang Somee.

3. Tạo migration và database. PowerShell, từ thư mục SourceCode:
```powershell
dotnet tool install --global dotnet-ef --version '10.0.*'
dotnet ef --version
$env:ASPNETCORE_ENVIRONMENT = 'Development'
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
dotnet ef database update
```
Nếu tool đã cài, dùng `dotnet tool update --global dotnet-ef --version '10.0.*'`. Nếu đã có migration, không tạo InitialCreate lần nữa. Mã đã tổng hợp các bước của đề thành một migration, tạo cả bảng Identity và 5 bảng nghiệp vụ. Với ảnh minh chứng theo từng lần tạo migration/scaffold ở đề, cần thực hành lại theo từng giai đoạn trong PDF trên một dự án/DB thử riêng; không dùng ảnh giả. Có thể báo cáo sự khác biệt cách triển khai nếu được giảng viên chấp nhận.

4. Chạy:
```powershell
dotnet run --launch-profile https
```
Mở URL `https://localhost:7186` (hoặc đúng URL Now listening on). Đăng ký email thử, mật khẩu đủ hoa/thường/số/ký tự đặc biệt. Trang Register confirmation có link xác nhận thử nếu Identity hiển thị. Đăng nhập rồi mở `/Genres` để tạo 3 thể loại. Sau đó vào `/Movies/Manage` → Thêm phim, nhập 6 phim theo file dữ liệu mẫu. Ảnh để sẵn trong `wwwroot/images`, chọn đúng JPG khi upload. Mã thể loại và dropdown phải trùng ID thực tế. Đặt ngày tương lai nếu ngày mẫu đã qua. Mở `/Movies` để thấy danh sách, thử xem/sửa/xóa (tạo một phim thử riêng để xóa), upload và giữ ảnh khi sửa, mua 2 phim, tăng số lượng, xóa và checkout. Không xóa phim đã có đơn hoặc thể loại đang có phim.

5. Cấu hình Gmail (tùy khả năng tài khoản): bật xác minh 2 bước, vào https://myaccount.google.com/apppasswords tạo mật khẩu ứng dụng rồi chạy:
```powershell
dotnet user-secrets set 'Email:Address' 'EMAIL_CUA_BAN'
dotnet user-secrets set 'Email:AppPassword' 'APP_PASSWORD_CUA_BAN'
```
Checkout dùng email do bạn sở hữu, thông tin địa chỉ và số điện thoại giả định. Nếu Gmail thiếu cấu hình/SMTP bị chặn, đơn vẫn lưu; trang Thanks báo chưa gửi được mail. Kiểm tra SSMS bằng login Somee, Trust server certificate, mở dbo.AspNetUsers, dbo.Movies, dbo.Genres, dbo.HoaDons, dbo.ChiTietHoaDons, dbo.ShippingDetails. Che email/mật khẩu/thông tin nhạy cảm khi chụp ảnh.

## 4. Publish lên Somee
```powershell
dotnet publish -c Release -o ./publish
```
User Secrets **không** được publish. Cấu hình connection string và email qua biến môi trường `ConnectionStrings__DefaultConnection`, `Email__Address`, `Email__AppPassword` trên hosting nếu có hỗ trợ. Nếu Somee không có giao diện cấu hình biến môi trường, chỉ sửa `publish/appsettings.json` trên máy bằng thông tin thật trước khi nén và tải riêng lên hosting của bạn, KHÔNG commit file đó. Không dùng HTTP để gửi dữ liệu thật. Nén các file bên trong publish, không nén thư mục bọc ngoài:
```powershell
Compress-Archive -Path ./publish/* -DestinationPath ./publish.zip -Force
```
Somee → Websites → CREATE → chọn tên còn trống, ví dụ `ueh31241021966` (chưa kiểm tra khả dụng) → File manager → xóa default.asp → chọn publish.zip → Overwrite existing files → Upload and Unzip archives. Kiểm tra web.config nằm ở gốc, thư mục wwwroot/images có quyền ghi nếu upload trên hosting. Nếu nhập phim trên local trước publish thì các ảnh upload phải nằm trong bản publish để hiển thị trên host. Mở URL thật của hosting, kiểm tra danh sách, đăng nhập, đặt hàng thử. Nếu lỗi 500.31/500.32, kiểm tra hỗ trợ .NET 10; có thể publish self-contained `dotnet publish -c Release -r win-x64 --self-contained true -o ./publish` (kiểm tra hạn mức). Nếu lỗi 500.30 xem stdout log rồi tắt ghi log sau khi xử lý. Nếu chưa deploy được, ghi lý do thật trong báo cáo.

## 5. GitHub và LMS
- Mở repository cũ của bạn trên GitHub; không tạo repo mới, không xóa bài cũ. Clone nếu chưa có bản local, copy nguyên thư mục ASP-CORE vào gốc repo cũ. Nếu đã có ASP-CORE thì hợp nhất cẩn thận.
- Chạy từ gốc repository (sau khi thêm báo cáo và ảnh thật):
```powershell
git status
git add ASP-CORE
git diff --cached --stat
git diff --cached
git commit -m 'Add ASP.NET Core MVC CineBox assignment'
git push
```
- Kiểm tra không có mật khẩu, chuỗi kết nối thật, app password, dữ liệu cá nhân hoặc publish/bin/obj. Repo Public theo đề. Lấy URL thư mục ASP-CORE đúng nhánh. Điền URL repo, lớp học phần, URL hosting thật vào báo cáo. Chèn ảnh chụp thật vào các vị trí và lưu tại Report/images.
- Nộp LMS: `BaiTap_CaNhan_ASP_CORE_LMS.docx` + link GitHub + link website hosting nếu thành công.

## 6. Kiểm thử cần thực hiện trước nộp
- `dotnet build`, `dotnet ef database update`, mở web local; kiểm tra 3 thể loại, 6 phim.
- Kiểm tra tiêu đề <3 ký tự, giá <5000, điểm >5, ngày cũ, sai mã thể loại, ảnh sai định dạng, giữ ảnh khi edit.
- Mua 2 sản phẩm; đổi số lượng, đối chiếu tổng; xóa; kiểm tra giỏ hàng trống; thử checkout email sai và ngày giao cũ; checkout đúng; kiểm tra ba bảng hóa đơn/giao hàng; kiểm tra email.
- Kiểm tra hiển thị điện thoại DevTools và website triển khai, chụp minh chứng. Không tuyên bố thành công nếu chưa thực hiện.
