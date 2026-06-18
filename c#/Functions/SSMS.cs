using System.Diagnostics;

namespace Work_Start_Functions;
public class SSMS
{
   public static void Start()
    {
        string Path_SSMS = @"C:\\Program Files\Microsoft SQL Server Management Studio 22\Release\Common7\IDE\SSMS.exe";
        string Path_Script = @"C:\Users\vne8725\OneDrive - Westinghouse Electric Company LLC\Documents\SQL Server Management Studio 22\Daily_Table_Refresh_Status_vne8725.sql";
        string Args_SSMS = $"-S SCRWPDSFIN01 -d Business_Reporting -C -nosplash \"{Path_Script}\"";
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
