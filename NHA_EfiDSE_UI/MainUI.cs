using NHA_Assets;
using System;
using System.Drawing;
using System.Windows.Forms;
using static NHADSE.NDSE_Core;

namespace N_EFI{
public partial class MainUI : Form{

public MainUI(){
InitializeComponent();    
this.Icon = System.Reflection.Assembly.GetExecutingAssembly().GetIcon("", "X_APPICON.ico");
MaximumSize=(MinimumSize = Size);
DSE_INFO.Text= CMD("c");
//DBG.Click     += (X, E) =>DSE_INFO.Text= EfiDSEFixV2();
Test.Click += (X, E) =>DSE_INFO.Text= CMD("c");
Get.Click += (X, E) =>DSE_INFO.Text= CMD("r")+"\r\n\r\n"+ CMD("i");

Color DISABLERCOLOR(bool State)=>
State?Color.Green : Color.Red;
bool LastState=false;

InstallUI installUI = new InstallUI();

DISABLE.Click += (X, E) =>{
DSE_INFO.Text= CMD(Is_DSE_Disabled() ? "e" : "d") +"\r\n\r\n"+ CMD("r");
DISABLE.ForeColor= DISABLERCOLOR(LastState = Is_DSE_Disabled());
DISABLE.Text= "(DSE) Driver Sign Enforcement: " + (LastState? "ENABLE" : "DISABLE");
};
DISABLE.ForeColor= DISABLERCOLOR(LastState=Is_DSE_Disabled());
DISABLE.Text= "(DSE) Driver Sign Enforcement: "+(LastState? "ENABLE" : "DISABLE");
string S=null;
INSTALL.Click += (X, E) =>installUI.ShowDialog();
GC.Collect(GC.GetGeneration(this),GCCollectionMode.Forced);
}

}}