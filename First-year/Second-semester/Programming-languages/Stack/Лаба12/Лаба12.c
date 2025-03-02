#include <stdlib.h>
#include <stdio.h>

void main()
{
	struct Stack
	{
		int Num;
		struct Stack *Next;
	};
	struct Stack *Head1, *Head2, *Head3, *C1, *C2, *C3;
	int i;

	C1 = Head1 = (struct Stack *) malloc(sizeof(struct Stack));
	C1->Next = NULL;
	C1->Num = 22;

	C2 = Head2 = (struct Stack *) malloc(sizeof(struct Stack));
	C2->Next = NULL;
	C2->Num = 21;

	for (i = 20; i > 0; i--)
	{
		if (i % 2 == 0) 
		{
			C1 = C1->Next = (struct Stack *) malloc(sizeof(struct Stack));
			C1->Num = i;
		}
		else
		{
			C2 = C2->Next = (struct Stack *) malloc(sizeof(struct Stack));
			C2->Num = i;
		}
	}
	C1->Next = C2->Next = NULL;

	C3 = Head3 = (struct Stack *) malloc(sizeof(struct Stack));
	if (Head1->Num <= Head2->Num)
	{
		Head3->Num = Head2->Num;
		Head2 = Head2->Next;
	}
	else
	{
		Head3->Num = Head1->Num;
		Head1 = Head1->Next;
	}

	while ((Head1 != NULL) && (Head2 != NULL))
	{
		C3 = C3->Next = (struct Stack *) malloc(sizeof(struct Stack));
		if (Head1->Num <= Head2->Num)
		{
			C3->Num = Head2->Num;
			Head2 = Head2->Next;
		}
		else
		{
			C3->Num = Head1->Num;
			Head1 = Head1->Next;
		}
	}

	while (Head1 != NULL)
	{
		C3 = C3->Next = (struct Stack *) malloc(sizeof(struct Stack));

		C3->Num = Head1->Num;
		Head1 = Head1->Next;
	}

	while (Head2 != NULL)
	{
		C3 = C3->Next = (struct Stack *) malloc(sizeof(struct Stack));

		C3->Num = Head2->Num;
		Head2 = Head2->Next;
	}

	C3->Next = NULL;

	while (Head3 != NULL)
	{
		printf("%d\n", Head3->Num);
		Head3 = Head3->Next;
	}

	getchar();
	return 0;
}