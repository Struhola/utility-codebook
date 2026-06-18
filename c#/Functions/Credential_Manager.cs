

using System.Runtime.InteropServices;

public class Credential_Manager
{
    [DllImport("advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CredRead(string target, int type, int reserved, out IntPtr credentialPtr);

    [DllImport("advapi32.dll", EntryPoint = "CredFree", SetLastError = true)]
    private static extern void CredFree(IntPtr credentialPtr);
    public static string? Get_Password(string Target_Name)
    {
        IntPtr ptr = IntPtr.Zero;
        try
        {
            if (CredRead(Target_Name, 1, 0, out ptr))
            {
                var cred = Marshal.PtrToStructure<NativeCredential>(ptr);
                return Marshal.PtrToStringUni(cred.CredentialBlob);
            }
            
        }
        finally
        {
            if (ptr != IntPtr.Zero) CredFree(ptr);
        }
        return null;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NativeCredential
    {
        public uint Flags; public uint Type; public string Target_Name;
        public string Comment; public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize; public IntPtr CredentialBlob;
        public uint Persist; public uint AttributeCount; public IntPtr Attributes;
        public string TargetAlias; public string UserName;
    }
}