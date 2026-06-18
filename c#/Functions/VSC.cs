
using System.Diagnostics;

namespace Work_Start_Functions;


public class VSC
{
    public static void Start()
    {
        Process proc = System.Diagnostics.Process.Start(new ProcessStartInfo 
        {
            FileName = "cmd.exe",
            Arguments = "/c start \"\" \"C:\\Users\\[USER]\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe",
            UseShellExecute = true,
            CreateNoWindow = false
            // WindowStyle = ProcessWindowStyle.Hidden
        })?? throw new Exception("Failed to start VSC.");
        
        var timeout = DateTime.Now.AddSeconds(15);
        while (DateTime.Now < timeout)
        {
            if (proc.MainWindowHandle != IntPtr.Zero)
                break;
            Thread.Sleep(500);
            proc.Refresh();
        }
        //App.Wait_To_Be_ready(proc);

    }
}

