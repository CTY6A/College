#ifndef UNICODE
#define UNICODE
#endif
#pragma comment(lib, "mpr.lib")

#include <winsock2.h>
#include <iphlpapi.h>
#include <stdio.h>
#include <windows.h>
#include <winnetwk.h>
#pragma comment(lib, "IPHLPAPI.lib")
#define MALLOC(x) HeapAlloc(GetProcessHeap(), 0, (x))
#define FREE(x) HeapFree(GetProcessHeap(), 0, (x))

void DisplayStruct(DWORD dwNesting, LPNETRESOURCE lpnrLocal);

BOOL WINAPI EnumerateFunc(LPNETRESOURCE lpnr, DWORD dwNesting)
{
	DWORD dwResult, dwResultEnum;
	HANDLE hEnum;
	DWORD cbBuffer = 16384;
	DWORD cEntries = -1; // Search for all objects
	LPNETRESOURCE lpnrLocal;
	DWORD i;

	// Вызов функции WNetOpenEnum для начала перечисления.
	dwResult = WNetOpenEnum(RESOURCE_GLOBALNET, RESOURCETYPE_ANY, 0, lpnr, &hEnum);
	// RESOURCES_GLOBALNET - все сетевые ресурсы
	// RESOURCETYPE_ANY - все типы ресурсов
	// 0 - перечислить все ресурсы
	// lpnr = NULL при первом вызове функции
	// hEnum - дескриптор ресурса

	// Обработка ошибок
	if (dwResult != NO_ERROR) {		
		printf("WnetOpenEnum failed with error %d\n", dwResult);
		return FALSE;
	}

	// Вызвов функции GlobalAlloc для выделения ресурсов.
	lpnrLocal = (LPNETRESOURCE)GlobalAlloc(GPTR, cbBuffer);
	if (lpnrLocal == NULL) {
		printf("WnetOpenEnum failed with error %d\n", dwResult);
		return FALSE;
	}

	do {
		// Инициализируем буфер.
		ZeroMemory(lpnrLocal, cbBuffer);

		// Вызов функции WNetEnumResource для продолжения перечисления
		// доступных ресурсов сети.
		dwResultEnum = WNetEnumResource(hEnum, &cEntries, lpnrLocal, &cbBuffer);

		// Если вызов был успешен, то структуры обрабатываются циклом.
		if (dwResultEnum == NO_ERROR) {
			for (i = 0; i < cEntries; i++) {
				// Функция для отображения содержимого структур NetResources
				DisplayStruct(dwNesting, &lpnrLocal[i]);
				if (RESOURCEUSAGE_CONTAINER == (lpnrLocal[i].dwUsage & RESOURCEUSAGE_CONTAINER))
					if (!EnumerateFunc(&lpnrLocal[i], dwNesting + 1)) 
						printf("EnumerateFunc returned FALSE\n");
			}
		}
		// Обработка ошибок
		else if (dwResultEnum != ERROR_NO_MORE_ITEMS) {
			printf("WNetEnumResource failed with error %d\n", dwResultEnum);
			break;
		}
	} while (dwResultEnum != ERROR_NO_MORE_ITEMS);

	// Вызов функции GlobalFree для очистки ресурсов
	GlobalFree((HGLOBAL)lpnrLocal);

	// Вызов WNetCloseEnum для остановки перечисления
	dwResult = WNetCloseEnum(hEnum);

	if (dwResult != NO_ERROR) {
		printf("WNetCloseEnum failed with error %d\n", dwResult);
		return FALSE;
	}

	return TRUE;
}

void DisplayStruct(DWORD dwNesting, LPNETRESOURCE lpnrLocal)
{
	for (int i = 0; i < dwNesting; i++) {
		printf("  ");
	}

	printf("%S\n",lpnrLocal->lpRemoteName);
	if (lpnrLocal->lpComment) {
		for (int i = 0; i < dwNesting; i++) {
			printf("  ");
		}

		printf("Comment: %S\n", lpnrLocal->lpComment);
	}

	for (int i = 0; i < dwNesting; i++) {
		printf("  ");
	}

	printf("Type: ");
	switch (lpnrLocal->dwType) {
	case (RESOURCETYPE_ANY):
		printf("any\n");
		break;
	case (RESOURCETYPE_DISK):
		printf("disk\n");
		break;
	case (RESOURCETYPE_PRINT):
		printf("print\n");
		break;
	default:
		printf("unknown type %d\n", lpnrLocal->dwType);
		break;
	}

	for (int i = 0; i < dwNesting; i++) {
		printf("  ");
	}

	printf("DisplayType: ");
	switch (lpnrLocal->dwDisplayType) {
	case (RESOURCEDISPLAYTYPE_GENERIC):
		printf("generic\n");
		break;
	case (RESOURCEDISPLAYTYPE_DOMAIN):
		printf("domain\n");
		break;
	case (RESOURCEDISPLAYTYPE_SERVER):
		printf("server\n");
		break;
	case (RESOURCEDISPLAYTYPE_SHARE):
		printf("share\n");
		break;
	case (RESOURCEDISPLAYTYPE_FILE):
		printf("file\n");
		break;
	case (RESOURCEDISPLAYTYPE_GROUP):
		printf("group\n");
		break;
	case (RESOURCEDISPLAYTYPE_NETWORK):
		printf("network\n");
		break;
	default:
		printf("unknown display type %d\n", lpnrLocal->dwDisplayType);
		break;
	}

	printf("\n");
}



int GetMacAddress() {
	PIP_ADAPTER_INFO pAdapterInfo;
	PIP_ADAPTER_INFO pAdapter = NULL;
	DWORD dwRetVal = 0;
	UINT i;

	ULONG ulOutBufLen = sizeof(IP_ADAPTER_INFO);
	pAdapterInfo = (IP_ADAPTER_INFO *)MALLOC(sizeof(IP_ADAPTER_INFO));
	if (pAdapterInfo == NULL) {
		printf("Error allocating memory needed to call GetAdaptersinfo\n");
		return 1;
	}

	if (GetAdaptersInfo(pAdapterInfo, &ulOutBufLen) == ERROR_BUFFER_OVERFLOW) {
		FREE(pAdapterInfo);
		pAdapterInfo = (IP_ADAPTER_INFO *)MALLOC(ulOutBufLen);
		if (pAdapterInfo == NULL) {
			printf("Error allocating memory needed to call GetAdaptersinfo\n");
			return 1;
		}
	}

	if ((dwRetVal = GetAdaptersInfo(pAdapterInfo, &ulOutBufLen)) == NO_ERROR) {
		pAdapter = pAdapterInfo;
		while (pAdapter) {
			printf("\tAdapter Name: \t%s\n", pAdapter->AdapterName);
			printf("\tAdapter Desc: \t%s\n", pAdapter->Description);
			printf("\tAdapter Addr: \t");

			for (i = 0; i < pAdapter->AddressLength; i++) {
				if (i == (pAdapter->AddressLength - 1))
					printf("%.2X\n", (int)pAdapter->Address[i]);
				else
					printf("%.2X-", (int)pAdapter->Address[i]);
			}
			
			pAdapter = pAdapter->Next;
			printf("\n");
		}
	}
	else {
		printf("GetAdaptersInfo failed with error: %d\n", dwRetVal);
	}
	if (pAdapterInfo)
		FREE(pAdapterInfo);
	return 0;
}


int main()
{
	GetMacAddress();

	printf("All working groups, computers in the network and their resources:\n\n");

	if (EnumerateFunc(NULL, 0) == FALSE)
		printf("Call to EnumerateFunc failed\n");

	getchar();
	return 0;
}