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
            var v = (ComboBox)comboBox1;
            v.SelectedItem = "Normal Format (Calculator Style)";
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
