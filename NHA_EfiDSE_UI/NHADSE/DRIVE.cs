using System;

namespace NHADSE{
public class DRIVE:IDisposable {
public string VolumeLabel { get; private set; }=null;
public string DriveLetter { get; private set; } = null;
public string ModelName { get; private set; } = null;
public string PhysicalDrive { get; private set; } = null;
public bool IsFat32 { get; private set; } = false;

public DRIVE(string VolumeLabel,string DriveLetter,string ModelName,string PhysicalDrive,bool IsFat32){
if(VolumeLabel!=null? VolumeLabel.Length == 0 : true)
VolumeLabel="UNNAMEDDRIVE";
this.VolumeLabel = VolumeLabel;
this.DriveLetter = DriveLetter;
this.ModelName = ModelName;
this.PhysicalDrive = PhysicalDrive;
this.IsFat32 = IsFat32;
}

public override string ToString()=>
"["+VolumeLabel+"] "+DriveLetter+" > "+ ModelName + " > "+ PhysicalDrive+" > "+(IsFat32? "Fat32":"NotFat32");


public void Dispose(){
this.VolumeLabel = null;
this.DriveLetter = null;
this.ModelName = null;
this.PhysicalDrive = null;
GC.Collect(GC.GetGeneration(this),GCCollectionMode.Forced);
}

}}