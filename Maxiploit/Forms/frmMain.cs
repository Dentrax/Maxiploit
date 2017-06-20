using Maxiploit.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maxiploit
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetupTitle();
        }

        private void SetupTitle() { 
            var asm = Assembly.GetExecutingAssembly();
            var title = asm.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            var version = asm.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            var configuration = asm.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
            var informationalVersion = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            var product = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var copyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

            base.Text = string.Format("{0} {1} ({2}) [{3}] {4}",
                title,
                version,
                System.IO.File.GetLastWriteTime(asm.Location),
                string.IsNullOrEmpty(configuration) ? "Undefined" : string.Format("{0}", configuration),
                string.IsNullOrEmpty(informationalVersion) ? "" : string.Format("<{0}>", informationalVersion));

            //MessageBox.Show(copyright);
        }

        private void btnVSRO_Click(object sender, EventArgs e)
        {
            frmVSRO vsro = new frmVSRO();
            vsro.Show();
        }
    }
}
