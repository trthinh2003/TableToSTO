using System;
using System.Collections.Generic;

namespace TableToSTO.Models.Entities;

public partial class PhieuNhap
{
    public long Id { get; set; }

    public string? SoPhieu { get; set; }

    public DateTime? NgayLapPhieu { get; set; }

    public virtual ICollection<PhieuNhapCt> PhieuNhapCts { get; set; } = new List<PhieuNhapCt>();
}
