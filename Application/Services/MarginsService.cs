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

namespace Application.Services
{
    public class MarginsService : IMarginsService
    {
        private readonly IMarginsRepository _repository;
        private readonly IMapper _mapper;
        public MarginsService(IMarginsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> AddAsync(List<CreateMarginsDto> cMarginsDto)
        {
            bool res = await _repository.AddAsync(_mapper.Map<List<margins>>(cMarginsDto));
            return res;
        }

        public async Task<string> DeleteAsync(int id)
        {
            string res = await _repository.DeleteAsync(id);
            return res;
        }

        public async Task<List<MarginsDto>> GetAllAsync()
        {
            return _mapper.Map<List<MarginsDto>>(await _repository.GetAllAsync());
        }

        public async Task<List<MarginsDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<MarginsDto>>(await _repository.GetByCIdAsync(cid));
        }

        public async Task<MarginsDto> GetByIdAsync(int id)
        {
            return _mapper.Map<MarginsDto>(await _repository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateMarginsDto uMarginsDto)
        {
            await _repository.UpdateAsync(_mapper.Map<margins>(uMarginsDto));
        }
    }
}
