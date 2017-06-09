/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomePiFaceDigital2 is a program who use 
 *   a PiFace Digital 2, it's an electronic card who 
 *   can be use to plug electronic component. This 
 *   program use the PiFace Digital 2 to activate 
 *   light and store. 
\*--------------------------------------------------*/

namespace RaspiHomePiFaceDigital2
{
    public class PiFaceDigital2
    {
        // Output
        public const byte LED0 = 0x08;     // I/O Direction Register
        public const byte LED1 = 0x09;      // 1 = Input (default), 0 = Output
        public const byte LED2 = 0x0A;     // MCP23x17 Input Polarity Register
        public const byte LED3 = 0x0B;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)
        public const byte LED4 = 0x0C;     // MCP23x17 Interrupt on Change Pin Assignements
        public const byte LED5 = 0x0D;      // 1 = Input (default), 0 = Output
        public const byte LED6 = 0x0E;     // MCP23x17 Input Polarity Register
        public const byte LED7 = 0x0F;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)

        // Input
        public const byte IN0 = 0x00;     // I/O Direction Register
        public const byte IN1 = 0x01;      // 1 = Input (default), 0 = Output
        public const byte IN2 = 0x02;     // MCP23x17 Input Polarity Register
        public const byte IN3 = 0x03;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)
        public const byte IN4 = 0x04;     // MCP23x17 Interrupt on Change Pin Assignements
        public const byte IN5 = 0x05;      // 1 = Input (default), 0 = Output
        public const byte IN6 = 0x06;     // MCP23x17 Input Polarity Register
        public const byte IN7 = 0x07;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)

        // Switch / Button
        public const byte Sw0 = IN0;     // I/O Direction Register
        public const byte Sw1 = IN1;      // 1 = Input (default), 0 = Output
        public const byte Sw2 = IN2;     // MCP23x17 Input Polarity Register
        public const byte Sw3 = IN3;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)

        // Relay
        public const byte RelayA = LED1;     // MCP23x17 Input Polarity Register
        public const byte RelayB = LED0;     // 0 = Normal (default)(low reads as 0), 1 = Inverted (low reads as 1)
    }
}
