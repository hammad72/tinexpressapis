using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserTypesService : IUserTypesService
    {
        private readonly IUserTypesRepository _uRepository;
        private readonly IMapper _mapper;
        public UserTypesService(IUserTypesRepository uRepository, IMapper mapper)
        {
            _mapper = mapper;
            _uRepository = uRepository;
        }

        public async Task<int> AddAsync(CreateUserTypesDto cUserTypesDto)
        {
            int ptid = await _uRepository.AddAsync(_mapper.Map<usertypes>(cUserTypesDto));
            return ptid;
        }

        public async Task DeleteAsync(int id)
        {
            await _uRepository.DeleteAsync(id);
        }

        public async Task<List<UserTypesDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserTypesDto>>(await _uRepository.GetAllAsync());
        }

        public async Task<List<UserTypesDto>> GetUserTypesAllByPlatform(int pid)
        {
            return _mapper.Map<List<UserTypesDto>>(await _uRepository.GetUserTypesAllByPlatform(pid));
        }

        public async Task<UserTypesDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserTypesDto>(await _uRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateUserTypesDto uUserTypesDto)
        {
            await _uRepository.UpdateAsync(_mapper.Map<usertypes>(uUserTypesDto));
        }
    }
}
