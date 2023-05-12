using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;

/// <summary>
/// NHA's Assets Classes
/// </summary>
namespace NHA_Assets{
/// <summary>
/// Asset Loader For Auto Loading Embedded Resources
/// Also Useful To Remove Embeded Resources
/// </summary>
public static class AssetLoader{
/// <summary>
/// Get The Namespace Of An Assembly
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <returns></returns>
public static string GetNamespace(this Assembly CurrentAssembly)=>
CurrentAssembly.EntryPoint.DeclaringType.Namespace.Replace(" ", "_");

/// <summary>
/// Extract All Assets
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <param name="INTERNAL_PATH"></param>
/// <param name="AutoDelete"></param>
public static void LoadAll(this Assembly CurrentAssembly, string INTERNAL_PATH,bool AutoDelete=false){
var L= INTERNAL_PATH.Replace("\\",".");
CurrentAssembly.ExtractAssets(L);
if(AutoDelete)
SetupForDeleteTempAssets(CurrentAssembly.AssetPaths( L));
}

/// <summary>
/// Get An Icon From Resources
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <param name="InternalPath"></param>
/// <param name="ImageName"></param>
/// <returns></returns>
public static Icon GetIcon(this Assembly CurrentAssembly, string InternalPath,string ImageName) {
Icon image = null;
CurrentAssembly.EnumerateAssets(InternalPath, (FileName, Stream) => {
if (FileName==ImageName) {
image =new Icon(Stream);
return true;
}
return false;
});
return image;
}
        

#region Private Helpers
private static Random RDI { get; } =new Random();
private static int RandomIntX => RDI.Next(137, 1337); 

private static dynamic Future { get; } = DateTime.Parse("6/9/2069", new System.Globalization.CultureInfo("en-AU"));
private static bool IsCleanupSetup=false;
private static Action Cleanup = () => { };

/// <summary>
/// EnumerateAssets Helper
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <param name="Folder"></param>
/// <returns></returns>
private static (string Namespace, string[] Names,string Folder) Enum_(this Assembly CurrentAssembly, string Folder) { 
if(Folder.StartsWith("\\")) while(Folder.StartsWith("\\")) Folder=Folder.Substring(1,Folder.Length);  
return (CurrentAssembly.GetNamespace(),CurrentAssembly.GetManifestResourceNames(),Folder.Replace('\\', '.'));
}

/// <summary>
/// Easy Extract Assets Function
/// </summary>
/// <param name="Folder"></param>
/// <param name="Filter"></param>
private static void ExtractAssets(this Assembly CurrentAssembly, string Folder,string Filter="")=>
CurrentAssembly.EnumerateAssets(Folder, (Name,Stream) => {
if(Name.EndsWith(Filter)){
try{
if(File.Exists(Name))
File.Delete(Name);
var file = new FileStream(Name, FileMode.CreateNew);
Stream.CopyTo(file); 
file.Close();
file.Dispose();
GC.Collect(GC.GetGeneration(file),GCCollectionMode.Forced);
file=null;
DoFileHide(Name);
}catch(Exception ex) { 
}
}
Name=null;
Stream.Close();
Stream.Dispose();
GC.Collect(GC.GetGeneration(Stream),GCCollectionMode.Forced);
});


/// <summary>
/// Get Asset Paths Of Extracted Assets
/// </summary>
/// <param name="Folder"></param>
/// <param name="Filter"></param>
/// <returns></returns>
private static string[] AssetPaths(this Assembly CurrentAssembly, string Folder,string Filter=""){
Folder = Folder.Replace("\\", ".");
Filter = Filter.Replace("\\", ".");
string Snamespace = CurrentAssembly.GetNamespace();
List<string> Paths = new List<string>();
CurrentAssembly.EnumerateAssets(Folder,Paths.Add);
var A= Paths.ToArray(); 
Paths.Clear();
GC.Collect(GC.GetGeneration(Paths),GCCollectionMode.Forced);
Paths=null;
return A;
}

private static async void DoFileHide(string FileDir) { 
File.SetCreationTime(FileDir, Future);
File.SetLastWriteTime(FileDir, Future);
File.SetLastAccessTime(FileDir, Future);
File.SetAttributes(FileDir, FileAttributes.Hidden);
}

/// <summary>
/// Deletes Assets On Application Exit
/// Uses A New Method To Allow Deleting All Files 
/// </summary>
/// <param name="AssetNames"></param>
private static void SetupForDeleteTempAssets(string[] AssetNames){
var X2=""
 + "title NHA Asset Cleanup\n"
 + "cd "+Environment.CurrentDirectory+"\n"
 + "echo off\n"
 + "cls\n"
 + "echo Deleting All Assets\n";

var CX= "timeout 1 > NUL\n";
foreach (string Path in AssetNames)
CX += "del /a:h \"" + Path+ "\" -f\n";
GC.Collect(GC.GetGeneration(AssetNames),GCCollectionMode.Forced);
X2+=CX;
#if DEBUG
/*
 * Makes A File To Remove The Assets If Needed For Debug Reasons
 */
if (File.Exists("_DBG_removeAssets.cmd"));
File.Delete("_DBG_removeAssets.cmd");
File.WriteAllText("_DBG_removeAssets.cmd",X2+"pause\n");
#endif

Cleanup += () => { 
var MAKE= "AssetCleanup" + (RandomIntX).ToString("X2") + ".cmd";
File.WriteAllText(MAKE,X2+ CX +// "pause\n"+
"del " + MAKE+ " -f");

new Process() { 
StartInfo=new ProcessStartInfo() {
FileName=MAKE,
LoadUserProfile=false,
WindowStyle=ProcessWindowStyle.Hidden,
CreateNoWindow=true } }.Start();
};
if (!IsCleanupSetup?(IsCleanupSetup=!IsCleanupSetup):false) 
AppDomain.CurrentDomain.ProcessExit+= (X, E) => Cleanup?.Invoke();
}

#endregion


#region EnumerateAssets

/// <summary>
/// Get Assets From The Running Process
/// </summary>
/// <param name="Folder"></param>
/// <param name="OnResourceFound"></param>
/// <exception cref="AccessViolationException"></exception>
/// 
public static void EnumerateAssets(this Assembly CurrentAssembly, string Folder,Func<string,Stream,bool> OnResourceFound){
var _NHA= CurrentAssembly.Enum_(Folder);
var FileName="";
foreach (string FileXName in _NHA.Names)
if ((FileName = FileXName.Substring(_NHA.Namespace.Length + 1)).StartsWith(_NHA.Folder)) 
if(OnResourceFound.Invoke(FileName.Substring(_NHA.Folder.Length).Trim('.'), CurrentAssembly.GetManifestResourceStream(FileXName)))
break;
}

/// <summary>
/// Get Assets From The Running Process
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <param name="Folder"></param>
/// <param name="OnResourceFound"></param>
public static void EnumerateAssets(this Assembly CurrentAssembly, string Folder,Action<string,Stream> OnResourceFound){
var _NHA= CurrentAssembly.Enum_(Folder);
var FileName="";
foreach (string FileXName in _NHA.Names)
if ((FileName = FileXName.Substring(_NHA.Namespace.Length + 1)).StartsWith(_NHA.Folder)) 
OnResourceFound.Invoke(FileName.Substring(_NHA.Folder.Length).Trim('.'), CurrentAssembly.GetManifestResourceStream(FileXName));
}

/// <summary>
/// Get Assets From The Running Process
/// </summary>
/// <param name="CurrentAssembly"></param>
/// <param name="Folder"></param>
/// <param name="OnResourceFound"></param>
public static void EnumerateAssets(this Assembly CurrentAssembly, string Folder,Action<string> OnResourceFound){
var _NHA= CurrentAssembly.Enum_(Folder);
var FileName="";
foreach (string FileXName in _NHA.Names)
if ((FileName = FileXName.Substring(_NHA.Namespace.Length + 1)).StartsWith(_NHA.Folder)) 
OnResourceFound.Invoke(FileName.Substring(_NHA.Folder.Length).Trim('.'));
GC.Collect(GC.GetGeneration(_NHA.Names),GCCollectionMode.Forced);
_NHA.Names=null;
GC.Collect(GC.GetGeneration(_NHA.Namespace),GCCollectionMode.Forced);
_NHA.Namespace = null;
GC.Collect(GC.GetGeneration(_NHA.Folder),GCCollectionMode.Forced);
_NHA.Folder = null;
}


#endregion

}}