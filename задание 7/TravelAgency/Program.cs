using TravelAgency.Forms;

namespace TravelAgency;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new ToursForm());
    }
}
