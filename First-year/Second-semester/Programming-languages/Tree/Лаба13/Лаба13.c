#include <stdio.h>
#include <stdlib.h>
struct tree
{
	struct tree *First, *Second;
};
int x = 0;
int Rec(struct tree *);
void main()
{
	struct tree *Head, *Cur;

	Cur = Head = (struct tree *) malloc(sizeof(struct tree));
	Cur->First = (struct tree *) malloc(sizeof(struct tree));
	Cur->Second = (struct tree *) malloc(sizeof(struct tree));
	Cur->First->First = (struct tree *) malloc(sizeof(struct tree));
	Cur->First->First->First = NULL;
	
	Cur->First->First->Second = (struct tree *) malloc(sizeof(struct tree));
	Cur->First->First->Second->First = NULL;
	Cur->First->First->Second->Second = NULL;

	Cur->First->Second = (struct tree *) malloc(sizeof(struct tree));
	Cur->First->Second->First = NULL;
	Cur->First->Second->Second = (struct tree *) malloc(sizeof(struct tree));
	Cur->First->Second->Second->First = NULL;
	Cur->First->Second->Second->Second = NULL;
	Cur->Second->First = NULL;
	Cur->Second->Second = NULL;

	Rec(Head);
	printf("%d", x);

	getchar();
	return 0;
}
int Rec(struct tree *Cur)
{
	if (Cur != NULL)
	{
		if (Rec(Cur->First) && Rec(Cur->Second))
			x++;

		return 1;
	}
	else
		return 0;
}
