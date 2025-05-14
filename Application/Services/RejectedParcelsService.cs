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
    public class RejectedParcelsService : IRejectedParcelsService
    {
        private readonly IRejectedParcelsRepository _repository;
        private readonly IMapper _mapper;

        public RejectedParcelsService(IRejectedParcelsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> AddAsync(RejectedParcelsDto rp, List<RejectedParcelItemsDto> rpi)
        {
            try
            {
                int rpid = await _repository.AddAsync(_mapper.Map<rejectedparcels>(rp), _mapper.Map<List<rejectedparcelitems>>(rpi));
                return rpid;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool res = await _repository.DeleteAsync(id);
            return res;
        }

        public async Task<List<RejectedParcelsDto>> GetAllAsync()
        {
            return _mapper.Map<List<RejectedParcelsDto>>(await _repository.GetAllAsync());
        }

        public async Task<List<RejectedParcelItemsDto>> GetAllItemsByRPIdAsync(int rpid)
        {
            return _mapper.Map<List<RejectedParcelItemsDto>>(await _repository.GetAllItemsByRPIdAsync(rpid));
        }

        public async Task<RPDto> GetRPwithItemsByRPIdAsync(int rpid)
        {
            return _mapper.Map<RPDto> (await _repository.GetRPwithItemsByRPIdAsync(rpid));
        }

        public async Task<RejectedParcelsDto> GetAsync(int id)
        {
            return _mapper.Map<RejectedParcelsDto>(await _repository.GetAsync(id));
        }

        public async Task<List<RejectedParcelsDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<RejectedParcelsDto>>(await _repository.GetByCIdAsync(cid));
        }
        public async Task<PaginatedList<RejectedParcelsDto>> GetByCId_P(int pageIndex, int pageSize,/* int? ordSource,*/ int? opt, string? search, int? customerID)
        {
            //var pagedOrderDetails = await _repository.GetOrderDetailsAsync(filterDto.OrderSource, filterDto.Option, filterDto.Search, filterDto.PageNumber, filterDto.PageSize);           
            var paginatedList = await _repository.GetByCId_P(pageIndex, pageSize,/* ordSource,*/ opt, search, customerID);

            var rp = _mapper.Map<PaginatedList<RejectedParcelsDto>>(paginatedList);
            return rp;

        }
    }
}
