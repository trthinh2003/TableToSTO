CREATE PROCEDURE [dbo].[S0305_DSPhieuNhap]
AS
BEGIN
	SELECT 
		pn.SoPhieu,
		dm_hh.ID AS IDHH,
		dm_hh.TenHangHoa,
		pn_ct.SoLuong AS SoLuongNhap,
		pn_ct.Gia AS DonGia,
		pn.NgayLapPhieu,
		N'Lâm Văn Hưng' AS NhanVienLap,
		N'Nhà cung cấp ABC' AS NhaCungCap
	FROM DM_HangHoa dm_hh 
	INNER JOIN PhieuNhapCT pn_ct
		ON dm_hh.ID = pn_ct.IDHH
	INNER JOIN PhieuNhap pn
		ON pn_ct.IDPN = pn.ID
END;