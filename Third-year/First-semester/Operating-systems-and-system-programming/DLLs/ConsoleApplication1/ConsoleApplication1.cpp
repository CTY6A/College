#include "pch.h"
#include <windows.h>
#include <tchar.h>

#pragma comment ( lib, "DllReplaceAllStrings.lib" )

WINGDIAPI BOOL WINAPI ReadVirtualMemory(DWORD);

int main()
{
	HWND hFind = FindWindow(NULL, TEXT("C:\\Users\\User\\Desktop\\check for l3\\x64\\Debug\\a1.exe"));
	if (hFind == NULL) {
		_tprintf(TEXT("fail FindWindow\n"));
	}
	DWORD dwProcessId = 0;
	GetWindowThreadProcessId(hFind, &dwProcessId);
	if (ReadVirtualMemory(dwProcessId)) {
		_tprintf(TEXT("true\n"));
	}
	else {
		_tprintf(TEXT("this process %d isn't running\n"), dwProcessId);
	}
	return 0;
}
