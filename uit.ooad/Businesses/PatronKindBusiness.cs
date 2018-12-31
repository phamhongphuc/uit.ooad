using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uit.ooad.DataAccesses;
using uit.ooad.Models;

namespace uit.ooad.Businesses
{
    public class PatronKindBusiness
    {
        public static Task<PatronKind> Add(PatronKind patronKind) => PatronKindDataAccess.Add(patronKind);
        public static PatronKind Get(int patronKindId) => PatronKindDataAccess.Get(patronKindId);
        public static IEnumerable<PatronKind> Get() => PatronKindDataAccess.Get();

        public static Task<PatronKind> Update(PatronKind patronKind)
        {
            PatronKind patronKindInDatabase = GetAndCheckValid(patronKind.Id);
            return PatronKindDataAccess.Update(patronKindInDatabase, patronKind);
        }

        private static PatronKind GetAndCheckValid(int patronKindId)
        {
            var patronKindInDatabase = Get(patronKindId);
            if (patronKindInDatabase == null)
                throw new Exception("Loại khách hàng có ID: " + patronKindId + " không tồn tại");
            return patronKindInDatabase;
        }

        public static void Delete(int patronKindId)
        {
            var patronKindInDatabase = GetAndCheckValid(patronKindId);
            if (patronKindInDatabase.Patrons.Count() > 0)
                throw new Exception("Loại khách hàng đang được sử dụng. Không thể cập xóa.");

            PatronKindDataAccess.Delete(patronKindInDatabase);
        }
    }
}
