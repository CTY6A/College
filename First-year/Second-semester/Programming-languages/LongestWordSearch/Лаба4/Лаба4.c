#include <stdio.h>
#include <string.h>

int main()
{
	char str[100];
	int i, len, min;
	gets_s(str,100);
	
	min = 100000;
	len = 0;

	for (i = 0; i < (strlen(str)); i++)
		if (str[i] != ' ')
			len++;
		else
		{
			if ((len < min) && (len != 0))
			{
				min = len;
			}
			len = 0;
		}

	if (len < min)
	{
		min = len;
		len = 0;
	}
	
	printf("%d", min);

	getchar();
	return 0;
}