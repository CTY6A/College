/*
 * GccApplication1.c
 *
 * Created: 04.11.2020 0:25:37
 * Author : andre
 */ 

#define F_CPU 8000000
#include <avr/io.h>
#include <util/delay.h>
#define MS 50

int main(void)
{	
	DDRB = 0xFF;
    PORTB = 0b00000000;
	
	DDRD = 0x00;
	PORTD = 0b00000001;
	
    while(1)
    {		
		if (!(PIND & 0b00000001))
		{
			for (uint8_t i = 0; i < 8; i++)
			{
				PORTB |= (1 << i);
				_delay_ms(MS);
				PORTB &= ~(1 << i);
			}
			
			for (uint8_t i = 6; i > 0; i--)
			{
				PORTB |= (1 << i);
				_delay_ms(MS);
				PORTB &= ~(1 << i);
			}
		}	
		else
		{
			PORTB = 0;
			//_delay_ms(5000);				
		}
    }
}

