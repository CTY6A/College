#include <stdio.h>
#include <string.h>

int main()
{
	char str[100];
	int i, fl, n;
	gets_s(str, 100);

	fl = n = 0;

	for (i = 0; i < (strlen(str)); i++)
	{
		if (str[i] == ' ')
		{
			if (fl) n++;
			fl = 0;
		}
		else if ((str[i] == 'A') && (!fl))
			fl = 1;
	}

	if (fl) n++;

	printf("%d", n);

	getchar();
	return 0;
}