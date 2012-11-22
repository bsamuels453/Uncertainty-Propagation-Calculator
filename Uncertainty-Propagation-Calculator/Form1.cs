#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Uncertainty_Propagation_Calculator{
    public partial class Form1 : Form{
        public Form1(){
            InitializeComponent();

            EqationInputFmtDropdown.SelectedItem = "Normal (Calculator Style)";
            OutputFmtDropdown.SelectedItem = "LibreMath";

            VariableEntryGrid.UpdateCellErrorText(0, 0);
            VariableEntryGrid.UpdateCellErrorText(0, 0);
        }

        void GetKeyButClick(object sender, EventArgs e){
            const string target1 = "https://developer.wolframalpha.com/portal/apisignup.html";
            try{
                Process.Start(target1);
            }
            catch (Win32Exception noBrowser){
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other){
                MessageBox.Show(other.Message);
            }
        }

        void OpenOutputImageLocButClick(object sender, EventArgs e){
            Process prc = new Process();
            prc.StartInfo.FileName = Directory.GetCurrentDirectory() + "/Output";
            prc.Start();
        }

        private void CalculateBut_Click(object sender, EventArgs e) {

        }

        private void SaveKeyButClick(object sender, EventArgs e) {
            string key = WolframApiTextBox.Text;
            var sw = new StreamWriter("apikey.txt", false);
            sw.Write(key);
            sw.Close();
            KeySavedLabel.Visible = true;
        }
    }
}