#include <stdio.h>
#include <time.h>
#include <stdlib.h>

int main()
{
	int Matrix[5][5];
	int i, j, Zap, Ind_i, Ind_j;
	srand(time(NULL));
	for (i = 0; i<5; i++)
	{
		for (j = 0; j<5; j++)
		{		
			printf("%d ", Matrix[i][j] = rand() % 100);
		}
		printf("\n");
	}

	/*Находит максимальное число на побчной и главной диагоналях*/
	Ind_i = Ind_j = 2;
	j = 4;
	for (i = 0; i<5; i++)
	{
		if (Matrix[Ind_i][Ind_j]<Matrix[i][i])
		{
			Ind_i = Ind_j = i;
		}
		if (Matrix[Ind_i][Ind_j]<Matrix[i][j])
		{
			Ind_i = i;
			Ind_j = j;
		}
		j--;
	}

	/*Меняет местами максимальный элемент и средний*/
	Zap = Matrix[2][2];
	Matrix[2][2] = Matrix[Ind_i][Ind_j];
	Matrix[Ind_i][Ind_j] = Zap;

	printf("\n");
	for (i = 0; i<5; i++)
	{
		for (j = 0; j<5; j++)
		{
			printf("%d ", Matrix[i][j]);
		}
		printf("\n");
	}

	getch();
	return 0;
}
