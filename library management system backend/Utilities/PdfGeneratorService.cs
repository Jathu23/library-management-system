using DinkToPdf.Contracts;
using DinkToPdf;

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

        public byte[] GeneratePdf(string htmlContent)
        {
            var pdfDocument = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            return _converter.Convert(pdfDocument);
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
