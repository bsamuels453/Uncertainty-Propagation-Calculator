using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreekSymbols {
    public partial class GreekSymbols : Form {
        public GreekSymbols() {
            InitializeComponent();
        }

        #region based hardcoding

        void button5_Click(object sender, EventArgs e) {

            Clipboard.SetText("ρ");
        }

        void button6_Click(object sender, EventArgs e) {

            Clipboard.SetText("π");
        }

        void button7_Click(object sender, EventArgs e) {

            Clipboard.SetText("Ω");
        }

        void button8_Click(object sender, EventArgs e) {

            Clipboard.SetText("Δ");
        }

        void button9_Click(object sender, EventArgs e) {

            Clipboard.SetText("δ");
        }

        void button10_Click(object sender, EventArgs e) {

            Clipboard.SetText("α");
        }

        void button11_Click(object sender, EventArgs e) {

            Clipboard.SetText("μ");
        }

        void button12_Click(object sender, EventArgs e) {

            Clipboard.SetText("ω");
        }

        void button13_Click(object sender, EventArgs e) {

            Clipboard.SetText("θ");
        }

        void button14_Click(object sender, EventArgs e) {

            Clipboard.SetText("κ");
        }

        void button15_Click(object sender, EventArgs e) {

            Clipboard.SetText("σ");
        }

        void button16_Click(object sender, EventArgs e) {
            Clipboard.SetText("β");
        }

        void button17_Click(object sender, EventArgs e) {
            Clipboard.SetText("γ");
        }

        void button18_Click(object sender, EventArgs e) {
            Clipboard.SetText("η");
        }

        void button19_Click(object sender, EventArgs e) {
            Clipboard.SetText("λ");
        }

        void button20_Click(object sender, EventArgs e) {
            Clipboard.SetText("ν");
        }

        void button21_Click(object sender, EventArgs e) {
            Clipboard.SetText("ξ");
        }

        void button22_Click(object sender, EventArgs e) {
            Clipboard.SetText("τ");
        }

        void button23_Click(object sender, EventArgs e) {
            Clipboard.SetText("υ");
        }

        void button24_Click(object sender, EventArgs e) {
            Clipboard.SetText("φ");
        }

        void button25_Click(object sender, EventArgs e) {
            Clipboard.SetText("ψ");
        }

        void button26_Click(object sender, EventArgs e) {
            Clipboard.SetText("ε");
        }

        private void button1_Click(object sender, EventArgs e) {
            Clipboard.SetText("±");
        }

        #endregion


    }
}
