#include <stdio.h>
#include <stdlib.h>

int main()
{
	int Matrix[6][6];
	int i, j, i1, j1, t, f1, o, z;
	srand(NULL);
	for (i = 0; i<6; i++)
	{
		for (j = 0; j<6; j++)
		{
			printf("%d ", Matrix[i][j] = 2 + rand() % 15);
		}
		printf("\n");
	}
	printf("\n");
	/**/

	for (i = 0; i<6; i++)
	{
		o = z = 0;
		for (j = 0; j<6; j++)
		{
			f1 = 1;
			if ((Matrix[i][j]) > 1)
			{
				t = Matrix[i][j];
				Matrix[i][j] = 0;
				i1 = i;
				j1 = j;
				for (i1 = 0; i1 < 6; i1++)
				{
					for (j1 = 0; j1 < 6; j1++)
					{
						if (t == Matrix[i1][j1])
						{
							Matrix[i1][j1] = 1;
							f1 = 0;
						}
					}
				}
				if (!f1)
				{
					Matrix[i][j] = 1;
					o++;
				}
				else z++;
			}
			else o++;
			

			printf("%d ", Matrix[i][j]);
		}
		printf("one - %d, zero - %d \n", o, z);
	}

	for (j = 0; j < 6; j++)
	{	
		o = z = 0;
		for (i = 0; i < 6; i++)
		{
			if (Matrix[i][j] == 0)
				z++;
			else o++;
		}
		printf("%d%d", o, z);
	}

	getch();
	return 0;
}