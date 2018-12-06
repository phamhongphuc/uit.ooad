﻿using uit.ooad.Queries.Interface;
using uit.ooad.Queries.Query;

namespace uit.ooad.Queries
{
    public class AppQuery : RootObjectGraphType
    {
        public AppQuery()
        {
            AddFields(
                new FloorQuery(),
                new EmployeeeQuery(),
                new PatronQuery(),
                new PatronKindQuery()
            );
        }
    }
}
