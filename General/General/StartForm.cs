using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace General
{
    public partial class StartForm : Form
    {
        
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            RegisterForm form2 = new RegisterForm(game);
            Hide();
            form2.ShowDialog();
            Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
