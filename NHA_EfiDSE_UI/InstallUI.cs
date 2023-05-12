using NHA_Assets;
using NHADSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NHADSE.NDSE_Installer;

namespace N_EFI{
public partial class InstallUI : Form{
private static string[] Columns { get; } = new string[] {
"Drive Letter",
"Volume Label",
"Model Name",
"Physical Drive",
"Is Compatible",
"Already Setup",
};

public InstallUI(){
InitializeComponent();
this.Icon = System.Reflection.Assembly.GetExecutingAssembly().GetIcon("", "X_APPICON.ico");

InstallToDrive_M.Click += (X, E) => { 
if(ITEMS.SelectedItems.Count > 0?DRIVESCACHE[ITEMS.SelectedIndices[0]].IsFat32:false){
DRIVESCACHE[ITEMS.SelectedIndices[0]].Install();
RefreshList();
}
};
ITEMS.MouseDoubleClick += (X, E) => {
if(ITEMS.SelectedItems.Count > 0 ? E.Button==MouseButtons.Left: false){
var S= Cursor.Position;
Menu.Show(new Point(S.X-15,S.Y-8));
InstallToDrive_M.Enabled= DRIVESCACHE[ITEMS.SelectedIndices[0]].IsFat32;
}
};
ITEMS.Columns.Clear();
foreach(string Column in Columns)
ITEMS.Columns.Add(Column);

FormClosing += (X, E) => {
if(E.CloseReason == CloseReason.UserClosing){
E.Cancel = true;
Hide();
Application.OpenForms[0].BringToFront();
Application.OpenForms[0].Show();
Application.OpenForms[0].BringToFront();
}
};

REFRESH.Click+=(X,E)=>RefreshList();

VisibleChanged += (X, E) => {
if(Visible) RefreshList();
GC.Collect(GC.GetGeneration(this),GCCollectionMode.Forced);
};

var SS = @"
Install/Setup/Update The EFI Driver To A Drive,
Double Click A Drive Then Select 'Install To Drive',
Once Installed Restart Your Pc And Boot Into That Drive!
For More Info Please Consult The Github:
https://github.com/dr-NHA/NHA_EfiGuard
".Trim();
INFO.Text=SS;
INFO.DetectUrls=true;
INFO.LinkClicked+= (X, E) =>Process.Start(E.LinkText);
var I= SS.IndexOf("https://");
var D= SS.Substring(I); 
D = (D.Substring(0, I = D.Contains("\n") ? D.IndexOf('\n') : D.Length));
INFO.SelectionStart= INFO.Text.IndexOf(D)-0x4;
INFO.SelectionLength=  D.Length;
INFO.SelectionFont=new Font(Font, FontStyle.Bold);
INCOMPATIBLE.CheckedChanged+=(X,E)=>RefreshList();
GC.Collect(GC.GetGeneration(this),GCCollectionMode.Forced);
}


private ListViewItem PresetListViewItem(DRIVE D) =>new ListViewItem(new string[] {
D.DriveLetter,
D.VolumeLabel,
D.ModelName,
D.PhysicalDrive,
D.IsFat32?"True":"False",
D.IsInstalled()?"True":"False",
});

private List<DRIVE> DRIVESCACHE =null;
private ColumnHeaderAutoResizeStyle GetColumnHeaderAutoResizeStyle(Graphics G2,int ColumnId){
ColumnHeaderAutoResizeStyle OUT= ColumnHeaderAutoResizeStyle.HeaderSize;
var ColumnSize = G2.MeasureString(Columns[ColumnId],Font).Width;
foreach(ListViewItem ITEM in ITEMS.Items) 
if (G2.MeasureString(ITEM.SubItems[ColumnId].Text, Font).Width> ColumnSize){
OUT= ColumnHeaderAutoResizeStyle.ColumnContent;
break;
}
return OUT;
}
public void RefreshList() {
ITEMS.SuspendLayout();
ITEMS.Items.Clear();
List<ListViewItem> LVIs=new List<ListViewItem>();
var D = GetCompatibleDrives(INCOMPATIBLE.Checked);
D.Wait();
var DOCK=Parallel.ForEach(D.Result, DB => LVIs.Add(PresetListViewItem(DB))); 
while(!DOCK.IsCompleted)
Task.Delay(1).Wait();
ITEMS.Items.AddRange(LVIs.ToArray());
LVIs.Clear();
LVIs=null;
DRIVESCACHE = D.Result;
D.Dispose();
GC.Collect(GC.GetGeneration(D), GCCollectionMode.Forced);
D = null;

var G=this.CreateGraphics();
for(var C=0;C<Columns.Length;C++) ITEMS.AutoResizeColumn(C, GetColumnHeaderAutoResizeStyle(G,C));
G.Dispose();
G=null;
ITEMS.ResumeLayout();
}



}}