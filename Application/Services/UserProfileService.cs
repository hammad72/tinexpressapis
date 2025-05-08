using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _upRepository;
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public UserProfileService(IUserProfileRepository upRepository, IMapper mapper, IUserLoginsRepository ulRepository)
        {
            _mapper = mapper;
            _upRepository = upRepository;
            _ulRepository = ulRepository;
        }
        public async Task<int> AddAsync(CreateUserProfileDto_Ex cup)
        {
            bool res = await _ulRepository.checkUserExist(cup.email);
            if (res == false)
            {
                CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(cup.email, "123123", cup.user_role_id, true, "", cup.created_by);
                int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

                CreateUserProfileDto cc = new CreateUserProfileDto(ulid, cup.first_name, cup.last_name, cup.emp_num,
                cup.email, cup.dob, cup.enrollment_date, cup.joining_date, cup.address, cup.postal_code, cup.city_id,
                cup.phone_number, cup.other, cup.created_by, 1);
                int upid = await _upRepository.AddAsync(_mapper.Map<userprofile>(cc));
                return upid;
            }
            else
            {
                return -5;
            }
        }
        public async Task DeleteAsync(int id)
        {
            await _upRepository.DeleteAsync(id);
        }
        public async Task<List<UserProfileDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserProfileDto>>(await _upRepository.GetAllAsync());
        }
        public async Task<PaginatedList<UserProfileDto>> GetAllAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await _upRepository.GetAllAsync(pageIndex, pageSize);
            var userProfileDtos = _mapper.Map<PaginatedList<UserProfileDto>>(paginatedList);
            return userProfileDtos;
        }

        public async Task<UserProfileDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserProfileDto>(await _upRepository.GetAsync(id));
        }
        public async Task UpdateAsync(UpdateUserProfileDto uUserProfileDto)
        {
            await _upRepository.UpdateAsync(_mapper.Map<userprofile>(uUserProfileDto));
        }
    }
}
