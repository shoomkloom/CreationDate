using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CreationDate
{
    public partial class FormWelcome : Form
    {
        public int mHourDiff = 0;

        public FormWelcome()
        {
            InitializeComponent();
        }

        private void mButtonOK_Click(object sender, EventArgs e)
        {
            mHourDiff = (int)mNUDHours.Value;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void mButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
