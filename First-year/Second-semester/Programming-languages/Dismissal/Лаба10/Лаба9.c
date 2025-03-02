#include <stdio.h>
#include <string.h>


int main() {


	struct TWorker
	{
		char Name[20];
		char Special[15];
		int Salary;
	};
	struct TWorker Worker[4];


	strcpy_s(Worker[0].Name, 20, "SASHA");
	strcpy_s(Worker[0].Special, 15, "INGENER");
	Worker[0].Salary = 2000;

	strcpy_s(Worker[1].Name, 20, "PASHA");
	strcpy_s(Worker[1].Special, 15, "MANAGER");
	Worker[1].Salary = 7000;

	strcpy_s(Worker[2].Name, 20, "KATYA");
	strcpy_s(Worker[2].Special, 15, "DOCTOR");
	Worker[2].Salary = 6000;

	strcpy_s(Worker[3].Name, 20, "ANTON");
	strcpy_s(Worker[3].Special, 15, "SAILER");
	Worker[3].Salary = 9000;


	FILE *p1;
	fopen_s(&p1, "C:/Users/User/Desktop/First.txt", "w");

	int i;
	for (i = 0; i <= 3; i++)
	{
		fprintf(p1, "Worker - %s ", Worker[i].Name);
		fputs(Worker[i].Special, p1);
		fprintf(p1, ", %d\n", Worker[i].Salary);
	}

	fclose(p1);

	
	FILE *p2;
	fopen_s(&p2, "C:/Users/User/Desktop/Second.txt", "w");

	int max;
	printf("Enter max salary: ");
	scanf_s("%d", &max, 1);

	for (i = 0; i <= 3; i++)
	{
		if (Worker[i].Salary < max)
		{
			fprintf(p1, "Worker - %s ", Worker[i].Name);
			fputs(Worker[i].Special, p1);
			fprintf(p1, ", %d\n", Worker[i].Salary);
		}
	}

	fclose(p2);

	getchar();
	return 0;
}