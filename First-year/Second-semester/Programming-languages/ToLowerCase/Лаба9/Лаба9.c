#include <stdio.h>

int main() {

	FILE *p1;
	fopen_s(&p1, "C:/Users/User/Desktop/F.txt", "r");
	FILE *p2;
	fopen_s(&p2, "C:/Users/User/Desktop/G.txt", "w");

	char x;
	fscanf_s(p1, "%c", &x);
	while (!feof(p1))
	{
		
		if ((x >= 65) && (x <= 90))
			x = x + 32;
		fprintf(p2, "%c", x);
		fscanf_s(p1, "%c", &x);
	}

	fclose(p1);
	fclose(p2);

	getchar();
	return 0;
}