using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserLoginsService : IUserLoginsService
    {
        private readonly IUserLoginsRepository _ulRepository;
        private readonly IMapper _mapper;
        public UserLoginsService(IUserLoginsRepository ulRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ulRepository = ulRepository;
        }
        public async Task<int> AddAsync(CreateUserLoginsDto cUserLoginsDto)
        {
            int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));
            return ulid;
        }

        public async Task DeleteAsync(int id)
        {
            await _ulRepository.DeleteAsync(id);
        }

        public async Task<List<UserLoginsDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserLoginsDto>>(await _ulRepository.GetAllAsync());
        }

        public async Task<UserLoginsDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserLoginsDto>(await _ulRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateUserLoginsDto uUserLoginsDto)
        {
            await _ulRepository.UpdateAsync(_mapper.Map<userlogins>(uUserLoginsDto));
        }
    }
}
