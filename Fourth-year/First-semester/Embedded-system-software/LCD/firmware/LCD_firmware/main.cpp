#include "main.h"
#include "lcd.h"
#include "rtc.h"
#include "twi.h"
#include "dht11.h"

unsigned char sec;
unsigned char min;
unsigned char hour;
unsigned char day;
unsigned char date;
unsigned char month;
unsigned char year;

TH_t th = {0, 0};

#define DISPLAY_WIDTH 16
#define DATE_LENGTH 10
#define TIME_LENGTH 8
#define TEMPERATURE_LENGTH 1

char dateStr[DATE_LENGTH + 1];
char timeStr[TIME_LENGTH + 1];
char thStr[TEMPERATURE_LENGTH + 1];

void ReadTime()
{
	I2C_SendByteByADDR(0, 0b11010000);
	I2C_StartCondition();
	I2C_SendByte(0b11010001);
	
	sec = I2C_ReadByte();
	min = I2C_ReadByte();
	hour = I2C_ReadByte();
	day = I2C_ReadByte();
	date = I2C_ReadByte();
	month = I2C_ReadByte();
	year = I2C_ReadLastByte();
	
	I2C_StopCondition();
}

void SetTime()
{
	sec = RTC_ConvertFromDec(sec);
	min = RTC_ConvertFromDec(min);
	hour = RTC_ConvertFromDec(hour);
	day = RTC_ConvertFromDec(day);
	year = RTC_ConvertFromDec(year);
	month = RTC_ConvertFromDec(month);
	date = RTC_ConvertFromDec(date);
}

void FillDateAndTimeStrs()
{
	sprintf(timeStr, "%02d.%02d.%02d", hour, min, sec);
	sprintf(dateStr, "%02d.%02d.20%02d", date, month, year);
}

void FillTHString(bool error)
{
	if (error) 
	{
		sprintf(thStr, "-- %% -- *C");
	}
	else 
	{
		sprintf(thStr, "%u %% %u *C", th.humidity, th.temperature);
	}
}

int main(void)
{
	// Initialize ports
	PORTB = 0;
	PORTC = 0;
	PORTD = 0;
	DDRC = 0xFF;
	DDRB = 0xFF;
	DDRD = 0x00;
	
	I2C_Init();
	InitLcd();
	
	_delay_ms(100);
	
	bool showTime = true;
	bool showTimePrev = showTime;
	bool isSwitching = false;
	bool isStringShifting = false;
	
	while (true)
	{
		showTime = (0 == ((1 << PIND1 & PIND)));
		isStringShifting = ((1 << PIND2) & PIND);
		
		if (isStringShifting)
		{
			if (!isSwitching)
			{
				ShiftDisplay();
			}
			
			else
			{
				 isSwitching = false;
			}
		}
		
		if (showTime != showTimePrev)
		{
			ClearLcd();
			
			showTimePrev = showTime;
			isSwitching = true;
		}
		
		_delay_ms(50);
		if (showTime)
		{
			ReadTime();
			SetTime();
			FillDateAndTimeStrs();
			
			SetPos(0,0);
			OuputString(dateStr);
			SetPos(0,1);
			OuputString(timeStr);
		}
		else
		{
			FillTHString(DHT_read(0, &th) != 1);
			SetPos(0,0);
			
			OuputString(thStr);
		}
		
	}
}



