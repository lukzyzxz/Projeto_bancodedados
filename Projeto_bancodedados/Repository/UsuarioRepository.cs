using MySql.Data.MySqlClient;
using Projeto_bancodedados.Models;
using Projeto_bancodedados.Repository.Contract;
using System.Data;

namespace Projeto_bancodedados.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySQL;

        public UsuarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        }
        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "Update usuario set nomeUsu=@nomeUsu, Cargo=@Cargo, " +
                    "DataNasc=@DataNasc Where IdUsu=@IdUsu;",
                    conexao);

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.cargo;
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.dataNasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@IdUsu", MySqlDbType.VarChar).Value = usuario.IdUsu;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into usuario(nomeUsu, Cargo, DataNasc) " +
                                                " values (@nomeUsu, @Cargo, @DataNasc )", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nomeUsu", MySqlDbType.VarChar).Value = usuario.nomeUsu;
                cmd.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = usuario.cargo;
                // cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.DataNasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@DataNasc", MySqlDbType.VarChar).Value = usuario.dataNasc.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "delete from usuario where IdUsu=@IdUsu",
                    conexao
                );

                cmd.Parameters.AddWithValue("@IdUsu", id);

                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from usuario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(
                        new Usuario
                        {
                            IdUsu = Convert.ToInt32(dr["IdUsu"]),
                            nomeUsu = (string)dr["nomeUsu"],
                            cargo = (string)dr["Cargo"],
                            dataNasc = Convert.ToDateTime(dr["DataNasc"])

                        });
                }
                return UsuarioList;
            }
        }



        public Usuario ObterUsuario(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM USUARIO WHERE IdUsu = @IdUsu", conexao);
                cmd.Parameters.AddWithValue("@IdUsu", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    usuario.IdUsu = Convert.ToInt32(dr["IdUsu"]);
                    usuario.nomeUsu = (string)(dr["nomeUsu"]);
                    usuario.cargo = (string)(dr["Cargo"]);
                    usuario.dataNasc = Convert.ToDateTime(dr["DataNasc"]);

                }
                return usuario;
            }
        }
    }
}
