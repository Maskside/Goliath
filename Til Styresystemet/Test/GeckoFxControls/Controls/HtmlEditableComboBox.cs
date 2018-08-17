using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using Gecko;
using System.Linq;

namespace GeckoFxControls
{        
    /// <summary>
    /// A Html implementation of an editable combo box.
    /// This is needed to display certain nonroman text which WinForms combobox are unable to display correctly.
    /// </summary>
    public class HtmlEditableComboBox : HtmlBaseControl
    {
        #region html internals

        /// <summary>
        /// An editiable combo box that is implemented by using in input type="text and a select (readonly combob box) control.
        /// </summary>
        const string HTMLPage =
            @"<html>
				<head>			
				<style>
					body.fillpage
					{
                        position:fixed;
                        top: -10000px;
						width: 100%; 
						height: 100%; 
						padding: 0 0 0 0; 
						margin: 0 0 0 0; 
						overflow: hidden;
					}

					div.fillpage
					{
						position:absolute;
						top: 0px;
						left: 0px;
						width: 100%; 
						height: 100%; 
						display: inline;
					}

                    #selectcontrol
                    {
                        z-index:1;
                        position:absolute;
                        top:0px;
                        left:0px;
                        width:100%;                        
                        margin:0;
                        padding:0;
                        border-right: 0;
                        outline: none;
                    }

                    #displayValue
                    {
                        z-index:2;
                        position:absolute;
                        top:0px;
                        left:0px;
                        /* C# overrides this widths later with more accurate values */
                        width: 95%; /* for IE's benefit */
                        width:-moz-calc(100% - 18px);
                        height:100%;
                        margin:0;
                        padding:0;
                        font-size:100%;
                        border-right: 0;
                    }
				</style>
                <script>
                // When select/combo box is changed, update the input text box, and focus it.
                function SelectControlChangedHandler(control)
                {
                    document.getElementById('displayValue').value=control.options[control.selectedIndex].text;                     
                    document.getElementById('displayValue').focus();
                }
                </script>
				</head>
				<body contentEditable=""false"" class=""fillpage"" tabindex=""-1"">
<div class=""fillpage"" id=""container"">
<select tabindex=""-1"" id=""selectcontrol"" onChange=""SelectControlChangedHandler(this)"">
<option></option>
</select>
<input type=""text"" name=""displayValue"" id=""displayValue"">
</div>
</body>
</html>";

        #endregion

        public HtmlEditableComboBox()
        {
            // TODO: don't use LoadHtml
            _htmlEditor.LoadHtml(HTMLPage);
            _htmlEditor.DomFocus += HtmlComboBox_HtmlEvent;            

            Items = new HtmlComboBoxItems(this);

            Controls.Add(_htmlEditor);
        }       

        #region Member Variables
                            	   				
		/// <summary>
		/// _cached variables stores values assigned to control before it is initalized.
		/// </summary>
		internal List<string> _cachedItems = new List<string>();
		protected string _cachedText;
		protected int? _cachedDropDownWidth;
        protected bool? _cachedHasFocus;        
        protected Font _cachedFont;
        protected int? _cachedHeight;
        protected int? _cachedWidth;
        protected int? _cacheSelectionStart;
        protected int? _cacheSelectionLength;
        protected RightToLeft _cachedRightToLeft;

        /// <summary>
        /// Event that gets fired when Alt+X is pressed.
        /// </summary>
        public event EventHandler AltXDown;

        private const int ComboBoxButtonsWidthInPixels = 18;
				
		#endregion
					
        #region non interface helper methods.

        /// <summary>
        /// When the html page is loaded, apply all the cached variables.
        /// </summary>
        protected override void OnControlReady()
        {
            if (_cachedDropDownWidth != null)
                DropDownWidth = (int)_cachedDropDownWidth;

            if (_cachedText != null)
                _cachedItems.Insert(0, _cachedText);

            // Update the select control to reflect the _cachedItems.
            Items.EndAdd();

            if (_cachedText != null)
                SetTextWithOutChangingComboOptions(_cachedText);

            if (_cachedHasFocus != null)
                ((GeckoHtmlElement)GetDisplayTextBoxElement()).Focus();

            if (_cachedFont != null)
                Font = _cachedFont;

            if (_cacheSelectionStart != null)
                SelectionStart = (int)_cacheSelectionStart;

            if (_cacheSelectionLength != null)
                SelectionLength = (int)_cacheSelectionLength;

#if PORT
            _htmlEditor.SetEditDesigner(new HtmlEditableComboBoxEditDesigner(this));
#endif

            // If we were just supporting firefox then we could just use the autofocus html property.
            if (Focused)
            {
                _htmlEditor.Focus();
                (GetDisplayTextBoxElement()).Focus();
            }

            GetDisplayTextBoxElement().Style.SetPropertyValue("width", (String.Format("{0}px", _cachedWidth - ComboBoxButtonsWidthInPixels)));

            // We initally position the body element at -10000, so it is not actually painted while layout is occuring.
            // Once everything is sized correctly we move the body element to its normal position.
            GetBodyElement().Style.SetPropertyValue("top", "0px");
        }

        private void HtmlComboBox_HtmlEvent(object o, GeckoDomEventArgs htmlEventArgs)
		{
#if PORT
                if (htmlEventArgs.Event.SrcElement == null || (htmlEventArgs.Event.SrcElement.id != "displayvalue" && htmlEventArgs.Event.SrcElement.id != "selectcontrol"))
                    ((IHTMLElement2)GetDisplayTextBoxElement()).Focus();
#endif		
		}

        /// <summary>
        /// Creates html for a select combo box.        
        /// </summary>
        /// <param name="values">The select combo boxes selection options.</param>
        /// <returns></returns>
        internal static string CreateSelectBoxHtml(IEnumerable<string> values)
        {
            var builder = new StringBuilder();
            builder.Append(@"<select tabindex=""-1"" id=""selectcontrol"" onChange=""SelectControlChangedHandler(this)"">");
            foreach (var optionValue in values)
            {
                builder.AppendFormat(@"<option value=""{0}"">{0}</option>", optionValue);
            }
            builder.Append("</select>");

            return builder.ToString();
        }

        /// <summary>
        /// Replaces he existing select control with a new selection.
        /// IE 7 was behaved unreliably when updating a select combo box via the mshtml COMInterop interfaces.
        /// To solve this we replace the entire select control each time its modified.  
        /// </summary>
        /// <param name="newSelectHtml">the html of the new selection control</param>
        internal void ReplaceSelectControl(string newSelectHtml)
        {
#if PORT
            GetSelectElement().outerHTML = newSelectHtml;
#endif
        }
        
        internal protected virtual void ZoomLarger()
        {
            OnKeyDown(new KeyEventArgs(Keys.Oemplus | Keys.Control));
        }

        internal protected virtual void ZoomSmaller()
        {            
            OnKeyDown(new KeyEventArgs(Keys.OemMinus | Keys.Control));
        }

        internal protected virtual void ZoomActual()
        {            
            OnKeyDown(new KeyEventArgs(Keys.D0 | Keys.Control));
        }       

        internal protected virtual void Tab(bool forward)
        {
            if (ParentForm != null) 
                ParentForm.SelectNextControl(this, forward, true, true, true);
        }

        internal protected virtual void AltX()
        {
            if (AltXDown != null)
                AltXDown(this, EventArgs.Empty);
        }

        internal protected virtual bool EnterPressed()
        {
            if (ParentForm != null)
                ParentForm.AcceptButton.PerformClick();
            return true;            
        }

        internal protected virtual void ShowContextMenu()
        {            
            var tc = new List<ToolStripMenuItem>();         
            int selectionLength = SelectionLength;
#if PORT
            tc.Add(CreateToolStripMenuItem("Cut", null, (s,e) => _htmlEditor.Cut(), selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("Copy", null, (s,e) => _htmlEditor.Copy(), selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("Paste", null, (s,e) => _htmlEditor.Paste(), true));
            tc.Add(CreateToolStripMenuItem("Delete", null, (s,e) => _htmlEditor.DeleteSelection(), selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("-", null, null, true));
#endif
            tc.Add(CreateToolStripMenuItem("Select All", null, (s,e) => Select(0, Text.Length), selectionLength < Text.Length));            
            DisplaySubMenu(tc);
        }

        /// <summary>
        /// Replaces items in the combo box list with the specified options.        
        /// </summary>
        /// <param name="items"></param>
        internal void ReplaceItems(IEnumerable<string> items)
        {
            Items.Clear();

            // Must add the Text field first.
            Items.Add(Text ?? String.Empty);

            foreach (var item in items)
                Items.Add(item);

            // Ensure the value of text box does not change when we update the select box's html.
            string beforeUpdate = Text;
            Items.EndAdd();
            if (beforeUpdate != Text)
                SetTextWithOutChangingComboOptions(beforeUpdate);
        }
       
        /// <summary>
        /// Get upto the specified number of items from the start of the combo options
        /// </summary>
        /// <param name="number">The number of items to get.</param>
        /// <returns></returns>
        internal List<string> GetItems(int number)
        {
            // If combo isn't initalized yet just return what was passed to ReplaceItems
            if (!ControlReady)
            {
                return _cachedItems.Take(number).ToList();
            }

            var returnItems = new List<string>();

#if PORT
            var child = ((IHTMLDOMNode)GetSelectElement()).firstChild;
            if (child == null)
                return returnItems;

            // Add Option element values to returnItems list, up to the requested amount.
            while (child != null && returnItems.Count < number)
            {
                if (child.nodeType == (int)NodeType.Element)
                {
                    var element = (IHTMLElement)child;
                    if ((element).tagName.ToLowerInvariant() == "option")
                    {
                        returnItems.Add(element.innerText ?? String.Empty);
                    }
                }
                child = child.nextSibling;
            }
#endif

            return returnItems;
        }
        
        /// <summary>
        /// Set the value of the Text Box, but don't update the select options to reflect this.
        /// </summary>
        /// <param name="value"></param>
        public void SetTextWithOutChangingComboOptions(string value)
        {
            if (!ControlReady)
                return;

#if PORT
            IHTMLElement element = GetDisplayTextBoxElement();
            ((IHTMLInputElement)element).value = value;
#endif
        }
        
        private void SetControlHeightByMeasuringInternalHtmlControlHeight()
        {            
            SetControlHeightByMeasuringInternalHtmlControlHeight(GetDisplayTextBoxElement()); 
        }

        private GeckoHtmlElement GetContainerElement()
        {
            return GetElementById("container");
        }

        internal GeckoHtmlElement GetSelectElement()
        {
            return GetElementById("selectcontrol");
        }

        internal GeckoHtmlElement GetDisplayTextBoxElement()
        {
            return GetElementById("displayValue");
        }
				
		#endregion

        #region inner class HtmlComboBoxItems

        public class HtmlComboBoxItems : IEnumerable
        {
            readonly HtmlEditableComboBox _parent;

            public HtmlComboBoxItems(HtmlEditableComboBox parent)
            {
                _parent = parent;
            }

            public void Clear()
            {
                _parent._cachedItems.Clear();
            }

            public void Add(String item)
            {
                _parent._cachedItems.Add(item);
            }

            public void EndAdd()
            {
                if (_parent.ControlReady)
                {
                    _parent.ReplaceSelectControl(CreateSelectBoxHtml(_parent._cachedItems));
                }
            }

            public bool Contains(string item)
            {
                return _parent._cachedItems.Contains(item);
            }

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return _parent._cachedItems.GetEnumerator();
            }

            #endregion
        }

        #endregion

        #region ComboBox Methods + Properties

        public override string Text
		{
			get
			{
                if (!ControlReady)
					return _cachedText ?? String.Empty;
                
#if PORT
                	IHTMLElement element = GetDisplayTextBoxElement();
                    if (element == null)
                        return String.Empty;
                    return ((IHTMLInputElement)element).value ?? String.Empty;
#endif
			    return String.Empty;
			}
			set
			{
                if (!ControlReady)
				{
					_cachedText = value;
					return;
				}
                
                // The first item in the list is the Text Field. 
			    IEnumerable<string> existingComboItems = GetItems(int.MaxValue);
                if (existingComboItems.Any() && existingComboItems.ElementAt(0) == Text)
                    existingComboItems = existingComboItems.Skip(1);

			    SetTextWithOutChangingComboOptions(value);

                // Reset Items since first item has been updated.
                // ReplaceItems automaily adds the Text Field value if exists
                ReplaceItems(existingComboItems);
			}
		}

        public override RightToLeft RightToLeft
        {
            get { return _cachedRightToLeft; }
            set
            {
                _cachedRightToLeft = value;

                if (ControlReady)
                {
#if PORT
                    // ENHANCE: should we also set the direction of the combo box?
                    GetDisplayTextBoxElement().SetAttribute("DIR", value == RightToLeft.Yes ? "RTL" : "LTR", 0);
#endif
                }
            }
        }
		
		public HtmlComboBoxItems Items
		{
			get; private set;
		}
				
        // ENHANCE: Implement
		public int DropDownWidth
		{
            get; set;
		}

        // ENHANCE: Implement
		public int MaxDropDownItems
		{
			get; set;
		}

        public virtual int SelectionStart
        {
            get
            {
                if (!ControlReady)
                    return 0;

#if PORT
                IHTMLInputTextElement2 textElement = ((IHTMLInputTextElement2)GetDisplayTextBoxElement());
                int selectionStart = textElement.selectionStart;
                return selectionStart;
#endif
                return 0;
            }
            set
            {
                if (!ControlReady)
                {
                    _cacheSelectionStart = value;
                    return;
                }

#if PORT
                IHTMLInputTextElement2 textElement = ((IHTMLInputTextElement2)GetDisplayTextBoxElement());
                textElement.selectionStart = value;
#endif                
            }
        }
        
        public virtual int SelectionLength
		{
			get
			{
                if (!ControlReady)
                    return 0;

#if PORT
				IHTMLInputTextElement2 textElement = ((IHTMLInputTextElement2)GetDisplayTextBoxElement());
				
				int selectionStart = textElement.selectionStart;
				int selectionEnd = textElement.selectionEnd;
				return selectionEnd - selectionStart;                
#endif

			    return 0;
			}

			set
			{
                if (!ControlReady)
                { 
                    _cacheSelectionLength = value;
                    return;
                }

#if PORT
				IHTMLInputTextElement2 textElement = ((IHTMLInputTextElement2)GetDisplayTextBoxElement());
				textElement.selectionEnd = textElement.selectionStart + value;
#endif
			}
		}

        // ENHANCE: implement (if needed)
        public bool FormattingEnabled { get; set; }
		
        // ENHANCE: could do a more efficent implementation.
        public void Select(int start, int length)
        {
            SelectionStart = start;
            SelectionLength = length;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!ControlReady)
                _cachedHasFocus = true;
#if PORT
            else
                ((IHTMLElement2)GetDisplayTextBoxElement()).Focus();
#endif
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _cachedHasFocus = false;
            base.OnLostFocus(e);
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (!ControlReady)
                {
                    _cachedFont = value;
                    return;
                }

                if (base.Font.Equals(value))
                    return;

                base.Font = value;

#if PORT
                GetContainerElement().style.SetFontSize(value.SizeInPoints.ToString() + "pt");
                GetContainerElement().style.SetFontFamily(value.Name);

                GetDisplayTextBoxElement().style.SetFontFamily(value.Name);
                GetDisplayTextBoxElement().style.SetFontSize(value.SizeInPoints.ToString() + "pt");                

                // For IE's benefit only
                GetDisplayTextBoxElement().style.SetLineHeight(((value.SizeInPoints - 2) * 2).ToString() + "pt");

                // Set the input box font size to double the text size.
                GetDisplayTextBoxElement().style.SetHeight((value.SizeInPoints * 2).ToString() + "pt");

                GetSelectElement().style.SetFontFamily(value.Name);
                GetSelectElement().style.SetFontSize((value.SizeInPoints).ToString() + "pt");
                GetSelectElement().style.SetHeight("100%");
#endif
                                                
                SetControlHeightByMeasuringInternalHtmlControlHeight();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!ControlReady)
            {
                _cachedWidth = Width;
                _cachedHeight = Height;
                return;
            }

#if PORT
            GetDisplayTextBoxElement().style.SetWidth(String.Format("{0}px", Width - ComboBoxButtonsWidthInPixels));
#endif
        }

        #endregion
		
	
	}
}
