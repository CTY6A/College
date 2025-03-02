#include <stdio.h>
#include <time.h>
#include <stdlib.h>

int main()
{
	int Matrix[5][5];
	int i, j, Sum, Max, k, m;
	srand(time(NULL));

	//ввод матрицы

	for (i = 0; i<5; i++)
	{
		for (j = 0; j<5; j++)
		{
			printf("%d ", Matrix[i][j] = rand() % 100);
		}

		Max = 0;

		for (j = 0; j < 5; j++)
		{
			Max = Matrix[i][j] + Max;
		}
		printf("%d\n", Max);
	}

	printf("\n");

	//основной алгоритм

	for (m = 0; m<4; m++)
	{
		Max = 0;
		for (i = 0; i < 5; i++)
		{
			Max = Matrix[m][i] + Max;
		}

		j = m;

		for (k = m + 1; k<5; k++)
		{
			Sum = 0;

			for (i = 0; i < 5; i++)
			{
				Sum = Matrix[k][i] + Sum;
			}

			if (Sum > Max)
			{
				j = k;
				Max = Sum;
			}
		}
		
		if (j != m)
		{
			for (i = 0; i < 5; i++)
			{
				Max = Matrix[m][i];
				Matrix[m][i] = Matrix[j][i];
				Matrix[j][i] = Max;
			}
		}
	}


	//вывод матрицы

	for (i = 0; i<5; i++)
	{
		for (j = 0; j<5; j++) 
		{
			printf("%d ", Matrix[i][j]);
		}

		Max = 0;

		for (j = 0; j < 5; j++)
		{
			Max = Matrix[i][j] + Max;
		}
		printf("%d ", Max);
		printf("\n");
	}

	getch();
	return 0;
}