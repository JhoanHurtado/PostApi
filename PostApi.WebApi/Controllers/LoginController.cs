using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostApi.Data.Interface;
using PostApi.Dtos.Dtos;
using PostApi.Models.Models;
using PostApi.WebApi.Servicios;
using System;
using System.Threading.Tasks;

namespace PostApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioReposotorio _uSuarioRepositorio;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public LoginController(IUsuarioReposotorio usuarioReposotorio, IMapper mapper, TokenService tokenService)
        {
            _uSuarioRepositorio = usuarioReposotorio;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioDto>> Post(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                var usuario = _mapper.Map<Usuario>(loginDto);

                if (!await _uSuarioRepositorio.ExistUser(usuario))
                {
                    return NotFound();
                }
                var result = await _uSuarioRepositorio.ValidarLogin(usuario);
                if (!result.Resultado)
                {
                    return BadRequest("Usuario o contraseña incorrectos.");
                }
            var usuarioDto = _mapper.Map<UsuarioDto>(result.Usuario);
            usuarioDto.token = _tokenService.GenerarTokent(result.Usuario);
            return usuarioDto;
        }
    }
}
