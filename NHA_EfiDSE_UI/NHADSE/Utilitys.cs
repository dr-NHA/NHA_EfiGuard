using System.Reflection;
using System.Security.Principal;

namespace NHADSE{
public static class Utilitys{
public static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
public static bool IsAdmin { get; }= new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

public static string ToFormattedString(this byte[] data, string Format = "X2"){
var S = "";
foreach (byte b in data) S += (Format.ToLower() == "x2" ? "0x" : "") + b.ToString(Format) + " ";
return S.Trim();
}

}}