using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;


namespace ClassLibrary1forPlugIn
{
    public class Class1 : WebTestPlugin
    {
        public override void PostWebTest(object sender,
          PostWebTestEventArgs e)
        {
        }

        public override void PreWebTest(object sender,
          PreWebTestEventArgs e)
        {
            e.WebTest.Context["TestParameter"] = "Test Value";
        }

    }
}

