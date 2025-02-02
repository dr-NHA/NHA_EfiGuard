#include "NHADSE.h"
#include <ntstatus.h>
#include "main.h"
#include <cstdio>
#include <stdio.h>
#include <corecrt_wstdio.h>
#include <iostream>
#include <string>
#include "Converters.cpp"


DECLSPEC_NOINLINE static VOID PrintUsage() {
std::cout << ToString(
L"Usage: -? or --?Word  = [COMMAND]\n\n"
L"Commands:\n\n"
L"-c, --check	= Test Backdoor Hook\n"
L"-r, --read	= Read Current g_CiOptions Value\n"
L"-d, --disable	= Disable DSE\n"
L"-e, --enable	= (Re)Enable DSE\n"
L"-i, --info	= Dump System Info\n");
std::cout.flush();
}

DECLSPEC_NOINLINE static VOID ParseCommandLine(_In_ PWCHAR CommandLine,_Out_opt_ PWCHAR* Argv,_Out_opt_ PWCHAR Arguments,_Out_ PULONG Argc,_Out_ PULONG NumChars){
*NumChars = 0;
*Argc = 1;

// Copy the executable name and and count bytes
PWCHAR p = CommandLine;
if (Argv != nullptr)
*Argv++ = Arguments;

// Handle quoted executable names
BOOLEAN InQuotes = FALSE;
WCHAR c;
do{
if (*p == '"'){
InQuotes = !InQuotes;
c = *p++;
continue;
}

++*NumChars;
if (Arguments != nullptr)
*Arguments++ = *p;
c = *p++;
} while (c != '\0' && (InQuotes || (c != ' ' && c != '\t')));

if (c == '\0')
--p;
else if (Arguments != nullptr)
*(Arguments - 1) = L'\0';

// Iterate over the arguments
InQuotes = FALSE;
for (; ; ++*NumChars){
if (*p != '\0'){
while (*p == ' ' || *p == '\t')
++p;
}
if (*p == '\0')
break; // End of arguments

if (Argv != nullptr)
*Argv++ = Arguments;
++*Argc;

// Scan one argument
for (; ; ++p){
BOOLEAN CopyChar = TRUE;
ULONG NumSlashes = 0;

while (*p == '\\'){
// Count the number of slashes
++p;
++NumSlashes;
}

if (*p == '"'){
// If 2N backslashes before: start/end a quote. Otherwise copy literally
if ((NumSlashes & 1) == 0){
if (InQuotes && p[1] == '"')
++p; // Double quote inside a quoted string
else{
// Skip first quote and copy second
CopyChar = FALSE; // Don't copy quote
InQuotes = !InQuotes;
}
}
NumSlashes >>= 1;
}

// Copy slashes
while (NumSlashes--){
if (Arguments != nullptr)
*Arguments++ = '\\';
++*NumChars;
}

// If we're at the end of the argument, go to the next
if (*p == '\0' || (!InQuotes && (*p == ' ' || *p == '\t')))
break;

// Copy character into argument
if (CopyChar){
if (Arguments != nullptr)
*Arguments++ = *p;
++*NumChars;
}
}

if (Arguments != nullptr)
*Arguments++ = L'\0';
}
}

NTSTATUS NTAPI NtProcessStartupW(_In_ PPEB Peb){
// On Windows XP (heh...) rcx does not contain a PEB pointer, but garbage
Peb = Peb != nullptr ? NtCurrentPeb() : NtCurrentTeb()->ProcessEnvironmentBlock; // And this turd is to get Resharper to shut up about assigning to Peb before reading from it. Note LHS == RHS

// Get the command line from the startup parameters. If there isn't one, use the executable name
const PRTL_USER_PROCESS_PARAMETERS Params = RtlNormalizeProcessParams(Peb->ProcessParameters);
const PWCHAR CommandLineBuffer = Params->CommandLine.Buffer == nullptr || Params->CommandLine.Buffer[0] == L'\0'
? Params->ImagePathName.Buffer
: Params->CommandLine.Buffer;

// Count the number of arguments and characters excluding quotes
ULONG Argc, NumChars;
ParseCommandLine(CommandLineBuffer,nullptr,nullptr,&Argc,&NumChars);

// Allocate a buffer for the arguments and a pointer array
const ULONG ArgumentArraySize = (Argc + 1) * sizeof(PVOID);
PWCHAR *Argv = static_cast<PWCHAR*>(RtlAllocateHeap(RtlProcessHeap(),HEAP_ZERO_MEMORY,ArgumentArraySize + NumChars * sizeof(WCHAR)));
if (Argv == nullptr)
return NtTerminateProcess(NtCurrentProcess, STATUS_NO_MEMORY);

// Copy the command line arguments
ParseCommandLine(CommandLineBuffer,Argv,reinterpret_cast<PWCHAR>(&Argv[Argc + 1]),&Argc,&NumChars);

// Call the main function and terminate with the exit status
const NTSTATUS Status = wmain(static_cast<int>(Argc), Argv);
return NtTerminateProcess(NtCurrentProcess, Status);
}




extern "C" _declspec(dllexport) long TestFunc() {
	ULONG CiOptionsValue = 0;
	ULONG OldCiOptionsValue;
	const NTSTATUS Status = AdjustCiOptions(CiOptionsValue, &OldCiOptionsValue, TRUE);
    // Print result
	if (!NT_SUCCESS(Status))
		return 0xDEADBEEF;
	else
		return CiOptionsValue;
}
