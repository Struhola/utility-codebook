using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Work_Start_Functions;

public class SAP
{
    public static void Start(string Db, string User, string? Password)
    {
        if (Password != null)
        {
            Console.WriteLine($"Logging into {Db}.");
            string SAP_Args = $"-system={Db} -client=010 -user={User} -pw={Password}";
            Process? proc = System.Diagnostics.Process.Start("C:\\Program Files\\SAP\\FrontEnd\\SAPgui\\sapshcut.exe", SAP_Args);
            App.Wait_To_Be_ready(proc);
        }
    }

    public static object Get_Session_By_Database_ID(string Database_ID)
    {
        for (int i =0; i<6; i++)
        {
            try 
            {
                Type ROT_Wrapper_Type = Type.GetTypeFromProgID("SapROTWr.SapRotWrapper")?? throw new Exception("SAP ROTWrapped not found. (ROT_Wrapper_Type)");
                object ROT_Wrapper = Activator.CreateInstance(ROT_Wrapper_Type) ?? throw new Exception("SAP ROTWrapper err. (ROT_Wrapper)");
                object SAP_GUI = ROT_Wrapper.GetType().InvokeMember("GetROTEntry", System.Reflection.BindingFlags.InvokeMethod, null, ROT_Wrapper, new object[]{"SAPGUI"}) ?? throw new Exception("GetROTEntry err.");
                dynamic SAP_App = SAP_GUI.GetType().InvokeMember("GetScriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, SAP_GUI, null) ?? throw new Exception("SAP GUI not found. (SAP_App)");

                foreach (var connection in SAP_App.Connections)
                {
                    foreach (var session in connection.Sessions)
                    {
                        if (session.Busy == false)
                        {
                            if (session.Info.SystemName == Database_ID)
                            {
                                return session;
                            }
                        }
                    }
                }
            }
            catch
            {
                //pass
            }
            System.Threading.Thread.Sleep(1500);
        }
        throw new Exception("Timed out, No free SAP sessions found.");
    }

}