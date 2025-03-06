#include <windows.h>
#include <stdio.h>
#include <string.h>
#include <fstream>
#include <vector>

LRESULT CALLBACK WndProc(HWND hWnd, UINT message,
	WPARAM wParam, LPARAM lParam);

HDC memDC;
HBITMAP memBmp;
HBRUSH hBrush;

int wndx, wndy;

std::vector<WCHAR *> vect;
char path[] = "1.txt";

int M = 4, N = 5;
int textWidth;
int penWidth = 1;
int indent = 10;

int APIENTRY WinMain(HINSTANCE hInstance, 
	HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	WNDCLASSEX wcex; 	
	wcex.cbSize = sizeof(WNDCLASSEX);
	wcex.style = NULL;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = NULL;
	wcex.lpszClassName = L"HelloWorldClass";
	wcex.hIconSm = wcex.hIcon;
	RegisterClassEx(&wcex);
		
	HWND hWnd = CreateWindow(L"HelloWorldClass", L"Hello, World!",
		WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, 0,
		CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);	

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);
	
	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return (int)msg.wParam;
}

void ReadFile(char *path, int M, int N, std::vector<WCHAR *>& vect) {
	std::wifstream ifile(path);	
	while (!ifile.eof() && vect.size() < M * N) {	
		WCHAR *str = new WCHAR[100];
		ifile.getline(str, 99, L'\n');
		vect.push_back(str);
	}
	ifile.close();	

	for (int i = vect.size(); i < M * N; i++) {
		WCHAR *str = new WCHAR[6];
		lstrcpyW(str, L"X X X");
		vect.push_back(str);
	}
}

void FillMemDC(HDC hDC, int x1, int y1, int x2, int y2) {
	RECT rt;
	SetRect(&rt, x1, y1, x2, y2);
	FillRect(hDC, &rt, hBrush);
}

void DrawRect(HDC hDC, int x, int y, int width, int height, WCHAR* text) {
	HPEN hPen = (HPEN)GetStockObject(BLACK_PEN);
	SelectObject(hDC, hPen);
	Rectangle(hDC, x, y, x + width, y + height);

	RECT rt;
	SetRect(&rt, x + 1, y + 1, x + width - 1, y + height - 1);
	DrawText(hDC, text, -1, &rt, DT_EDITCONTROL | DT_WORDBREAK);
}

void DrawTable(HDC hDC, const std::vector<WCHAR *>& vect, int M, int N) {
	int y = indent;
	std::vector<int> vHeight;
	for (int i = 0; i < M; i++) {
		int textHeight = 0;		
		for (int j = 0; j < N; j++) {
			RECT rt;
			SetRect(&rt, 0, 0, textWidth, 0);
			DrawText(hDC, vect[i * N + j], -1, &rt, DT_EDITCONTROL | DT_WORDBREAK | DT_CALCRECT);
			if (textHeight < rt.bottom) {
				textHeight = rt.bottom;
			}
		}
		vHeight.push_back(textHeight);
		y += textHeight + penWidth;
	}

	if (M) {
		int delta = (wndy - indent - y) / M;
		if (delta > 0) {
			for (int i = 0; i < vHeight.size(); i++) {
				vHeight[i] += delta;
			}
		}
	}

	y = indent;
	int x = indent;
	for (int i = 0; i < M; i++) {
		for (int j = 0; j < N; j++) {
			DrawRect(hDC, x, y, textWidth + 2 * penWidth, vHeight[i] + 2 * penWidth, vect[i * N + j]);
			x += textWidth + penWidth;
		}
		x = indent;
		y += vHeight[i] + penWidth;
	}
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message,
	WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_CREATE: {
		int scrx = GetSystemMetrics(SM_CXSCREEN);
		int scry = GetSystemMetrics(SM_CYSCREEN);

		HDC winDC = GetDC(hWnd);
		memDC = CreateCompatibleDC(winDC);		
		memBmp = CreateCompatibleBitmap(winDC, scrx, scry);
		ReleaseDC(hWnd, winDC);
		SelectObject(memDC, memBmp);
		SetBkMode(memDC, TRANSPARENT);
		hBrush = CreateSolidBrush(RGB(253, 221, 230));
		SelectObject(memDC, hBrush);
		FillMemDC(memDC, 0, 0, scrx, scry);

		ReadFile(path, M, N, vect);
		break;
	}
	case WM_SIZE: {
		wndy = HIWORD(lParam);
		wndx = LOWORD(lParam);
		if (N) {
			textWidth = (wndx - 2 * indent) / N - 1;
		}
		FillMemDC(memDC, 0, 0, wndx, wndy);
		DrawTable(memDC, vect, M, N);
		InvalidateRect(hWnd, NULL, false);
		break;
	}
	case WM_PAINT: {
		PAINTSTRUCT ps;
		HDC winDC = BeginPaint(hWnd, &ps);
		BitBlt(winDC, 0, 0, wndx, wndy, memDC, 0, 0, SRCCOPY);
		EndPaint(hWnd, &ps);
		break;
	}
	case WM_DESTROY:
		while (!vect.empty())
		{
			delete[] vect.back(); 
			vect.pop_back();
		}
		DeleteObject(hBrush);
		DeleteObject(memBmp);
		DeleteDC(memDC);
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}