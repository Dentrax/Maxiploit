using Maxiploit.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maxiploit.Forms
{
    public partial class frmVSRO : Form
    {
        public frmVSRO()
        {
            InitializeComponent();
        }

        private void frmVSRO_Load(object sender, EventArgs e) {

        }

        private void frmVSRO_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult result = MessageBox.Show("Are you sure you want to exit Maxiploit ?", "Maxiploit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes) {
                Environment.Exit(0);
            }
            e.Cancel = true;
        }

        #region Globals

        private bool HasServerSettings() {
            bool flag = true;


            if (!IPUtils.ByteValidateIPv4(this.txtServerIP.Text) /*|| IPUtils.IPValidateIPv4(this.txtServerIP.Text)*/) {
                this.WriteToLogPanel("Invalid IP address");
                flag = false;
            }

            int num2 = 0;
            if (!int.TryParse(this.txtServerPort.Text, out num2) || num2 <= 0 || num2 >= 65535) {
                this.WriteToLogPanel("Invalid port");
                flag = false;
            }
            int num3 = 0;
            if (!int.TryParse(this.txtServerLocale.Text, out num3) || num3 <= 0) {
                this.WriteToLogPanel("Invalid locale");
                flag = false;
            }
            int num4 = 0;
            if (!int.TryParse(this.txtServerVersion.Text, out num4) || num4 <= 0) {
                this.WriteToLogPanel("Invalid version");
                flag = false;
            }
            int num5 = 0;
            if (!int.TryParse(this.txtServerShardID.Text, out num5) || num5 <= 0) {
                this.WriteToLogPanel("Invalid shard id");
                flag = false;
            }

            if (!cbServerInfoReady.Checked) {
                this.WriteToLogPanel("Check ready");
                flag = false;
            }

            return flag;
        }

        #endregion


        #region TAB_BRUTE_FORCE



        #region ACCOUNT

        private bool m_isAccountBruteRunning = false;

        private void btnAccountBruteStartStop_Click(object sender, EventArgs e) {
            bool flag = true;

            if (!HasServerSettings()) {
                this.WriteToLogPanel("Check your server settings first");
                flag = false;
            }

            if (!File.Exists(this.txtAccountBruteFileInputPath.Text)) {
                this.WriteToLogPanel("Invalid account list path");
                flag = false;
            }
            if (!File.Exists(this.txtAccountBruteOutputFilePath.Text)) {
                this.WriteToLogPanel("Invalid output path");
                flag = false;
            }

            if (flag) {

            }


            m_isAccountBruteRunning = !m_isAccountBruteRunning;
            if (m_isAccountBruteRunning) {
                lblAccountBruteStatus.ForeColor = Color.DarkGreen;
                lblAccountBruteStatus.Text = "Running";
            } else {
                lblAccountBruteStatus.ForeColor = Color.DarkRed;
                lblAccountBruteStatus.Text = "Not Running";
            }
        }

        private void btnAccountBruteFileInput_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.txt";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                this.txtAccountBruteFileInputPath.Text = openFileDialog.FileName;
                return;
            }
            this.WriteToLogPanel("Valid account list path must be specified");
        }

        private void btnAccountBruteFileOutput_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.txt";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                this.txtAccountBruteOutputFilePath.Text = openFileDialog.FileName;
                return;
            }
            this.WriteToLogPanel("Valid output path must be specified");
        }

        #endregion

        #endregion

        #region Logging

        private void WriteToLogPanel(string log) {
            WriteToLogPanel(log, null);
        }

        private void WriteToLogPanel(string log, params object[] args) {

        }

        #endregion


        private void lbLog_SelectedIndexChanged(object sender, EventArgs e) {

        }

    }
}
