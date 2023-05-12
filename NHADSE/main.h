#pragma once
#include <iostream>
#include <string>
#include "Converters.cpp"


DECLSPEC_NOINLINE static VOID PrintUsage();

int wmain(int argc, wchar_t** argv) {
#ifdef NDEBUG
	NT_ASSERT(argc != 0);
#endif // ! Debug

	std::cout<< "NHA EFI Backdoor\n";
	std::cout.flush();

    if (argc == 1 || argc > 3 || (argc == 3 && wcstoul(argv[2], nullptr, 16) == 0)) {
        PrintUsage();
        return 0;
    }

    // Parse command line params
    const BOOLEAN Win8OrHigher = (RtlNtMajorVersion() >= 6 && RtlNtMinorVersion() >= 2) || RtlNtMajorVersion() > 6;
    const ULONG EnabledCiOptionsValue = Win8OrHigher ? 0x6 : CODEINTEGRITY_OPTION_ENABLED;
    const PCWCHAR CiOptionsName = Win8OrHigher ? L"g_CiOptions" : L"g_CiEnabled";
    ULONG CiOptionsValue = 0;
    BOOLEAN ReadOnly = FALSE;

    if (wcsncmp(argv[1], L"-c", sizeof(L"-c") / sizeof(WCHAR) - 1) == 0 ||
        wcsncmp(argv[1], L"--check", sizeof(L"--check") / sizeof(WCHAR) - 1) == 0) {
        std::cout<<"Checking For Working EFI SetVariable() Backdoor...\n";
		std::cout.flush();
        const NTSTATUS Status = TestSetVariableHook();
        if (NT_SUCCESS(Status)){ // Any errors have already been printed
            std::cout << "Success!\n";
		std::cout.flush();
		}
        return Status;
    }
    if (wcsncmp(argv[1], L"-r", sizeof(L"-r") / sizeof(WCHAR) - 1) == 0 ||
        wcsncmp(argv[1], L"--read", sizeof(L"--read") / sizeof(WCHAR) - 1) == 0) {
        CiOptionsValue = 0;
        ReadOnly = TRUE;
		std::cout << "Querying g_CiOptions Value...\n";
		std::cout.flush();
    }else if (wcsncmp(argv[1], L"-d", sizeof(L"-d") / sizeof(WCHAR) - 1) == 0 ||
        wcsncmp(argv[1], L"--disable", sizeof(L"--disable") / sizeof(WCHAR) - 1) == 0) {
        CiOptionsValue = 0;
        std::cout<<"Disabling DSE...\n";
		std::cout.flush();
    }else if (wcsncmp(argv[1], L"-e", sizeof(L"-e") / sizeof(WCHAR) - 1) == 0 ||
        wcsncmp(argv[1], L"--enable", sizeof(L"--enable") / sizeof(WCHAR) - 1) == 0) {

		CiOptionsValue = EnabledCiOptionsValue;
		std::cout << "(Re)Enabling DSE...\n";
		std::cout.flush();
    }else if (wcsncmp(argv[1], L"-i", sizeof(L"-i") / sizeof(WCHAR) - 1) == 0 ||
        wcsncmp(argv[1], L"--info", sizeof(L"--info") / sizeof(WCHAR) - 1) == 0) {
        return DumpSystemInformation();
    }

    // Trigger EFI driver exploit and write new value to g_CiOptions/g_CiEnabled
    ULONG OldCiOptionsValue;
    const NTSTATUS Status = AdjustCiOptions(CiOptionsValue, &OldCiOptionsValue, ReadOnly);

    // Print result
    if (!NT_SUCCESS(Status)) {
        std::cout<<"Backdoor: AdjustCiOptions Failed: 0x"+ ToHex(Status) +"\n";
		std::cout.flush();
//std::cout << "Execute Used: " + ToString(argv[1]);std::cout.flush();
		
    }else {
        if (ReadOnly)
            std::cout<<"Success.";
		else{
			std::cout << "Successfully "+ ToString(CiOptionsValue == 0 ? L"Disabled" : L"(Re)Enabled") + " DSE. Original";
		}
		std::cout.flush();
		std::cout << "g_CiOptions Value: 0x" + ToHex(OldCiOptionsValue) + "\n";
		std::cout.flush();
    }
    return Status;
}
