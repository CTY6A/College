#pragma once

#include "main.h"

#define e1     PORTC |= 0b00000001
#define e0     PORTC &= 0b11111110
#define rs1    PORTC |= 0b00000010
#define rs0    PORTC &= 0b11111101

void SendHalfByte(unsigned char c)
{
	c <<= 4;
	e1; //enable e line
	_delay_us(51);
	PORTB &= 0b00001111;
	PORTB |= c;
	e0; //disable e line
	_delay_us(51);
}

void SendByte(unsigned char c, unsigned char mode)
{
	if (mode==0)
	{
		 rs0;
	}
		
	else
	{ 
		rs1;	
	}
	
	unsigned char hc=0;
	hc = c>>4;
	
	SendHalfByte(hc); 
	SendHalfByte(c);
}

void SendChar(unsigned char c)
{
	SendByte(c,1);
}

void SetPos(unsigned char x, unsigned y)
{
	char adress;
	adress = (0x40*y+x) | 0b10000000;
	SendByte(adress, 0);
}

void InitLcd()
{
	_delay_ms(15);
	SendHalfByte(0b00000011);
	
	_delay_ms(4);
	SendHalfByte(0b00000011);
	
	_delay_us(100);
	SendHalfByte(0b00000011);
	
	_delay_ms(1);
	SendHalfByte(0b00000010);
	
	_delay_ms(1);
	SendByte(0b00101000, 0); //4 bit mode (DL=0), use two lines (N=1)
	
	_delay_ms(1);
	SendByte(0b00001100, 0); //enable display picture (D=1), disable cursors (C=0, B=0)
	
	_delay_ms(1);
	SendByte(0b00000110, 0); //invisible cursor will move left
	_delay_ms(1);
}

void ClearLcd()
{
	SendByte(0b00000001, 0);
	_delay_us(1501);
}

void OuputString(char str1[])
{
	wchar_t n;
	for (n=0; str1[n]!='\0'; n++) 
	{ 
		SendChar(str1[n]);
	}
}

void ShiftDisplay()
{
	unsigned char command = 0b0000011100;
	SendByte(command, 0);
	_delay_us(54);
}
