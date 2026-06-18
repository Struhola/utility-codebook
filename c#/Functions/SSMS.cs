using System.Diagnostics;

namespace Work_Start_Functions;
public class SSMS
{
   public static void Start()
    {
        string Path_SSMS = @"C:\\Program Files\Microsoft SQL Server Management Studio 22\Release\Common7\IDE\SSMS.exe";
        string Path_Script = @"";
        string Args_SSMS = $"-S [server] -d [db] -C -nosplash \"{Path_Script}\"";
        Console.WriteLine($"Logging into SSMS.");

        try
        {
            Process? proc = Process.Start(new ProcessStartInfo
            {
                FileName = Path_SSMS,
                Arguments = Args_SSMS,
                UseShellExecute = true
            });
            App.Wait_To_Be_ready(proc);
        }
        catch (System.ComponentModel.Win32Exception)
        {
            System.Console.WriteLine("Failed to Start SSMS.");
        }
    }
}
