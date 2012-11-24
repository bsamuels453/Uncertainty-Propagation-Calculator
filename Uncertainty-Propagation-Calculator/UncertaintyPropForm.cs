﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using PdfToImage;

#endregion

namespace Uncertainty_Propagation_Calculator{
    /// <summary>
    /// Most of the business end of the program that does all the uncertainty prop
    /// stuff are semi-pure classes. This includes ExpressionManip, 
    /// LibreMathConverter, and LatexConverter. UncertaintyCalculator is kinda pure in that it's purely an 
    /// input-output helper class, however it calls very unpure code like WolframEvaluator.
    /// </summary>
    public partial class UncertaintyPropForm : Form{
        readonly LatexToImg _latexConverter;
        readonly string[] _symbolBlacklist = new[]{"+", "*", "-", "/", "^", ".", "n", "l", "log", "e", "(", ")"};

        public UncertaintyPropForm(){
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

        void SaveKeyButClick(object sender, EventArgs e){
            string key = WolframApiTextBox.Text;
            var sw = new StreamWriter("apikey.txt", false);
            sw.Write(key);
            sw.Close();
            KeySavedLabel.Visible = true;
        }

        void PdfConversionCallback(){
            pictureBox1.ImageLocation = Directory.GetCurrentDirectory() + "\\LeanAndMeanPdfLatex\\bin\\win32\\formula.jpg";
        }

        void RenderButClick(object sender, EventArgs e){
            string s = LatexConverter.ToLatex(EquationEntryTextBox.Text);
            _latexConverter.QueueNewUpdate(s);
        }

        void CalculateButClick(object sender, EventArgs e){
            string s;
            if (!IsFormulaValid(out s)){
                DataInputErrLabel.Text = s;
                DataInputErrLabel.Visible = true;
                return;
            }
            if (!IsVariableTableValid(out s)){
                DataInputErrLabel.Text = s;
                DataInputErrLabel.Visible = true;
                return;
            }
            DataInputErrLabel.Visible = false;

            UncertaintyCalculator.UncertCalcInput input;

            input.ApiKey = WolframApiTextBox.Text;
            input.Equation = EquationEntryTextBox.Text;
            input.VariableNames = new List<string>();
            input.VariableValues = new List<string>();
            input.VariableUncertainties = new List<string>();
            for (int i = 0; i < VariableEntryGrid.RowCount; i++){
                if (VariableEntryGrid[0, i].Value != null){
                    string val = ((string) VariableEntryGrid[1, i].Value).Replace("*10^", "E");
                    string uncertainty = ((string) VariableEntryGrid[2, i].Value).Replace("*10^", "E");

                    input.VariableNames.Add((string) VariableEntryGrid[0, i].Value);
                    input.VariableValues.Add(val);
                    input.VariableUncertainties.Add(uncertainty);
                }
            }

            var results = UncertaintyCalculator.Calculate(input);
            PartialDerivsGrid.Enabled = true;
            PlugPartialDerivGrid.Enabled = true;
            FinalPropEquationField.Enabled = true;

            PartialDerivsGrid.RowCount = results.PartialDerivs.Count();
            PlugPartialDerivGrid.RowCount = results.PartialDerivs.Count();

            for (int i = 0; i < results.PartialDerivs.Count(); i++){
                PartialDerivsGrid[0, i].Value = results.PartialDerivs[i];
                PlugPartialDerivGrid[0, i].Value = results.PluggedPartialDerivs[i];
            }

            PartialDerivsGrid.ClearSelection();
            PlugPartialDerivGrid.ClearSelection();

            FinalPropEquationField.Text = results.PropEquation;
        }

        #endregion

        bool IsVariableTableValid(out string retText){
            var variableNames = new List<string>();

            int numNullRows = 0;
            for (int i = 0; i < VariableEntryGrid.RowCount; i++){
                //check for empty cells
                //skip empty rows
                if (VariableEntryGrid[0, i].Value == null && VariableEntryGrid[1, i].Value == null && VariableEntryGrid[2, i].Value == null){
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

                string variableName = (string) VariableEntryGrid[0, i].Value;
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
                string sciNotFix = (string) (VariableEntryGrid[1, i].Value);
                sciNotFix = sciNotFix.Replace("*10^", "E");
                if (!double.TryParse(sciNotFix, out _)){
                    retText = "ERROR: Row " + i + "'s variable value is invalid, it should be a number";
                    return false;
                }

                //second value column
                sciNotFix = (string) (VariableEntryGrid[2, i].Value);
                sciNotFix = sciNotFix.Replace("*10^", "E");
                if (!double.TryParse(sciNotFix, out _)){
                    retText = "ERROR: Row " + i + "'s uncertainty value is invalid, it should be a number";
                    return false;
                }
            }

            if (numNullRows == VariableEntryGrid.RowCount){
                retText = "ERROR: No variable data entered";
                return false;
            }

            //now make sure none of the variable names contain substrings of other variable names
            foreach (var varName in variableNames){
                foreach (var varNameToSub in variableNames){
                    if (varName.Contains(varNameToSub) && varName != varNameToSub){
                        retText = "ERROR: Variable " + varName + " contains an substring identical to variable " + varNameToSub;
                        return false;
                    }
                }
            }
            retText = "";
            return true;
        }

        bool IsFormulaValid(out string retText){
            string equation = (string) EquationEntryTextBox.Text;

            //count brackets
            int numBrackets = 0;
            foreach (var chr in equation){
                if (chr == '('){
                    numBrackets++;
                }
                if (chr == ')'){
                    numBrackets--;
                }
            }
            if (numBrackets != 0){
                retText = "ERROR: Equation is malformatted (bad brackets)";
                return false;
            }

            //there should be one =
            int numEquals = 0;
            foreach (var chr in equation){
                if (chr == '=')
                    numEquals++;
            }

            if (numEquals != 1){
                retText = "ERROR: Equation is malformatted (more or less than one equals sign detected)";
                return false;
            }

            //now make sure all the variables entered in the table exist in the equation
            var variableNames = new List<string>();
            var sides = equation.Split('=');
            string eqclone = (string) sides[1].Clone();

            for (int i = 0; i < VariableEntryGrid.RowCount; i++){
                if (VariableEntryGrid[0, i].Value != null){
                    variableNames.Add((string) VariableEntryGrid[0, i].Value);
                }
            }

            foreach (var name in variableNames){
                if (!eqclone.Contains(name)){
                    retText = "ERROR: Variable " + name + " was not found in the equation";
                    return false;
                }
                eqclone = eqclone.Replace(name, "");
            }

            foreach (var str in _symbolBlacklist){
                eqclone = eqclone.Replace(str, " ");
            }
            //now eqclone should only have numbers

            bool eqfail = false;
            foreach (var chr in eqclone){
                if (!char.IsDigit(chr) && chr != ' '){
                    eqfail = true;
                    //retText = "ERROR: Unknown independent variable '"+chr+"' found in equation";
                    //return false;
                }
            }
            if (eqfail){
                string s = "";
                foreach (var chr in eqclone){
                    if (char.IsDigit(chr)){
                        s += " ";
                    }
                    else{
                        s += chr;
                    }
                }
                var unknownVars = s.Split(' ').ToList();
                for (int i = 0; i < unknownVars.Count(); i++){
                    if (unknownVars[i] == ""){
                        unknownVars.RemoveAt(i);
                        i--;
                    }
                }
                string errTxt = "";
                foreach (var unknownVar in unknownVars){
                    errTxt += unknownVar + ", ";
                }
                retText = "ERROR: Unknown independent variables found: " + errTxt;
                return false;
            }

            retText = "";
            return true;
        }

        private void button2_Click(object sender, EventArgs e) {
            Process symbols = new Process();

            symbols.StartInfo.FileName = "GreekSymbols.exe";
            symbols.Start();
        }
    }
}