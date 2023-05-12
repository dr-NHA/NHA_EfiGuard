#pragma once
#include <string>
#include <guiddef.h>
#include <wtypes.h>

template<typename T> static std::string ToHex(T w) {
size_t hex_len = sizeof(T) << 1;
static const char* digits = "0123456789ABCDEF";
std::string rc(hex_len, '0');
for (size_t i = 0, j = (hex_len - 1) * 4; i < hex_len; ++i, j -= 4)
rc[i] = digits[(w >> j) & 0x0f];
return rc;
}

#pragma region ToString

template<typename T> static std::string __TRY_ToString(T W) {
std::wstring cws(W);
return std::string(cws.begin(), cws.end());
}
static std::string ToString(WCHAR* W) { return __TRY_ToString(W); }
static std::string ToString(const WCHAR* W) { return __TRY_ToString(W); }


static std::string ToString(const unsigned char* W) {
return std::string(reinterpret_cast<const char*>(W));
}

static std::string ToString(unsigned char W) { return std::to_string(W); }
static std::string ToString(char W) { return std::to_string(W); }
static std::string ToString(unsigned long W) { return std::to_string(W); }
static std::string ToString(long W) { return std::to_string(W); }
static std::string ToString(unsigned short W) { return std::to_string(W); }
static std::string ToString(short W) { return std::to_string(W); }
static std::string ToString(unsigned int W) { return std::to_string(W); }
static std::string ToString(int W) { return std::to_string(W); }
static std::string BToString(BOOLEAN W) { return W?"True":"False"; }

static std::string ToString(GUID Guid) {
return
ToHex(Guid.Data1)+
ToHex(Guid.Data2)+
ToHex(Guid.Data4[0])+
ToHex(Guid.Data4[1])+
ToHex(Guid.Data4[2])+
ToHex(Guid.Data4[3])+
ToHex(Guid.Data4[4])+
ToHex(Guid.Data4[5])+
ToHex(Guid.Data4[6])+
ToHex(Guid.Data4[7]);
}
#pragma endregion


