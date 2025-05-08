using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CouriersService : ICouriersService
    {
        private readonly ICouriersRepository _cRepository;
        private readonly ICourierUserProfileRepository _cupRepository;
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public CouriersService(ICouriersRepository cRepository, IMapper mapper, ICourierUserProfileRepository cupRepository, IUserLoginsRepository ulRepository)
        {
            _mapper = mapper;
            _cRepository = cRepository;
            _cupRepository = cupRepository;
            _ulRepository = ulRepository;
        }
        
        public async Task<int> AddAsync(CreateCouriersDto cCouriersDto)
        {
            bool res = await _ulRepository.checkUserExist(cCouriersDto.email);
            if (res == false)
            {
                int cid = await _cRepository.AddAsync(_mapper.Map<couriers>(cCouriersDto));

            CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(cCouriersDto.email, "123123", 9, true, "", cCouriersDto.created_by);
            int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

            CreateCourierUserProfileDto cCourierUserProfileDto = new CreateCourierUserProfileDto(
                ulid, cCouriersDto.email, null, null, cCouriersDto.email, null, cCouriersDto.enrollment_date, null, 
                cCouriersDto.primary_location, cCouriersDto.postal_code, cCouriersDto.city_id, cCouriersDto.phone, null, cid,
                cCouriersDto.created_by, 1);

            int cpid = await _cupRepository.AddAsync(_mapper.Map<courieruserprofile>(cCourierUserProfileDto));
            return cid;
            }
            else
            {
                return -5;
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _cRepository.DeleteAsync(id);
        }

        public async Task<List<CouriersDto>> GetAllAsync()
        {
            return _mapper.Map<List<CouriersDto>>(await _cRepository.GetAllAsync());
        }

        public async Task<CouriersDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CouriersDto>(await _cRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateCouriersDto uCouriersDto)
        {
            await _cRepository.UpdateAsync(_mapper.Map<couriers>(uCouriersDto));
        }
    }
}
