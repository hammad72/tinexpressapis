using Domain.Entities;
using Domain.Interfaces;
using Google;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly OrderDbContext _context;
        private readonly ICustomerUserProfileRepository _customerUserProfileRepository;

        public SupportRepository(OrderDbContext context, ICustomerUserProfileRepository customerUserProfileRepository)
        {
            _context = context;
            _customerUserProfileRepository = customerUserProfileRepository;
        }

        public async Task<supportcomplains> CreateSupportRequestAsync(SupportEnityRef supportDto, List<string> filePaths)
        {
            try
            {
                var customer = await _customerUserProfileRepository.GetAsync(supportDto.customer_id);
                var support = new supportcomplains
                {
                    request_type = supportDto.request_type,
                    total_packages = supportDto.total_packages,
                    receivedpackages = supportDto.received_packages,
                    courier_reference = supportDto.courier_reference,
                    weight_dimensions = supportDto.weight_dimensions,
                    package_description = supportDto.package_description,
                    feedback = supportDto.feedback,
                    reference_number = supportDto.reference_number,
                    customer_id=supportDto.customer_id,
                    customer_name=customer.first_name,
                    created_by=supportDto.customer_id,
                    status=1,
                    file_paths = filePaths.Count > 0 ? string.Join(",", filePaths) : null
                };

                await _context.supportcomplains.AddAsync(support);
                await _context.SaveChangesAsync();

                return support;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
      
        }

        public async Task<PaginatedList<supportcomplains>> getAll(int pageIndex, int pageSize, int? cid)
        {
            try
            {
                var query = _context.supportcomplains.AsQueryable();
                if (cid != null)
                {
                    query = query.Where(x => x.customer_id == cid);
                }
         

                var projectedQuery = query
                .OrderByDescending(o => o.id);

                var totalCount = await projectedQuery.CountAsync();
                var items = await projectedQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<supportcomplains>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<int> updateStatus(int id , int status, int userid)
        {
            var entity = await _context.supportcomplains.FindAsync(id);
            if (entity == null)
                return 0;

            entity.status = status;
            entity.updated_by = userid;
            entity.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
