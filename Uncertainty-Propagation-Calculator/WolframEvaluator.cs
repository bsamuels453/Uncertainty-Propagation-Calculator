#region

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

#endregion

namespace Uncertainty_Propagation_Calculator{
    /// <summary>
    ///   Evaluates queries using wolframalpha.
    /// </summary>
    internal class WolframEvaluator{
        string _apiKey;
        string _prevQuery;
        QueryResults _prevResults;

        public WolframEvaluator(string apiKey){
            if (!apiKey.Contains('-') || apiKey.Contains(' ')){ //extremely ruidmentary validation check
                throw new Exception("invalid api key");
            }
            _apiKey = apiKey;
            _prevQuery = "";
        }

        public string ApiKey{
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        public string CalculatePartialDeriv(string equation, char dependent){
            string s = "(d/d" + dependent + ") " + equation;
            //replace equation operators with web-equivalent
            s = FormatEquationForURL(s);

            if (s == _prevQuery){
                return _prevResults.GetDerivative();
            }

            var results = QueryWolfram(s);
            _prevQuery = s;
            _prevResults = results;

            return results.GetDerivative();
        }

        /// <summary>
        ///   checks whether or not a key is valid using a test query to wolframalpha.
        /// </summary>
        public bool IsKeyValid(){
            var client = new WebClient();
            string crawlerData = client.DownloadString("http://api.wolframalpha.com/v2/query?input=" + "5*5" + "&appid=" + _apiKey);
            if(crawlerData.Contains("Invalid appid")){
                return false;
            }
            return true;
        }

        QueryResults QueryWolfram(string query){
            var client = new WebClient();

            //System.Console.WriteLine("sending API query to wolframalpha...");
            string crawlerData = client.DownloadString("http://api.wolframalpha.com/v2/query?input=" + query + "&appid=" + _apiKey);
            //System.Console.WriteLine("response recieved.");
            return new QueryResults(crawlerData);
        }

        /// <summary>
        ///   removes special characters in an equation and replaces them with their url-safe equivalent
        /// </summary>
        /// <param name="equation"> </param>
        /// <returns> </returns>
        static string FormatEquationForURL(string equation){
            equation = equation.Replace("(", "%28");
            equation = equation.Replace(")", "%29");
            equation = equation.Replace("/", "%2F");
            equation = equation.Replace("+", "%2B");
            equation = equation.Replace("^", "%5E");
            equation = equation.Replace(" ", "");
            return equation;
        }

        #region Nested type: QueryResults

        class QueryResults{
            readonly string _xmlDoc;

            public QueryResults(string xmlDoc){
                _xmlDoc = xmlDoc;
            }

            public string GetDerivative(){
                //var strmRdr = new StreamReader("query.xml");
                //string crawlerData = strmRdr.ReadLine();
                Stream strm = ConvertStringToStream(_xmlDoc);
                var strmRdr = new StreamReader(strm);

                string deriv = " ";

                //an extremely lazy method of finding the xml line that contains the derivative.
                //the reasoning behind this hack is that i'd probably spend more time deobfuscating 
                //how to use the .net xml library than time saved by this program in the first place
                while (true){
                    if (deriv != null){
                        if (!deriv.Contains("plaintext")){
                            deriv = strmRdr.ReadLine();
                        }
                        else{
                            break;
                        }
                    }
                    else{
                        deriv = strmRdr.ReadLine();
                    }
                }

                if (deriv == null)
                    throw new Exception("cannot locate derivative information from the crawled wolfram page");

                int eqpos = deriv.IndexOf('=');
                deriv = deriv.Substring(eqpos + 1, deriv.Count() - eqpos - 1);

                int endbracketpos = deriv.IndexOf('<');
                deriv = deriv.Substring(0, endbracketpos);
                deriv = deriv.Trim();
                deriv = deriv.Replace(' ', '*');

                return deriv;
            }

            public string GetResult(){
                //the result is stored within the second plaintext field
                Stream strm = ConvertStringToStream(_xmlDoc);
                var strmRdr = new StreamReader(strm);

                string result = " ";
                int numPlainTextHits = 0;
                while (true){
                    if (result != null){
                        if (!result.Contains("plaintext")){
                            result = strmRdr.ReadLine();
                        }
                        else{
                            numPlainTextHits++;
                            if (numPlainTextHits == 2){
                                break;
                            }
                            result = strmRdr.ReadLine();
                        }
                    }
                    else{
                        result = strmRdr.ReadLine();
                    }
                }
                if (result == null)
                    throw new Exception("cannot locate solution information from the crawled wolfram page");
                int endBracketPos = result.IndexOf('>');
                result = result.Substring(endBracketPos + 1, result.Count() - endBracketPos - 1);

                int startBracketPos = result.IndexOf('<');
                result = result.Substring(0, startBracketPos);
                result = result.Trim();

                return result;
            }

            Stream ConvertStringToStream(string str){
                byte[] byteArray = Encoding.UTF8.GetBytes(str); //XXX does utf-8 have all those greek symbols and shit?
                return new MemoryStream(byteArray);
            }
        }

        #endregion
    }
}