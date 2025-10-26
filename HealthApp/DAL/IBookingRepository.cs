using HealthApp.Models;

namespace HealthApp.DAL
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<Booking?> GetItemById(int id);
        //filter metoden
        Task<IEnumerable<Booking>> GetBookingsByMonthAsync(int year, int month);
        Task Create(Booking booking);
        Task Update(Booking booking);
        Task<bool> Delete(int id);
    }
}