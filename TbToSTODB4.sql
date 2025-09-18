CREATE PROCEDURE [dbo].[S0305_CreatePhieuNhapDynamic]
(
    @SoPhieu NVARCHAR(50),
    @NguoiLap NVARCHAR(100),
    @NhaCungCap NVARCHAR(255),
    @TypeDefinition NVARCHAR(MAX), -- Chuỗi Base64 mô tả Type (không dùng nữa nhưng giữ cho interface)
    @ChiTietJson NVARCHAR(MAX) -- Dữ liệu chi tiết dạng JSON
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Parse JSON thành bảng tạm (không cần tạo Type)
        DECLARE @ChiTietTable TABLE (
            IDHH BIGINT,
            TenHangHoa NVARCHAR(250),
            SoLuong INT,
            DonGia FLOAT
        )

        INSERT INTO @ChiTietTable (IDHH, TenHangHoa, SoLuong, DonGia)
        SELECT 
            IDHH, 
            TenHangHoa, 
            SoLuong, 
            DonGia
        FROM OPENJSON(@ChiTietJson)
        WITH (
            IDHH BIGINT '$.IDHH',
            TenHangHoa NVARCHAR(250) '$.TenHangHoa',
            SoLuong INT '$.SoLuong',
            DonGia FLOAT '$.DonGia'
        )

        -- Thực hiện insert dữ liệu
        DECLARE @PhieuNhapId BIGINT;

        INSERT INTO PhieuNhap(SoPhieu, NgayLapPhieu)
        VALUES (@SoPhieu, GETDATE());

        SET @PhieuNhapId = SCOPE_IDENTITY();

        INSERT INTO PhieuNhapCT(IDPN, IDHH, SoLuong, Gia)
        SELECT @PhieuNhapId, IDHH, SoLuong, DonGia
        FROM @ChiTietTable;

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
