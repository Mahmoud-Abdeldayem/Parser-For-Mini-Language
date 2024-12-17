using System.Runtime.CompilerServices;

namespace UILayer
{
    public partial class InputForm : Form
    { 

        public InputForm()
        {
            InitializeComponent();
            this.ExecuteBtn.BackColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var outputForm = new OutPutForm();
            outputForm.Show();
        }
    }
}
