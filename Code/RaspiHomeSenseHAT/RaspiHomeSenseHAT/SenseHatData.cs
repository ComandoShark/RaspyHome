/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeSenseHAT is a program who use a 
 *   Sense HAT, it's an electronic card who can be 
 *   mesured value with sensor. This program use 
 *   the Sense HAT to mesure the temperature, the 
 *   humidity and the pressure. 
\*--------------------------------------------------*/

namespace RaspiHomeSenseHAT
{
    public class SenseHatData
    {
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? Temperature { get; set; }
    }
}
