/*
 * GccApplication1.c
 *
 * Created: 03.11.2020 21:39:17
 * Author : andre
 */ 

//#include <avr/io.h>
//
//
//int main(void)
//{
    ///* Replace with your application code */
    //while (1) 
    //{
    //}
//}

#ifndef F_CPU
#define F_CPU 8000000UL
#endif


#include <avr/io.h>
#include <util/delay.h>

int main(void)
{
	DDRC = 0xFF;
	
	while(1)
	{
		PORTC = 0xFF;
		_delay_ms(1000);
		PORTC = 0x00;
		_delay_ms(1000);
	}
}

