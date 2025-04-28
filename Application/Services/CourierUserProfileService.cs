using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CourierUserProfileService : ICourierUserProfileService
    {
        private readonly ICourierUserProfileRepository _cupRepository;
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public CourierUserProfileService(ICourierUserProfileRepository cupRepository, IMapper mapper, IUserLoginsRepository ulRepository)
        {
            _mapper = mapper;
            _cupRepository = cupRepository;
            _ulRepository = ulRepository;
        }
        public async Task<int> AddAsync(CreateCourierUserProfileDto_Ex ccupe)
        {
            CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(ccupe.email, "123123", ccupe.user_role_id, true, "", ccupe.created_by);
            int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

            CreateCourierUserProfileDto cc = new CreateCourierUserProfileDto(ulid, ccupe.first_name, ccupe.last_name,ccupe.emp_num, 
                ccupe.email, ccupe.dob, ccupe.enrollment_date, ccupe.joining_date, ccupe.address, ccupe.postal_code, ccupe.city_id, 
                ccupe.phone_number, ccupe.other, ccupe.courier_id, ccupe.created_by, 1);
            int cpid = await _cupRepository.AddAsync(_mapper.Map<courieruserprofile>(cc));
            return cpid;
        }

        public async Task DeleteAsync(int id)
        {
            await _cupRepository.DeleteAsync(id);
        }

        public async Task<List<CourierUserProfileDto>> GetAllAsync()
        {
            return _mapper.Map<List<CourierUserProfileDto>>(await _cupRepository.GetAllAsync());
        }

        public async Task<CourierUserProfileDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CourierUserProfileDto>(await _cupRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateCourierUserProfileDto uCourierUserProfileDto)
        {
            await _cupRepository.UpdateAsync(_mapper.Map<courieruserprofile>(uCourierUserProfileDto));
        }
    }
}
