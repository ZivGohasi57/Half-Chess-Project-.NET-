using Chess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class BeforeGameForm : Form
    {

        TblUsers user;
        public BeforeGameForm()
        {
            InitializeComponent();
            InitializeTimerOptions();
        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }

        private void Timer_Label_Click(object sender, EventArgs e)
        {

        }

        private async void Start_Game_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Timer_ComboBox.SelectedItem.ToString(), out int selectedTime))
            {
                int initialTimeInSeconds = selectedTime;



                user = await GetUserFromApi();
                
                
                Board boardForm = new Board(initialTimeInSeconds, user);
                boardForm.Show();
            }
            else
            {

                MessageBox.Show("Please select a valid time.");
            }
        }




        private async Task<bool> UpdateGameCountAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/"); 
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/TblUsers/UpdateGameCount");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    MessageBox.Show("Failed to update GameCount.");
                    return false; 
                }
            }
        }


        private async Task<string> GetUserNameFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/"); 
                HttpResponseMessage response = await client.GetAsync("api/TblUsers/GetUserName");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    MessageBox.Show("Failed to fetch user name from the server.");
                    return "Guest";
                }
            }
        }


        private async Task<TblUsers> GetUserFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/"); 
                HttpResponseMessage response = await client.GetAsync("api/TblUsers/GetUser");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                       
                        TblUsers user = JsonConvert.DeserializeObject<TblUsers>(responseContent);
                        return user;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to parse user from the server response. Error: {ex.Message}");
                        return null; 
                    }
                }
                else
                {
                    MessageBox.Show("Failed to fetch user from the server.");
                    return null; 
                }
            }


        }











        private void Timer_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void InitializeTimerOptions()
        {
            Timer_ComboBox.Items.Clear(); 
            Timer_ComboBox.Items.Add(20); 
            Timer_ComboBox.Items.Add(30); 
            Timer_ComboBox.Items.Add(60); 
            Timer_ComboBox.Items.Add(120);
            Timer_ComboBox.Items.Add(180);
            Timer_ComboBox.Items.Add(300);

            Timer_ComboBox.SelectedIndex = 0; 
        }

        private async void BeforeGameForm_Load(object sender, EventArgs e)
        {
            string userName = await GetUserNameFromApi();
            UserName.Text = $"WELCOME {userName}";
        }

    }
}