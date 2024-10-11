using Microsoft.Data.SqlClient;

namespace ProjetoAllVoyaje.Data
{
    public class ConexaoSql : IConexaoSql
    {
        private readonly string _stringDeConexao;

        public ConexaoSql(string connStr)
        {
            _stringDeConexao = connStr;
        }

        public SqlConnection getConexao()
        {
            return new SqlConnection(_stringDeConexao);
        }
    }
}
