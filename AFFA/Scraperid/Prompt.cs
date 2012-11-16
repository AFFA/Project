using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFFA.Scraperid
{
    public static class Prompt
    {
        public static string[] ShowDialog(string caption)
        {
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 180;
            prompt.Text = caption;
            
            Label textLabel = new Label() { Left = 10, Top = 10, Text = "Username:" };
            TextBox textBox = new TextBox() { Left = 120, Top = 10, Width = 100 };
            Label textLabel1 = new Label() { Left = 10, Top = 50, Text = "Password:" };
            TextBox psw = new TextBox() { Left = 120, Top = 50, Width = 100 };
            psw.PasswordChar = '*';
            Button confirmation = new Button() { Text = "Ok", Left = 170, Width = 50, Top = 100 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(textLabel1);
            prompt.Controls.Add(psw);
            prompt.ShowDialog();
            return new string[]{textBox.Text, psw.Text};
        }
    }
}
