using Microsoft.Data.SqlClient;

namespace ContactosMAVI.Server.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection GetConnection { get; }
    }
}
