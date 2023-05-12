#pragma once

#include <Protocol/LoadedImage.h>

#ifndef ZYDIS_DISABLE_FORMATTER
#include <Zydis/Formatter.h>
#endif

#define CR0_WP ((UINTN)0x10000) // CR0.WP


//
// Waits for a timer event for N milliseconds.
// Requires current TPL to be TPL_APPLICATION.
//
EFI_STATUS EFIAPI RtlSleep(IN UINTN Milliseconds);

//
// Stalls CPU for N milliseconds.
//
EFI_STATUS RtlStall(IN UINTN Milliseconds);

// 
// Prints info about a loaded image
// 
VOID EFIAPI PrintLoadedImageInfo(IN CONST EFI_LOADED_IMAGE *ImageInfo);

//
// Similar to Print(), but for use during the kernel patching phase.
// Do not call this unless the message is specifically intended for (delayed) display output only.
// Instead use the PRINT_KERNEL_PATCH_MSG() macro so the boot debugger receives messages with no delay.
//
VOID EFIAPI AppendKernelPatchMessage(IN CONST CHAR16 *Format,...);

//
// Prints the contents of the kernel patch string buffer to the screen using OutputString() calls.
// This is a separate function because the buffer consists of zero or more null-terminated strings,
// which are printed sequentially to prevent issues with platforms that have small Print() buffer limits
//
VOID EFIAPI PrintKernelPatchInfo(VOID);

//
// Wrapper for CopyMem() that disables write protection prior to copying if needed.
//
VOID* EFIAPI CopyWpMem(OUT VOID *Destination,IN CONST VOID *Source,IN UINTN Length);

//
// Wrapper for SetMem() that disables write protection prior to copying if needed.
//
VOID* EFIAPI SetWpMem(OUT VOID *Destination,IN UINTN Length,IN UINT8 Value);

//
// Case-insensitive string comparison.
//
INTN EFIAPI StrniCmp(IN CONST CHAR16 *FirstString,IN CONST CHAR16 *SecondString,IN UINTN Length);

//
// Waits for a key to be pressed before continuing execution.
// Returns FALSE if ESC was pressed to abort, TRUE otherwise.
//
BOOLEAN EFIAPI WaitForKey(VOID);

//
// Sets the foreground colour while preserving the background colour and optionally clears the screen.
// Returns the original console mode attribute.
//
INT32 EFIAPI SetConsoleTextColour(IN UINTN TextColour,IN BOOLEAN ClearScreen);

//
// Finds a byte pattern starting at the specified address
//
EFI_STATUS EFIAPI FindPattern(IN CONST UINT8* Pattern,IN UINT8 Wildcard,IN UINT32 PatternLength,IN CONST VOID* Base,IN UINT32 Size,OUT VOID **Found);

//
// Finds a byte pattern starting at the specified address (with lots of debug spew)
//
EFI_STATUS EFIAPI FindPatternVerbose(IN CONST UINT8* Pattern,IN UINT8 Wildcard,IN UINT32 PatternLength,IN CONST VOID* Base,IN UINT32 Size,OUT VOID **Found);

//
// Zydis instruction decoder context.
//
typedef struct _ZYDIS_CONTEXT{
	ZydisDecoder Decoder;
	ZydisDecodedInstruction Instruction;
	ZydisDecodedOperand Operands[ZYDIS_MAX_OPERAND_COUNT];

	ZyanU64 InstructionAddress;
	UINTN Length;
	UINTN Offset;

#ifndef ZYDIS_DISABLE_FORMATTER
	ZydisFormatter Formatter;
	CHAR8 InstructionText[256];
#endif
} ZYDIS_CONTEXT, *PZYDIS_CONTEXT;

//
// Initializes a decoder context.
//
ZyanStatus EFIAPI ZydisInit(IN PEFI_IMAGE_NT_HEADERS NtHeaders,OUT PZYDIS_CONTEXT Context);

//
// Finds the start of a function given an address within it.
// Returns NULL if AddressInFunction is NULL (this simplifies error checking logic in calling functions).
//
UINT8* EFIAPI BacktrackToFunctionStart(IN CONST UINT8* ImageBase,IN PEFI_IMAGE_NT_HEADERS NtHeaders,IN CONST UINT8* AddressInFunction);
