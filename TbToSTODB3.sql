CREATE PROCEDURE dbo.S0305_CreatePhieuNhapDynamic
(
    @SoPhieu NVARCHAR(50),
    @NguoiLap NVARCHAR(100),
    @NhaCungCap NVARCHAR(255),
    @TypeDefinition NVARCHAR(MAX), -- Chuỗi Base64 mô tả Type
    @ChiTietJson NVARCHAR(MAX) -- Dữ liệu chi tiết dạng JSON
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Giải mã Base64
        --DECLARE @TypeScript NVARCHAR(MAX)
        --SET @TypeScript = CONVERT(NVARCHAR(MAX), CAST(N'' AS XML).value('xs:base64Binary(sql:variable("@TypeDefinition"))', 'VARBINARY(MAX)'))

        ---- Kiểm tra và xóa Type nếu đã tồn tại
        --IF EXISTS (SELECT 1 FROM sys.types WHERE name = 'PhieuNhapChiTietType' AND is_user_defined = 1)
        --BEGIN
        --    DROP TYPE dbo.PhieuNhapChiTietType
        --END

        -- Thực thi script tạo Type
        EXEC sp_executesql @TypeScript

        -- Parse JSON thành bảng tạm
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

        -- Cleanup: Xóa Type sau khi sử dụng
        DROP TYPE dbo.PhieuNhapChiTietType

    END TRY
    BEGIN CATCH
        -- Cleanup nếu có lỗi
        IF EXISTS (SELECT 1 FROM sys.types WHERE name = 'PhieuNhapChiTietType' AND is_user_defined = 1)
        BEGIN
            DROP TYPE dbo.PhieuNhapChiTietType
        END
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO