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
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IMapper _mapper;

        public SupportService(ISupportRepository supportRepository, IMapper mapper)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;

        }

        public async Task<SupportResponseDto> CreateSupportRequestAsync(SupportDto supportDto, string fileUploadPath)
        {
            // Handle file uploads
            var filePaths = new List<string>();

            if (supportDto.files != null && supportDto.files.Count > 0)
            {
                foreach (var file in supportDto.files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                        var filePath = Path.Combine(fileUploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        filePaths.Add(filePath);
                    }
                }
            }
            var objSupportRef = new SupportEnityRef
            {
                request_type = supportDto.request_type,
                total_packages = supportDto.total_packages,
                received_packages = supportDto.received_packages,
                courier_reference = supportDto.courier_reference,
                weight_dimensions = supportDto.weight_dimensions,
                package_description = supportDto.package_description,
                feedback = supportDto.feedback,
                reference_number = supportDto.reference_number,
                customer_id=supportDto.customer_id,
                files = supportDto.files 
            };
            var support = await _supportRepository.CreateSupportRequestAsync(objSupportRef, filePaths);

            return new SupportResponseDto
            {
                id = support.id,
                reference_number = support.reference_number,
                created_at = support.created_at
            };
        }

        public async Task<PaginatedList<supportcomplainsDTO>> getALL(int pageIndex, int pageSize, int? cid)
        {
            //var pagedOrderDetails = await _repository.GetOrderDetailsAsync(filterDto.OrderSource, filterDto.Option, filterDto.Search, filterDto.PageNumber, filterDto.PageSize);           
            var paginatedList = await _supportRepository.getAll(pageIndex, pageSize, cid);

            var shipm = _mapper.Map<PaginatedList<supportcomplainsDTO>>(paginatedList);
            return shipm;

        }

        public async Task<int> updateStatus(int id, int status, int userid)
        {
            var up = await _supportRepository.updateStatus(id, status, userid);
            return up;
        }
    }
}
