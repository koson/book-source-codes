using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;
using Microsoft.VisualStudio.TestTools.WebTesting.Rules;

namespace CustomRules
{
    public class CustomExtractionRule : ExtractionRule
    {
        public string ParameterName { get; set; }  //Name of the QueryString parameter to extract
                
        public override string RuleName // specify the name for the Rule
        {
            get { return "New Custom Extraction Rule"; }
        }

        public override string RuleDescription   // specify the description for the rule
        {
            get { return "This is a rule for extraction the value from input"; }
        }

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (e.Request.HasQueryStringParameters)
            {
                foreach (QueryStringParameter parameter in e.Request.QueryStringParameters)
                { 
                    if (parameter.Name.Equals(ParameterName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (parameter.Value != null)
                        {
                            e.Success = true;
                            e.Message = String.Format("Paramter Found with Value {0}", ParameterName);    
                        }
                        return;
                    }
                    e.Success = false;
                    e.Message = String.Format("Paramter {0} not Found ", ParameterName);

                }
            }
                    e.Success = false;
                    e.Message = String.Format("Paramter {0} not Found ", ParameterName);
        }
    }


    public class CustomValidationRule : ValidationRule
    {
        public string stringValueToFind;
        public override string RuleName // specify the name for the Rule
        {
            get { return "New Custom Extraction Rule"; }
        }

        public override string RuleDescription   // specify the description for the rule
        {
            get { return "This is a rule for extraction the value from input"; }
        }

        public string StringValueToFind 
        {
            get { return stringValueToFind; }
            set { stringValueToFind = value; } 
        }

        public override void Validate(object sender, ValidationEventArgs e)
        {
            string htmlDocument = string.Empty;
            if (!String.IsNullOrEmpty(e.Response.BodyString))
            {
                htmlDocument = e.Response.BodyString;
                e.IsValid = htmlDocument.Equals(stringValueToFind, StringComparison.CurrentCultureIgnoreCase);
                e.Message = "The string Found Successfully";
            }

            if (!e.IsValid)
            {
                e.Message = String.Format("The string {0} is not found", stringValueToFind);
            }
        }
    }
}
