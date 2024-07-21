using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
 
namespace DBBACKUP
{
    public partial class SpeechRecognition : Form
    {
        private static readonly string apiKey = Environment.GetEnvironmentVariable("sk-proj-8FbSgIbMH63eQep6cwDCT3BlbkFJm5qGVvhn4vPoVi9RVkhv");
         
        public SpeechRecognition()
        {
            InitializeComponent();
        }

        private void SpeechRecognition_Load(object sender, EventArgs e)
        { 
        }

       

       
 

        private static async Task GenerateSpeechAsync()
        {
             

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestContent = new StringContent(
                    new JObject
                    {
                        { "model", "tts-1" },
                        { "voice", "alloy" },
                        { "input", "Today is a wonderful day to build something people love!" }
                    }.ToString(),
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/audio/speech", requestContent);
                response.EnsureSuccessStatusCode();

                var speechFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "speech.mp3");
                using (var fs = new FileStream(speechFilePath, FileMode.Create, FileAccess.Write))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
        }

        private async void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                await GenerateSpeechAsync();
                MessageBox.Show("Speech file created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
