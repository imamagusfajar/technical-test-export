using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using System.IO;

namespace ExportApplication.Controllers
{
    public class ExportController : Controller
    {
        // Action untuk halaman utama
        public IActionResult Index()
        {
            return View();
        }

        // Action untuk ekspor Excel
        public IActionResult ExportToExcel()
        {
            // Membuat workbook baru dengan ClosedXML
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Data");

            // Menambahkan header
            worksheet.Cell("A1").Value = "Name";
            worksheet.Cell("B1").Value = "Age";

            // Menambahkan data
            worksheet.Cell("A2").Value = "John Doe";
            worksheet.Cell("B2").Value = 30;

            // Menyimpan dan mengunduh file Excel
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DataExport.xlsx");
            }
        }

        // Action untuk ekspor PDF
        public IActionResult ExportToPDF()
        {
            try
            {
                // Model data untuk PDF
                var model = new { Name = "John Doe", Age = 30 };

                // Menggunakan Rotativa untuk menghasilkan PDF
                return new ViewAsPdf("ExportPDF", model)
                {
                    FileName = "GeneratedPDF.pdf" // Nama file PDF yang akan diunduh
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
                // Log lebih lanjut atau penanganan kesalahan
                return View();  
            }

            
        }
    }
}
