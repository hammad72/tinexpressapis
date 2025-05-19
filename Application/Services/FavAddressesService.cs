using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class FavAddressesService : IFavAddressesService
    {
        private readonly IFavAddressesRepository _repository;
        private readonly IMapper _mapper; 
        public FavAddressesService(IFavAddressesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> AddAsync(CreateFavAddressesDto cFavAddressesDto)
        {
            int faid = await _repository.AddAsync(_mapper.Map<favaddresses>(cFavAddressesDto));
            return faid;
        }

        public async Task<string> DeleteAsync(int id)
        {
            string res = await _repository.DeleteAsync(id);
            return res;
        }

        public async Task<List<FavAddressesDto>> GetAllAsync()
        {
            return _mapper.Map<List<FavAddressesDto>>(await _repository.GetAllAsync());
        }

        public async Task<FavAddressesDto> GetByIdAsync(int id)
        {
            return _mapper.Map<FavAddressesDto>(await _repository.GetAsync(id));
        }

        public async Task<List<FavAddressesDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<FavAddressesDto>>(await _repository.GetByCIdAsync(cid));
        }

        public async Task UpdateAsync(UpdateFavAddressesDto uFavAddressesDto)
        {
            await _repository.UpdateAsync(_mapper.Map<favaddresses>(uFavAddressesDto));
        }
    }
}
