using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class TipsForm : Form
    {
        public TipsForm()
        {
            InitializeComponent();
        }

        private void TipsForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
