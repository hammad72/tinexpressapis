using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerProfileRepository _cpRepository;
        private readonly ICustomerUserProfileRepository _cupRepository;
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public CustomerProfileService(ICustomerProfileRepository cpRepository, ICustomerUserProfileRepository cupRepository, IMapper mapper, IUserLoginsRepository ulRepository)
        {
            _mapper = mapper;
            _cpRepository = cpRepository;
            _cupRepository = cupRepository;
            _ulRepository = ulRepository;
        }
        public async Task<int> AddAsync(CreateCustomerProfileDto_Ex ccp)
        {
            bool res = await _ulRepository.checkUserExist(ccp.email);
            if (res == false)
            {
                CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(ccp.email, "123123", ccp.user_role_id, true, "", ccp.created_by);
                int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

                CreateCustomerProfileDto cp = new CreateCustomerProfileDto(ulid, ccp.name, ccp.legal_name, ccp.email, ccp.phone,
                    ccp.payment_method_id, ccp.referral_name, ccp.fav_pickup_address, ccp.fav_dropoff_address, ccp.address,
                    ccp.enrollment_date, ccp.business_type_id, ccp.invoice_frequency, ccp.sales_tax_num, ccp.proof_of_delivery,
                    ccp.desired_attempts, ccp.other, ccp.created_by, 1);
                int cpid = await _cpRepository.AddAsync(_mapper.Map<customerprofile>(cp));

                CreateCustomerUserProfileDto cup = new CreateCustomerUserProfileDto(ulid, ccp.name, "", "",
                ccp.email, "", ccp.enrollment_date, ccp.enrollment_date, ccp.address, "", 0,
                ccp.phone, ccp.other, cpid, ccp.created_by, 1);
                int upid = await _cupRepository.AddAsync(_mapper.Map<customeruserprofile>(cup));
                return cpid;
            }
            else
            {
                return -5;
            }
        }
        public async Task DeleteAsync(int id)
        {
            await _cpRepository.DeleteAsync(id);
        }
        public async Task<List<CustomerProfileDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerProfileDto>>(await _cpRepository.GetAllAsync());
        }
        public async Task<CustomerProfileDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerProfileDto>(await _cpRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateCustomerProfileDto uCustomerProfileDto)
        {
            await _cpRepository.UpdateAsync(_mapper.Map<customerprofile>(uCustomerProfileDto));
        }
    }
}