using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using Newtonsoft.Json;
using API.Models;
using API.Context;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        private readonly EcommerceContext _context;

        private static readonly HttpClient client = new HttpClient();

        public MercadoPagoController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePreference(string id, int qtd)
        {
            ProdutoController produtos = new ProdutoController(_context);

            Produto produto = produtos.Obter(id);

            string token = "TEST-2450536401786321-070318-881767511e3715a6067bd0adbb40d523-622149077";

            string content = $@"{{
    ""items"": [
  {{
      ""id"": ""{produto.Id}"",
      ""title"": ""{produto.Nome}"",
      ""quantity"": {qtd},
      ""unit_price"": {produto.Valor}
  }}
]
}}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.mercadopago.com/checkout/preferences"),
                Headers =
            {
                { "Accept", "application/json" },
                { "Authorization", $"Bearer {token}" },
            },
                Content = new StringContent(content)
                {
                    Headers =
                    {
                    ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return Ok(body);
            }
        }

    }
}
