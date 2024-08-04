using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBBACKUP
{
    public partial class ErrorMessagesChatGPT : Form
    {
        private readonly ChatGPTClient _chatGPTClient;


        public ErrorMessagesChatGPT()
        {
            InitializeComponent();
            // Replace with your actual API key
            _chatGPTClient = new ChatGPTClient("sk-proj-5JswxIYtPE1IFZFLmZBZT3BlbkFJUislZwLVmqwzdJa8Zteg");
        }


        private void ErrorMessagesChatGPT_Load(object sender, EventArgs e)
        {

        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            // Display the user's message in the chat box
            rtbChat.AppendText($"You: {userMessage}\n");

            // Send the message to the ChatGPT API and get the response
            string response = _chatGPTClient.SendMessage(userMessage);

            // Display the ChatGPT response in the chat box
            rtbChat.AppendText($"Chatbot: {response}\n");

            // Clear the input text box
            txtInput.Clear();
        }

     
    }

public class ErrorMessageBox
    {

       static public void ShowErrorMessage(string message)
        {
            Form errorForm = new Form
            {
                Text = "Error",
                Width = 600,
                Height = 400,
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                Text = message,
                ScrollBars = ScrollBars.Vertical
            };

            errorForm.Controls.Add(textBox);
            errorForm.ShowDialog();
        }

    }



    public class ChatGPTClient
    {
        private readonly string _apiKey;
        private readonly RestClient _client;

        public ChatGPTClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new RestClient("https://api.openai.com/v1/engines/text-davinci-003/completions");
        }

        public string SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "Sorry, I didn't receive any input. Please try again!";
            }

            try
            {
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    prompt = message,
                    max_tokens = 100,
                    n = 1,
                    stop = (string)null,
                    temperature = 0.7,
                };

                request.AddJsonBody(JsonConvert.SerializeObject(requestBody));

                var response = _client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

                    // Check if the response has the expected structure
                    if (jsonResponse != null && jsonResponse.choices != null && jsonResponse.choices.Count > 0)
                    {
                        return jsonResponse.choices[0]?.text?.ToString()?.Trim() ?? "No response from the chatbot.";
                    }
                    else
                    {
                        return "Unexpected response structure from the chatbot.";
                    }
                }
                else
                {
                    return "Failed to receive a valid response from the chatbot. Please check your connection.";
                }
            }
            catch (Exception ex)
            {
                return $"Sorry, there was an error processing your request. Please try again later. Error: {ex.Message}";
            }
        }
    }
}
