using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;

namespace BISPAPIORA.Models.DTOS.EmploymentOtherSpecificationDTO
{
    public class AddEmploymentOtherSpecificationDTO
    {
        public string employmentOtherSpecification { get; set; } = string.Empty;
        public string fkCitizen { get; set; } = string.Empty;
    }
    public class UpdateEmploymentOtherSpecificationDTO : AddEmploymentOtherSpecificationDTO
    {
        public string employmentOtherSpecificationId { get; set; } = string.Empty;
    }
    public class EmploymentOtherSpecificationResponseDTO : AddEmploymentOtherSpecificationDTO
    {
        public string employmentOtherSpecificationId { get; set; } = string.Empty;
    }
}
