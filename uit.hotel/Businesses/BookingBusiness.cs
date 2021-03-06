using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uit.hotel.DataAccesses;
using uit.hotel.Models;

namespace uit.hotel.Businesses
{
    public static class BookingBusiness
    {
        public static int _CheckInDayTime = 13;
        public static int _CheckOutDayTime = 11;
        public static int _CheckInNightTime = 21;
        public static int _CheckOutNightTime = 11;
        public static int _MaxCheckInNightTime = 6;
        public static int _ToleranceTimeSpan = 4;
        public static int _HourTimeSpan = 4;

        public static Task<Booking> CheckIn(Employee employee, int bookingId)
        {
            var bookingInDatabase = Get(bookingId);
            if (bookingInDatabase == null)
                throw new Exception("Mã Booking không tồn tại");
            if (bookingInDatabase.Status != BookingStatusEnum.Booked)
                throw new Exception("Phòng đã được check-in, không thể check-in lại");
            if (bookingInDatabase.Room.IsClean == false)
                throw new Exception("Phòng chưa được dọn, không thể check-in");
            if (!bookingInDatabase.IsEmpty(true))
                throw new Exception("Phòng đang/sẽ được sử dụng, không thể check-in");

            return BookingDataAccess.CheckIn(employee, bookingInDatabase);
        }

        public static Task<Booking> CheckOut(Employee employee, int bookingId)
        {
            var bookingInDatabase = Get(bookingId);
            if (bookingInDatabase == null)
                throw new Exception("Mã Booking không tồn tại");
            if (bookingInDatabase.Status != BookingStatusEnum.CheckedIn)
                throw new Exception("Booking chưa check-in, không thể check-out");

            return BookingDataAccess.CheckOut(bookingInDatabase, employee);
        }

        public static void Cancel(int bookingId)
        {
            var bookingInDatabase = Get(bookingId);
            if (bookingInDatabase == null)
                throw new Exception("Mã Booking không tồn tại");

            if (bookingInDatabase.Status != BookingStatusEnum.Booked)
                throw new Exception("Không thể hủy đặt phòng. Booking đã hoặc đang được sử dụng.");

            BookingDataAccess.Delete(bookingInDatabase);
        }

        public static Task<Booking> AddBookingToBill(Employee employee, Bill bill, Booking booking)
        {
            bill = bill.GetManaged();
            booking.Room = booking.Room.GetManaged();

            if (!booking.Room.IsActive)
                throw new Exception("Phòng có Id: " + booking.Room.Id + " đã ngưng hoạt động");

            if (booking.BookCheckInTime >= booking.BookCheckOutTime || booking.BookCheckInTime < DateTimeOffset.Now)
                throw new Exception("Ngày check-in, check-out dự kiến không hợp lệ");

            if (!booking.IsEmpty())
                throw new Exception("Phòng đã được đặt hoặc đang được sử dụng");

            return BookingDataAccess.AddBookingToBill(employee, bill, booking);
        }

        public static Booking Get(int bookingId) => BookingDataAccess.Get(bookingId);
        public static IEnumerable<Booking> Get() => BookingDataAccess.Get();
    }
}
