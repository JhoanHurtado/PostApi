using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.Data.Interface;
using PostApi.Dtos.Dtos;
using PostApi.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostReposotorio _postReposotorio;
        private readonly IMapper _mapper;

        public PostController(IPostReposotorio postReposotorio, IMapper mapper)
        {
            _postReposotorio = postReposotorio;
            _mapper = mapper;
        }


        // GET: api/<postController>
        [HttpGet("{usuarioId}/{filtro}/{skip}/{take}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetFilter(int usuarioId, string? filtro, int skip, int take = 10)
        {
            try
            {
                var posts = await _postReposotorio.GetByCondition(x => (x.Content.Contains(filtro) || x.Titulo.Contains(filtro)) && x.IdUsuario == usuarioId, skip, take);
                if(filtro.Equals("null") || filtro.Equals(""))
                {
                    posts = await _postReposotorio.GetByCondition(x => x.IdUsuario == usuarioId, skip, take);
                }
                if(posts.Count< 1)
                {
                    return NotFound();
                }else
                {
                    return _mapper.Map<List<PostDto>>(posts);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // GET api/<postController>/5
        [HttpDelete("{id}/{post}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostDto>> Delete(int? id,int? post)
        {
            if (id == null || post == null)
            {
                return BadRequest();
            }
            var deleteP = await _postReposotorio.DeleteByCondition(p => p.IdUsuario == id && p.Id == post);
            if (deleteP)
            {
                return NotFound();
            }
            return _mapper.Map<PostDto>(deleteP);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] PostDto postDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filepath = "img/" +postDto.file.FileName;
                    
                    using(var stream = System.IO.File.Create(filepath))
                    {
                        await postDto.file.CopyToAsync(stream);
                    }
                    var post = _mapper.Map<Post>(postDto);
                    post.CreateAt = DateTime.UtcNow;
                    post.Img = Path.GetFileName(postDto.file.FileName);
                    var newPost = await _postReposotorio.Add(post);
                    if (newPost == null)
                    {
                        return BadRequest();
                    }
                    var newPostDto = _mapper.Map<PostDto>(newPost);
                    return CreatedAtAction(nameof(Post), new { Id = newPostDto.Id }, newPostDto);
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
