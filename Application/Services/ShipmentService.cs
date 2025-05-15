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
        private readonly IOrderDetailsRepository _orderDetailRepo;
        private readonly IUserLoginsRepository _userLoginRepo;
        public ShipmentService(IShipmentRepository repository, IMapper mapper,IExportRepository exporter,IOrderDetailsRepository orderDetailsRepository,IUserLoginsRepository userLoginsRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _exporter = exporter;
            _orderDetailRepo = orderDetailsRepository;
            _userLoginRepo = userLoginsRepository;
        }
        public async Task<List<OrderSourceDto>> GetAllOrderSourceAsync()
        {
            return _mapper.Map<List<OrderSourceDto>>(await _repository.GetAllOrderSourceAsync());
        }
        public async Task<List<OptionsDto>> GetAllOptionsAsync()
        {
            return _mapper.Map<List<OptionsDto>>(await _repository.GetAllOptionsAsync());
        }
        public async Task<List<shipmentDto>>getAllShipment( int? ordSource, int? opt, string? search, int? customerID)
        {
            return _mapper.Map<List<shipmentDto>>(await _repository.getAllShipment(ordSource, opt, search, customerID));
        }
        public async Task<PaginatedList<shipmentDto>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search,int? customerID)
        {
            //var pagedOrderDetails = await _repository.GetOrderDetailsAsync(filterDto.OrderSource, filterDto.Option, filterDto.Search, filterDto.PageNumber, filterDto.PageSize);           
            var paginatedList = await _repository.GetShipmentAsync(pageIndex, pageSize, ordSource, opt, search,customerID);

            var shipm = _mapper.Map<PaginatedList<shipmentDto>>(paginatedList);
            return shipm;
     
        }

        public async Task<byte[]> ExportShipmentsToCsv(int? ordSource, int? opt, string? search, int? customerID)
        {
            var shipments = await _repository.getAllShipment(ordSource, opt, search, customerID);
            var result = await _exporter.ExportToCsvAsync(shipments, "shipments");
            return result.Content;
        }

        public async Task<byte[]> ExportShipmentsToExcel(int? ordSource, int? opt, string? search, int? customerID)
        {
            var shipments = await _repository.getAllShipment(ordSource, opt, search, customerID);
            var result = await _exporter.ExportToExcelAsync(shipments, "Shipments", "shipments");
            return result.Content;
        }
       public async Task<PaginatedList<OrderItemsDto>> getOrderItemsByConsignment(int pageIndex, int pageSize, string consignment)
        {
            var paginatedList = await _repository.getOrderItemsByConsignment(pageIndex, pageSize, consignment);

            var shipm = _mapper.Map<PaginatedList<OrderItemsDto>>(paginatedList);
            return shipm;

        }
        public async Task<ShipmentDetailOrderItemsDTO> getShipmentItemsAsync(int pageIndex, int pageSize, string consignment)
        {
            try
            {
                var orderDetail = await _orderDetailRepo.getOrderByConsignmentAsync(consignment);
                if (orderDetail == null)
                    return null;

                var userInfo = await _userLoginRepo.GetAsync((int)orderDetail.created_by);

                var paginatedListTask = _repository.getOrderItemsByConsignment(pageIndex, pageSize, consignment);
                var senderReceiverTask = Task.Run(() => _repository.getSenderRecieverOrderItems(orderDetail));
                var summaryTask = Task.Run(() => _repository.getSummaryOrderItems(orderDetail));
                var trackingTask = Task.Run(() => _repository.getTrackingOrderItems(orderDetail));
                var courierTask = Task.Run(() => _repository.getCourierInfo(orderDetail));

                await Task.WhenAll(paginatedListTask, senderReceiverTask, summaryTask, trackingTask,courierTask);

                //var activityLog = new ActivityLogOrderItemsDTO
                //{
                //    username = userInfo?.username,
                //    created_by = userInfo?.id,
                //    order_status_change_date = orderDetail.order_status_change_date
                //};

                var senderReceiverDto = _mapper.Map<SenderRecieverOrderItemsDto>(await senderReceiverTask);
                var summaryDto = _mapper.Map<SummaryOrderItemsDTO>(await summaryTask);
                var trackingDto = _mapper.Map<TrackingOrderItemsDTO>(await trackingTask);
                var courierDto = _mapper.Map<CourierInfoOrderItemsDTO>(await courierTask);

                return new ShipmentDetailOrderItemsDTO
                {
                    SenderRecieverOrderItemsDto = senderReceiverDto,
                    SummaryOrderItemsDTO = summaryDto,
                    TrackingOrderItemsDTO = trackingDto,
                    CourierInfoOrderItemsDTO= courierDto,
                    //ActivityLogOrderItemsDTO = activityLog,
                    paginatedListOrderItems = await paginatedListTask,
                 
                };
            }
            catch (Exception ex)
            {

                //_logger.LogError(ex, "Error getting shipment items for consignment {Consignment}", consignment);
                throw;
            }
        }


        public async Task<int> changeOrderStatus(string consignment,int order_status_id)
        {
            try
            {
                var orderDetail = await _orderDetailRepo.getOrderByConsignmentAsync(consignment);
                if (orderDetail == null) return 0;

                orderDetail.order_status_id = order_status_id;
                orderDetail.order_status_change_date=DateTime.Now;
                var uObj = await _orderDetailRepo.UpdateOrderDetail(orderDetail);
                if (uObj == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
