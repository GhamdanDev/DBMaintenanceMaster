using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBBACKUP
{
    [DefaultEvent(nameof(TextChanged))] 
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
        {
            InitializeComponent();
        }

        private void ClearableTextBox_Load(object sender, EventArgs e)
        {

        }
        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add => txtValue.TextChanged += value;
            remove => txtValue.TextChanged -= value;
        }
        [Browsable(true)]
        public new string Text
        {
            get => txtValue.Text;
            set => txtValue.Text = value;
        }
        [Browsable(true)]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }
        private void btnClear_Click(object sender, EventArgs e) =>
    Text = "";
        private void txtValue_TextAlignChanged(object sender, EventArgs e)
        {

        }

       
    }
}
