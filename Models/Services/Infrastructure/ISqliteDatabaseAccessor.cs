using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public interface ISqliteDatabaseAccessor
    {
        Task<int> CommandAsync(FormattableString formattableCommand);
        Task<DataSet> QueryAsync(FormattableString formattableQuery);
        Task<T> QueryScalarAsync<T>(FormattableString formattableQuery);
    }
}