using Projeto_bancodedados.Models;

namespace Projeto_bancodedados.Repository.Contract
{
    public interface IUsuarioRepository
    {
        //CRUD
        IEnumerable<Usuario> ObterTodosUsuarios();

        void Cadastrar(Usuario usuario);

        void Atualizar(Usuario usuario);

        Usuario ObterUsuario(int id);

        void Excluir(int id);
    }
}
