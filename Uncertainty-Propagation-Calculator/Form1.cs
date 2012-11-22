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
            //dataGridView1[0, 0].ErrorText = "no";

        }

        private void dataGridView1_CellErrorTextNeeded(object sender, DataGridViewCellErrorTextNeededEventArgs e) {
            e.ErrorText = "hello";
        }


    }
}
