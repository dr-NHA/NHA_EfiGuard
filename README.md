# Info
> This Is A Edit/Mod Of EFI Guard A Popular DSE Bypass!

> This Isnt Intended On Being Undetectable, 

> This Is More Just For Ease Of Use And For Learning A Bit More C++ Stuff!

> Note: I Did Try Exposing C++ Functions To C# But Found It Works Better To Just Run As A Extension/EXE Not Library!
Maybe Someone Else Can Expose The Functions Correctly (Specifically Outputting Strings From C++ To C#) :)

# Setup/Install
The UI .Exe Contains An Auto Setup/Install Feature,
This Is The Simplest Way To Setup/Install The EFI Files To A Compatible Fat32 Drive!

![image](https://github.com/dr-NHA/NHA_EfiGuard/assets/56168811/7b95306b-83aa-4723-ad75-955a8a782c12)

![image](https://github.com/dr-NHA/NHA_EfiGuard/assets/56168811/e88a6878-ffcb-431d-aa9f-f6c960112a95)

![image](https://github.com/dr-NHA/NHA_EfiGuard/assets/56168811/d3bf18f7-4aea-457b-8583-25f732eccd8b)

Once Installed Restart Your Pc And Boot Into The Drive U Installed The EFI Into,
Once Booted You Can Run The UI And See If The Backdoor Is Working (Itll Auto Check On "UI".exe Startup)
If The Backdoor Is Working You Can Disable/Enable DSE And Also Use The Get g_CIOptions + Sytem Info Button To Get A Nice Output Like So:

```
Querying g_CiOptions Value...
CI!g_CiOptions @ 0x14429418
Success.g_CiOptions Value: 0x00000006

SystemBootEnvironmentInformation:
BootIdentifier: 3CC95424DB4E8D7EA6ED91EFA615
FirmwareType: 0x00000002
BootFlags: 0x0000000000000000
SystemModuleInformation: Kernel: ntoskrnl.exe
\SystemRoot\system32\ntoskrnl.exe
SystemCodeIntegrityInformation: IntegrityOptions: 0x00000001
0x00000001: CODEINTEGRITY_OPTION_ENABLED
SystemKernelDebuggerInformation:
KernelDebuggerEnabled: False
KernelDebuggerNotPresent: True
SystemKernelDebuggerInformationEx:
DebuggerAllowed: False
DebuggerEnabled: False
DebuggerPresent: False
SharedUserData->KdDebuggerEnabled: 0x00000000
SystemKernelDebuggerFlags: 0x00000000
SystemCodeIntegrityPolicyInformation:
Options: 0x40000000
HVCIOptions: 0x00000000
SystemIsolatedUserModeInformation:
SecureKernelRunning: False
HvciEnabled: False
HvciStrictMode: False
DebugEnabled: False
FirmwarePageProtection: True
EncryptionKeyAvailable: False
TrustletRunning: False
HvciDisableAllowed: False
```

# Build
Please Make A Folder In Your EDK2 Workspace At:
"C:\edk2\NHAEFIPkg"
 In This Folder You Should Copy The Project Source Into,
 It Should End Up Looking Somewhat Like The Image Below
 
![image](https://github.com/dr-NHA/NHA_EfiGuard/assets/56168811/17a2a30e-3193-4a3a-86a4-6e24981e1375)

Compile Steps At The [ORIGINAL](https://github.com/Mattiwatti/EfiGuard) Repo
Note That The .GitModules May Not Work Correctly So Youll Have To Manually Go Grab The Lib Needed!

Follow The Steps On How To Compile Here,
Theres Pretty Much No Differences With Compilation

# Changes
* Added A UI Made IN C# For Sending Commands Threw The Backdoor
* Changed The DSEFIX To Output Using std::cout Instead Of The Customized C Method That Was Being Used
* Edited The Names Of Pretty Much Everything Visible To The User
* Added An Auto Setup/Install Option To The UI
