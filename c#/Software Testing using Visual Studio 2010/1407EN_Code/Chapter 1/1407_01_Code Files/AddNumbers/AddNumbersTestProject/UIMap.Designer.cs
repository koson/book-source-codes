﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 10.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace AddNumbersTestProject
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public partial class UIMap
    {
        
        /// <summary>
        /// RecordedMethodforAddNumbers - Use 'RecordedMethodforAddNumbersParams' to pass parameters into this method.
        /// </summary>
        public void RecordedMethodforAddNumbers()
        {
            #region Variable Declarations
            WinEdit uITextBoxNumberOneEdit = this.UIAddNumbersWindow.UITextBoxNumberOneWindow.UITextBoxNumberOneEdit;
            WinEdit uITextBoxNumberTwoEdit = this.UIAddNumbersWindow.UITextBoxNumberTwoWindow.UITextBoxNumberTwoEdit;
            WinButton uIAddButton = this.UIAddNumbersWindow.UIAddWindow.UIAddButton;
            WinButton uICloseButton = this.UIAddNumbersWindow.UIAddNumbersTitleBar.UICloseButton;
            #endregion

            // Type '5' in 'textBoxNumberOne' text box
            uITextBoxNumberOneEdit.Text =  this.RecordedMethodforAddNumbersParams.UITextBoxNumberOneEditText;

            // Type '{Tab}' in 'textBoxNumberOne' text box
            Keyboard.SendKeys(uITextBoxNumberOneEdit, this.RecordedMethodforAddNumbersParams.UITextBoxNumberOneEditSendKeys, ModifierKeys.None);

            // Type '4' in 'textBoxNumberTwo' text box
            uITextBoxNumberTwoEdit.Text = this.RecordedMethodforAddNumbersParams.UITextBoxNumberTwoEditText;

            // Type '{Tab}' in 'textBoxNumberTwo' text box
            Keyboard.SendKeys(uITextBoxNumberTwoEdit, this.RecordedMethodforAddNumbersParams.UITextBoxNumberTwoEditSendKeys, ModifierKeys.None);

            // Click 'Add' button
            Mouse.Click(uIAddButton, new Point(28, 9));

            // Click 'Close' button
            Mouse.Click(uICloseButton, new Point(17, 8));
        }
        
        /// <summary>
        /// AssertMethod1 - Use 'AssertMethod1ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void AssertMethod1()
        {
            #region Variable Declarations
            WinEdit uITextBoxResultEdit = this.UIAddNumbersWindow.UITextBoxResultWindow.UITextBoxResultEdit;
            #endregion

            // Verify that 'textBoxResult' text box's property 'ControlType' equals '12'
            Assert.AreEqual(this.AssertMethod1ExpectedValues.UITextBoxResultEditControlType, uITextBoxResultEdit.ControlType.ToString());

            // Verify that 'textBoxResult' text box's property 'ControlType' equals '13'
            Assert.AreEqual(this.AssertMethod1ExpectedValues.UITextBoxResultEditControlType1, uITextBoxResultEdit.ControlType.ToString());
        }
        
        /// <summary>
        /// AssertMethod2 - Use 'AssertMethod2ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void AssertMethod2()
        {
            #region Variable Declarations
            WinEdit uITextBoxResultEdit = this.UIAddNumbersWindow.UITextBoxResultWindow.UITextBoxResultEdit;
            #endregion

            // Verify that 'textBoxResult' text box's property 'ControlType' equals '12'
            Assert.AreEqual(this.AssertMethod2ExpectedValues.UITextBoxResultEditControlType, uITextBoxResultEdit.ControlType.ToString());
        }
        
        #region Properties
        public virtual RecordedMethodforAddNumbersParams RecordedMethodforAddNumbersParams
        {
            get
            {
                if ((this.mRecordedMethodforAddNumbersParams == null))
                {
                    this.mRecordedMethodforAddNumbersParams = new RecordedMethodforAddNumbersParams();
                }
                return this.mRecordedMethodforAddNumbersParams;
            }
        }
        
        public virtual AssertMethod1ExpectedValues AssertMethod1ExpectedValues
        {
            get
            {
                if ((this.mAssertMethod1ExpectedValues == null))
                {
                    this.mAssertMethod1ExpectedValues = new AssertMethod1ExpectedValues();
                }
                return this.mAssertMethod1ExpectedValues;
            }
        }
        
        public virtual AssertMethod2ExpectedValues AssertMethod2ExpectedValues
        {
            get
            {
                if ((this.mAssertMethod2ExpectedValues == null))
                {
                    this.mAssertMethod2ExpectedValues = new AssertMethod2ExpectedValues();
                }
                return this.mAssertMethod2ExpectedValues;
            }
        }
        
        public UIAddNumbersWindow UIAddNumbersWindow
        {
            get
            {
                if ((this.mUIAddNumbersWindow == null))
                {
                    this.mUIAddNumbersWindow = new UIAddNumbersWindow();
                }
                return this.mUIAddNumbersWindow;
            }
        }
        #endregion
        
        #region Fields
        private RecordedMethodforAddNumbersParams mRecordedMethodforAddNumbersParams;
        
        private AssertMethod1ExpectedValues mAssertMethod1ExpectedValues;
        
        private AssertMethod2ExpectedValues mAssertMethod2ExpectedValues;
        
        private UIAddNumbersWindow mUIAddNumbersWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'RecordedMethodforAddNumbers'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class RecordedMethodforAddNumbersParams
    {
        
        #region Fields
        /// <summary>
        /// Type '5' in 'textBoxNumberOne' text box
        /// </summary>
        public string UITextBoxNumberOneEditText = "5";
        
        /// <summary>
        /// Type '{Tab}' in 'textBoxNumberOne' text box
        /// </summary>
        public string UITextBoxNumberOneEditSendKeys = "{Tab}";
        
        /// <summary>
        /// Type '4' in 'textBoxNumberTwo' text box
        /// </summary>
        public string UITextBoxNumberTwoEditText = "4";
        
        /// <summary>
        /// Type '{Tab}' in 'textBoxNumberTwo' text box
        /// </summary>
        public string UITextBoxNumberTwoEditSendKeys = "{Tab}";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class AssertMethod1ExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that 'textBoxResult' text box's property 'ControlType' equals '12'
        /// </summary>
        public string UITextBoxResultEditControlType = "12";
        
        /// <summary>
        /// Verify that 'textBoxResult' text box's property 'ControlType' equals '13'
        /// </summary>
        public string UITextBoxResultEditControlType1 = "13";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertMethod2'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class AssertMethod2ExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that 'textBoxResult' text box's property 'ControlType' equals '12'
        /// </summary>
        public string UITextBoxResultEditControlType = "12";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIAddNumbersWindow : WinWindow
    {
        
        public UIAddNumbersWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Add Numbers";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public UITextBoxNumberOneWindow UITextBoxNumberOneWindow
        {
            get
            {
                if ((this.mUITextBoxNumberOneWindow == null))
                {
                    this.mUITextBoxNumberOneWindow = new UITextBoxNumberOneWindow(this);
                }
                return this.mUITextBoxNumberOneWindow;
            }
        }
        
        public UITextBoxNumberTwoWindow UITextBoxNumberTwoWindow
        {
            get
            {
                if ((this.mUITextBoxNumberTwoWindow == null))
                {
                    this.mUITextBoxNumberTwoWindow = new UITextBoxNumberTwoWindow(this);
                }
                return this.mUITextBoxNumberTwoWindow;
            }
        }
        
        public UIAddWindow UIAddWindow
        {
            get
            {
                if ((this.mUIAddWindow == null))
                {
                    this.mUIAddWindow = new UIAddWindow(this);
                }
                return this.mUIAddWindow;
            }
        }
        
        public UIAddNumbersTitleBar UIAddNumbersTitleBar
        {
            get
            {
                if ((this.mUIAddNumbersTitleBar == null))
                {
                    this.mUIAddNumbersTitleBar = new UIAddNumbersTitleBar(this);
                }
                return this.mUIAddNumbersTitleBar;
            }
        }
        
        public UITextBoxResultWindow UITextBoxResultWindow
        {
            get
            {
                if ((this.mUITextBoxResultWindow == null))
                {
                    this.mUITextBoxResultWindow = new UITextBoxResultWindow(this);
                }
                return this.mUITextBoxResultWindow;
            }
        }
        #endregion
        
        #region Fields
        private UITextBoxNumberOneWindow mUITextBoxNumberOneWindow;
        
        private UITextBoxNumberTwoWindow mUITextBoxNumberTwoWindow;
        
        private UIAddWindow mUIAddWindow;
        
        private UIAddNumbersTitleBar mUIAddNumbersTitleBar;
        
        private UITextBoxResultWindow mUITextBoxResultWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITextBoxNumberOneWindow : WinWindow
    {
        
        public UITextBoxNumberOneWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "textBoxNumberOne";
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public WinEdit UITextBoxNumberOneEdit
        {
            get
            {
                if ((this.mUITextBoxNumberOneEdit == null))
                {
                    this.mUITextBoxNumberOneEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITextBoxNumberOneEdit.WindowTitles.Add("Add Numbers");
                    #endregion
                }
                return this.mUITextBoxNumberOneEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITextBoxNumberOneEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITextBoxNumberTwoWindow : WinWindow
    {
        
        public UITextBoxNumberTwoWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "textBoxNumberTwo";
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public WinEdit UITextBoxNumberTwoEdit
        {
            get
            {
                if ((this.mUITextBoxNumberTwoEdit == null))
                {
                    this.mUITextBoxNumberTwoEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITextBoxNumberTwoEdit.WindowTitles.Add("Add Numbers");
                    #endregion
                }
                return this.mUITextBoxNumberTwoEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITextBoxNumberTwoEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIAddWindow : WinWindow
    {
        
        public UIAddWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "buttonAdd";
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public WinButton UIAddButton
        {
            get
            {
                if ((this.mUIAddButton == null))
                {
                    this.mUIAddButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIAddButton.SearchProperties[WinButton.PropertyNames.Name] = "Add";
                    this.mUIAddButton.WindowTitles.Add("Add Numbers");
                    #endregion
                }
                return this.mUIAddButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIAddButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIAddNumbersTitleBar : WinTitleBar
    {
        
        public UIAddNumbersTitleBar(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public WinButton UICloseButton
        {
            get
            {
                if ((this.mUICloseButton == null))
                {
                    this.mUICloseButton = new WinButton(this);
                    #region Search Criteria
                    this.mUICloseButton.SearchProperties[WinButton.PropertyNames.Name] = "Close";
                    this.mUICloseButton.WindowTitles.Add("Add Numbers");
                    #endregion
                }
                return this.mUICloseButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUICloseButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITextBoxResultWindow : WinWindow
    {
        
        public UITextBoxResultWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "textBoxResult";
            this.WindowTitles.Add("Add Numbers");
            #endregion
        }
        
        #region Properties
        public WinEdit UITextBoxResultEdit
        {
            get
            {
                if ((this.mUITextBoxResultEdit == null))
                {
                    this.mUITextBoxResultEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITextBoxResultEdit.WindowTitles.Add("Add Numbers");
                    #endregion
                }
                return this.mUITextBoxResultEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITextBoxResultEdit;
        #endregion
    }
}
