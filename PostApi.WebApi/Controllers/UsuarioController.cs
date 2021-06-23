using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostApi.Data.Interface;
using PostApi.Dtos.Dtos;
using PostApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioReposotorio _uSuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioReposotorio usuarioReposotorio, IMapper mapper)
        {
            _uSuarioRepositorio = usuarioReposotorio;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioDto>> Login()
        {
            try
            {
                var usuarios = await _uSuarioRepositorio.Find(1);
                return _mapper.Map<UsuarioDto>(usuarios);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UsuarioPostDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _mapper.Map<Usuario>(usuarioDto);
                    usuario.CreateAt = DateTime.UtcNow;
                    var newUsuario = await _uSuarioRepositorio.Add(usuario);
                    if (newUsuario == null)
                    {
                        return BadRequest();
                    }
                    var newUsuarioDto = _mapper.Map<UsuarioDto>(newUsuario);
                    return CreatedAtAction(nameof(Post), new { Id = newUsuarioDto.Id }, newUsuarioDto);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
    }
}
