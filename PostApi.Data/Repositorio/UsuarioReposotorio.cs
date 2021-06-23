using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostApi.Data.Interface;
using PostApi.Models.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PostApi.Data.Repositorio
{
    public class UsuarioReposotorio : RepositorioGenerico<Usuario>, IUsuarioReposotorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Usuario> _dbSet;
        private readonly IPasswordHasher<Usuario> _passwordHasher;


        public UsuarioReposotorio(AppDbContext dbContext, 
            IPasswordHasher<Usuario> passwordHasher) : base(dbContext)
        {
            _appDbContext = dbContext;
            this._passwordHasher = passwordHasher;
            _dbSet = _appDbContext.Set<Usuario>();
        }

        public async Task<bool> ExistUser(Usuario usuario)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(usuario.Email));
            if(user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<(bool Resultado, Usuario Usuario)> ValidarLogin(Usuario usuario)
        {
            var usuarioBd = await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(usuario.Email));
            if(usuarioBd != null)
            {
                try
                {
                    var resultado = _passwordHasher.VerifyHashedPassword(usuarioBd, usuarioBd.Password, usuario.Password);
                    return (resultado == PasswordVerificationResult.Success ? true : false,usuarioBd);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return (false, null);
        }

        public new async Task<Usuario> Add(Usuario usuario)
        {
            usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);
            _dbSet.Add(usuario);
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }
    }
}
