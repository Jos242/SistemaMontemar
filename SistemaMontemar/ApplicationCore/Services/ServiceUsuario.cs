using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Utils;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuarioById(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioById(id);
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }
        public Usuario GetUsuario(string email, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(email, crytpPasswd);
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.Password = Cryptography.EncrypthAES(usuario.Password);
            return repository.Save(usuario);
        }

    }
}
