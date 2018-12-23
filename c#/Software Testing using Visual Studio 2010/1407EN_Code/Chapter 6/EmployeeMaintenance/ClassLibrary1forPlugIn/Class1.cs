using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace ClassLibrary1forPlugIn
{
    public class Class1 : WebTestPlugin
    {
        public override void PostTransaction(object sender, PostTransactionEventArgs e)
        { base.PostTransaction(sender, e); }

        public override void PreTransaction(object sender, PreTransactionEventArgs e)
        { base.PreTransaction(sender, e);  }

        public override void PrePage(object sender, PrePageEventArgs e)
        { base.PrePage(sender, e); }

        public override void PostPage(object sender, PostPageEventArgs e)
        { base.PostPage(sender, e); }

        public override void PreRequest(object sender, PreRequestEventArgs e)
        { base.PreRequest(sender, e); }

        public override void PostRequest(object sender, PostRequestEventArgs e)
        { base.PostRequest(sender, e); }

        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        { base.PreWebTest(sender, e);  }

        public override void PostWebTest(object sender, PostWebTestEventArgs e)
        { base.PostWebTest(sender, e); }

        public override void PreRequestDataBinding(object sender, PreRequestDataBindingEventArgs e)
        { base.PreRequestDataBinding(sender, e); }
    }
}

