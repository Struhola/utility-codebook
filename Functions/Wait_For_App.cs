using System.Diagnostics;

namespace Work_Start_Functions;

public class App
{
    public static void Wait_To_Be_ready(Process? proc, int Max_Wait_Sencond = 60)
    {
        if (proc == null)
        {
            Console.WriteLine("Process doesn't exist.");
            return;
        }
        int Attempts = Max_Wait_Sencond *2;
        bool Is_Ready = false;
        while (Attempts >0)
        {
            if (proc.HasExited)
            {
                return;
            }
            proc.Refresh();
            if (proc.MainWindowHandle != IntPtr.Zero)
            {
                Is_Ready = true;
                break;
            }
            Thread.Sleep(500);
            Attempts--;
        }

        if (Is_Ready)
        {
            Thread.Sleep(2000);
        }
    }
}