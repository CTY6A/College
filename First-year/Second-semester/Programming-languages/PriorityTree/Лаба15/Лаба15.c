#include <stdio.h>
#include <string.h>
#include <stdlib.h>

struct tree {
	char val;
	struct free *first, *second;
} *Head;
void rec(char *, int, struct tree *);
int rab(struct tree *);

void main()
{
	char *str = "x-f*t*p+w";
	rec(str, strlen(str), &Head);
	rab(Head);

	getchar();
	return;
}

void rec(char *str, int N, struct tree **cur)
{
	*cur = (struct tree *)malloc(sizeof(struct tree));

	if (N > 2)
	{
		int j = -1;
		for (int i = 0; i < N; i++)
			if ((str[i] == '+') | (str[i] == '-'))
				j = i;
		if (j == -1)
			for (int i = 0; i < N; i++)
				if ((str[i] == '*') | (str[i] == '/'))
					j = i;
		(*cur)->val = str[j];
		rec(str, j, &((*cur)->first));
		rec(str + j + 1, N - j - 1, &((*cur)->second));
	}
	else
	{
		(*cur)->val = str[0];
		(*cur)->second = (*cur)->first = NULL;
	}
	return;
}

int rab(struct tree *cur)
{
	if (cur != NULL)
	{
		putchar(cur->val);
		rab(cur->first);
		rab(cur->second);
	}
	return;
}