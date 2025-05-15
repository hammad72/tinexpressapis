using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DownloadLabelService : IDownloadLabelService
    {
        private readonly IDownloadLabelRepository _repository;
        private readonly IMapper _mapper;

        public DownloadLabelService(IDownloadLabelRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<orderdetails?> GetLabelAsync(string refNum)
        {
            //return _mapper.Map<OrderDetailsDto>(await _repository.GetByIdAsync(refNum));
            try
            {
                var entity = await _repository.GetByIdAsync(refNum);
                return entity is null ? null : entity;// _mapper.Map<orderdetails?>(entity);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
