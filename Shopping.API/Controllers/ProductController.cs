using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.API.Data;
using Shopping.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]

        public IEnumerable<Product> Get()
        {
            return ProductContext.Products;
        }
    }
}
