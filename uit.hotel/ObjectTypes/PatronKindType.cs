using System.Linq;
using GraphQL.Types;
using uit.hotel.Models;
using uit.hotel.Queries.Base;

namespace uit.hotel.ObjectTypes
{
    public class PatronKindType : ObjectGraphType<PatronKind>
    {
        public PatronKindType()
        {
            Name = nameof(PatronKind);
            Description = "Thông tin  một loại khách hàng";

            Field(x => x.Id).Description("Id của loại khách hàng");
            Field(x => x.Name).Description("Tên loại khách hàng");
            Field(x => x.Description).Description("Thông tin mô tả loại khách hàng");

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<PatronType>>>>(
                nameof(PatronKind.Patrons),
                "Danh sách các khách hàng thuộc loại khách hàng",
                resolve: context => context.Source.Patrons.ToList()
            );
        }
    }

    public class PatronKindIdInput : InputType<PatronKind>
    {
        public PatronKindIdInput()
        {
            Name = _Id;
            Description = "Input cho thông tin  một loại khách hàng";

            Field(x => x.Id).Description("Id của loại khách hàng");
        }
    }

    public class PatronKindCreateInput : InputType<PatronKind>
    {
        public PatronKindCreateInput()
        {
            Name = _Creation;

            Field(x => x.Name).Description("Tên loại khách hàng");
            Field(x => x.Description).Description("Thông tin mô tả loại khách hàng");
        }
    }

    public class PatronKindUpdateInput : InputType<PatronKind>
    {
        public PatronKindUpdateInput()
        {
            Name = _Updation;

            Field(x => x.Id).Description("Id của loại khách hàng");
            Field(x => x.Name).Description("Tên loại khách hàng");
            Field(x => x.Description).Description("Thông tin mô tả loại khách hàng");
        }
    }
}
