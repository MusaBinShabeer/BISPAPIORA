using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;

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
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddRegisteredBankOtherSpecification(AddRegisteredBankOtherSpecificationDTO model)
        {
            try
            {
                var fkBankinfo = Guid.Parse(model.fkCitizenFamilyBankInfo);
                var bankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.fk_citizen_family_bank_info==fkBankinfo ).FirstOrDefaultAsync();
                if (bankOtherSpecification == null)
                {
                    var newBankOtherSpecification = new tbl_bank_other_specification();
                    newBankOtherSpecification = _mapper.Map<tbl_bank_other_specification>(model);
                    db.tbl_bank_other_specifications.Add(newBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Other Specification {model.bankOtherSpecification} has been added successfully",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(newBankOtherSpecification),
                    };
                }
                else
                {
                    var existingBankOtherSpecification = _mapper.Map<UpdateRegisteredBankOtherSpecificationDTO>(model);
                    existingBankOtherSpecification.bankOtherSpecificationId = bankOtherSpecification.bank_other_specification_id.ToString();
                    var response = await UpdateRegisteredBankOtherSpecification(existingBankOtherSpecification);
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateRegisteredBankOtherSpecification(UpdateRegisteredBankOtherSpecificationDTO model)
        {
            try
            {
                var existingBankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.bank_other_specification_id == Guid.Parse(model.bankOtherSpecificationId)).FirstOrDefaultAsync();
                if (existingBankOtherSpecification != null)
                {
                    existingBankOtherSpecification = _mapper.Map(model, existingBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Bank Other Specification has been updated",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(existingBankOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddEnrolledBankOtherSpecification(AddEnrolledBankOtherSpecificationDTO model)
        {
            try
            {
                var fkBankinfo = Guid.Parse(model.fkCitizenBankInfo);
                var bankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.fk_citizen_bank_info== fkBankinfo).FirstOrDefaultAsync();
                if (bankOtherSpecification == null)
                {
                    var newBankOtherSpecification = new tbl_bank_other_specification();
                    newBankOtherSpecification = _mapper.Map<tbl_bank_other_specification>(model);
                    db.tbl_bank_other_specifications.Add(newBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Other Specification {model.bankOtherSpecification} has been added successfully",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(newBankOtherSpecification),
                    };
                }
                else
                {
                    var existingBankOtherSpecification = _mapper.Map<UpdateRegisteredBankOtherSpecificationDTO>(model);
                    existingBankOtherSpecification.bankOtherSpecificationId = bankOtherSpecification.bank_other_specification_id.ToString();
                    var response = await UpdateRegisteredBankOtherSpecification(existingBankOtherSpecification);
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateEnrolleedBankOtherSpecification(UpdateEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                var existingBankOtherSpecification = await db.tbl_bank_other_specifications.Where(x => x.bank_other_specification_id == Guid.Parse(model.CitizenBankInfoId)).FirstOrDefaultAsync();
                if (existingBankOtherSpecification != null)
                {
                    existingBankOtherSpecification = _mapper.Map(model, existingBankOtherSpecification);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Bank Other Specification has been updated",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(existingBankOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
