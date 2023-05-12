#include "NHADSE.h"
#include <ntstatus.h>
#include <iostream>
#include "Converters.cpp"


static constexpr PCWCHAR CodeIntegrityOptionNames[] ={
L"CODEINTEGRITY_OPTION_ENABLED",
L"CODEINTEGRITY_OPTION_TESTSIGN",
L"CODEINTEGRITY_OPTION_UMCI_ENABLED",
L"CODEINTEGRITY_OPTION_UMCI_AUDITMODE_ENABLED",
L"CODEINTEGRITY_OPTION_UMCI_EXCLUSIONPATHS_ENABLED",
L"CODEINTEGRITY_OPTION_TEST_BUILD",
L"CODEINTEGRITY_OPTION_PREPRODUCTION_BUILD",
L"CODEINTEGRITY_OPTION_DEBUGMODE_ENABLED",
L"CODEINTEGRITY_OPTION_FLIGHT_BUILD",
L"CODEINTEGRITY_OPTION_FLIGHTING_ENABLED",
L"CODEINTEGRITY_OPTION_HVCI_KMCI_ENABLED",
L"CODEINTEGRITY_OPTION_HVCI_KMCI_AUDITMODE_ENABLED",
L"CODEINTEGRITY_OPTION_HVCI_KMCI_STRICTMODE_ENABLED",
L"CODEINTEGRITY_OPTION_HVCI_IUM_ENABLED",
L"CODEINTEGRITY_OPTION_WHQL_ENFORCEMENT_ENABLED",
L"CODEINTEGRITY_OPTION_WHQL_AUDITMODE_ENABLED"
};

static VOID PrintCodeIntegrityOptions(_In_ ULONG CodeIntegrityOptions){
ULONG Value = 0;
for (ULONG i = 0; i < ARRAYSIZE(CodeIntegrityOptionNames); ++i)
if ((CodeIntegrityOptions & (Value = 1UL << i)) != 0) 
std::cout << "0x"+ ToHex(Value) +": " + ToString(CodeIntegrityOptionNames[i]) + "\n";
}

NTSTATUS DumpSystemInformation(){

#pragma region SystemBootEnvironmentInformation
SYSTEM_BOOT_ENVIRONMENT_INFORMATION BootInfo = {};
NTSTATUS Status = NtQuerySystemInformation(SystemBootEnvironmentInformation,&BootInfo,sizeof(BootInfo),nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemBootEnvironmentInformation: Error: 0x"+ ToHex(Status)+"\n";
else{
std::cout<<"SystemBootEnvironmentInformation:\nBootIdentifier: "+ ToString(BootInfo.BootIdentifier)+"\n"+
"FirmwareType: 0x" + ToHex((int)BootInfo.FirmwareType) +"\n"
+"BootFlags: 0x"+ToHex(BootInfo.BootFlags) + "\n";
}
#pragma endregion

#pragma region SystemModuleInformation
ULONG Size = 0;
Status = NtQuerySystemInformation(SystemModuleInformation,nullptr,0,&Size);
if (Status != STATUS_INFO_LENGTH_MISMATCH)
std::cout << "SystemModuleInformation: Error: 0x"+ ToHex((long)Status) + "\n";
else{
const PRTL_PROCESS_MODULES ModuleInfo = static_cast<PRTL_PROCESS_MODULES>(
RtlAllocateHeap(RtlProcessHeap(), HEAP_ZERO_MEMORY, 2 * static_cast<SIZE_T>(Size)));
Status = NtQuerySystemInformation(SystemModuleInformation,ModuleInfo,2 * Size,nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemModuleInformation: Error: 0x" + ToHex((long)Status) + "\n";
else{
const RTL_PROCESS_MODULE_INFORMATION Ntoskrnl = ModuleInfo->Modules[0];
std::cout << "SystemModuleInformation: Kernel: " +
ToString(Ntoskrnl.FullPathName + Ntoskrnl.OffsetToFileName)
+"\n"+
ToString(Ntoskrnl.FullPathName)
+ "\n";
}
RtlFreeHeap(RtlProcessHeap(), 0, ModuleInfo);
}
#pragma endregion

#pragma region SystemCodeIntegrityInformation
SYSTEM_CODEINTEGRITY_INFORMATION CodeIntegrityInfo = { sizeof(SYSTEM_CODEINTEGRITY_INFORMATION) };
Status = NtQuerySystemInformation(SystemCodeIntegrityInformation, &CodeIntegrityInfo, sizeof(CodeIntegrityInfo), nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemCodeIntegrityInformation: Error: 0x" + ToHex((long)Status) + "\n";
else {
std::cout << "SystemCodeIntegrityInformation: IntegrityOptions: 0x" + ToHex(CodeIntegrityInfo.CodeIntegrityOptions) + "\n";
PrintCodeIntegrityOptions(CodeIntegrityInfo.CodeIntegrityOptions);
}
#pragma endregion

#pragma region SystemKernelDebuggerInformation
SYSTEM_KERNEL_DEBUGGER_INFORMATION KernelDebuggerInfo = { 0 };
Status = NtQuerySystemInformation(SystemKernelDebuggerInformation, &KernelDebuggerInfo, sizeof(KernelDebuggerInfo), nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemKernelDebuggerInformation: Error: 0x" + ToHex((long)Status) + "\n";
else
std::cout << "SystemKernelDebuggerInformation:\nKernelDebuggerEnabled: "+
BToString(KernelDebuggerInfo.KernelDebuggerEnabled)
+"\nKernelDebuggerNotPresent: "+
BToString(KernelDebuggerInfo.KernelDebuggerNotPresent)
+"\n";

#pragma endregion

#pragma region SystemKernelDebuggerInformationEx
if ((RtlNtMajorVersion() >= 6 && RtlNtMinorVersion() >= 3) || RtlNtMajorVersion() > 6){
SYSTEM_KERNEL_DEBUGGER_INFORMATION_EX KernelDebuggerInfoEx = { 0 };
Status = NtQuerySystemInformation(SystemKernelDebuggerInformationEx,&KernelDebuggerInfoEx,sizeof(KernelDebuggerInfoEx),nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemKernelDebuggerInformationEx: Error: 0x" + ToHex((long)Status) + "\n\n";
else
std::cout<<"SystemKernelDebuggerInformationEx:\nDebuggerAllowed: "+BToString(KernelDebuggerInfoEx.DebuggerAllowed)
+"\nDebuggerEnabled: "+BToString(KernelDebuggerInfoEx.DebuggerEnabled)
+"\nDebuggerPresent: "+BToString(KernelDebuggerInfoEx.DebuggerPresent)+"\n";

}
#pragma endregion

const UCHAR KdDebuggerEnabled = SharedUserData->KdDebuggerEnabled;
std::cout << "SharedUserData->KdDebuggerEnabled: 0x" + ToHex((long)KdDebuggerEnabled) + "\n";

if (RtlNtMajorVersion() > 6){
#pragma region SystemKernelDebuggerFlags
UCHAR KernelDebuggerFlags = 0;
Status = NtQuerySystemInformation(SystemKernelDebuggerFlags,&KernelDebuggerFlags,sizeof(KernelDebuggerFlags),nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemKernelDebuggerFlags: Error: 0x" + ToHex((long)Status) + "\n";
else
std::cout << "SystemKernelDebuggerFlags: 0x" + ToHex((long)KernelDebuggerFlags) + "\n";
#pragma endregion

#pragma region SystemCodeIntegrityPolicyInformation
SYSTEM_CODEINTEGRITYPOLICY_INFORMATION CodeIntegrityPolicyInfo = { 0 };
Status = NtQuerySystemInformation(SystemCodeIntegrityPolicyInformation, &CodeIntegrityPolicyInfo, sizeof(CodeIntegrityPolicyInfo), nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemCodeIntegrityPolicyInformation: Error: 0x" + ToHex((long)Status) + "\n";
else
std::cout << "SystemCodeIntegrityPolicyInformation:\nOptions: 0x" + ToHex(CodeIntegrityPolicyInfo.Options) +
"\nHVCIOptions: 0x" + ToHex(CodeIntegrityPolicyInfo.HVCIOptions) + "\n";
#pragma endregion

#pragma region SystemIsolatedUserModeInformation
SYSTEM_ISOLATED_USER_MODE_INFORMATION IumInfo = { 0 };
Status = NtQuerySystemInformation(SystemIsolatedUserModeInformation,&IumInfo,sizeof(IumInfo),nullptr);
if (!NT_SUCCESS(Status))
std::cout << "SystemIsolatedUserModeInformation: Error: 0x" + ToHex((long)Status) + "\n";
else 
std::cout << (
"SystemIsolatedUserModeInformation:\nSecureKernelRunning: "+BToString(IumInfo.SecureKernelRunning) + "\n"
+"HvciEnabled: " + BToString(IumInfo.HvciEnabled) + "\n"
+"HvciStrictMode: " + BToString(IumInfo.HvciStrictMode) + "\n"
+"DebugEnabled: " + BToString(IumInfo.DebugEnabled) + "\n"
+"FirmwarePageProtection: " + BToString(IumInfo.FirmwarePageProtection) + "\n"
+"EncryptionKeyAvailable: " + BToString(IumInfo.EncryptionKeyAvailable) + "\n"
+"TrustletRunning: " + BToString(IumInfo.TrustletRunning) + "\n"
+"HvciDisableAllowed: " + BToString(IumInfo.HvciDisableAllowed) + "\n");
#pragma endregion

}

return Status;
}
