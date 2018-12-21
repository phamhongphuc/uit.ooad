using System.Collections.Generic;
using System.Threading.Tasks;
using uit.ooad.DataAccesses;
using uit.ooad.Models;

namespace uit.ooad.Businesses
{
    public class BillBusiness
    {
        public static Task<Bill> Add(Bill bill)
        {
            var billInDatabase = Get(bill.Id);
            if (billInDatabase != null) return null;

            bill.Patron = bill.Patron.GetManaged();
            bill.Employee = null;

            return BillDataAccess.Add(bill);
        }

        public static Bill Get(int billId) => BillDataAccess.Get(billId);
        public static IEnumerable<Bill> Get() => BillDataAccess.Get();
    }
}
