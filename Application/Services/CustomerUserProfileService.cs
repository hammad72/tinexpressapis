using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Services
{
    public class CustomerUserProfileService : ICustomerUserProfileService
    {
        private readonly ICustomerUserProfileRepository _cupRepository;
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public CustomerUserProfileService(ICustomerUserProfileRepository cupRepository, IMapper mapper, IUserLoginsRepository ulRepository)
        {
            _mapper = mapper;
            _cupRepository = cupRepository;
            _ulRepository = ulRepository;
        }
        public async Task<int> AddAsync(CreateCustomerUserProfileDto_Ex ccupe)
        {
            CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(ccupe.email, "123123", ccupe.user_role_id, true, "", ccupe.created_by);
            int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

            CreateCustomerUserProfileDto cc = new CreateCustomerUserProfileDto(ulid, ccupe.first_name, ccupe.last_name, ccupe.emp_num,
            ccupe.email, ccupe.dob, ccupe.enrollment_date, ccupe.joining_date, ccupe.address, ccupe.postal_code, ccupe.city_id,
            ccupe.phone_number, ccupe.other, ccupe.customer_id, ccupe.created_by, 1);
            int cpid = await _cupRepository.AddAsync(_mapper.Map<customeruserprofile>(cc));
            return cpid;
        }
        public async Task DeleteAsync(int id)
        {
            await _cupRepository.DeleteAsync(id);
        }
        public async Task<List<CustomerUserProfileDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerUserProfileDto>>(await _cupRepository.GetAllAsync());
        }
        public async Task<PaginatedList<CustomerUserProfileDto>> GetAllAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await _cupRepository.GetAllAsync(pageIndex, pageSize);
            var customerUserProfileDtos = _mapper.Map<PaginatedList<CustomerUserProfileDto>>(paginatedList);
            return customerUserProfileDtos;
        }
        public async Task<CustomerUserProfileDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerUserProfileDto>(await _cupRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateCustomerUserProfileDto uCustomerUserProfileDto)
        {
            await _cupRepository.UpdateAsync(_mapper.Map<customeruserprofile>(uCustomerUserProfileDto));
        }
    }
}