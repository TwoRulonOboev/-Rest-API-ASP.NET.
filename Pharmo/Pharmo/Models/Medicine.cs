using System;
using System.Collections.Generic;

namespace Pharmo.Models;

public partial class Medicine
{
    public int Id { get; set; }

    public int? MedicineCode { get; set; }

    public string? MedicineName { get; set; }

    public DateTime? ManufacturingDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? NumberOfPackages { get; set; }

    public int? PharmacyNumber { get; set; }

    public decimal? CostOfMedicine { get; set; }
}
