﻿#region

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
        readonly string[] _symbolBlacklist= new []{"+", "*", "-", "/", "^", ".", "ln", "log", "e", "(", ")"};

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

        #region handle ui stuff
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

        void SaveKeyButClick(object sender, EventArgs e) {
            string key = WolframApiTextBox.Text;
            var sw = new StreamWriter("apikey.txt", false);
            sw.Write(key);
            sw.Close();
            KeySavedLabel.Visible = true;
        }

        void PdfConversionCallback(){
            pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\formula.jpg";
        }

        void Button1Click(object sender, EventArgs e) {
            _latexConverter.QueueNewUpdate(EquationEntryTextBox.Text);
        }

        void CalculateButClick(object sender, EventArgs e) {
            string s;
            if (!IsVariableTableValid(out s)) {
                DataInputErrLabel.Text = s;
                DataInputErrLabel.Visible = true;
                return;
            }
            DataInputErrLabel.Visible = false;


        }
        #endregion

        bool IsVariableTableValid(out string retText){
            var variableNames = new List<string>();

            int numNullRows = 0;
            for (int i = 0; i < VariableEntryGrid.RowCount; i++){
                //check for empty cells
                //skip empty rows
                if (VariableEntryGrid[0, i].Value == null && VariableEntryGrid[1, i].Value == null && VariableEntryGrid[2, i].Value == null) {
                    numNullRows++;
                    continue;
                }

                //if at least one column has text in it in the row and the others dont, then incomplete
                if (VariableEntryGrid[0, i].Value != null || VariableEntryGrid[1, i].Value != null || VariableEntryGrid[2, i].Value != null){
                    if (VariableEntryGrid[0, i].Value == null || VariableEntryGrid[1, i].Value == null || VariableEntryGrid[2, i].Value == null){
                        retText = "ERROR: Row " + i + " Variable data incomplete";
                        return false;
                    }
                }

                string variableName = (string)VariableEntryGrid[0, i].Value;
                variableNames.Add(variableName);

                foreach (var s in _symbolBlacklist){
                    if (variableName.Contains(s)){
                        retText = "ERROR: Row " + i + "'s variable name contains invalid character(s) '" + s + "'";
                        return false;
                    }
                    foreach (var chr in variableName){
                        if (char.IsDigit(chr)){
                            retText = "ERROR: Row " + i + "'s variable name contains a number (not allowed)";
                            return false;
                        }
                    }
                }

                double _;
                //first value column
                string sciNotFix = (string)(VariableEntryGrid[1, i].Value);
                sciNotFix = sciNotFix.Replace("*10^", "E");
                if(!double.TryParse(sciNotFix, out _)){
                    retText = "ERROR: Row " + i + "'s variable value is invalid, it should be a number";
                    return false;
                }

                //second value column
                sciNotFix = (string)(VariableEntryGrid[2, i].Value);
                sciNotFix = sciNotFix.Replace("*10^", "E");
                if (!double.TryParse(sciNotFix, out _)) {
                    retText = "ERROR: Row " + i + "'s uncertainty value is invalid, it should be a number";
                    return false;
                }
            }

            if (numNullRows == VariableEntryGrid.RowCount){
                retText = "ERROR: No variable data entered";
                return false;
            }

            //now make sure none of the variable names contain substrings of other variable names
            foreach (var varName in variableNames) {
                foreach (var varNameToSub in variableNames) {
                    if (varName.Contains(varNameToSub) && varName != varNameToSub) {
                        retText = "ERROR: Variable " + varName + " contains an substring identical to variable " + varNameToSub;
                        return false;
                    }
                }
            }
            retText = "";
            return true;
        }

        bool IsFormulaValid(out string retText){

            retText = "";
            return true;
        }


        /*
        string equation = EquationEntryTextBox.Text;

        //split equation into two sides
        var sides = equation.Split('=');
        var leftSide = sides[0] + "=";
        var rightSide = sides[1];

        var symbolAliases = ExpressionManip.GenerateEquationAliases(rightSide);

        string aliasEquation = ExpressionManip.ConvertSymbolsIntoAliases(symbolAliases, rightSide);

        var solver = new WolframEvaluator(WolframApiTextBox.Text);
        */

    }
}