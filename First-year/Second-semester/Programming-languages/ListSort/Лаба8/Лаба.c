#include <stdio.h>
#include <string.h>

int main()
{
	struct Titem
	{
		char surname[20];
		char town[15];
		int mark;
	};
	struct Titem pupil[4], res[4];
	struct Titem t;
	char str[15], city[15];
	int i, j, num, fl, k;

	strcpy_s(pupil[0].surname, 20, "SASHA");
	strcpy_s(pupil[0].town, 15, "BREST");
	pupil[0].mark = 2;

	strcpy_s(pupil[1].surname, 20, "PASHA");
	strcpy_s(pupil[1].town, 15, "MINSK");
	pupil[1].mark = 7;

	strcpy_s(pupil[2].surname, 20, "KATYA");
	strcpy_s(pupil[2].town, 15, "MINSK");
	pupil[2].mark = 6;

	strcpy_s(pupil[3].surname, 20, "ANTON");
	strcpy_s(pupil[3].town, 15, "MINSK");
	pupil[3].mark = 9;

	num = 0;
	for (i = 0; i < 4; i++)
		if (strcmp("MINSK", pupil[i].town) == 0)
		{
			res[num] = pupil[i];
			num++;
		}

	fl = i = 0;
	while ((fl) || (i<num))
	{
		if (i == num)
		{
			i = 0;
			fl = 0;
		}
		if (strcmp(res[i].surname, "") != 0)
		{
			k = i;
			for (j = 0; j < num; j++)
			{
				if ((strcmp(res[k].surname, res[j].surname) > 0) && (strcmp(res[j].surname, "") != 0))
				{
					k = j;
				}
			}
			printf("%s %s %d \n", res[k].surname, res[k].town, res[k].mark);
			strcpy_s(res[k].surname, 15, "");

			fl = 1;
		}
		i++;
	}

	getchar();
	return 0;
}