using SchoolLearn.Services;

namespace SchoolLearn;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => UserMessages.Error(null, e.Exception);
        Application.ApplicationExit += (_, _) => ImageAssets.DisposeAll();
        Application.Run(new Form1());
    }
}
