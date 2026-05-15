
USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLiDichVuGiaiTriBien')
    DROP DATABASE QuanLiDichVuGiaiTriBien;
GO
CREATE DATABASE QuanLiDichVuGiaiTriBien;
GO
USE QuanLiDichVuGiaiTriBien;
GO

-- 1. BẢNG VAI TRÒ
CREATE TABLE VaiTro (
    ma_vai_tro  VARCHAR(20)  NOT NULL PRIMARY KEY,
    ten_vai_tro NVARCHAR(50) NOT NULL
);

-- 2. BẢNG NGƯỜI DÙNG
CREATE TABLE Nguoi_Dung (
    ma_nguoi_dung VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_vai_tro    VARCHAR(20)   NOT NULL,
    ho_ten        NVARCHAR(50)  NOT NULL,
    email         NVARCHAR(100) NOT NULL UNIQUE,
    matkhau       NVARCHAR(255) NOT NULL, -- Tăng độ dài để lưu hash password
    trang_thai    BIT           NOT NULL DEFAULT 1,
    ngay_tao      DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_NguoiDung_VaiTro FOREIGN KEY (ma_vai_tro) REFERENCES VaiTro(ma_vai_tro)
);

-- 3. BẢNG GIÁM ĐỐC (Kế thừa từ Nguoi_Dung)
CREATE TABLE GiamDoc (
    ma_giam_doc VARCHAR(20) NOT NULL PRIMARY KEY,
    ma_vai_tro  VARCHAR(20) NOT NULL,
    CONSTRAINT FK_GiamDoc_NguoiDung FOREIGN KEY (ma_giam_doc) REFERENCES Nguoi_Dung(ma_nguoi_dung),
    CONSTRAINT FK_GiamDoc_VaiTro FOREIGN KEY (ma_vai_tro) REFERENCES VaiTro(ma_vai_tro)
);

-- 4. BẢNG KẾ TOÁN (Kế thừa từ Nguoi_Dung)
CREATE TABLE KeToan (
    ma_ke_toan VARCHAR(20) NOT NULL PRIMARY KEY,
    ma_vai_tro VARCHAR(20) NOT NULL,
    CONSTRAINT FK_KeToan_NguoiDung FOREIGN KEY (ma_ke_toan) REFERENCES Nguoi_Dung(ma_nguoi_dung),
    CONSTRAINT FK_KeToan_VaiTro FOREIGN KEY (ma_vai_tro) REFERENCES VaiTro(ma_vai_tro)
);

-- 5. BẢNG ADMIN (Đồng bộ kiểu kế thừa như GiamDoc/KeToan)
CREATE TABLE Admin (
    ma_admin    VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_vai_tro  VARCHAR(20)   NOT NULL,
    ghi_chu     NVARCHAR(255) NULL,
    CONSTRAINT FK_Admin_NguoiDung FOREIGN KEY (ma_admin) REFERENCES Nguoi_Dung(ma_nguoi_dung),
    CONSTRAINT FK_Admin_VaiTro FOREIGN KEY (ma_vai_tro) REFERENCES VaiTro(ma_vai_tro)
);

-- 6. BẢNG KHÁCH HÀNG
CREATE TABLE KhachHang (
    ma_khach_hang  VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_nguoi_dung  VARCHAR(20)   NOT NULL,
    so_CCCD        VARCHAR(12)   NOT NULL UNIQUE,
    dia_chi        NVARCHAR(255) NULL,
    ngay_sinh      DATE          NULL,
    so_dien_thoai  VARCHAR(15)   NULL,
    anh_dai_dien   VARCHAR(255)  NULL,
    CONSTRAINT FK_KhachHang_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);

-- 7. BẢNG DANH MỤC DỊCH VỤ
CREATE TABLE DanhMucDichVu (
    ma_danh_muc  VARCHAR(20)   NOT NULL PRIMARY KEY,
    ten_danh_muc NVARCHAR(100) NOT NULL,
    mo_ta        NVARCHAR(MAX) NULL,
    loai_dich_vu NVARCHAR(50)  NULL
);

-- 8. BẢNG CƠ SỞ KINH DOANH
CREATE TABLE CosoKinhDoanh (
    ma_co_so_kinh_doanh VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_nguoi_dung       VARCHAR(20)   NOT NULL, -- Chủ cơ sở
    ten_co_so           NVARCHAR(150) NOT NULL,
    dia_chi             NVARCHAR(255) NULL,
    mo_ta               NVARCHAR(MAX) NULL,
    so_dkkd             VARCHAR(50)   NULL,
    trang_thai_duyet    NVARCHAR(50)  NULL,
    anh_giay_phep       VARCHAR(255)  NULL,
    CONSTRAINT FK_CSKD_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);

-- 9. BẢNG DỊCH VỤ
CREATE TABLE DichVu (
    ma_dich_vu          VARCHAR(20)    NOT NULL PRIMARY KEY,
    ma_co_so_kinh_doanh VARCHAR(20)    NOT NULL,
    ma_nguoi_dung       VARCHAR(20)    NOT NULL,
    ma_danh_muc         VARCHAR(20)    NOT NULL,
    ten_dich_vu         NVARCHAR(150)  NOT NULL,
    mota                NVARCHAR(MAX)  NULL,
    gia                 DECIMAL(18,2)  NOT NULL DEFAULT 0,
    soluong             INT            NULL,
    thoi_gian_bat_dau   TIME           NULL,
    thoi_gian_ket_thuc  TIME           NULL,
    trangthai           NVARCHAR(50)   NULL,
    CONSTRAINT FK_DichVu_CSKD FOREIGN KEY (ma_co_so_kinh_doanh) REFERENCES CosoKinhDoanh(ma_co_so_kinh_doanh),
    CONSTRAINT FK_DichVu_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung),
    CONSTRAINT FK_DichVu_DanhMuc FOREIGN KEY (ma_danh_muc) REFERENCES DanhMucDichVu(ma_danh_muc)
);

-- 10. BẢNG ẢNH DỊCH VỤ
CREATE TABLE ImageDichVU (
    ma_anh        INT IDENTITY(1,1) PRIMARY KEY,
    ma_dich_vu    VARCHAR(20)   NOT NULL,
    ma_nguoi_dung VARCHAR(20)   NOT NULL,
    image_url     VARCHAR(255)  NOT NULL,
    ngay_tai_anh  DATETIME      DEFAULT GETDATE(),
    CONSTRAINT FK_ImageDV_DichVu FOREIGN KEY (ma_dich_vu) REFERENCES DichVu(ma_dich_vu),
    CONSTRAINT FK_ImageDV_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);

-- 11. BẢNG MÃ GIẢM GIÁ
CREATE TABLE MaGiamGia (
    ma_giam_gia         VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_co_so_kinh_doanh VARCHAR(20)   NOT NULL,
    dieu_kien           NVARCHAR(255) NULL,
    gia_tri             DECIMAL(18,2) NOT NULL DEFAULT 0,
    soluong             INT           NULL,
    trang_thai_su_dung  NVARCHAR(50)  NULL,
    ngay_het_han        DATETIME      NULL,
    ngay_phat_hanh      DATETIME      NULL,
    kich_hoat           BIT           DEFAULT 1,
    CONSTRAINT FK_MaGiamGia_CSKD FOREIGN KEY (ma_co_so_kinh_doanh) REFERENCES CosoKinhDoanh(ma_co_so_kinh_doanh)
);

-- 12. BẢNG BOOKING
CREATE TABLE BooKing (
    ma_booking      VARCHAR(20)    NOT NULL PRIMARY KEY,
    ma_khach_hang   VARCHAR(20)    NOT NULL,
    ma_dich_vu      VARCHAR(20)    NOT NULL,
    ma_giam_gia     VARCHAR(20)    NULL,
    ngay_dat        DATETIME       NOT NULL DEFAULT GETDATE(),
    ngay_su_dung    DATETIME       NOT NULL,
    so_luong        INT            NOT NULL DEFAULT 1,
    tong_tien       DECIMAL(18,2)  NULL,
    tien_giam       DECIMAL(18,2)  NULL DEFAULT 0,
    tien_thanh_toan DECIMAL(18,2)  NULL,
    trang_thai      NVARCHAR(50)   NULL,
    CONSTRAINT FK_Booking_KhachHang FOREIGN KEY (ma_khach_hang) REFERENCES KhachHang(ma_khach_hang),
    CONSTRAINT FK_Booking_DichVu FOREIGN KEY (ma_dich_vu) REFERENCES DichVu(ma_dich_vu),
    CONSTRAINT FK_Booking_MaGiamGia FOREIGN KEY (ma_giam_gia) REFERENCES MaGiamGia(ma_giam_gia)
);

CREATE TABLE ChiTietBooking (
    ma_chi_tiet_hoa_don VARCHAR(20)    NOT NULL PRIMARY KEY,
    ma_hoa_don          VARCHAR(20)    NOT NULL,
    ma_dich_vu          VARCHAR(20)    NOT NULL,
    so_luong            INT            NOT NULL DEFAULT 1,
    don_gia             DECIMAL(10,2)  NOT NULL,
    thanh_tien          DECIMAL(18,2)  NOT NULL,
    CONSTRAINT FK_CTBooking_DichVu FOREIGN KEY (ma_dich_vu) REFERENCES DichVu(ma_dich_vu)
);

-- 13. BẢNG HÓA ĐƠN
CREATE TABLE HoaDon (
    ma_hoa_don           VARCHAR(50)    NOT NULL PRIMARY KEY,
    ma_booking           VARCHAR(20)    NOT NULL,
    ngay_tao             DATETIME       NOT NULL DEFAULT GETDATE(),
    tong_tien_dich_vu    DECIMAL(18,2)  NOT NULL DEFAULT 0,
    tien_giam_gia        DECIMAL(18,2)  NULL DEFAULT 0,
    thue_vat             DECIMAL(5,2)   NULL,
    tong_thanh_toan      DECIMAL(18,2)  NOT NULL DEFAULT 0,
    phuong_thuc_thanh_toan NVARCHAR(50)  NULL,
    trang_thai           NVARCHAR(50)   NULL,
    ghi_chu              NVARCHAR(MAX)  NULL,
    CONSTRAINT FK_HoaDon_Booking FOREIGN KEY (ma_booking) REFERENCES BooKing(ma_booking)
);

-- 14. BẢNG CHI TIẾT HÓA ĐƠN
CREATE TABLE ChiTietHoaDon (
    ma_chi_tiet_hoa_don VARCHAR(20)    NOT NULL PRIMARY KEY,
    ma_hoa_don          VARCHAR(50)    NOT NULL,
    ma_dich_vu          VARCHAR(20)    NOT NULL,
    ten_dich_vu         NVARCHAR(150)  NULL,
    so_luong            INT            NOT NULL DEFAULT 1,
    don_gia             DECIMAL(18,2)  NOT NULL,
    thanh_tien          DECIMAL(18,2)  NOT NULL,
    CONSTRAINT FK_CTHoaDon_HoaDon FOREIGN KEY (ma_hoa_don) REFERENCES HoaDon(ma_hoa_don),
    CONSTRAINT FK_CTHoaDon_DichVu FOREIGN KEY (ma_dich_vu) REFERENCES DichVu(ma_dich_vu)
);

-- 15. BẢNG HỢP ĐỒNG (Bổ sung ma_booking theo ERD)
CREATE TABLE HopDong (
    ma_hop_dong  VARCHAR(20)    NOT NULL PRIMARY KEY,
    ma_ke_toan   VARCHAR(20)    NOT NULL,
    ma_giam_doc  VARCHAR(20)    NOT NULL,
    ma_nguoi_dung VARCHAR(20)   NOT NULL, -- Đại diện CSKD
    ma_booking   VARCHAR(20)    NULL,     -- Bổ sung liên kết Booking
    ten_hop_dong NVARCHAR(150)  NOT NULL,
    ngay_lap     DATETIME       NOT NULL DEFAULT GETDATE(),
    ngay_ki      DATETIME       NULL,
    gia_tri      DECIMAL(18,2)  NULL,
    trang_thai   NVARCHAR(50)   NULL,
    noi_dung     NVARCHAR(MAX)  NULL,
    CONSTRAINT FK_HopDong_KeToan FOREIGN KEY (ma_ke_toan) REFERENCES KeToan(ma_ke_toan),
    CONSTRAINT FK_HopDong_GiamDoc FOREIGN KEY (ma_giam_doc) REFERENCES GiamDoc(ma_giam_doc),
    CONSTRAINT FK_HopDong_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung),
    CONSTRAINT FK_HopDong_Booking FOREIGN KEY (ma_booking) REFERENCES BooKing(ma_booking)
);

-- 16. BẢNG THÔNG BÁO
CREATE TABLE ThongBao (
    ma_thong_bao  VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_nguoi_dung VARCHAR(20)   NOT NULL,
    tieu_de       NVARCHAR(255) NOT NULL,
    noi_dung      NVARCHAR(MAX) NULL,
    loai          NVARCHAR(50)  NULL,
    trang_thai    NVARCHAR(50)  NULL,
    thoi_gian_gui DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ThongBao_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);

-- 17. BẢNG AI LOG
CREATE TABLE AI_Log (
    ma_log           VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_nguoi_dung    VARCHAR(20)   NOT NULL,
    loai_ai          NVARCHAR(50)  NULL,
    noi_dung_yeu_cau NVARCHAR(MAX) NULL,
    ket_qua_tra_ve   NVARCHAR(MAX) NULL,
    thoi_gian        DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_AILog_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);

-- 18. BẢNG GIAO DỊCH THANH TOÁN
CREATE TABLE GiaoDichThanhToan (
    ma_giao_dich    VARCHAR(50)    NOT NULL PRIMARY KEY,
    ma_booking      VARCHAR(20)    NOT NULL,
    so_tien         DECIMAL(18,2)  NOT NULL,
    loai_thanh_toan NVARCHAR(50)   NULL,
    trang_thai      NVARCHAR(50)   NULL,
    thoi_gian       DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_GDTT_Booking FOREIGN KEY (ma_booking) REFERENCES BooKing(ma_booking)
);

-- 19. BẢNG ĐÁNH GIÁ
CREATE TABLE DanhGia (
    ma_danh_gia   VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_booking    VARCHAR(20)   NOT NULL,
    ma_khach_hang VARCHAR(20)   NOT NULL,
    ma_dich_vu    VARCHAR(20)   NOT NULL,
    so_sao        INT           NULL CHECK (so_sao BETWEEN 1 AND 5),
    noi_dung      NVARCHAR(MAX) NULL, -- Sửa từ INT sang NVARCHAR
    ngay_danh_gia DATETIME      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_DanhGia_Booking FOREIGN KEY (ma_booking) REFERENCES BooKing(ma_booking),
    CONSTRAINT FK_DanhGia_KhachHang FOREIGN KEY (ma_khach_hang) REFERENCES KhachHang(ma_khach_hang),
    CONSTRAINT FK_DanhGia_DichVu FOREIGN KEY (ma_dich_vu) REFERENCES DichVu(ma_dich_vu)
);

-- 20. BẢNG ĐƠN HÀNG CƠ SỞ
CREATE TABLE DonHangCoSo (
    ma_don_hang         VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_booking          VARCHAR(20)   NOT NULL,
    ma_co_so_kinh_doanh VARCHAR(20)   NOT NULL,
    trang_thai_xu_ly    NVARCHAR(50)  NULL,
    thoi_gian_xac_nhan  DATETIME      NULL,
    ghi_chu_co_so       NVARCHAR(MAX) NULL,
    tong_tien           DECIMAL(18,2) NULL,
    CONSTRAINT FK_DHCS_Booking FOREIGN KEY (ma_booking) REFERENCES BooKing(ma_booking),
    CONSTRAINT FK_DHCS_CSKD FOREIGN KEY (ma_co_so_kinh_doanh) REFERENCES CosoKinhDoanh(ma_co_so_kinh_doanh)
);

-- 21. BẢNG LỊCH SỬ GIAO DỊCH
CREATE TABLE LichSuGiaoDich (
    ma_lich_su    VARCHAR(20)   NOT NULL PRIMARY KEY,
    ma_giao_dich  VARCHAR(50)   NOT NULL,
    ma_nguoi_dung VARCHAR(20)   NOT NULL,
    hanh_dong     NVARCHAR(100) NULL,
    trang_thai_cu NVARCHAR(50)  NULL,
    trang_thai_moi NVARCHAR(50) NULL,
    so_tien       DECIMAL(18,2) NULL,
    thoi_gian     DATETIME      NOT NULL DEFAULT GETDATE(),
    ghi_chu       NVARCHAR(MAX) NULL,
    CONSTRAINT FK_LSGD_GiaoDich FOREIGN KEY (ma_giao_dich) REFERENCES GiaoDichThanhToan(ma_giao_dich),
    CONSTRAINT FK_LSGD_NguoiDung FOREIGN KEY (ma_nguoi_dung) REFERENCES Nguoi_Dung(ma_nguoi_dung)
);
GO

CREATE OR ALTER TRIGGER trg_TinhTongTienBooking
ON BooKing
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE b
    SET
        tong_tien       = dv.gia * i.so_luong,
        tien_giam       = ISNULL(mg.gia_tri, 0),
        tien_thanh_toan = (dv.gia * i.so_luong) - ISNULL(mg.gia_tri, 0)
    FROM BooKing b
    INNER JOIN inserted i ON b.ma_booking = i.ma_booking
    INNER JOIN DichVu dv ON i.ma_dich_vu = dv.ma_dich_vu
    LEFT JOIN MaGiamGia mg ON i.ma_giam_gia = mg.ma_giam_gia;
END;
GO

-- ============================================================
-- TRIGGER 2: Giảm số lượng mã giảm giá khi booking sử dụng
-- ============================================================
CREATE OR ALTER TRIGGER trg_GiamSoLuongMaGiamGia
ON BooKing
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE mg
    SET soluong = mg.soluong - 1
    FROM MaGiamGia mg
    INNER JOIN inserted i ON mg.ma_giam_gia = i.ma_giam_gia
    WHERE i.ma_giam_gia IS NOT NULL AND mg.soluong > 0;

    -- Đánh dấu hết hạn nếu số lượng = 0
    UPDATE MaGiamGia
    SET trang_thai_su_dung = N'Hết'
    WHERE soluong <= 0;
END;
GO

-- ============================================================
-- TRIGGER 3: Tự động tạo hóa đơn khi booking được xác nhận
-- ============================================================
CREATE OR ALTER TRIGGER trg_TaoHoaDonKhiBookingXacNhan
ON DonHangCoSo
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Khi trạng thái chuyển sang 'Đã xác nhận'
    INSERT INTO HoaDon (ma_hoa_don, ma_booking, ngay_tao, tong_tien_dich_vu, tien_giam_gia, tong_thanh_toan, trang_thai)
    SELECT
        CONCAT('HD', REPLACE(CONVERT(VARCHAR, GETDATE(), 112), '', ''), i.ma_don_hang),
        i.ma_booking,
        GETDATE(),
        b.tong_tien,
        b.tien_giam,
        b.tien_thanh_toan,
        N'Chờ thanh toán'
    FROM inserted i
    INNER JOIN deleted d ON i.ma_don_hang = d.ma_don_hang
    INNER JOIN BooKing b ON i.ma_booking = b.ma_booking
    WHERE i.trang_thai_xu_ly = N'Đã xác nhận'
      AND d.trang_thai_xu_ly <> N'Đã xác nhận'
      AND NOT EXISTS (SELECT 1 FROM HoaDon hd WHERE hd.ma_booking = i.ma_booking);
END;
GO

-- ============================================================
-- TRIGGER 4: Cập nhật tổng thanh toán hóa đơn khi thêm chi tiết
-- ============================================================
CREATE OR ALTER TRIGGER trg_CapNhatTongHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ma_hoa_don VARCHAR(20);

    SELECT @ma_hoa_don = ISNULL(i.ma_hoa_don, d.ma_hoa_don)
    FROM inserted i
    FULL OUTER JOIN deleted d ON i.ma_chi_tiet_hoa_don = d.ma_chi_tiet_hoa_don;

    UPDATE HoaDon
    SET tong_tien_dich_vu = (
            SELECT ISNULL(SUM(thanh_tien), 0)
            FROM ChiTietHoaDon
            WHERE ma_hoa_don = @ma_hoa_don
        ),
        tong_thanh_toan = (
            SELECT ISNULL(SUM(thanh_tien), 0)
            FROM ChiTietHoaDon
            WHERE ma_hoa_don = @ma_hoa_don
        ) - ISNULL(tien_giam_gia, 0)
    WHERE ma_hoa_don = @ma_hoa_don;
END;
GO

-- ============================================================
-- TRIGGER 5: Ghi lịch sử giao dịch khi trạng thái thay đổi
-- ============================================================
CREATE OR ALTER TRIGGER trg_GhiLichSuGiaoDich
ON GiaoDichThanhToan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LichSuGiaoDich (
        ma_lich_su, ma_giao_dich, ma_nguoi_dung,
        hanh_dong, trang_thai_cu, trang_thai_cu2,
        so_tien, thoi_gian, ghi_chu
    )
    SELECT
        CONCAT('LS', CAST(NEWID() AS VARCHAR(36))),
        i.ma_giao_dich,
        b.ma_khach_hang,
        N'Cập nhật trạng thái',
        d.trang_thai,
        i.trang_thai,
        i.so_tien,
        GETDATE(),
        N'Tự động ghi nhận'
    FROM inserted i
    INNER JOIN deleted d ON i.ma_giao_dich = d.ma_giao_dich
    INNER JOIN BooKing b ON i.ma_booking = b.ma_booking
    WHERE i.trang_thai <> d.trang_thai;
END;
GO

-- ============================================================
-- TRIGGER 6: Gửi thông báo khi booking thay đổi trạng thái
-- ============================================================
CREATE OR ALTER TRIGGER trg_ThongBaoBooking
ON DonHangCoSo
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Thông báo cho khách hàng khi đơn được xác nhận
    INSERT INTO ThongBao (ma_thong_bao, ma_nguoi_dung, tieu_de, noi_dung, loai, trang_thai, thoi_gian_gui)
    SELECT
        CONCAT('TB', CAST(NEWID() AS VARCHAR(36))),
        kh.ma_nguoi_dung,
        N'Đơn hàng được xác nhận',
        CONCAT(N'Đơn đặt dịch vụ ', i.ma_booking, N' đã được xác nhận.'),
        N'Booking',
        N'Chưa đọc',
        GETDATE()
    FROM inserted i
    INNER JOIN deleted d ON i.ma_don_hang = d.ma_don_hang
    INNER JOIN BooKing b ON i.ma_booking = b.ma_booking
    INNER JOIN KhachHang kh ON b.ma_khach_hang = kh.ma_khach_hang
    WHERE i.trang_thai_xu_ly = N'Đã xác nhận'
      AND d.trang_thai_xu_ly <> N'Đã xác nhận';

    -- Thông báo khi bị từ chối
    INSERT INTO ThongBao (ma_thong_bao, ma_nguoi_dung, tieu_de, noi_dung, loai, trang_thai, thoi_gian_gui)
    SELECT
        CONCAT('TB', CAST(NEWID() AS VARCHAR(36))),
        kh.ma_nguoi_dung,
        N'Đơn hàng bị từ chối',
        CONCAT(N'Đơn đặt dịch vụ ', i.ma_booking, N' đã bị từ chối. Vui lòng liên hệ hỗ trợ.'),
        N'Booking',
        N'Chưa đọc',
        GETDATE()
    FROM inserted i
    INNER JOIN deleted d ON i.ma_don_hang = d.ma_don_hang
    INNER JOIN BooKing b ON i.ma_booking = b.ma_booking
    INNER JOIN KhachHang kh ON b.ma_khach_hang = kh.ma_khach_hang
    WHERE i.trang_thai_xu_ly = N'Từ chối'
      AND d.trang_thai_xu_ly <> N'Từ chối';
END;
GO

-- ============================================================
-- TRIGGER 7: Kiểm tra số lượng dịch vụ còn khi đặt booking
-- ============================================================
CREATE OR ALTER TRIGGER trg_KiemTraSoLuongDichVu
ON BooKing
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN DichVu dv ON i.ma_dich_vu = dv.ma_dich_vu
        WHERE dv.soluong IS NOT NULL AND dv.soluong < i.so_luong
    )
    BEGIN
        RAISERROR(N'Số lượng dịch vụ không đủ để thực hiện booking!', 16, 1);
        RETURN;
    END;

    -- Nếu đủ số lượng, thực hiện insert
    INSERT INTO BooKing (ma_booking, ma_khach_hang, ma_dich_vu, ma_giam_gia,
                         ten_dich_vu, ngay_dat, ngay_su_dung, so_luong, tong_tien, tien_giam, tien_thanh_toan)
    SELECT ma_booking, ma_khach_hang, ma_dich_vu, ma_giam_gia,
           ten_dich_vu, ngay_dat, ngay_su_dung, so_luong, tong_tien, tien_giam, tien_thanh_toan
    FROM inserted;

    -- Giảm số lượng tồn
    UPDATE dv
    SET soluong = dv.soluong - i.so_luong
    FROM DichVu dv
    INNER JOIN inserted i ON dv.ma_dich_vu = i.ma_dich_vu
    WHERE dv.soluong IS NOT NULL;
END;
GO

-- ============================================================
-- TRIGGER 8: Ghi log AI khi người dùng thực hiện truy vấn AI
-- ============================================================
CREATE OR ALTER TRIGGER trg_GhiLogAI
ON AI_Log
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Tự động gửi thông báo cho admin khi có log AI bất thường (loai_ai = 'Error')
    INSERT INTO ThongBao (ma_thong_bao, ma_nguoi_dung, tieu_de, noi_dung, loai, trang_thai, thoi_gian_gui)
    SELECT
        CONCAT('TB_AI_', CAST(NEWID() AS VARCHAR(36))),
        a.ma_admin,
        N'Cảnh báo lỗi AI',
        CONCAT(N'Người dùng ', i.ma_nguoi_dung, N' gặp lỗi AI lúc ', CONVERT(VARCHAR, i.thoi_gian, 120)),
        N'HệThống',
        N'Chưa đọc',
        GETDATE()
    FROM inserted i
    CROSS JOIN Admin a
    WHERE i.loai_ai = 'Error';
END;
GO

-- ============================================================
-- TRIGGER 9: Tự động cập nhật trang thái mã giảm giá hết hạn
-- ============================================================
CREATE OR ALTER TRIGGER trg_KiemTraHanMaGiamGia
ON BooKing
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Vô hiệu hóa mã giảm giá đã hết hạn
    UPDATE mg
    SET trang_thai_su_dung = N'Hết hạn'
    FROM MaGiamGia mg
    INNER JOIN inserted i ON mg.ma_giam_gia = i.ma_giam_gia
    WHERE mg.ngay_het_han < GETDATE()
      AND mg.trang_thai_su_dung <> N'Hết hạn';

    -- Nếu mã hết hạn, báo lỗi
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN MaGiamGia mg ON i.ma_giam_gia = mg.ma_giam_gia
        WHERE mg.trang_thai_su_dung = N'Hết hạn'
    )
    BEGIN
        RAISERROR(N'Mã giảm giá đã hết hạn hoặc không còn hiệu lực!', 16, 1);
    END;
END;
GO

-- ============================================================
-- TRIGGER 10: Ngăn xóa dịch vụ nếu còn booking chưa hoàn thành
-- ============================================================
CREATE OR ALTER TRIGGER trg_BaoVeDichVu
ON DichVu
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM deleted d
        INNER JOIN BooKing b ON d.ma_dich_vu = b.ma_dich_vu
        INNER JOIN DonHangCoSo dh ON b.ma_booking = dh.ma_booking
        WHERE dh.trang_thai_xu_ly NOT IN (N'Hoàn thành', N'Từ chối', N'Đã hủy')
    )
    BEGIN
        RAISERROR(N'Không thể xóa dịch vụ vì còn booking đang xử lý!', 16, 1);
        RETURN;
    END;

    DELETE FROM DichVu
    WHERE ma_dich_vu IN (SELECT ma_dich_vu FROM deleted);
END;
GO

-- ============================================================
-- DỮ LIỆU MẪU
-- ============================================================

INSERT INTO VaiTro VALUES ('VT001', N'Quản trị viên'), ('VT002', N'Khách hàng'),
    ('VT003', N'Chủ cơ sở'), ('VT004', N'Giám đốc'), ('VT005', N'Kế toán');

INSERT INTO Nguoi_Dung VALUES
    ('ND001','VT004',N'Nguyễn Văn An','an.nv@beach.vn','hash_pwd_1',1,GETDATE()),
    ('ND002','VT005',N'Trần Thị Bình','binh.tt@beach.vn','hash_pwd_2',1,GETDATE()),
    ('ND003','VT003',N'Lê Văn Cường','cuong.lv@beach.vn','hash_pwd_3',1,GETDATE()),
    ('ND004','VT002',N'Phạm Thị Dung','dung.pt@beach.vn','hash_pwd_4',1,GETDATE()),
    ('ND005','VT002',N'Hoàng Văn Em','em.hv@beach.vn','hash_pwd_5',1,GETDATE());
INSERT INTO GiamDoc VALUES ('ND001','VT004');
INSERT INTO KeToan VALUES ('ND002','VT005');

INSERT INTO KhachHang VALUES
    ('KH001','ND004','012345678901',N'123 Trần Phú, Đà Nẵng','1990-05-15','0901234567',NULL),
    ('KH002','ND005','012345678902',N'456 Nguyễn Văn Linh, Đà Nẵng','1995-08-20','0907654321',NULL);

INSERT INTO DanhMucDichVu VALUES
    ('DM001',N'Lướt sóng',N'Dịch vụ lướt sóng biển',N'Thể thao nước'),
    ('DM002',N'Lặn biển',N'Lặn ngắm san hô',N'Thể thao nước'),
    ('DM003',N'Chèo thuyền',N'Chèo kayak, thuyền buồm',N'Thể thao nước'),
    ('DM004',N'Dù lượn',N'Dù lượn trên biển',N'Trên không'),
    ('DM005',N'Thuê thiết bị',N'Cho thuê thiết bị biển',N'Dịch vụ');

INSERT INTO CosoKinhDoanh VALUES
    ('CS001','ND003',N'Beach Fun Đà Nẵng',N'Bãi biển Mỹ Khê',N'Trung tâm giải trí biển hàng đầu','DKKD-001',N'Đã duyệt',NULL),
    ('CS002','ND003',N'Sea Adventure',N'Bãi biển Non Nước',N'Khám phá đại dương','DKKD-002',N'Đã duyệt',NULL);

INSERT INTO DichVu VALUES
    ('DV001','CS001','ND003','DM001',N'Lướt sóng cơ bản',N'Khóa học 2 tiếng',350000,20,NULL,NULL,N'Hoạt động'),
    ('DV002','CS001','ND003','DM002',N'Lặn biển ngắm san hô',N'Tour 3 tiếng kèm hướng dẫn viên',550000,10,NULL,NULL,N'Hoạt động'),
    ('DV003','CS002','ND003','DM004',N'Dù lượn biển',N'Bay trên biển 15 phút',750000,5,NULL,NULL,N'Hoạt động'),
    ('DV004','CS002','ND003','DM003',N'Chèo kayak đôi',N'Thuê kayak 1 giờ',200000,15,NULL,NULL,N'Hoạt động');

INSERT INTO MaGiamGia VALUES
    ('MGG001','CS001',N'Không điều kiện',50000,100,N'Còn dùng','2025-12-31',GETDATE(),NULL),
    ('MGG002','CS002',N'Hóa đơn trên 500k',100000,50,N'Còn dùng','2025-12-31',GETDATE(),NULL);

INSERT INTO Nguoi_Dung VALUES
('ND006','VT002',N'Nguyễn Hải Nam','nam@gmail.com','hash6',1,GETDATE()),
('ND007','VT002',N'Trần Minh Tú','tu@gmail.com','hash7',1,GETDATE());
INSERT INTO KhachHang VALUES
('KH003','ND006','012345678903',N'Đà Nẵng','1998-02-10','0911111111',NULL),
('KH004','ND007','012345678904',N'Hội An','1999-03-15','0922222222',NULL);

INSERT INTO DichVu VALUES
('DV005','CS001','ND003','DM005',N'Thuê ghế nằm biển',N'Thuê ghế 1 ngày',100000,50,NULL,NULL,N'Hoạt động'),
('DV006','CS002','ND003','DM002',N'Lặn VIP',N'Có hướng dẫn riêng',900000,8,NULL,NULL,N'Hoạt động');
INSERT INTO MaGiamGia VALUES
('MGG003','CS001',N'Hóa đơn trên 300k',30000,20,N'Còn dùng','2026-12-31',GETDATE(),1),
('MGG999','CS001',N'Test',50000,10,N'Còn dùng','2027-12-31',GETDATE(),1);


GO

