using System;
using SpeechLib;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace RaspiHomeTextToSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            RaspiSpeecher raspi = new RaspiSpeecher();
            string cmdText = "";

            while (true)
            {
                if (!raspi.IsCalled)
                {
                    //Simule appel de Raspi
                    Console.WriteLine("Call Raspi");
                    cmdText = Console.ReadLine();
                    raspi.CalleRaspi(cmdText);
                    Console.WriteLine();
                }

                if (raspi.IsCalled)
                {
                    Console.WriteLine("Send a command");
                    cmdText = Console.ReadLine();                    
                    raspi.ReceiveCommand(cmdText);
                    Console.WriteLine();
                    Console.WriteLine(raspi.SendCommand());
                }

            }
            /*
            while (true)
            {
                //Simule appel de Raspi
                Console.WriteLine("Call Raspi");
                cmdText = Console.ReadLine();
                Console.WriteLine();


                // Commande vocale en MAJUSCULE
                if (cmdText.ToUpper() == _raspi.ToUpper())
                {
                    cmdText = "";
                    _isCalled = true;

                    while (_isCalled)
                    {
                        // Répond lorsque on l'appel
                        _voice.Speak(_speecherWhenCalling[rnd.Next(0, _speecherWhenCalling.Count - 1)], SpeechVoiceSpeakFlags.SVSFDefault);
                        Thread.Sleep(100);

                        Console.WriteLine("Write your texte bellow:");
                        cmdText = Console.ReadLine() + " @#@";//Caractères de fin de chaine empèche suite 

                        lstSentenceSplited = cmdText.Split(',').ToList();

                        foreach (string str in lstSentenceSplited)
                        {
                            //Action
                            if (_raspiHomeActionKnown.Contains(str))
                            {

                                continue;
                            }

                            //Objet
                            if (_raspiHomeActionKnown.Contains(str))
                            {
                                continue;
                            }

                            //Location
                            if (_raspiHomeLocationKnown.Contains(str))
                            {
                                continue;
                            }
                        }


                        //if (cmdText != "")
                        //{
                        //    // Optionnel lit la commence
                        //    _voice.Speak(cmdText, SpeechVoiceSpeakFlags.SVSFDefault);
                        //    Thread.Sleep(100);
                        //}

                        Console.WriteLine();
                    }
                }
                else
                {
                    // En cas de réponse erronée
                    cmdText = "";
                    _voice.Speak(_speecherWhenCallingError[rnd.Next(0, _speecherWhenCallingError.Count - 1)], SpeechVoiceSpeakFlags.SVSFDefault);
                    Thread.Sleep(100);

                    _isCalled = false;
                }
            }
            */
        }
    }
}
