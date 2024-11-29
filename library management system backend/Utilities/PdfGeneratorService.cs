using DinkToPdf.Contracts;
using DinkToPdf;

namespace library_management_system.Utilities
{
    public class PdfGeneratorService
    {

        private readonly IConverter _converter;

        public PdfGeneratorService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4, // Set to A4 or other sizes
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
                }
            };

            // Add ObjectSettings to the read-only Objects collection
            pdfDocument.Objects.Add(new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = "utf-8"
                }
            });

            return _converter.Convert(pdfDocument);
        }
    }
}
