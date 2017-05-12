EESchema Schematic File Version 2
LIBS:power
LIBS:device
LIBS:transistors
LIBS:conn
LIBS:linear
LIBS:regul
LIBS:74xx
LIBS:cmos4000
LIBS:adc-dac
LIBS:memory
LIBS:xilinx
LIBS:microcontrollers
LIBS:dsp
LIBS:microchip
LIBS:analog_switches
LIBS:motorola
LIBS:texas
LIBS:intel
LIBS:audio
LIBS:interface
LIBS:digital-audio
LIBS:philips
LIBS:display
LIBS:cypress
LIBS:siliconi
LIBS:opto
LIBS:atmel
LIBS:contrib
LIBS:valves
EELAYER 25 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 1
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L CONN_02X10 J1
U 1 1 59087B77
P 3850 2050
F 0 "J1" H 3850 2600 50  0000 C CNN
F 1 "CONN_02X10" V 3850 2050 50  0000 C CNN
F 2 "Pin_Headers:Pin_Header_Straight_2x10_Pitch2.54mm" H 3850 850 50  0001 C CNN
F 3 "" H 3850 850 50  0001 C CNN
	1    3850 2050
	1    0    0    -1  
$EndComp
Wire Wire Line
	4100 1600 4100 1700
Connection ~ 4100 1650
Text Label 4200 1650 0    60   ~ 0
VCC
$Comp
L CONN_01X05 J2
U 1 1 59087D0F
P 5650 1500
F 0 "J2" H 5650 1800 50  0000 C CNN
F 1 "MAX9814" H 5650 1200 50  0000 C CNN
F 2 "Pin_Headers:Pin_Header_Straight_1x05_Pitch2.54mm" H 5650 1500 50  0001 C CNN
F 3 "" H 5650 1500 50  0001 C CNN
	1    5650 1500
	1    0    0    -1  
$EndComp
$Comp
L CONN_01X07 J3
U 1 1 59087DE2
P 5650 2450
F 0 "J3" H 5650 2850 50  0000 C CNN
F 1 "MAX98357A" H 5650 2050 50  0000 C CNN
F 2 "Pin_Headers:Pin_Header_Straight_1x07_Pitch2.54mm" H 5650 2450 50  0001 C CNN
F 3 "" H 5650 2450 50  0001 C CNN
	1    5650 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	5000 1400 5450 1400
Wire Wire Line
	5000 1600 5450 1600
Text Label 4500 1400 0    60   ~ 0
OUT
Text Label 5000 1600 0    60   ~ 0
VCC
Wire Wire Line
	5450 2150 5150 2150
Wire Wire Line
	5450 2250 5000 2250
Wire Wire Line
	5450 2350 5150 2350
Wire Wire Line
	5450 2650 5000 2650
Wire Wire Line
	5450 2750 5150 2750
Text Label 5000 2650 0    60   ~ 0
GND
Text Label 5150 2750 0    60   ~ 0
VCC
Text Label 5150 2350 0    60   ~ 0
DIN
Text Label 5000 2250 0    60   ~ 0
BCLK
Text Label 5150 2150 0    60   ~ 0
LRC
Wire Wire Line
	4100 1650 4350 1650
Wire Wire Line
	4100 1800 4350 1800
Wire Wire Line
	4100 2200 4350 2200
Text Label 4350 2300 0    60   ~ 0
LRC
Text Label 3350 2500 0    60   ~ 0
DIN
Wire Wire Line
	3350 1700 3600 1700
Wire Wire Line
	3600 2500 3350 2500
Wire Wire Line
	4100 2300 4500 2300
Text Label 4200 2400 0    60   ~ 0
BCLK
Wire Wire Line
	4100 2400 4400 2400
Text Label 3350 1700 0    60   ~ 0
OUT
NoConn ~ 3600 1800
NoConn ~ 3600 1900
NoConn ~ 4100 1900
NoConn ~ 4100 2000
NoConn ~ 4100 2100
NoConn ~ 4100 2500
NoConn ~ 3600 2400
NoConn ~ 3600 2300
NoConn ~ 3600 2200
NoConn ~ 3600 2100
$Comp
L CP C1
U 1 1 59088883
P 4850 1400
F 0 "C1" V 4900 1450 50  0000 L CNN
F 1 "CP" V 4900 1250 50  0000 L CNN
F 2 "Capacitors_SMD:CP_Elec_8x5.4" H 4888 1250 50  0001 C CNN
F 3 "" H 4850 1400 50  0001 C CNN
	1    4850 1400
	0    1    1    0   
$EndComp
Wire Wire Line
	4700 1400 4500 1400
NoConn ~ 3600 1600
NoConn ~ 5450 1300
NoConn ~ 5450 2450
NoConn ~ 5450 2550
$Comp
L GND #PWR01
U 1 1 59088DF0
P 3400 1100
F 0 "#PWR01" H 3400 850 50  0001 C CNN
F 1 "GND" H 3400 950 50  0000 C CNN
F 2 "" H 3400 1100 50  0001 C CNN
F 3 "" H 3400 1100 50  0001 C CNN
	1    3400 1100
	1    0    0    -1  
$EndComp
$Comp
L PWR_FLAG #FLG02
U 1 1 59089003
P 3400 1000
F 0 "#FLG02" H 3400 1075 50  0001 C CNN
F 1 "PWR_FLAG" H 3400 1150 50  0000 C CNN
F 2 "" H 3400 1000 50  0001 C CNN
F 3 "" H 3400 1000 50  0001 C CNN
	1    3400 1000
	1    0    0    -1  
$EndComp
Text Label 3600 1050 0    60   ~ 0
GND
Wire Wire Line
	3400 1100 3400 1000
Wire Wire Line
	3400 1050 3750 1050
Connection ~ 3400 1050
Text Label 4200 1800 0    60   ~ 0
GND
Text Label 4200 2200 0    60   ~ 0
GND
Wire Wire Line
	5450 1500 5400 1500
Wire Wire Line
	5400 1500 5400 1600
Connection ~ 5400 1600
Wire Wire Line
	5450 1700 5250 1700
Text Label 5250 1700 0    60   ~ 0
GND
Wire Wire Line
	3600 2000 3350 2000
Text Label 3350 2000 0    60   ~ 0
GND
$EndSCHEMATC
