using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Entities;

namespace AppView.Controllers
{
    public class GiaoDichTaiChinhsController : Controller
    {
        private readonly HttpClient _httpClient;

        public GiaoDichTaiChinhsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7282/api/"); 
        }

        public async Task<IActionResult> Index()
        {
            var giaoDichTaiChinhList = await _httpClient.GetFromJsonAsync<List<GiaoDichTaiChinh>>("GiaoDichTaiChinh");
            return View(giaoDichTaiChinhList);
        }

        // GET: GiaoDichTaiChinhs/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var giaoDichTaiChinh = await _httpClient.GetFromJsonAsync<GiaoDichTaiChinh>($"GiaoDichTaiChinh/{id}");
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }
            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GiaoDichTaiChinhs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiaoDichTaiChinh giaoDichTaiChinh)
        {
            if (ModelState.IsValid)
            {
                giaoDichTaiChinh.Id = Guid.NewGuid();
                var response = await _httpClient.PostAsJsonAsync("GiaoDichTaiChinh", giaoDichTaiChinh);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không thể tạo giao dịch.");
            }
            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var giaoDichTaiChinh = await _httpClient.GetFromJsonAsync<GiaoDichTaiChinh>($"GiaoDichTaiChinh/{id}");
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }
            return View(giaoDichTaiChinh);
        }

        // POST: GiaoDichTaiChinhs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GiaoDichTaiChinh giaoDichTaiChinh)
        {
            if (id != giaoDichTaiChinh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"GiaoDichTaiChinh/{id}", giaoDichTaiChinh);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Không thể cập nhật giao dịch.");
            }
            return View(giaoDichTaiChinh);
        }

        // GET: GiaoDichTaiChinhs/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var giaoDichTaiChinh = await _httpClient.GetFromJsonAsync<GiaoDichTaiChinh>($"GiaoDichTaiChinh/{id}");
            if (giaoDichTaiChinh == null)
            {
                return NotFound();
            }

            return View(giaoDichTaiChinh);
        }

        // POST: GiaoDichTaiChinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"GiaoDichTaiChinh/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Không thể xóa giao dịch.");
            return View();
        }
    }
}

