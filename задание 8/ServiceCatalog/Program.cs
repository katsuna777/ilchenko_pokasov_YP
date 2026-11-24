using ServiceCatalog.Forms;

namespace ServiceCatalog;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new ServicesForm());
    }
}
