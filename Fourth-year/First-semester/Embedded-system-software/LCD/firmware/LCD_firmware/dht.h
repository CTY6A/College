#include "main.h"
#include "lcd.h"

#define DHT11_PIN 0

uint8_t c=0,I_RH,D_RH,I_Temp,D_Temp,CheckSum;

void SendRequest()
{
	DDRD |= (1<<DHT11_PIN);
	PORTD &= ~(1<<DHT11_PIN);
	_delay_ms(20);
	PORTD |= (1<<DHT11_PIN);
}

void GetResponse()
{
	DDRD &= ~(1<<DHT11_PIN);
	while(PIND & (1<<DHT11_PIN));
	while((PIND & (1<<DHT11_PIN))==0);
	while(PIND & (1<<DHT11_PIN));
}

uint8_t ReceiveData()
{
	for (int q=0; q<8; q++)
	{
		while((PIND & (1<<DHT11_PIN)) == 0);
		_delay_us(30);
		if(PIND & (1<<DHT11_PIN))
			c = (c<<1)|(0x01);
		else
			c = (c<<1);
		while(PIND & (1<<DHT11_PIN));
	}
	return c;
}
