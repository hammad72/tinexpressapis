namespace Domain.Interfaces
{
    public interface IGetQuoteRepository
    {
        Task<object> getQuoteZoom2u(object data);
        Task<string> getQuoteCourierPlease(object data);
    }
}
