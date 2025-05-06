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
    public class AgingReportService : IAgingReportService
    {
        private readonly IAgingReportRepository _repository;
        private readonly IMapper _mapper;

        public AgingReportService(IAgingReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedListForAgingReport<AgingReport>> GetAgingReportAsync(AgingReportDTO filters)
        {
            // Default excluded statuses: Delivered(5), Failed(6), Lost(7), Returned(8), Cancelled(9)
            //var excludedStatusIds = new[] { 5, 6, 7, 8, 9 };

            // Calculate date range based on filter
            var (fromDate, toDate) = CalculateDateRange(filters.DateRange);

            var result = await _repository.GetAgingReportAsync(
                fromDate,
                toDate,
                 //excludedStatusIds,
                 filters.OrderNumber,
                filters.statusID,
               
                //filters.StatusIds,
                filters.PageNumber,
                filters.PageSize);

            return new PaginatedListForAgingReport<AgingReport>
            {
                Items = result.Items,
                TotalCount = result.TotalCount,
                PageIndex = result.PageIndex,  
                PageSize = result.PageSize,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize)
            };
        }

        private (DateTime? fromDate, DateTime? toDate) CalculateDateRange(string dateRange)
        {
            //if (dateRange == "custom" && customFromDate.HasValue && customToDate.HasValue)
            //{
            //    return (customFromDate, customToDate);
            //}

            return dateRange switch
            {
                "3days" => (DateTime.Now.AddDays(-3), null),
                "week" => (DateTime.Now.AddDays(-7), null),
                "month" => (DateTime.Now.AddMonths(-1), null),
                "year" => (DateTime.Now.AddYears(-1), null),
                _ => (DateTime.Now.AddDays(-30), null) // default to last 30 days
            };
        }
    }
}
