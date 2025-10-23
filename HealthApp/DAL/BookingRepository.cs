using HealthApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthApp.DAL
{

    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _db;

        public BookingRepository(BookingDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _db.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetItemById(int id)
        {
            return await _db.Bookings.FindAsync(id);
        }

        public async Task Create(Booking booking)
        {
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Booking booking)
        {
            _db.Bookings.Update(booking);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return false;
            }

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}