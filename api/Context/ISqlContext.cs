using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace api.Context
{
    public interface ISqlContext
    {
        IDbConnection GetSqlConnection();
    }
}