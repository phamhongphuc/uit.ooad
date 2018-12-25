using Microsoft.VisualStudio.TestTools.UnitTesting;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.test.Helper;

namespace uit.ooad.test._GraphQL.Booking
{
    [TestClass]
    public class _Booking
    {
        [TestMethod]
        public void Bookings()
        {
            SchemaHelper.Execute(
                @"/_GraphQL/Booking/query.bookings.gql",
                @"/_GraphQL/Booking/query.bookings.schema.json"
            );
        }
        [TestMethod]
        public void Booking()
        {
            SchemaHelper.Execute(
                @"/_GraphQL/Booking/query.booking.gql",
                @"/_GraphQL/Booking/query.booking.schema.json",
                @"/_GraphQL/Booking/query.booking.variable.json"
            );
        }
        // [TestMethod]
        // public void CreateBooking()
        // {
        //     SchemaHelper.Execute(
        //         @"/_GraphQL/Booking/mutation.createBooking.gql",
        //         @"/_GraphQL/Booking/mutation.createBooking.schema.json",
        //         @"/_GraphQL/Booking/mutation.createBooking.variable.json",
        //         p => p.PermissionManageHiringRooms = true
        //     );
        // }
    }
}
