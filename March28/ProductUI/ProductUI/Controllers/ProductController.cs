using Microsoft.AspNetCore.Mvc;
using ProductUI.Models;

namespace ProductUI.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();

        string baseUrl = "https://localhost:7129/api/Product"; // 🔥 change port if needed

        // GET ALL
        public async Task<IActionResult> Index()
        {
            var data = await client.GetFromJsonAsync<List<Product>>(baseUrl);
            return View(data);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await client.PostAsJsonAsync(baseUrl, product);
            return RedirectToAction("Index");
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await client.DeleteAsync($"{baseUrl}/{id}");
            return RedirectToAction("Index");
        }

        // EDIT PAGE
        public async Task<IActionResult> Edit(int id)
        {
            var product = await client.GetFromJsonAsync<Product>($"{baseUrl}/{id}");
            return View(product);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await client.PutAsJsonAsync(baseUrl, product);
            return RedirectToAction("Index");
        }
    }
}