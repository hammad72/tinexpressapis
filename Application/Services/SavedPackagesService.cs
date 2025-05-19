using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SavedPackagesService : ISavedPackagesService
    {
        private readonly ISavedPackagesRepository _repository;
        private readonly IMapper _mapper;
        public SavedPackagesService(ISavedPackagesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> AddAsync(List<CreateSavedPackagesDto> cSavedPackagesDto)
        {
            int spid = await _repository.AddAsync(_mapper.Map< List<savedpackages>>(cSavedPackagesDto));
            return spid;
        }

        public async Task<string> DeleteAsync(int id)
        {
            string res = await _repository.DeleteAsync(id);
            return res;
        }

        public async Task<List<SavedPackagesDto>> GetAllAsync()
        {
            return _mapper.Map<List<SavedPackagesDto>>(await _repository.GetAllAsync());
        }

        public async Task<List<SavedPackagesDto>> GetAsyncBySPCode(string spCode)
        {
            return _mapper.Map<List<SavedPackagesDto>>(await _repository.GetAsyncBySPCode(spCode));
        }

        public async Task<List<SavedPackagesDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<SavedPackagesDto>>(await _repository.GetByCIdAsync(cid));
        }

        public async Task UpdateAsync(UpdateSavedPackagesDto uSavedPackagesDto)
        {
            throw new NotImplementedException();
        }
    }
}
