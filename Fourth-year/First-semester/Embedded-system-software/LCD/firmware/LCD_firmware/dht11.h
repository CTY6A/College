#include "main.h"

#define DHT_PIN PIND
#define DHT_PORT PORTD
#define DHT_DDR  DDRD


typedef struct th {
	uint8_t temperature;
	uint8_t humidity;
} TH_t;

int DHT_read(uint8_t _PIN, TH_t* _DATA) 
{
	uint8_t DHT_RESPONSE[5] = {0, 0, 0, 0, 0};
		
	DHT_DDR |= (1 << _PIN);
	DHT_PORT &= ~(1 << _PIN);
	_delay_ms(18);
	
	DHT_PORT |= (1 << _PIN);
	DHT_DDR &= ~(1 << _PIN);
	_delay_us(240);
	
	uint8_t _bit, _byte;
	while (!(DHT_PIN & (1 << _PIN)));
	
	for (_byte = 0; _byte < 5; _byte++) {
		DHT_RESPONSE[_byte] = 0;
		for (_bit = 0; _bit < 8; _bit++) {
			_delay_us(40);
			if (DHT_PIN & (1 << _PIN))
			{
				DHT_RESPONSE[_byte] |= 1 << (7 - _bit);
			}
			while(!(DHT_PIN & (1 << _PIN)));
		}
	}
	
	_DATA -> humidity = DHT_RESPONSE[0]; 
	_DATA -> temperature = DHT_RESPONSE[2];
	
	return 1;
}
