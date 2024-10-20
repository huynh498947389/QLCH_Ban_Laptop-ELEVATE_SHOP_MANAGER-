USE [master]
GO
/****** Object:  Database [Elevate_store]    Script Date: 19/10/2024 18:23:17 ******/
CREATE DATABASE [Elevate_store]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Elevate_store_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Elevate_store.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Elevate_store_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Elevate_store.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Elevate_store] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Elevate_store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Elevate_store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Elevate_store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Elevate_store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Elevate_store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Elevate_store] SET ARITHABORT OFF 
GO
ALTER DATABASE [Elevate_store] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Elevate_store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Elevate_store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Elevate_store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Elevate_store] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Elevate_store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Elevate_store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Elevate_store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Elevate_store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Elevate_store] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Elevate_store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Elevate_store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Elevate_store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Elevate_store] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Elevate_store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Elevate_store] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Elevate_store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Elevate_store] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Elevate_store] SET  MULTI_USER 
GO
ALTER DATABASE [Elevate_store] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Elevate_store] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Elevate_store] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Elevate_store] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Elevate_store] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Elevate_store] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Elevate_store] SET QUERY_STORE = ON
GO
ALTER DATABASE [Elevate_store] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Elevate_store]
GO
/****** Object:  Table [dbo].[ChiTietDonDatHang]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonDatHang](
	[MaDonDat] [nvarchar](10) NOT NULL,
	[MaSP] [nvarchar](10) NOT NULL,
	[SoLuong] [int] NULL,
	[TenSanPham] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonDat] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonNhap]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonNhap](
	[MaDonNhap] [nvarchar](10) NOT NULL,
	[MaSanPham] [nvarchar](10) NOT NULL,
	[SeriSP] [nvarchar](10) NOT NULL,
	[TenSanPham] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_ChiTietDonNhap] PRIMARY KEY CLUSTERED 
(
	[SeriSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhieuBanHang]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuBanHang](
	[MaPhieu] [nvarchar](10) NOT NULL,
	[MaSanPham] [nvarchar](10) NOT NULL,
	[TenSanPham] [nvarchar](100) NOT NULL,
	[SeriSP] [nvarchar](10) NOT NULL,
	[Giaban] [decimal](11, 0) NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuBanHang] PRIMARY KEY CLUSTERED 
(
	[SeriSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatHang]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatHang](
	[MaDonDat] [nvarchar](10) NOT NULL,
	[NgayDat] [date] NULL,
	[MaNV] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonDat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonNhap]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonNhap](
	[MaDonNhap] [nvarchar](10) NOT NULL,
	[NgayNhap] [date] NULL,
	[MaNV] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dulieuthaydoigia]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dulieuthaydoigia](
	[Mathaydoi] [nvarchar](10) NOT NULL,
	[MaSP] [nvarchar](10) NOT NULL,
	[Ngaythaydoi] [date] NOT NULL,
	[Giabancu] [decimal](11, 0) NOT NULL,
	[Giabanmoi] [decimal](11, 0) NOT NULL,
	[MaNV] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Dulieuthaydoigia] PRIMARY KEY CLUSTERED 
(
	[Mathaydoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [nvarchar](10) NOT NULL,
	[MaPhieuBanHang] [nvarchar](10) NULL,
	[TenKH] [nvarchar](100) NULL,
	[Ngaytt] [date] NULL,
	[SDT_KH] [nvarchar](20) NULL,
	[SoTienCanThanhToan] [decimal](11, 0) NULL,
 CONSTRAINT [PK__HoaDon__835ED13B2D212118] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoHang]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoHang](
	[MaSP] [nvarchar](10) NOT NULL,
	[TenSanPham] [nvarchar](100) NULL,
	[MoTa] [nvarchar](500) NULL,
	[AnhSanPham] [nvarchar](255) NULL,
	[GiaBan] [decimal](11, 0) NULL,
	[GiaCost] [decimal](11, 0) NULL,
	[SoLuongTonKho] [int] NULL,
	[Hang] [nvarchar](50) NULL,
	[TinhTrang] [nvarchar](50) NULL,
 CONSTRAINT [PK__KhoHang__2725081C1D2E191C] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [nvarchar](10) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[LoaiTaiKhoan] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[AnhNhanVien] [nvarchar](255) NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuBanHang]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuBanHang](
	[MaPhieu] [nvarchar](10) NOT NULL,
	[TenKH] [nvarchar](100) NULL,
	[SoDienThoaiKH] [nvarchar](20) NULL,
	[NgayMuaHang] [date] NULL,
	[MaNV] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seri_sanpham]    Script Date: 19/10/2024 18:23:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seri_sanpham](
	[MaSP] [nvarchar](10) NOT NULL,
	[SeriSP] [nvarchar](10) NOT NULL,
	[TinhTrang] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Seri_sanpham] PRIMARY KEY CLUSTERED 
(
	[SeriSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[NhanVien] ([MaNV], [HoTen], [SoDienThoai], [Email], [ChucVu], [LoaiTaiKhoan], [MatKhau], [AnhNhanVien], [TrangThai]) VALUES (N'AD0001', N'Ngô Hữu Huỳnh', N'0399914942', N'nhhuynh1711@gmail.com', N'Quản lí', N'Admin', N'ad0001', N'D:\TTLT.NET\BTL\CHLT\CHLT\image\laptop.jpg', N'ON')
GO
ALTER TABLE [dbo].[ChiTietDonDatHang]  WITH CHECK ADD FOREIGN KEY([MaDonDat])
REFERENCES [dbo].[DatHang] ([MaDonDat])
GO
ALTER TABLE [dbo].[ChiTietDonDatHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDon__MaSP__45F365D3] FOREIGN KEY([MaSP])
REFERENCES [dbo].[KhoHang] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietDonDatHang] CHECK CONSTRAINT [FK__ChiTietDon__MaSP__45F365D3]
GO
ALTER TABLE [dbo].[ChiTietDonNhap]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDo__MaDon__5AEE82B9] FOREIGN KEY([MaDonNhap])
REFERENCES [dbo].[DonNhap] ([MaDonNhap])
GO
ALTER TABLE [dbo].[ChiTietDonNhap] CHECK CONSTRAINT [FK__ChiTietDo__MaDon__5AEE82B9]
GO
ALTER TABLE [dbo].[ChiTietDonNhap]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDo__MaSan__3F466844] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[KhoHang] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietDonNhap] CHECK CONSTRAINT [FK__ChiTietDo__MaSan__3F466844]
GO
ALTER TABLE [dbo].[ChiTietPhieuBanHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietPh__MaPhi__4BAC3F29] FOREIGN KEY([MaPhieu])
REFERENCES [dbo].[PhieuBanHang] ([MaPhieu])
GO
ALTER TABLE [dbo].[ChiTietPhieuBanHang] CHECK CONSTRAINT [FK__ChiTietPh__MaPhi__4BAC3F29]
GO
ALTER TABLE [dbo].[ChiTietPhieuBanHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietPh__MaSan__4CA06362] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[KhoHang] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietPhieuBanHang] CHECK CONSTRAINT [FK__ChiTietPh__MaSan__4CA06362]
GO
ALTER TABLE [dbo].[DatHang]  WITH CHECK ADD  CONSTRAINT [FK__DatHang__MaNV__4222D4EF] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[DatHang] CHECK CONSTRAINT [FK__DatHang__MaNV__4222D4EF]
GO
ALTER TABLE [dbo].[DonNhap]  WITH CHECK ADD  CONSTRAINT [FK__DonNhap__MaNV__3B75D760] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[DonNhap] CHECK CONSTRAINT [FK__DonNhap__MaNV__3B75D760]
GO
ALTER TABLE [dbo].[Dulieuthaydoigia]  WITH CHECK ADD  CONSTRAINT [FK_Dulieuthaydoigia_KhoHang] FOREIGN KEY([MaSP])
REFERENCES [dbo].[KhoHang] ([MaSP])
GO
ALTER TABLE [dbo].[Dulieuthaydoigia] CHECK CONSTRAINT [FK_Dulieuthaydoigia_KhoHang]
GO
ALTER TABLE [dbo].[Dulieuthaydoigia]  WITH CHECK ADD  CONSTRAINT [FK_Dulieuthaydoigia_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[Dulieuthaydoigia] CHECK CONSTRAINT [FK_Dulieuthaydoigia_NhanVien]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK__HoaDon__MaPhieuB__4F7CD00D] FOREIGN KEY([MaPhieuBanHang])
REFERENCES [dbo].[PhieuBanHang] ([MaPhieu])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK__HoaDon__MaPhieuB__4F7CD00D]
GO
ALTER TABLE [dbo].[PhieuBanHang]  WITH CHECK ADD  CONSTRAINT [FK__PhieuBanHa__MaNV__48CFD27E] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhieuBanHang] CHECK CONSTRAINT [FK__PhieuBanHa__MaNV__48CFD27E]
GO
ALTER TABLE [dbo].[Seri_sanpham]  WITH CHECK ADD  CONSTRAINT [FK_Seri_sanpham_KhoHang] FOREIGN KEY([MaSP])
REFERENCES [dbo].[KhoHang] ([MaSP])
GO
ALTER TABLE [dbo].[Seri_sanpham] CHECK CONSTRAINT [FK_Seri_sanpham_KhoHang]
GO
USE [master]
GO
ALTER DATABASE [Elevate_store] SET  READ_WRITE 
GO
