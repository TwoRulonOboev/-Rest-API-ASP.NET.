using System;
using System.Collections.Generic;

namespace Pharmo.Models;

public partial class Pharmacy
{
    public int Id { get; set; }

    public int? MedicineCode { get; set; }

    public string? MedicineName { get; set; }

    public decimal? PricePerUnit { get; set; }
}
