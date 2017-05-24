using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace testsynthetize
{
    public class SynthetizeClass
    {
        public SynthetizeClass()
        {

        }

        public async void talk(string message)
        {
            // The media object for controlling and playing audio.
            MediaElement mediaElement = new MediaElement();

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            //VoiceInformation voice = SpeechSynthesizer.DefaultVoice;

            foreach (var voice in SpeechSynthesizer.AllVoices)
            {
                if (voice.DisplayName == "Microsoft Hortense Mobile")
                {
                    synth.Voice = voice;
                    break;
                }
            }

            //IEnumerable<VoiceInformation> frenchVoices = from voice in InstalledVoices.All where voice.Language == "fr-FR" select voice;

            // Set the voice as identified by the query.
           

            try
            {
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(message);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Volume = 1;
                mediaElement.Play();
            }
            catch (Exception e)
            {
                string output = e.Message;
            }
        }
    }
}
