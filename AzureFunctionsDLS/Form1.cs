using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureFunctionsDLS
{
    public partial class Form1 : Form
    {
        #region UI code
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_ClickAsync(sender, e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 && textBox1.Text.Length % 8 != 0)
            {
                label2.Visible = true;
                button1.Enabled = false;
            }
            else
            {
                label2.Visible = false;
                button1.Enabled = true;
            }
        }
        #endregion

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string url = null;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    url = "https://reverseastring.azurewebsites.net/api/HttpTriggerCSharp1?code=c6N4xBM1lpO0CD3DPbbx2OhIaWNLu6Dx0xpJcwdFyk8FQ20pTVa7hA==";
                    break;
                case 1:
                    url = "https://binarytotext.azurewebsites.net/api/BinaryToText";
                    break;
                case 2:
                    url = "https://stringsortedalphabetically.azurewebsites.net/api/HttpTriggerCSharp1?code=nK/jsKeGu8L2AGepIYXYHPlYMxDQO0aUoGF/c2XKY4fra9AqqljLhQ==";
                    break;
                case 3:
                    url = "https://stringencryption.azurewebsites.net/api/HttpTriggerCSharp1?code=1004mtSQPpCzR9sKmB3HVu4IZaPp5ePqgdzpSpxxCO6iFAh5HNuWhQ==";
                    break;
            }
            textBox2.Text = await azureFunction(textBox1.Text, url);
        }

        public static async Task<string> azureFunction(string text, string url)
        {
            var _httpClient = new HttpClient();

            string str = "{\"str\":\"" + text + "\"}";
            _httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var content = new StringContent(str, Encoding.UTF8, "application/json"))
            {
                var result = await _httpClient.PostAsync($"{url}", content).ConfigureAwait(false);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return await result.Content.ReadAsStringAsync();
                }
                else
                {
                    // Something wrong happened
                    return "Ups! Something wrong happened";
                }
            }
        }
    }
}
