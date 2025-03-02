#include <stdio.h>
#include <string.h>

int main()
{
	char str[100], data[4][8], trans[4][8], word[10];
	int i, j, k, fl;
	gets_s(str, 100);

	strcpy_s(data[0], 8, "MADE");
	strcpy_s(data[1], 8, "AMERICA");
	strcpy_s(data[2], 8, "GREAT");
	strcpy_s(data[3], 8, "AGAIN");

	strcpy_s(trans[0], 8, "SDELAEM");
	strcpy_s(trans[1], 8, "AMERICU");
	strcpy_s(trans[2], 8, "VELIKOI");
	strcpy_s(trans[3], 8, "SNOVA");

	i = 0;

	while (i < strlen(str))
	{
		if (!((str[i] >= 65) && (str[i] <= 90)))
		{
			printf("%c", str[i]);
			i++;
		}
		else
		{
			j = i;
			while ((str[j] >= 65) && (str[j] <= 90) && (j < strlen(str)))				
				j++;

			strncpy_s(word, 10, str + i, j - i);

			fl = 0;

			for (k = 0; k < 4; k++)
			{
				if (strcmp(data[k], word) == 0)
				{
					printf("%s", trans[k]);
					fl = 1;
				}
			}			
			if (!fl) printf("%s", word);
			i = j;
		}
	}


	getchar();
	return 0;
}