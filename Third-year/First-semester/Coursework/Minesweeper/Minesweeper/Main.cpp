#define CELL_SIDE_LENGTH 20
#define IDB_TOP_Cell_Click 1111
#define IDB_RBUTTON_Cell_Click 2222

#include <windows.h>
#include <malloc.h>
#include <math.h>
#include <iomanip>

HINSTANCE hInst;
HWND hWnd;
HBRUSH hBrush;
HPEN hPen;
int Cmd;
int XCellNum = 19, YCellNum = 9, BombsNum = 10;
BYTE **map;
HWND **cellhWnd;
BOOL GAME_IN_PROCESS = false;
int dx[8] = { 0,0,1,-1,1,1,-1,-1 };
int dy[8] = { 1,-1,1,1,0,-1,-1,0 };

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
void Draw_new_field(HDC hdc);
void Create_map();
COLORREF Choose_color(int cell);
void Delete_Cells();
bool Clean_field();
void Delete_cell(int x, int y);
void Check_near_cells(int x, int y);
void Open_cells_around(int x, int y);



int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	hInst = hInstance;
	Cmd = nCmdShow;
	WNDCLASSEX wcex;
	wcex.cbSize = sizeof(WNDCLASSEX);
	wcex.style = CS_DBLCLKS;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	hBrush = CreateSolidBrush(RGB(220, 220, 220));
	wcex.hbrBackground = hBrush;
	wcex.lpszMenuName = NULL;
	wcex.lpszClassName = "HelloWorldClass";
	wcex.hIconSm = wcex.hIcon;
	RegisterClassEx(&wcex);

	 hWnd = CreateWindow("HelloWorldClass", "Minesweeper", WS_OVERLAPPEDWINDOW & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX, CW_USEDEFAULT, 0,
			(CELL_SIDE_LENGTH ) * XCellNum + 2, (CELL_SIDE_LENGTH + 1) * YCellNum + GetSystemMetrics(SM_CYSIZE) + 2, NULL, NULL, hInstance, NULL);

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

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_COMMAND:
	{

		if (wParam == IDB_TOP_Cell_Click)
		{
			HWND hCurrWnd = (HWND)lParam;
			int x = (int)GetProp(hCurrWnd, "ind_x");
			int y = (int)GetProp(hCurrWnd, "ind_y");
			if ((wParam & MK_LBUTTON) == MK_LBUTTON)
			{
				if (!(bool)GetProp(cellhWnd[x][y], "flag"))
				{
					if ((bool)GetProp(cellhWnd[x][y], "bomb")) {						
						GAME_IN_PROCESS = false;
						DeleteObject(map);
						Delete_Cells();
						MessageBox(NULL, "You lose!", "Game end", MB_OK);
					}
					else
					{
						Check_near_cells(x,y);
						if (Clean_field())
						{
							MessageBox(NULL, "You win!", "Game end", MB_OK);
						}
					}
				}
			}

		}
		if (wParam == IDB_RBUTTON_Cell_Click)
		{

			HWND hCurrWnd = (HWND)lParam;
			int x = (int)GetProp(hCurrWnd, "ind_x");
			int y = (int)GetProp(hCurrWnd, "ind_y");
			if (!GetProp(cellhWnd[x][y], "flag"))
			{
				SetProp(cellhWnd[x][y], "flag", (HANDLE)true);
				if ((bool)GetProp(cellhWnd[x][y], "bomb"))
					BombsNum--;
				HBITMAP hbFlag = (HBITMAP)LoadImage(hInst, "flag.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
				SendMessage(cellhWnd[x][y], BM_SETIMAGE, IMAGE_BITMAP, (LPARAM)hbFlag);
			}
			else {
				if ((bool)GetProp(cellhWnd[x][y], "bomb"))
					BombsNum++;
				SetProp(cellhWnd[x][y], "flag", (HANDLE)false);
				SendMessage(cellhWnd[x][y], BM_SETIMAGE, IMAGE_BITMAP, 0);
			}
		}
	}
	break;

	case WM_CONTEXTMENU: 
	{
		RECT wind_param;
		GetWindowRect(hWnd, &wind_param);
		int xPos = (int)LOWORD(lParam) - wind_param.left;
		int yPos = (int)HIWORD(lParam) - wind_param.top - GetSystemMetrics(SM_CYCAPTION);
		int x = (int)floor(xPos / (CELL_SIDE_LENGTH));
		int y = (int)floor(yPos/ (CELL_SIDE_LENGTH));
		if(cellhWnd[x][y] != NULL)
			SendMessage(hWnd, WM_COMMAND, IDB_RBUTTON_Cell_Click, (LPARAM)cellhWnd[x][y]);
	}
	break;

	case WM_LBUTTONDOWN:
	{
		SetFocus(NULL);
	}
	break;

	case WM_RBUTTONUP:
	{
		RECT wind_param;
		GetClientRect(hWnd, &wind_param);
		int xPos = (int)LOWORD(lParam); //-wind_param.left - 2;
		int yPos = (int)HIWORD(lParam);// -wind_param.top - GetSystemMetrics(SM_CYCAPTION) - 2;
		int x = (int)floor(xPos / (CELL_SIDE_LENGTH));
		int y = (int)floor(yPos / (CELL_SIDE_LENGTH));
		if ((map[x][y] > 0) && (map[x][y] < 9))
			Open_cells_around(x, y);
	}
	break;

	case WM_LBUTTONDBLCLK:
	{
		if (GAME_IN_PROCESS)
		{
			DeleteObject(map);
			Delete_Cells();
		}
		SendMessage(hWnd, WM_CREATE, wParam, lParam);
	}
	break;

	case WM_DESTROY:
	{
		if (GAME_IN_PROCESS) {
			DeleteObject(map);
			Delete_Cells();
		}
		PostQuitMessage(0);
	}
	break;

	case WM_CREATE:
	{
			GAME_IN_PROCESS = true;
			BombsNum = 10;
			Create_map();
			cellhWnd = (HWND**)malloc(XCellNum * sizeof(HWND*));
			for (int i = 0; i < XCellNum; i++)
			{
				cellhWnd[i] = (HWND*)malloc(YCellNum * sizeof(HWND));
			}


			for (int i = 0; i < XCellNum; i++)
				for (int j = 0; j < YCellNum; j++)
				{
					cellhWnd[i][j] = CreateWindow("button", "", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON | BS_BITMAP,
						i * (CELL_SIDE_LENGTH - 1) + 2, j * (CELL_SIDE_LENGTH - 1) + 2, CELL_SIDE_LENGTH - 1, CELL_SIDE_LENGTH - 1, hWnd, (HMENU)IDB_TOP_Cell_Click, hInst, NULL);

					SetProp(cellhWnd[i][j], ("ind_x"), (HANDLE)i);
					SetProp(cellhWnd[i][j], ("ind_y"), (HANDLE)j);
					SetProp(cellhWnd[i][j], ("flag"), (HANDLE)false);
					if (map[i][j] == 9)
					{
						SetProp(cellhWnd[i][j], ("bomb"), (HANDLE)true);
					}
					else
						SetProp(cellhWnd[i][j], ("bomb"), (HANDLE)false);

				}
	
	}

	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hWnd, &ps);
		Draw_new_field(hdc);
		EndPaint(hWnd, &ps);
	}
	break;

	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
	
}

void Draw_new_field(HDC hdc)
{
   hPen = CreatePen(PS_SOLID, 1, RGB(140, 140, 140));
	SelectObject(hdc, hPen);
	hBrush = CreateSolidBrush(RGB(220, 220, 220));
	SelectObject(hdc, hBrush);

	int y = 0;
	int x = 0;
	for (int i = 0; i < XCellNum; i++) {
		for (int j = 0; j < YCellNum; j++) 
		{
			Rectangle(hdc, x, y, x + CELL_SIDE_LENGTH, y + CELL_SIDE_LENGTH);
			RECT rt;
			SetRect(&rt, x + 1, y + 1, x + CELL_SIDE_LENGTH - 1, y + CELL_SIDE_LENGTH - 1);
			SetBkMode(hdc, TRANSPARENT);

			if ((map[i][j] > 0) & (map[i][j] < 9)) {		
				SetTextColor(hdc, Choose_color(map[i][j]));
				TCHAR s[2];
				_itoa_s(map[i][j], s, 16);
				DrawText(hdc, s , -1, &rt, DT_CENTER | DT_VCENTER);
			}
			if (map[i][j] == 9) {
				HBITMAP hbbomb = (HBITMAP)LoadImage(hInst, "bomb.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
				HDC hMemDC = CreateCompatibleDC(hdc);
				BITMAP info;
				GetObject(hbbomb, sizeof(info), &info);
				SelectObject(hMemDC, hbbomb);
				TransparentBlt(hdc, x, y,  CELL_SIDE_LENGTH-1, CELL_SIDE_LENGTH-1, hMemDC, 0, 0, info.bmWidth, info.bmHeight, RGB(255,255,255));	
			}

			y += CELL_SIDE_LENGTH - 1;
		}
		y = 0;
		x += CELL_SIDE_LENGTH - 1;
	}

	return; 
}

void Create_map()
{
	int pi, pj;
	srand(time(0));
	map = (byte**)malloc(XCellNum * sizeof(byte*));
	for (int i = 0; i < XCellNum; i++)
	{
		map[i] = (byte*)malloc(YCellNum * sizeof(byte));
	}
	for (int i = 0; i < XCellNum; i++)
		for (int j = 0; j < YCellNum; j++)
		{
			map[i][j] = 0;
		}
	for (int i = 0; i < BombsNum; i++)
	{
		do {
			pi = rand() % XCellNum;
			pj = rand() % YCellNum;
		} while (map[pi][pj] == 9);
		map[pi][pj] = 9;
	}
	for (int i = 0; i < XCellNum; i++)
		for (int j = 0; j < YCellNum; j++)
		{
			if (map[i][j] != 9)
			{
				int count = 0;
				if (i != 0 && j != 0 && map[i - 1][j - 1] == 9)
					count++;
				if (i != 0 && map[i - 1][j] == 9)
					count++;
				if (j != 0 && map[i][j - 1] == 9)
					count++;
				if (i != XCellNum - 1 && j != YCellNum - 1 && map[i + 1][j + 1] == 9)
					count++;
				if (i != XCellNum - 1 && map[i + 1][j] == 9)
					count++;
				if (j != YCellNum - 1 && map[i][j + 1] == 9)
					count++;
				if (i != XCellNum - 1 && j != 0 && map[i + 1][j - 1] == 9)
					count++;
				if (i != 0 && j != YCellNum - 1 && map[i - 1][j + 1] == 9)
					count++;
				map[i][j] = count;
			}

		}
	return;
}

COLORREF Choose_color(int cell)
{
	COLORREF rgb;
	switch (cell) {
		case 1:
			rgb = RGB(0, 20, 200);
			break;
		case 2:
			rgb = RGB(0, 100, 0);
			break;
		case 3:
			rgb = RGB(255, 0, 0);
			break;
		case 4:
			rgb = RGB(0, 0, 120);
			break;
		case 5:
			rgb = RGB(120, 0, 0);
			break;
		case 6:
			rgb = RGB(0, 100, 170);
			break;
		case 7:
			rgb = RGB(120, 0, 130);
			break;
		case 8:
			rgb = RGB(50, 0, 0);
			break;
	}
	return rgb;
}

void Delete_Cells() {
	for (int i = 0; i < XCellNum; i++)
		for (int j = 0; j < YCellNum; j++) 
		{
			if (cellhWnd[i][j] != NULL)
			{
				RemoveProp(cellhWnd[i][j], "flag");
				RemoveProp(cellhWnd[i][j], "bomb");
				RemoveProp(cellhWnd[i][j], "ind_x");
				RemoveProp(cellhWnd[i][j], "ind_y");
				DestroyWindow(cellhWnd[i][j]);
				cellhWnd[i][j] = NULL;
			}
		}
	return;
}

void Delete_cell(int x, int y) {
	RemoveProp(cellhWnd[x][y], "flag");
	RemoveProp(cellhWnd[x][y], "bomb");
	RemoveProp(cellhWnd[x][y], "ind_x");
	RemoveProp(cellhWnd[x][y], "ind_y");
	DestroyWindow(cellhWnd[x][y]);
	cellhWnd[x][y] = NULL;
	return;
}

bool Clean_field() {
	for (int i = 0; i < XCellNum; i++)
		for (int j = 0; j < YCellNum; j++)
		{
			if ((cellhWnd[i][j] != NULL) && (!(bool)GetProp(cellhWnd[i][j], "bomb")))
			{
				return false;
			}
		}
	return true;
}

void Check_near_cells(int x, int y)
{
		if (cellhWnd[x][y] == NULL) return; 
		Delete_cell(x, y);
		if (map[x][y] == 0) 
		{
			for (int k = 0; k < 8; k++) 
			{
				if (( (x + dx[k])>= 0) && ((x + dx[k] < XCellNum)) && (((y + dy[k]) >=0) && ((y +dy[k]) < YCellNum)))
					Check_near_cells(x + dx[k], y + dy[k]); 
			}
		}
}

void Open_cells_around(int x, int y)
{
	int num = map[x][y];
	for (int k = 0; k < 8; k++)
	{
		if (((x + dx[k]) >= 0) && ((x + dx[k] < XCellNum)) && (((y + dy[k]) >= 0) && ((y + dy[k]) < YCellNum)))
			if ((cellhWnd[x + dx[k]][y + dy[k]] != NULL) && ((bool)GetProp(cellhWnd[x + dx[k]][y + dy[k]], "flag") == true))
				num--;
	}
	if (num == 0)
	{
		for (int k = 0; k < 8; k++)
		{
			if (((x + dx[k]) >= 0) && ((x + dx[k] < XCellNum)) && (((y + dy[k]) >= 0) && ((y + dy[k]) < YCellNum)))
				if (cellhWnd[x + dx[k]][y + dy[k]] != NULL)

					SendMessage(hWnd, WM_COMMAND, IDB_TOP_Cell_Click, (LPARAM)cellhWnd[x + dx[k]][y + dy[k]]);
		}
	}
	return;
}
