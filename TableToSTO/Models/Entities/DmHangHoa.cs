using System;
using System.Collections.Generic;

namespace TableToSTO.Models.Entities;

public partial class DmHangHoa
{
    public long Id { get; set; }

    public string? TenHangHoa { get; set; }

    public double? DonGia { get; set; }

    public virtual ICollection<PhieuNhapCt> PhieuNhapCts { get; set; } = new List<PhieuNhapCt>();
}
