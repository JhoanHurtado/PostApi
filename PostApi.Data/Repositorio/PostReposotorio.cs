using Amazon.S3;
using PostApi.Data.Interface;
using PostApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostApi.Data.Repositorio
{
    public class PostReposotorio : RepositorioGenerico<Post>, IPostReposotorio
    {
        public PostReposotorio(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
