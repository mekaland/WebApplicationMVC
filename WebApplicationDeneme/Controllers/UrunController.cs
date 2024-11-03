using Microsoft.AspNetCore.Mvc;
using WebApplicationDeneme.Models;

namespace WebApplicationDeneme.Controllers
{
	public class UrunController : Controller
	{

		private static List<Urun> urunListesi = new List<Urun>
		{
			new Urun { UrunId = 1, UrunAd = "Laptop" ,Kategori= "bilgisayar", Fiyat =1500},
			new Urun {UrunId = 2 , UrunAd = "dizüstü", Kategori = "elektronik", Fiyat = 2000}
		};
			
		public IActionResult Index()
		{
			return View(urunListesi);
		}
		//yeni ürün ekleme formu
		public IActionResult Ekle()
		{
			return View();
		}
        //yeni ürün ekleme işlemi
        [HttpPost]
        public IActionResult Ekle(Urun yeniUrun)
		{
			yeniUrun.UrunId = urunListesi.Count + 1;
			urunListesi.Add(yeniUrun);
			return RedirectToAction("Index");
		}
        // Ürün düzenleme formu
        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            var urun = urunListesi.FirstOrDefault(u => u.UrunId == id);
            if (urun == null)
            {
                return NotFound(); // Eğer ürün bulunamazsa 404 döndür
            }
            return View(urun); // Ürünü düzenleme sayfasına gönder
        }

        // Ürün düzenleme işlemi (POST isteği)
        [HttpPost]
        public IActionResult Duzenle(Urun duzenlenenUrun)
        {
            var urun = urunListesi.FirstOrDefault(u => u.UrunId == duzenlenenUrun.UrunId);
            if (urun == null)
            {
                return NotFound(); // Eğer ürün bulunamazsa 404 döndür
            }

            urun.UrunAd = duzenlenenUrun.UrunAd;
            urun.Kategori = duzenlenenUrun.Kategori;
            urun.Fiyat = duzenlenenUrun.Fiyat;
            return RedirectToAction("Index"); // Güncelleme sonrası ana sayfaya yönlendir
        }

        //ÜRÜN SİLME İŞLEMİ
        public IActionResult Sil(int id)
        {
            var urun = urunListesi.FirstOrDefault(u => u.UrunId == id);
            urunListesi.Remove(urun);
            return RedirectToAction("Index");
        }

    }
}
