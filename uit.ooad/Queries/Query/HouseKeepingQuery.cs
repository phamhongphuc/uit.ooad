using GraphQL.Types;
using uit.ooad.Businesses;
using uit.ooad.Models;
using uit.ooad.ObjectTypes;
using uit.ooad.Queries.Base;

public class HouseKeepingQuery : QueryType<HouseKeeping>
{
    public HouseKeepingQuery()
    {
        Field<NonNullGraphType<ListGraphType<NonNullGraphType<HouseKeepingType>>>>(
            _List,
            "Trả về một danh sách các công việc dọn dẹp",
            resolve: _CheckPermission_List(
                p => p.PermissionGetHouseKeeping,
                context => HouseKeepingBusiness.Get()
            )
        );

        Field<NonNullGraphType<HouseKeepingType>>(
            _Item,
            "Trả về thông tin một công việc dọn dẹp",
            _IdArgument(),
            _CheckPermission_Object(
                p => p.PermissionGetHouseKeeping,
                context => HouseKeepingBusiness.Get(_GetId<int>(context))
            )
        );
    }
}