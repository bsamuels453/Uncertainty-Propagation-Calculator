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
        LatexToImg _latexConverter;

        public Form1(){
            InitializeComponent();

            EqationInputFmtDropdown.SelectedItem = "Normal (Calculator Style)";
            OutputFmtDropdown.SelectedItem = "LibreMath";

            VariableEntryGrid.UpdateCellErrorText(0, 0);
            VariableEntryGrid.UpdateCellErrorText(0, 0);
            _latexConverter = new LatexToImg(Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\");
            _latexConverter.OnConversionCompletion += PdfConversionCallback;

            /*
            _pdfConverter = new PDFConvert();
            _pdfConverter.OutputFormat = "jpeg"; //format
            _pdfConverter.ResolutionX = 200; //dpi
            _pdfConverter.ResolutionY = 200;
            _pdfConverter.GraphicsAlphaBit = 4;
            _pdfConverter.TextAlphaBit = 4;
            /*_pdfConverter.FontPath = new List<string>();
            _pdfConverter.FontPath.Add("arial.ttf");
            _pdfConverter.DisablePlatformFonts = true;
            _pdfConverter.DisablePrecompiledFonts = true;*/
            /*
                RenderLatexToPdf(true);
             */
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

        private void CalculateButClick(object sender, EventArgs e) {

        }

        private void SaveKeyButClick(object sender, EventArgs e) {
            string key = WolframApiTextBox.Text;
            var sw = new StreamWriter("apikey.txt", false);
            sw.Write(key);
            sw.Close();
            KeySavedLabel.Visible = true;
        }

        void PdfConversionCallback(){
            //EquationImagePanel.ImageLocation = Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\formula.png";
            FormulaRenderWindow.Refresh();
        }

        private void Button1Click(object sender, EventArgs e) {
            _latexConverter.QueueNewUpdate(EquationEntryTextBox.Text);
        }
    }
}