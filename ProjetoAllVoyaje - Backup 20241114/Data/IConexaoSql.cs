using Microsoft.Data.SqlClient;

namespace ProjetoAllVoyaje.Data
{
    public interface IConexaoSql
    {
        SqlConnection getConexao();
    }
}
