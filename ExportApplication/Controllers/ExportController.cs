using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using System.IO;
using static ExportApplication.Repository.Interface.Interface;

namespace ExportApplication.Controllers
{
    public class ExportController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ExportController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Action untuk halaman utama
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }

        // Action untuk ekspor Excel
        public IActionResult ExportToExcel()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Position";
            worksheet.Cell(1, 3).Value = "Age";

            int row = 2;
            foreach (var emp in employees)
            {
                worksheet.Cell(row, 1).Value = emp.Name;
                worksheet.Cell(row, 2).Value = emp.Position;
                worksheet.Cell(row, 3).Value = emp.Age;
                row++;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Employees.xlsx");
            }
        }

        // Action untuk ekspor PDF
        public IActionResult ExportToPDF()
        {
            try
            {
                var employees = _employeeRepository.GetAllEmployees();
                return new ViewAsPdf("ExportPDF", employees)
                {
                    FileName = "Employees.pdf"
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
