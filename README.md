# PHẦN MỀM QUẢN LÝ MUA BÁN LAPTOP CHO CỬA HÀNG

## Giới thiệu

Phần mềm này được xây dựng bằng C# WinForms và SQL Server để hỗ trợ nhân viên cửa hàng laptop trong việc quản lý sản phẩm,
khách hàng, hóa đơn và kho hàng. Ứng dụng hoạt động nội bộ,
dễ sử dụng, phù hợp với các cửa hàng vừa và nhỏ.

## Tính năng chính

- Đăng nhập hệ thống (tài khoản nhân viên, có phân quyền)
- Quản lý sản phẩm laptop (tên, hãng, cấu hình, giá, tồn kho)
- Lập hóa đơn bán hàng, tính tổng tiền
- Quản lí xuất nhập kho
- Tự động cập nhật số lượng tồn kho sau bán hàng
- Tìm kiếm sản phẩm, hóa đơn theo từ khóa hoặc ngày
- Thống kê doanh thu theo ngày / tháng
- In hóa đơn (tuỳ chọn)

## Công nghệ sử dụng

- Giao diện: C# WinForms
- Cơ sở dữ liệu: SQL Server
- Kết nối dữ liệu: ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
- Ngôn ngữ lập trình: C#
  
## Tư tưởng thiêt kế
Không áp dụng mô hình phức tạp: Vì đây là ứng dụng nhỏ, tôi ưu tiên cách viết đơn giản, dễ triển khai, dễ học và dễ bảo trì.

Giao diện thân thiện: Hướng đến người dùng cuối là nhân viên bán hàng không chuyên CNTT.

Cấu trúc dữ liệu rõ ràng: Dùng SQL Server để đảm bảo toàn vẹn dữ liệu và dễ thống kê.

Tách kết nối DB hợp lý: Đảm bảo không lặp lại chuỗi kết nối nhiều nơi.
## Những thứ tôi làm 
Tự tay thiết kế giao diện WinForms với bố cục dễ thao tác

Viết các hàm xử lý nghiệp vụ trực tiếp trong từng form (không áp dụng mô hình MVC)

Thiết kế cơ sở dữ liệu quan hệ đầy đủ với khóa chính, khóa ngoại, chuẩn hóa tốt

Xử lý logic thêm, sửa, xóa, tìm kiếm, và tính tổng đơn hàng bằng các hàm SQL kết hợp với ADO.NET

Viết class DBConnection để quản lý kết nối một cách tái sử dụng
