using System.Runtime.InteropServices;
namespace Work_Start_Functions;

class VD
{
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        private const byte VK_LWIN = 0x5B;
        private const byte VK_CONTROL = 0x11;
        private const byte VK_LEFT = 0x25;
        private const byte VK_RIGHT = 0x27;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        
        public static void Next() => Send_Shortcut(VK_RIGHT);
        public static void Previous() => Send_Shortcut(VK_LEFT);

        private static void Send_Shortcut(byte Arrow_Key)
        {
            keybd_event(VK_CONTROL,0,0,0);
            keybd_event(VK_LWIN,0,0,0);
            keybd_event(Arrow_Key,0,0,0);
            
            keybd_event(Arrow_Key,0,KEYEVENTF_KEYUP,0);
            keybd_event(VK_LWIN,0,KEYEVENTF_KEYUP,0);
            keybd_event(VK_CONTROL,0,KEYEVENTF_KEYUP,0);
            Thread.Sleep(500);
        }
        /*
        (string Shell_Script = $@"$ex = New-Object -ComObject VirtualDesktopManagerInternal;" +
        $"$d = $ex.GetDesktops() | Where-Object {{$_.GetName() -eq '{Target_Name}' }}; " +
        $"if ($d) {{ $ex.SwitchDesktop($d) }}";

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{Shell_Script}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            })?.WaitForExit();
        }
        catch (System.ComponentModel.Win32Exception)
        {
            System.Console.WriteLine($"Failed to switch desktops to: {Target_Name}");
        }
        */
}
