using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

namespace uit.ooad.Queries.Query
{
    public class FloorQuery : QueryType<Floor>
    {
        public FloorQuery()
        {
            Field<ListGraphType<FloorType>>(
                _List,
                "Trả về một danh sách các tầng",
                resolve: context => FloorBusiness.Get()
            );
            Field<FloorType>(
                _Item,
                "Trả về thông tin một tầng",
                IdArgument(),
                context => FloorBusiness.Get(GetId<int>(context))
            );
        }
    }
}
