using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDownloadLabelService
    {
        Task<orderdetails?> GetLabelAsync(string refNum);
    }
}
