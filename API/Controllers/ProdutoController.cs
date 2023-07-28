using API.Context;

using API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft;
using static System.Net.Mime.MediaTypeNames;
using API.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ProdutoController(EcommerceContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var produtos = _context.Produtos;
            return Ok(produtos);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Produto produto)
        {

            var uploadService = new FileUpload();
            produto.Foto = uploadService.UploadBase64IMage(produto.Foto, "fotos");

            _context.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = produto.Id }, produto);
        }

        [HttpGet]
        [Route("ObterPorId")]
        public Produto Obter(string id)
        {
            int Id = int.Parse(id);
            var produto = _context.Produtos.Find(Id); 
            if (produto == null)
            {
                return null;
            }
            return produto;
        }

    }
}
