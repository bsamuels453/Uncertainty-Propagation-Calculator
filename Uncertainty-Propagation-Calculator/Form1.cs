using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uncertainty_Propagation_Calculator {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            EqationInputFmtDropdown.SelectedItem = "Normal (Calculator Style)";
            OutputFmtDropdown.SelectedItem = "LibreMath";

            VariableEntryGrid.UpdateCellErrorText(0, 0);
            VariableEntryGrid.UpdateCellErrorText(0, 0);
        }

        private void GetKeyButClick(object sender, EventArgs e) {
            const string target1 = "https://developer.wolframalpha.com/portal/apisignup.html";
            try {
                System.Diagnostics.Process.Start(target1);
            }
            catch (Win32Exception noBrowser) {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other) {
                MessageBox.Show(other.Message);
            }
        }
    }
}
