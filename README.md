# Devises
Short program to convert an amount of money to another currency according to the exchange rates given through a file.

The file must be formatted as it follows :
```
EUR;550;JPY
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571
```

* 1st line : SOURCE_CURRENCY;AMOUNT_TO_CONVERT;TARGET_CURRENCY
* 2nd line : NUMBER_OF_EXCHANGE_RATES
* The remaining lines : SOURCE_CURRENCY;TARGET_CURRENCY;EXCHANGE_RATE

Notes:
* SOURCE_CURRENCY and TARGET_CURRENCY must be three uppercase letters
* NUMBER_OF_EXCHANGE_RATES must match the number of exchange rates lines
* EXCHANGE_RATE must be a decimal value with 4 decimals containing a '.'

All other formats are rejected by the program

## Usage
LuccaDevises/bin/Release/net6.0/publish/LuccaDevises.exe PATH_TO_FILE

## Program details
.NET 6 / C# 10
