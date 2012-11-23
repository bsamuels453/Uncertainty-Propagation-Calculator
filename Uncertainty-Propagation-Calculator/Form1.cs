#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PdfToImage;

#endregion

namespace Uncertainty_Propagation_Calculator{
    public partial class Form1 : Form{
        readonly LatexToImg _latexConverter;

        public Form1(){
            InitializeComponent();

            EqationInputFmtDropdown.SelectedItem = "Normal (Calculator Style)";
            OutputFmtDropdown.SelectedItem = "LibreMath";

            var sr = new StreamReader("apikey.txt");
            WolframApiTextBox.Text = sr.ReadToEnd();
            sr.Close();


            _latexConverter = new LatexToImg(Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\");
            _latexConverter.OnConversionCompletion += PdfConversionCallback;

            _latexConverter.QueueNewUpdate(" ");
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

        private void SaveKeyButClick(object sender, EventArgs e) {
            string key = WolframApiTextBox.Text;
            var sw = new StreamWriter("apikey.txt", false);
            sw.Write(key);
            sw.Close();
            KeySavedLabel.Visible = true;
        }

        void PdfConversionCallback(){
            //FormulaRenderWindow.Refresh();
            pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\formula.jpg";
        }

        private void Button1Click(object sender, EventArgs e) {
            _latexConverter.QueueNewUpdate(EquationEntryTextBox.Text);
        }

    }
}