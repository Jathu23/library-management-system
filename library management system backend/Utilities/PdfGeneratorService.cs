using DinkToPdf;
using DinkToPdf.Contracts;
using library_management_system.DTOs.LentRecord;
using System.Text;

namespace library_management_system.Utilities
{
    public class PdfGeneratorService
    {
        private readonly IConverter _converter;

        public PdfGeneratorService()
        {
            // Path to the libwkhtmltox.dll
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

            _converter = new SynchronizedConverter(new PdfTools());
        }

       
        public async Task<byte[]> GeneratePdfAsync(string htmlContent, string documentTitle = "Report")
        {
            return await Task.Run(() =>
            {
                var pdfDocument = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                        DocumentTitle = documentTitle
                    },
                    Objects = { new ObjectSettings { HtmlContent = htmlContent, WebSettings = { DefaultEncoding = "utf-8" } } }
                };

                return _converter.Convert(pdfDocument);
            });
        }

        public async Task<byte[]> GenerateLendReportPdfAsync(LendReportDto report)
        {
            string htmlContent = GenerateLendReportHtml(report);
            return await GeneratePdfAsync(htmlContent, "Lend Report");
        }

        private string GenerateLendReportHtml(LendReportDto report)
        {
            var sb = new StringBuilder();

            sb.Append(@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Lend Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            line-height: 1.6;
        }
        h1, h2, h3 {
            text-align: center;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }
        th, td {
            padding: 10px;
            text-align: left;
            border: 1px solid #ccc;
        }
        th {
            background-color: #f4f4f4;
        }
        .summary-table {
            margin: 20px 0;
            width: 50%;
        }
        .summary-table th {
            text-align: center;
        }
        .section-title {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <h1>Library Lend Report</h1>
    <h3>Date: " + report.Date.ToString("yyyy-MM-dd") + @"</h3>
    <table class='summary-table'>
        <thead>
            <tr>
                <th>Total Rengings</th>
                <th>Pending</th>
                <th>On Time</th>
                <th>Later</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>" + report.TotalRengings + @"</td>
                <td>" + report.Pending + @"</td>
                <td>" + report.OnTime + @"</td>
                <td>" + report.Later + @"</td>
            </tr>
        </tbody>
    </table>");

            sb.Append(GenerateSectionHtml("Pending Lends", report.PendingLent));
            sb.Append(GenerateSectionHtml("On Time Lends", report.OnTimeLent));
            sb.Append(GenerateSectionHtml("Late Lends", report.LaterLent));

            sb.Append(@"
</body>
</html>");
            return sb.ToString();
        }


        private string GenerateSectionHtml(string title, List<LentHistoryAdminDto>? records)
        {
            if (records == null || records.Count == 0) return "";

            var sb = new StringBuilder();
            sb.Append("<div class='section-title'>" + title + "</div>");
            sb.Append(@"
        <table>
            <thead>
                <tr>
                    <th>Book Title</th>
                    <th>ISBN</th>
                    <th>User Name</th>
                    <th>Lent Date</th>
                    <th>Due Date</th>
                    <th>Return Date</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>");

            foreach (var record in records)
            {
                sb.Append(@"
                <tr>
                    <td>" + record.BookTitle + @"</td>
                    <td>" + record.BookISBN + @"</td>
                    <td>" + record.UserName + @"</td>
                    <td>" + record.LentDate.ToString("yyyy-MM-dd") + @"</td>
                    <td>" + record.DueDate.ToString("yyyy-MM-dd") + @"</td>
                    <td>" + (record.ReturnDate?.ToString("yyyy-MM-dd") ?? "Not Returned") + @"</td>
                    <td>" + record.Status + @"</td>
                </tr>");
            }

            sb.Append(@"
            </tbody>
        </table>");
            return sb.ToString();
        }

        public async Task<byte[]> GenerateBookLendingReportPdfAsync(BookLendingReportsDto report)
        {
            // Generate HTML content for the report
            string htmlContent = await Task.Run(() => GenerateBookLendingReportHtml(report));

            return await GeneratePdfAsync(htmlContent, "Book Lending Report");
            //return await Task.Run(() => _converter.Convert(pdfDoc));

        }

        private string GenerateBookLendingReportHtml(BookLendingReportsDto report)
        {
            var sb = new StringBuilder();

            // Add report header
            sb.Append(@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Book Lending Report</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            line-height: 1.6;
        }
        h1, h2, h3 {
            text-align: center;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }
        th, td {
            padding: 10px;
            text-align: left;
            border: 1px solid #ccc;
        }
        th {
            background-color: #f4f4f4;
        }
        .section-title {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <h1>Library Book Lending Report</h1>
    <h3>Created on: " + report.Created.ToString("yyyy-MM-dd HH:mm:ss") + @"</h3>");

            // Iterate through each report
            foreach (var bookReport in report.Reports)
            {
                sb.Append(@"
    <div class='section-title'>Book: " + bookReport.BookTitle + @" (ISBN: " + bookReport.ISBN + @")</div>
    <table>
        <thead>
            <tr>
                <th>Book Copy ID</th>
                <th>User Name</th>
                <th>Issuing Admin</th>
                <th>Receiving Admin</th>
                <th>Lend Date</th>
                <th>Due Date</th>
                <th>Return Date</th>
            </tr>
        </thead>
        <tbody>");

                foreach (var detail in bookReport.BookRentDetails)
                {
                    sb.Append(@"
            <tr>
                <td>" + detail.BookCopyId + @"</td>
                <td>" + detail.UserName + @"</td>
                <td>" + detail.IssuingAdmin + @"</td>
                <td>" + (detail.ReceivingAdmin ?? "Not Returned") + @"</td>
                <td>" + detail.LendDate.ToString("yyyy-MM-dd") + @"</td>
                <td>" + detail.DueDate.ToString("yyyy-MM-dd") + @"</td>
                <td>" + (detail.ReturnDate?.ToString("yyyy-MM-dd") ?? "Not Returned") + @"</td>
            </tr>");
                }

                sb.Append(@"
        </tbody>
    </table>");
            }

            sb.Append(@"
</body>
</html>");
            return sb.ToString();
        }

    }





    public class CustomAssemblyLoadContext : System.Runtime.Loader.AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }
    }
}
