# AI Usage Log - Nhật Ký Sử Dụng AI

## 1. Công Cụ AI Sử Dụng
- **Công cụ:** Google Gemini / ChatGPT
- **Mục đích:** Hỗ trợ phân tích giao diện UX/UI website Shopee và gợi ý cấu trúc HTML5/CSS3 Responsive cho thẻ sản phẩm.

## 2. Lịch Sử Các Câu Lệnh Prompt

### Prompt 1: Phân tích 5 khu vực giao diện Shopee
> *"Bạn là chuyên gia phân tích nghiệp vụ và thiết kế trải nghiệm người dùng cho website thương mại điện tử. Tôi đang khảo sát website: Shopee (https://shopee.vn). Hãy giúp tôi lập bảng phân tích các khu vực: 1. Trang chủ, 2. Danh mục sản phẩm, 3. Chi tiết sản phẩm, 4. Tìm kiếm, 5. Giỏ hàng..."*

### Prompt 2: Đề xuất phương án thiết kế thẻ sản phẩm
> *"Tôi đang thực hiện bài tập phát triển giao diện website thương mại điện tử bằng HTML5 và CSS3. Tôi muốn cải tiến khu vực: Thẻ sản phẩm (Product Card) hiển thị giá khuyến mãi và tỷ lệ giảm giá rõ ràng hơn dựa trên khảo sát Shopee..."*

### Prompt 3: Sinh mã HTML5 và CSS3
> *"Dựa trên phương án đã thống nhất, hãy tạo một trang giao diện thương mại điện tử bằng HTML5 và CSS3..."*

## 3. Đánh Giá & Tự Điều Chỉnh Của Sinh Viên
- **Phát hiện lỗi của AI:** AI nhận diện nhầm Shopee có chức năng "Mua hàng không cần đăng nhập (Guest Checkout)". Sinh viên đã loại bỏ thông tin sai này trong báo cáo.
- **Tự điều chỉnh CSS:**
  - Sửa màu nút bấm từ màu xanh dương do AI sinh ra thành màu cam đỏ đặc trưng của Shopee (`#ee4d2d`).
  - Bổ sung thuộc tính CSS `-webkit-line-clamp: 2;` để giới hạn tên sản phẩm tối đa 2 dòng, giúp kích thước thẻ cân đối trên mọi thiết bị.