using Microsoft.AspNetCore.Mvc;
using StokEkstresiApp.Helpers;
using System;
using System.Data;

namespace StokEkstresiApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArdoDbHelper _dbHelper;

        // Constructor dependency injection ile ArdoDbHelper sınıfını alıyoruz
        public HomeController(ArdoDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetStokEkstresi(string malKodu, DateTime baslangic, DateTime bitis)
        {
            try
            {
                // Veritabanından stok ekstresi verisini alıyoruz
                DataTable stokEkstresi = _dbHelper.GetirStokEkstresi(malKodu, baslangic, bitis);

                // Sonuçları "Index" view'ına gönderiyoruz
                return View("Index", stokEkstresi);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj veriyoruz
                ViewBag.ErrorMessage = $"Veri alınırken bir hata oluştu: {ex.Message}";
                return View("Index");
            }
        }
    }
}
