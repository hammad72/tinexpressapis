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
    public class ShipmentService:IShipmentService
    {
        private readonly IShipmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExportRepository _exporter;
        public ShipmentService(IShipmentRepository repository, IMapper mapper,IExportRepository exporter)
        {
            _mapper = mapper;
            _repository = repository;
            _exporter = exporter;
        }
        public async Task<List<OrderSourceDto>> GetAllOrderSourceAsync()
        {
            return _mapper.Map<List<OrderSourceDto>>(await _repository.GetAllOrderSourceAsync());
        }
        public async Task<List<OptionsDto>> GetAllOptionsAsync()
        {
            return _mapper.Map<List<OptionsDto>>(await _repository.GetAllOptionsAsync());
        }
        public async Task<List<shipmentDto>>getAllShipment( int? ordSource, int? opt, string? search)
        {
            return _mapper.Map<List<shipmentDto>>(await _repository.getAllShipment(ordSource, opt, search));
        }
        public async Task<PaginatedList<shipmentDto>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search)
        {
            //var pagedOrderDetails = await _repository.GetOrderDetailsAsync(filterDto.OrderSource, filterDto.Option, filterDto.Search, filterDto.PageNumber, filterDto.PageSize);           
            var paginatedList = await _repository.GetShipmentAsync(pageIndex, pageSize, ordSource, opt, search);

            var shipm = _mapper.Map<PaginatedList<shipmentDto>>(paginatedList);
            return shipm;
     
        }

        public async Task<byte[]> ExportShipmentsToCsv(int? ordSource, int? opt, string? search)
        {
            var shipments = await _repository.getAllShipment(ordSource, opt, search);
            var result = await _exporter.ExportToCsvAsync(shipments, "shipments");
            return result.Content;
        }

        public async Task<byte[]> ExportShipmentsToExcel(int? ordSource, int? opt, string? search)
        {
            var shipments = await _repository.getAllShipment(ordSource, opt, search);
            var result = await _exporter.ExportToExcelAsync(shipments, "Shipments", "shipments");
            return result.Content;
        }
    }
}
