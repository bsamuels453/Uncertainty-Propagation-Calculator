#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using PdfToImage;

#endregion

namespace Uncertainty_Propagation_Calculator{
    //maybe multithread this in the future, for now too much effort
    internal class LatexToImg{
        #region Delegates

        public delegate void LatexToPngCallback();

        #endregion

       
        const string _texPrefix = @"
            \setlength{\pdfpagewidth}{8in}
            \setlength{\pdfpageheight}{2in}
            \setlength{\voffset}{-1.76in}
            \setlength{\textheight}{200pt}
            \setlength{\hoffset}{-2.05in}
            \setlength{\headsep}{14pt}
            \setlength{\oddsidemargin}{0pt}
            \documentclass{article}
            \begin{document}
            $";
        const string _texSuffix = @"$\end{document}";

        const int _baseResolution = 200;
        readonly PDFConvert _converter;
        readonly string _pathToDirectory;
        readonly List<string> _queuedUpdates;
        bool _isConversionRunning;

        public LatexToImg(string pathToDirectory){
            _converter = new PDFConvert();
            _converter.OutputFormat = "jpeg"; //format
            _converter.ResolutionX = _baseResolution; //dpi
            _converter.ResolutionY = _baseResolution;
            _converter.GraphicsAlphaBit = 4;
            _converter.TextAlphaBit = 4;
            _converter.UseMutex = true;

            _queuedUpdates = new List<string>();
            _isConversionRunning = false;
            _pathToDirectory = pathToDirectory;
        }

        public event LatexToPngCallback OnConversionCompletion; 

        public void QueueNewUpdate(string latex){
            _queuedUpdates.Add(latex);
            ProcLatexConversion();
        }

        void ProcLatexConversion(){
            if (!_isConversionRunning && _queuedUpdates.Count > 0){
                _isConversionRunning = true;

                string strToConvert = _queuedUpdates.Last();
                _queuedUpdates.Clear();

                _isConversionRunning = true;
                var sw = new StreamWriter(_pathToDirectory + "formula.tex");
                sw.Write(_texPrefix + strToConvert + _texSuffix);

                sw.Close();

                var processStartInfo = new ProcessStartInfo();
                processStartInfo.CreateNoWindow = true;
                processStartInfo.FileName = _pathToDirectory + "pdflatex.exe";
                processStartInfo.Arguments = _pathToDirectory + "formula.tex";
                processStartInfo.WorkingDirectory = _pathToDirectory;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;

                var process = Process.Start(processStartInfo);
                process.EnableRaisingEvents = true;
                process.Exited += ConvertPdfToImage;
            }
        }

        void ConvertPdfToImage(object obj, EventArgs eventArgs){
            _converter.Convert(
                _pathToDirectory+"formula.pdf",
                _pathToDirectory+"formula.jpg"
                );
            _isConversionRunning = false;
            if (OnConversionCompletion != null)
                OnConversionCompletion.Invoke();
        }
    }
}