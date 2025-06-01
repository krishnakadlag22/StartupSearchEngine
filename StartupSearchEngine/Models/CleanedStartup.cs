using System;
using System.Collections.Generic;

namespace StartupSearchEngine.Models;

public partial class CleanedStartup
{
    public int Id { get; set; }

    public string? Company { get; set; }

    public string? City { get; set; }

    public string? StartingYear { get; set; }

    public string? Founders { get; set; }

    public string? Industry { get; set; }

    public string? Description { get; set; }

    public string? EmployeeRange { get; set; }

    public string? NoOfInvestors { get; set; }

    public string? InvestmentStage { get; set; }
}
