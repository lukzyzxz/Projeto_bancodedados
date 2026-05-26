using Microsoft.AspNetCore.Mvc;
using Projeto_bancodedados.Models;
using Projeto_bancodedados.Repository.Contract;

namespace Projeto_bancodedados.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

        }
        public IActionResult Index()
        {
            return View(_usuarioRepository.ObterTodosUsuarios());
        }

        [HttpGet]
        public IActionResult CadastrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Cadastrar(usuario);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AtualizarUsuario(int id)
        {
            return View(_usuarioRepository.ObterUsuario(id));
        }

        [HttpPost]
        public IActionResult AtualizarUsuario(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);

            return RedirectToAction(nameof(Index));
        }
    }
}
