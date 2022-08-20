using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using Shopping.Client.Data;
using Shopping.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("ShoppingAPIClient");
        }

        public async Task<IActionResult> Index()
        {
            //var response = await _httpClient.GetAsync("/product");
            //var content = await response.Content.ReadAsStringAsync();
            //return View(JsonConvert.DeserializeObject<IEnumerable<Product>>(content));

            return View(new List<Product>());
        }
        public IActionResult GenerateQRCode(string QRString)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRString, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap bitMap = qrCode.GetGraphic(20);
                bitMap.Save(ms, ImageFormat.Png);
                var teste =  "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                TesteModel testeModel = new TesteModel();
                testeModel.QrCodeImage = teste;
                return RedirectToAction("Teste", testeModel);
            }
        }

        public IActionResult GerarImagemQrCode(string chave)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(chave, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap bitMap = qrCode.GetGraphic(20);
                bitMap.Save(ms, ImageFormat.Png);
                var base64Image = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                TesteModel model = new TesteModel();
                model.QrCodeImage = base64Image;
                return View(model);
            }
                              
        }

        public IActionResult Privacy()
        {


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
