/*
 * GccApplication1.c
 *
 * Created: 03.11.2020 22:30:18
 * Author : andre
 */ 

#include <avr/io.h>
#include <avr/eeprom.h>

int main(void)
{
    uint8_t x1, x2, x3, x4;
    void* start = 0x40;
    int i = 0;
    eeprom_write_byte((uint8_t*)(start + sizeof(uint8_t)*i++), 0x3);
    eeprom_write_byte((uint8_t*)(start + sizeof(uint8_t)*i++), 0x3);
    eeprom_write_byte((uint8_t*)(start + sizeof(uint8_t)*i++), 0x3);
    eeprom_write_byte((uint8_t*)(start + sizeof(uint8_t)*i++), 0x3);
    i = 0;
    x1 = eeprom_read_byte((uint8_t*)(start + sizeof(uint8_t)*i++));
    x2 = eeprom_read_byte((uint8_t*)(start + sizeof(uint8_t)*i++));
    x3 = eeprom_read_byte((uint8_t*)(start + sizeof(uint8_t)*i++));
    x4 = eeprom_read_byte((uint8_t*)(start + sizeof(uint8_t)*i++));
    //float result = (3*x1 + x2*x3) / (x4-2);
	//eeprom_write_float((void*)(start + sizeof(uint8_t)*i++), result);
	uint8_t result = (3*x1 + x2*x3) / (x4-2);
	eeprom_write_byte((uint8_t*)(start + sizeof(uint8_t)*i++), result);
}

