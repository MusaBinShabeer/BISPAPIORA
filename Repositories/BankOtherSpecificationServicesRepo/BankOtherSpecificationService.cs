using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;

namespace BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo
{
    public class BankOtherSpecificationService : IBankOtherSpecificationService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public BankOtherSpecificationService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<BankOtherSpecificationResponseDTO>> AddBankOtherSpecification(AddBankOtherSpecificationDTO model)
        {
            try
            {
                var bankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.bank_other_specification.ToLower().Equals(model.bankOtherSpecification.ToLower())).FirstOrDefaultAsync();
                if (bankOtherSpecification == null)
                {
                    var newBankOtherSpecification = new tbl_bank_other_specification();
                    newBankOtherSpecification = _mapper.Map<tbl_bank_other_specification>(model);
                    db.tbl_bank_other_specifications.Add(newBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Other Specification {model.bankOtherSpecification} has been added successfully",
                        data = _mapper.Map<BankOtherSpecificationResponseDTO>(newBankOtherSpecification),
                    };
                }
                else
                {
                    var existingBankOtherSpecification = _mapper.Map<UpdateBankOtherSpecificationDTO>(model);
                    existingBankOtherSpecification.bankOtherSpecificationId = bankOtherSpecification.bank_other_specification_id.ToString();
                    var response = await UpdateBankOtherSpecification(existingBankOtherSpecification);
                    return new ResponseModel<BankOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankOtherSpecificationResponseDTO>> UpdateBankOtherSpecification(UpdateBankOtherSpecificationDTO model)
        {
            try
            {
                var existingBankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.bank_other_specification_id == Guid.Parse(model.bankOtherSpecificationId)).FirstOrDefaultAsync();
                if (existingBankOtherSpecification != null)
                {
                    existingBankOtherSpecification = _mapper.Map(model, existingBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Bank Other Specification has been updated",
                        data = _mapper.Map<BankOtherSpecificationResponseDTO>(existingBankOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
