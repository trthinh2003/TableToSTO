using System;
using System.Collections.Generic;

namespace TableToSTO.Models.Entities;

public partial class PhieuNhapCt
{
    public long Id { get; set; }

    public long? Idpn { get; set; }

    public long? Idhh { get; set; }

    public int? SoLuong { get; set; }

    public double? Gia { get; set; }

    public virtual DmHangHoa? IdhhNavigation { get; set; }

    public virtual PhieuNhap? IdpnNavigation { get; set; }
}
