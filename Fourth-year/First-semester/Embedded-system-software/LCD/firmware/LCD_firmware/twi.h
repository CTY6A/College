#pragma once

#include "main.h"

void I2C_Init (void);
void I2C_StartCondition(void);
void I2C_StopCondition(void);
void I2C_SendByte(unsigned char c);
void I2C_SendByteByADDR(unsigned char c,unsigned char addr);
unsigned char I2C_ReadByte(void);
unsigned char I2C_ReadLastByte(void);

void I2C_Init (void)
{
	TWBR=0x20;//transfer speed(when freq = 8 MHz we get 100 kHz, what we exactly need for communication with ds1307)
}

void I2C_StartCondition(void)
{
	TWCR = (1<<TWINT)|(1<<TWSTA)|(1<<TWEN);
	while (!(TWCR & (1<<TWINT)));
}

void I2C_StopCondition(void)
{
	TWCR = (1<<TWINT)|(1<<TWSTO)|(1<<TWEN);
}

void I2C_SendByte(unsigned char c)
{
	TWDR = c;// write byte to data register
	TWCR = (1<<TWINT)|(1<<TWEN);//enable data transfer
	while (!(TWCR & (1<<TWINT)));
}

void I2C_SendByteByADDR(unsigned char c,unsigned char addr)
{
	I2C_StartCondition();
	I2C_SendByte(addr); // send device address + read|write bit
	I2C_SendByte(c);
	I2C_StopCondition();
}

unsigned char I2C_ReadByte(void)
{
	TWCR = (1<<TWINT)|(1<<TWEN)|(1<<TWEA);
	while (!(TWCR & (1<<TWINT)));
	return TWDR;// read data register
}

unsigned char I2C_ReadLastByte(void)
{
	TWCR = (1<<TWINT)|(1<<TWEN);
	while (!(TWCR & (1<<TWINT)));
	return TWDR;// read data register
}
