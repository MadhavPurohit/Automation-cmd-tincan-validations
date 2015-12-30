using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automation_UserCMD
{
    public partial class FrmAutomationSelector : Form
    {
        public FrmAutomationSelector()
        {
            InitializeComponent();
        }

        private void lblStartDate_Click(object sender, EventArgs e)
        {

        }

        private void btnNavigateCmdAutomation_Click(object sender, EventArgs e)
        {
            FrmCMDValidation frmcmd = new FrmCMDValidation();
            frmcmd.Show();
            this.Hide();
            //this.Close();
        }

        private void btnNavigateTincanAutomation_Click(object sender, EventArgs e)
        {
            FrmTincanValidations frmtincan = new FrmTincanValidations();
            frmtincan.Show();
            this.Hide();
           // this.Close();
        }
    }
}
