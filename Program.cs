using System;
using Work_Start_Functions;

class Program
{
    [STAThread]
    static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, EventArgs) =>
        {
            Console.WriteLine($"Uncaught error: {EventArgs.ExceptionObject}");
            Console.WriteLine("Press Enter to close.");
            Console.WriteLine();
        };
        var SAP_Credentials = new Dictionary<string, string?>
        {
            {"EP1", Credential_Manager.Get_Password("SAP_EP1_Password")},
            {"EP4", Credential_Manager.Get_Password("SAP_EP4_Password")}
        };
        string User_Name = Environment.UserName.ToUpper();
        VD.Next();
        SSMS.Start();
        VSC.Start();
        VD.Previous();
        foreach (var system in SAP_Credentials)
        {
            SAP.Start(system.Key, User_Name, system.Value);
            dynamic session = SAP.Get_Session_By_Database_ID(system.Key);
            session.FindById("wnd[0]").maximize();
            session.findById("wnd[0]/tbar[0]/okcd").text = "C# Test";
            
        }

        Environment.Exit(0);
    }
}
//Framework dependent - need .NET
//dotnet publish -c Release -r win-x64 --self-contained false

//SELF CONTAINTED BIG FILE FOR CCLIENTS
//dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

//NATIVE AOT No Runtime
//dotnet publish -c Release -r win-x64 -p:PublishAot=true