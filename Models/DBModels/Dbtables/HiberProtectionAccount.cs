using System;
using System.Collections.Generic;

namespace BISPAPIORA.Models.DBModels.Dbtables;

public partial class HiberProtectionAccount
{
    public decimal? Cnic { get; set; }

    public string? Name { get; set; }

    public decimal? ContactNo { get; set; }

    public decimal? MobileNo { get; set; }

    public string? Address { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Tehsil { get; set; }

    public decimal? SpouseCnic { get; set; }

    public string? SpouseName { get; set; }

    public string? EmployementStatus { get; set; }

    public string? Bank { get; set; }

    public string? BankAccount { get; set; }

    public string? OtherEmployementStatus { get; set; }

    public string? EmployementStatusSpouse { get; set; }

    public string? OtherEmpStatusSpouse { get; set; }

    public decimal? HasBank { get; set; }

    public string? AccountNature { get; set; }

    public string? BankOther { get; set; }

    public string? AccountTitle { get; set; }

    public decimal? BankAccess { get; set; }

    public string? EducationalStatus { get; set; }

    public string? MonthlyEarning { get; set; }

    public decimal? IsValidBeneficiary { get; set; }

    public string? Gender { get; set; }

    public string? MaritalStatus { get; set; }

    public string? SpouseEducationalStatus { get; set; }

    public DateTime? InsertionTimestamp { get; set; }

    public decimal? UniqueHhId { get; set; } // Unique HouseholdId

    public DateTime? SubmissionDate { get; set; } // through form submission date

    public string? Pmt { get; set; } // poverty score 40 =< eligible
}
