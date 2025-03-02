#include <stdio.h>
#include <string.h>

int main()
{
	char str[100], res[10][10];
	int i, len, j , fl;
	gets_s(str, 100);

	for (i = 0; i < 10; i++)
		strcpy_s(res[i], 10, "");

	len = j = 0;

	for (i = 0; i < (strlen(str)); i++)
		if (str[i] != ' ')
			len++;
		else if ((len != 0) && (j < 10))
		{
			strncpy_s(res[j], 10, str + i - len, len);
			j++;

			len = 0;
		}
	if ((len != 0) && (j < 10))
		strncpy_s(res[j], 10, str + i - len, len);

	fl = i = 0;
	while ((fl) || (i<10))
	{
		if (i == 10)
		{
			i = 0;
			fl = 0;
		}
		if (strcmp(res[i], "") != 0)
		{
			len = i;
			for (j = 0; j < 10; j++)
			{
				if ((strcmp(res[len], res[j]) > 0) && (strcmp(res[j], "") != 0))
				{
					len = j;
				}
			}
			printf("%s ", res[len]);
			strcpy_s(res[len], 10, "");

			fl = 1;
		}
		i++;
	}
		



	getchar();
	return 0;
}