#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() 
{
	struct TPupil 
	{
		char surname[10];
		int group, rating;
	};

	struct TPupil *Arr = NULL;
	char direct = '0';
	char time[10];
	int Length = 0, i, j;

	FILE *p1;

	fopen_s(&p1, "C:/Users/User/Desktop/List.txt", "r");
	if (p1 != NULL)
	{
		while (!feof(p1))
		{
			Arr = (struct TPupil *)realloc(Arr, ++Length * sizeof(struct TPupil));

			fscanf_s(p1, "%s %d %d\n", &Arr[Length - 1].surname, 10, &Arr[Length - 1].group, &Arr[Length - 1].rating);
		}

		fclose(p1);
	}

	for (i = 0; i < Length; i++)
		printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);

	printf("----------------------------------------------------------------------------------------------------------------------\n");

	while (direct != '5')
	{

		direct = '0';
		printf("1-Add pupil\n2-Edit pupil\n3-Delete pupil\n4-Show list\n5-Exit\n\n");

		direct = getchar();
		if (direct == '\n')
			direct = getchar();

		printf("\n");
		
		if (direct == '1')
		{
			Arr = (struct TPupil *)realloc(Arr, ++Length * sizeof(struct TPupil));
			printf("Input Name: ");
			scanf_s("%s", &Arr[Length - 1].surname, 10);
			printf("Input Group: ");
			scanf_s("%d", &Arr[Length - 1].group);
			printf("Input Rating: ");
			scanf_s("%d", &Arr[Length - 1].rating);

			for (i = 0; i < Length; i++)
				printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
		}

		if (direct == '2')
		{
			printf("Input Name: ");
			scanf_s("%s", &time, 10);

			for (i = 0; i < Length; i++)
			{
				if (strcmp(time, Arr[i].surname) == 0)
				{

					printf("\n");

					printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
					printf("Input New Name: ");
					scanf_s("%s", &Arr[i].surname, 10);
					printf("Input New Group: ");
					scanf_s("%d", &Arr[i].group);
					printf("Input New Rating: ");
					scanf_s("%d", &Arr[i].rating);
				}
			}

			for (i = 0; i < Length; i++)
				printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
		}

		if (direct == '3')
		{
			printf("Input Name: ");
			scanf_s("%s", &time, 10);

			for (i = 0; i < Length; i++)
			{
				if (strcmp(time, Arr[i].surname) == 0)
				{
					if (i < --Length)
						for (j = i; j < Length; j++)
							Arr[j] = Arr[j + 1];

					Arr = (struct TPupil *)realloc(Arr, Length * sizeof(struct TPupil));
				}
			}

			for (i = 0; i < Length; i++)
				printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
		}

		if (direct == '4')
		{
			printf("Input Group: ");
			scanf_s("%d", &j);

			for (i = 0; i < Length; i++)
				if (j == Arr[i].group)
					printf("%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
		}

		printf("----------------------------------------------------------------------------------------------------------------------\n");
	};

	fopen_s(&p1, "C:/Users/User/Desktop/List.txt", "w");
	if (Arr != NULL)
	{
		for (i=0;i<Length;i++)
			fprintf(p1, "%s %d %d\n", Arr[i].surname, Arr[i].group, Arr[i].rating);
		
		free(Arr);
	}
	fclose(p1);

	getchar();
	return 0;
}