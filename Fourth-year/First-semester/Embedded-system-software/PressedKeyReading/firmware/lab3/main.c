#include <avr/io.h>
#include <avr/eeprom.h>

#define RESET_KEY		-2
#define SEG_COUNT 4

#define BIT(n) (1 << n)
#define GET_BIT(port, n) (port & BIT(n))
// https://chipenable.ru/index.php/programming-avr/item/138-avr-spi-module-part2.html?tmpl=component&print=1

void send_to_MAX7219(uint8_t reg, uint8_t digit)
{
	PORTB &= ~BIT(PORTB2);	
	SPDR = reg;
	while (!(SPSR & BIT(SPIF)));
	SPDR = digit;
	while (!(SPSR & BIT(SPIF)));
	PORTB |= BIT(PORTB2);
}

void clear()
{	
	for (int i = 0; i < SEG_COUNT; i++)
	{
		send_to_MAX7219(i, 0xF);		
	}
}

void init_MAX7219()
{
	DDRB |= (BIT(PORTB2) | BIT(PORTB3) | BIT(PORTB5));
	PORTB &= ~(BIT(PORTB2) | BIT(PORTB3) | BIT(PORTB5));
	SPCR |= BIT(SPE) | BIT(MSTR)| BIT(SPR0);
	
	send_to_MAX7219(0x09, 0x0F);// set decode mode
	send_to_MAX7219(0x0B, SEG_COUNT - 1);// set bit count
	send_to_MAX7219(0x0A, 0x02);// set brightness
	send_to_MAX7219(0x0C, 1);	// set switch on
	
	clear();
}

int keys[4][3] = {
	{1, 2, 3},
	{4, 5, 6},
	{7, 8, 9},
	{RESET_KEY, 0, RESET_KEY}
};

char t,nt;
int get_pressed_key()
{
	DDRD = 0x00; 
	PORTD = 0x0F;
	DDRC = 0x0F;
	PORTC = 0x00;
	int i = 4;
	if (!GET_BIT(PIND, PIND0)) 
		i = 0;
	if (!GET_BIT(PIND, PIND1)) 
		i = 1;
	if (!GET_BIT(PIND, PIND2)) 
		i = 2;
	if (!GET_BIT(PIND, PIND3)) 
		i = 3;
		
	DDRD = 0x0F;
	PORTD = 0x00;
	DDRC = 0x00;
	PORTC = 0x0F;

	int j = 3;
	if (!GET_BIT(PINC, PINC0)) 
		j = 0;
	if (!GET_BIT(PINC, PINC1)) 
		j = 1;
	if (!GET_BIT(PINC, PINC2)) 
		j = 2;
		
	if ((i < 4) && (j < 3))
		return keys[i][j];
	return -1;
}

uint8_t decimal_to_Jons[8][4] = {
	{0, 0, 0, 0}, // 0
	{0, 0, 0, 1}, // 1
	{0, 0, 1, 1}, // 2
	{0, 1, 1, 1}, // 3
	{1, 1, 1, 1}, // 4
	{1, 1, 1, 0}, // 5
	{1, 1, 0, 0}, // 6
	{1, 0, 0, 0}, // 7
};

uint8_t JonsNumber[4] = {0, 0, 0, 0};

void to_Jons(int number)
{	
	for (int i = 0; i < 4; i++)
	{		
		JonsNumber[i] = 0;
	}
	for (int i = 0; i < number; i++)
	{
		uint8_t firstBit = JonsNumber[0];
		firstBit = !firstBit;
		for (int j = 0; j < 3; j++)
		{
			JonsNumber[j] = JonsNumber[j+1];
		}
		JonsNumber[3] = firstBit;
	}
}

int main(void)
{
	init_MAX7219();
	while (1)
	{
		int key = get_pressed_key();
		if(key >= 0 && key <= 7)
		{
			to_Jons(key);
			send_to_MAX7219(1, JonsNumber[0]);			
			send_to_MAX7219(2, JonsNumber[1]);
			send_to_MAX7219(3, JonsNumber[2]);
			send_to_MAX7219(4, JonsNumber[3]);
		} else if (key == RESET_KEY)
		{
			clear();
		}
	}
}

