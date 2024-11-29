using Chess.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessBoard2 : Form
    {
        static HttpClient client = new HttpClient();
        private const string PATH = "https://localhost:7243/";
        
        


        public ChessBoard2()
        {
            InitializeComponent();

        }


        private void ChessBoard2_Load(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            bindingSource1.DataSource = null;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;





        }

        async Task<TblUsers> GetUsersAsync(string path)
        {
            TblUsers user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<TblUsers>();

            }
            return user;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            const string ALL_USERS = "api/TblUsers";
            await GetUsersAsync(PATH + ALL_USERS);     
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
