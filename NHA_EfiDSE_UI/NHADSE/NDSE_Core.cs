using NHA_Assets;
using System;
using System.Diagnostics;

namespace NHADSE{
public static class NDSE_Core {
private static string _ExtensionName= "NHADSE";
public static string ExtensionName {
get=> _ExtensionName; 
private set{
ExecutableName=((_ExtensionName = value).Contains(".") ?
_ExtensionName.Substring(0, _ExtensionName.IndexOf(".")) :
_ExtensionName)
+ ".exe";
}}

public static string ExecutableName { get; private set; } =
(_ExtensionName.Contains(".") ?
_ExtensionName.Substring(0, _ExtensionName.IndexOf(".")) :
_ExtensionName)
+ ".exe";


public static void SetupNDSE(){
var F=false;
string AssetPath= @"NHADSE\EXE\";
///Extract The NHADSE.exe From NHADSE\EXE\
Utilitys.Assembly.LoadAll(AssetPath, true);
var S="";
Utilitys.Assembly.EnumerateAssets(AssetPath, (X) => {
if(X.ToLower().EndsWith(".exe")){
if(!F?(F=!F):false)
ExtensionName = X;
S+=X+", ";
}
});
#if DEBUG
Debug.WriteLine("EXE FILES:");
Debug.WriteLine((S=S.Trim().Trim(',')+"\n"));
Debug.WriteLine("NDSE SETUP!");
#endif
GC.Collect(GC.GetGeneration(S),GCCollectionMode.Forced);
S=null;
GC.Collect(GC.GetGeneration(AssetPath),GCCollectionMode.Forced);
AssetPath=null;
}

/// <summary>
/// Run A NHADSE Command
/// </summary>
/// <param name="args"></param>
/// <returns></returns>
public static string CMD(string args = null){
ProcessStartInfo psi = new ProcessStartInfo(ExecutableName){
WorkingDirectory = Environment.CurrentDirectory,
WindowStyle = ProcessWindowStyle.Hidden,
Arguments = args != null ? (args = ("-" + (args.ToLower().TrimStart('-')))) : null,
};
psi.CreateNoWindow = (psi.RedirectStandardOutput = !(psi.LoadUserProfile = (psi.UseShellExecute = false)));
var p = new Process() { StartInfo = psi };
p.Start();
var S = p.StandardOutput.ReadToEnd();
p.Close();
p.Dispose();
GC.Collect(GC.GetGeneration(p), GCCollectionMode.Forced);
p = null;
GC.Collect(GC.GetGeneration(psi), GCCollectionMode.Forced);
psi = null;
args = null;
if (S.StartsWith("NHA EFI Backdoor\r\n")) S = S.Substring("NHA EFI Backdoor\r\n".Length);
else if (S.StartsWith("NHA EFI Backdoor\n")) S = S.Substring("NHA EFI Backdoor\n".Length);
else if (S.StartsWith("NHA EFI Backdoor")) S = S.Substring("NHA EFI Backdoor".Length);
if (S.Contains("\0"))
S = S.Replace("\0", "");
GC.Collect();
return S= S.Trim().Trim('\n').Trim();
}

public static bool Is_DSE_Disabled() { 
dynamic ST= CMD("r");
return (ST = Convert.ToInt32(ST.Substring(ST.LastIndexOf("0x")).Trim(), 16)) == 0;
}


}}