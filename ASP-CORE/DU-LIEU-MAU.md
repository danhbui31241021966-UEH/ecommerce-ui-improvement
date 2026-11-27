# Dữ liệu để tự nhập qua giao diện CineBox

Tất cả phim là sản phẩm giả định, ảnh bìa thiết kế riêng cho bài học. Tạo 3 thể loại: Viễn tưởng, Tâm lý, Hoạt hình. Dùng ID thực tế hiển thị ở /Genres cho trường Mã thể loại và chọn đúng dropdown tương ứng. Ngày mẫu là ngày tương lai so với 28/11/2026; nếu làm sau ngày mẫu, chọn ngày tương lai mới.

| Tiêu đề | Thể loại | Ngày phát hành | Giá (đ) | Xếp hạng | Tóm tắt | File ảnh trong SourceCode/wwwroot/images |
|---|---|---|---:|---:|---|---|
| Chân trời sao băng | Viễn tưởng | 2027-03-12 | 79000 | 4.8 | Một nhóm nhà thám hiểm truy tìm tín hiệu lạ giữa vùng không gian chưa được khám phá. | 01-chan-troi.jpg |
| Hành tinh thứ chín | Viễn tưởng | 2027-04-16 | 89000 | 4.6 | Chuyến hành trình tìm kiếm một hành tinh bí ẩn thay đổi cách con người nhìn về vũ trụ. | 02-hanh-tinh.jpg |
| Ga cuối mùa thu | Tâm lý | 2027-05-08 | 59000 | 4.4 | Cuộc gặp gỡ tại ga tàu mở ra hành trình hàn gắn và tìm lại những ước mơ cũ. | 03-ga-cuoi.jpg |
| Thành phố sau mưa | Tâm lý | 2027-06-18 | 65000 | 4.5 | Hai người xa lạ học cách tìm thấy hy vọng giữa những đổi thay của thành phố. | 04-thanh-pho.jpg |
| Mèo nhỏ phiêu lưu | Hoạt hình | 2027-07-09 | 55000 | 4.7 | Chú mèo nhỏ và nhóm bạn vượt qua thử thách để trở về ngôi nhà thân yêu. | 05-meo.jpg |
| Khu rừng biết hát | Hoạt hình | 2027-08-20 | 69000 | 4.9 | Những cư dân rừng cùng tìm lại giai điệu kỳ diệu đã mất. | 06-rung.jpg |

Ví dụ kiểm tra giỏ: 2 Chân trời sao băng (79.000 × 2) + 1 Mèo nhỏ phiêu lưu (55.000) = **213.000 đ**. Xóa Mèo nhỏ phiêu lưu còn **158.000 đ**. Tạo dữ liệu giả định; email checkout nên dùng chính email bạn kiểm soát để kiểm tra nhận thư.
