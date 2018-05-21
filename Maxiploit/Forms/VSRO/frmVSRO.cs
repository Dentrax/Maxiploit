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
            this.Initialize();
        }

        private void Initialize() {
            try {
                bool flag = true;

                lvLog.Columns.Add("Import Status", 100, HorizontalAlignment.Left);
                lvLog.Columns.Add("Price", 80, HorizontalAlignment.Left);
                lvLog.Columns.Add("Date", 120, HorizontalAlignment.Left);

                if (flag) {
                    var item1 = new ListViewItem(new[] { "Initialization success", "Test", DateTime.Now.ToString() });
                    lvLog.Items.Add(item1);
                }

            } catch (Exception ex) {

                throw;
            }
           

           
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


            if (!IPUtils.IPValidateIPv4(this.txtSetupServerIP.Text) /*|| IPUtils.IPValidateIPv4(this.txtServerIP.Text)*/) {
                this.WriteToLogPanel("Invalid IP address");
                flag = false;
            }

            int num2 = 0;
            if (!int.TryParse(this.txtSetupServerPort.Text, out num2) || num2 <= 0 || num2 >= 65535) {
                this.WriteToLogPanel("Invalid port");
                flag = false;
            }
            int num3 = 0;
            //if (!int.TryParse(this.txtServerLocale.Text, out num3) || num3 <= 0) {
            //    this.WriteToLogPanel("Invalid locale");
            //    flag = false;
            //}
            //int num4 = 0;
            //if (!int.TryParse(this.txtServerVersion.Text, out num4) || num4 <= 0) {
            //    this.WriteToLogPanel("Invalid version");
            //    flag = false;
            //}
            int num5 = 0;
            if (!int.TryParse(this.txtSetupServerIP.Text, out num5) || num5 <= 0) {
                this.WriteToLogPanel("Invalid shard id");
                flag = false;
            }

            if (!cbSetupServerReady.Checked) {
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
            switch (dialogResult) {
                case DialogResult.OK:
                    this.txtAccountBruteFileInputPath.Text = openFileDialog.FileName;
                    break;
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.Yes:
                case DialogResult.No:
                    this.WriteToLogPanel("Valid account list path must be specified");
                    break;
            }
        }

        private void btnAccountBruteFileOutput_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.txt";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            DialogResult dialogResult = openFileDialog.ShowDialog();
            switch (dialogResult) {
                case DialogResult.OK:
                    this.txtAccountBruteOutputFilePath.Text = openFileDialog.FileName;
                    break;
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.Yes:
                case DialogResult.No:
                    this.WriteToLogPanel("Valid account list path must be specified");
                    break;
            }
            this.WriteToLogPanel("Valid output path must be specified");
        }

        #endregion

        #endregion

        #region Logging

        private void WriteToLogPanel(string log) {
            this.WriteToLogPanel(log, null);
        }


        private void WriteToLogPanel(string log, params object[] args) {
        }

        #endregion

        #region 1-SETUP

        #region SOCKET

        private void btnSetupSocketAdd_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(this.txtSetupSocketName.Text)) {
                MessageBox.Show(this, "SocketName field can't be null or empty.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSocketName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cbSetupSocketType.Text) || this.cbSetupSocketType.SelectedIndex == -1) {
                MessageBox.Show(this, "SocketType field can't be null or empty.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSocketType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cbSetupSocketProtocolType.Text) || this.cbSetupSocketProtocolType.SelectedIndex == -1) {
                MessageBox.Show(this, "SocketProtocolType field can't be null or empty.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSocketProtocolType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cbSetupSocketAddressFamilyType.Text) || this.cbSetupSocketAddressFamilyType.SelectedIndex == -1) {
                MessageBox.Show(this, "SocketAddressFamilyType field can't be null or empty.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSocketAddressFamilyType.Focus();
                return;
            }

            if (!this.cbSetupSocketReady.Checked) {
                MessageBox.Show(this, "Please check the Ready to continue.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSocketReady.Focus();
                return;

            }

            //add

            MessageBox.Show(this, $"Socket ({this.txtSetupSocketName.Text}) successfully added.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);


            this.ClearSetupSocketFields(true);
        }

        private void btnSetupSocketClear_Click(object sender, EventArgs e) {
            this.ClearSetupSocketFields(false);
        }

        private void ClearSetupSocketFields(bool force) {
            if (!force && this.cbSetupSocketReady.Checked) {
                MessageBox.Show(this, "Please uncheck the Ready to clear.", "Maxiploit [Setup::Socket]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSocketReady.Focus();
                return;
            }

            this.txtSetupSocketName.Clear();
            this.cbSetupSocketType.ResetText();
            this.cbSetupSocketType.SelectedIndex = -1;
            this.cbSetupSocketProtocolType.ResetText();
            this.cbSetupSocketProtocolType.SelectedIndex = -1;
            this.cbSetupSocketAddressFamilyType.ResetText();
            this.cbSetupSocketAddressFamilyType.SelectedIndex = -1;
            this.cbSetupSocketReady.Checked = false;
        }

        #endregion

        #region SERVER

        private void btnSetupServerAdd_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(this.txtSetupServerName.Text)) {
                MessageBox.Show(this, "ServerName field can't be null or empty.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupServerName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSetupServerIP.Text)) {
                MessageBox.Show(this, "ServerIP field can't be null or empty.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupServerIP.Focus();
                return;
            } else {
                if (!IPUtils.IsIPv4Input(this.txtSetupServerIP.Text)) {
                    MessageBox.Show(this, "ServerIP field must be IPv4 format.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    this.txtSetupServerIP.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.txtSetupServerPort.Text)) {
                MessageBox.Show(this, "ServerPort field can't be null or empty.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupServerPort.Focus();
                return;
            } else {
                if (int.TryParse(this.txtSetupServerPort.Text, out int port)) {
                    if (port < 1024 || port > 65535) {
                        MessageBox.Show(this, "ServerPort field must be in 1024-65535 range.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        this.txtSetupServerPort.Focus();
                        return;
                    }
                } else {
                    MessageBox.Show(this, "ServerPort field must be integer value.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    this.txtSetupServerPort.Focus();
                    return;
                }
            }

            if (!this.cbSetupServerReady.Checked) {
                MessageBox.Show(this, "Please check the Ready to continue.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupServerReady.Focus();
                return;
            }

            //add

            this.ClearSetupServerFields(true);
        }

        private void btnSetupServerClear_Click(object sender, EventArgs e) {
            this.ClearSetupServerFields(false);
        }

        private void ClearSetupServerFields(bool force) {
            if (!force && this.cbSetupServerReady.Checked) {
                MessageBox.Show(this, "Please uncheck the Ready to clear.", "Maxiploit [Setup::Server]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupServerReady.Focus();
                return;
            }

            this.txtSetupServerName.Clear();
            this.txtSetupServerIP.Clear();
            this.txtSetupServerPort.Clear();
            this.cbSetupServerReady.Checked = false;
        }


        private void cbSetupServerUseProxy_CheckedChanged(object sender, EventArgs e) {
            this.txtSetupServerProxyIP.Enabled = cbSetupServerUseProxy.Checked;
            this.txtSetupServerProxyPort.Enabled = cbSetupServerUseProxy.Checked;
        }

        private void txtSetupServerPort_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion

        #region SQL

        private void btnSetupSQLAdd_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(this.txtSetupSQLName.Text)) {
                MessageBox.Show(this, "SQLName field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cbSetupSQLType.Text) || this.cbSetupSQLType.SelectedIndex == -1) {
                MessageBox.Show(this, "SQLType field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSQLType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSetupSQLDBName.Text)) {
                MessageBox.Show(this, "DBName field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLDBName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSetupSQLHostname.Text)) {
                MessageBox.Show(this, "HostName field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLHostname.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSetupSQLPort.Text)) {
                MessageBox.Show(this, "SQLPort field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLPort.Focus();
                return;
            } else {
                if (int.TryParse(this.txtSetupSQLPort.Text, out int port)) {
                    if (port < 1024 || port > 65535) {
                        MessageBox.Show(this, "SQLPort field must be in 1024-65535 range.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        this.txtSetupSQLPort.Focus();
                        return;
                    }
                } else {
                    MessageBox.Show(this, "SQLPort field must be integer value.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    this.txtSetupSQLPort.Focus();
                    return;
                }
            }


            if (string.IsNullOrEmpty(this.txtSetupSQLID.Text)) {
                MessageBox.Show(this, "SQLID field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtSetupSQLPW.Text)) {
                MessageBox.Show(this, "SQLPW field can't be null or empty.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.txtSetupSQLPW.Focus();
                return;
            }

            //add

            MessageBox.Show(this, $"SQL ({this.txtSetupSQLName.Text}) successfully added.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);


            this.ClearSetupSQLFields(true);
        }

        private void btnSetupSQLClear_Click(object sender, EventArgs e) {
            this.ClearSetupSQLFields(false);
        }

        private void ClearSetupSQLFields(bool force) {
            if (!force && this.cbSetupSQLReady.Checked) {
                MessageBox.Show(this, "Please uncheck the Ready to clear.", "Maxiploit [Setup::SQL]", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                this.cbSetupSQLReady.Focus();
                return;
            }

            this.txtSetupSQLName.Clear();
            this.txtSetupSQLDBName.Clear();
            this.txtSetupSQLHostname.Clear();
            this.txtSetupSQLPort.Clear();
            this.txtSetupSQLID.Clear();
            this.txtSetupSQLPW.Clear();
            this.cbSetupSQLType.ResetText();
            this.cbSetupSQLType.SelectedIndex = -1;
        }

        private void txtSetupSQLPort_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion

        #endregion

        #region 2-CONNECT



        #endregion


    }
}
