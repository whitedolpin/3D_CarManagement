using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Car
{
    public int CarId { get; set; }

    public string? CreatedBy { get; set; }

    public string? CheckBy { get; set; }

    public int? PositionId { get; set; }

    public string? BrandName { get; set; }

    public string? ModelName { get; set; }

    public string? Scale { get; set; }

    public string? Material { get; set; }

    public string? Color { get; set; }

    public string? CarAdvanceFeature { get; set; }

    public string? Packaging { get; set; }

    public string? AgeRange { get; set; }

    public decimal? Price { get; set; }

    public string? CarStatus { get; set; }

    public DateTime? MonthlyCheck { get; set; }

    public string? StatusCheck { get; set; }

    public string? File3D { get; set; }

    public string? OtherInfo { get; set; }

    public virtual Position? Position { get; set; }
}
