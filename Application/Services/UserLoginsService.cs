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

        public async Task<UserLoginsDto> GetByEmailandPassword(string email, string password)
        {
            var user = await _ulRepository.GetByEmailandPassword(email, password);
            if (user == null)
            {
                Console.WriteLine("User not found for email: " + email);
                return null;
            }
            var mappedUser = _mapper.Map<UserLoginsDto>(user);
            if (mappedUser == null)
            {
                Console.WriteLine("Mapping failed for user.");
            }

            return mappedUser;
            //return user != null ? _mapper.Map<UserLoginsDto>(user) : null;
        }

        public async Task AddLoginDetailAsync(CreateLoginDetailDto ld)
        {
            //await _ulRepository.AddAsync
            await _ulRepository.AddLoginDetailAsync(_mapper.Map<logindetail>(ld));
        }
        public async Task AddAndUpdateLoginModel(CreateAndUpdateLoginModelDto ld)
        {
            //await _ulRepository.AddAsync
            await _ulRepository.AddUpdateLoginModel(_mapper.Map<loginmodel>(ld));
        }

        public async Task<LoginModelDto> GetLoginModel(string refreshToken)
        {
            var user = await _ulRepository.GetLoginModel(refreshToken);
            return user != null ? _mapper.Map<LoginModelDto>(user) : null;
        }

        public async Task<SocialInfoDTO> socialGoogle(string requestToken)
        {
            var user = await _ulRepository.socialGoogle(requestToken);
            return user != null ? _mapper.Map<SocialInfoDTO>(user) : null;
        }

        public async Task<SocialInfoDTO> socialFb(string requestToken)
        {
            var user = await _ulRepository.socialFb(requestToken);
            return user != null ? _mapper.Map<SocialInfoDTO>(user) : null;
        }

        public async Task<UserLoginsDto> GetByEmaila(string email)
        {
            var user = await _ulRepository.GetByEmail(email);
            if (user == null)
            {
                Console.WriteLine("User not found for email: " + email);
                return null;
            }
            var mappedUser = _mapper.Map<UserLoginsDto>(user);
            if (mappedUser == null)
            {
                Console.WriteLine("Mapping failed for user.");
            }

            return mappedUser;
        }
    }
}
