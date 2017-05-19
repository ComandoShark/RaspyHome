using RaspiHomeSpeechNSynthetize.Speech;
using RaspiHomeSpeechNSynthetize.Synthetize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize
{
    class Program
    {
        static void Main(string[] args)
        {
            Speecher speech = new Speecher();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
