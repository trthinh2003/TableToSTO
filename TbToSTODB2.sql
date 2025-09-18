CREATE TYPE dbo.PhieuNhapChiTietType AS TABLE
(
    IDHH BIGINT,
    TenHangHoa NVARCHAR(250),
    SoLuong INT,
    DonGia FLOAT
);
GO

CREATE PROCEDURE dbo.S0305_CreatePhieuNhap
(
    @SoPhieu NVARCHAR(50),
    --@NguoiLap NVARCHAR(100),
    --@NhaCungCap NVARCHAR(255),
    @ChiTiet dbo.PhieuNhapChiTietType READONLY
	--@chuoiMaHoa NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PhieuNhapId INT;

    INSERT INTO PhieuNhap(SoPhieu, NgayLapPhieu)
    VALUES (@SoPhieu, GETDATE());

    SET @PhieuNhapId = SCOPE_IDENTITY();

    INSERT INTO PhieuNhapCT(IDPN, IDHH, SoLuong, Gia)
    SELECT @PhieuNhapId, IDHH, SoLuong, DonGia
    FROM @ChiTiet;
END
