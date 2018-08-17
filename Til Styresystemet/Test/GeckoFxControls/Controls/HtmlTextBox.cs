using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using Gecko;
using Gecko.DOM;

namespace GeckoFxControls
{
    public class HtmlTextBox : HtmlBaseControl
	{
		#region Member Variables
		
		/// <summary>
		/// _cached variables stores values assigned to control before it is initalized.
		/// </summary>
		protected string _cachedText;
        protected int? _cachedMaxLength;
		protected int? _cachedSelectionLength;
		protected int? _cachedSelectionStart;
		protected int? _cachedSelectStart;
		protected int? _cachedSelectEnd;
		protected Font _cachedFont;
		protected string _cachedFontFeature;
		protected int? _cachedWidth;
		protected int? _cachedHeight;
		protected RightToLeft? _cachedRightToLeft;
		protected bool _cachedFocus;

        /// <summary>
        /// Event that gets fired when Alt+X is pressed.
        /// </summary>
        public event EventHandler AltXDown;

		// TODO: on firefox we could use autofocus="autofocus"
		const string _htmlPage =
            @"<html>
				<head>

				<script type='text/javascript'>

                // This function is not called by IE7. If HtmlEditor is ever upgraded to use IE9 then this function would be called.
				function OnInput(event) 
				{					
                    location.href=location.href='http://somehost/textchanged.php?'+ event.target.value;
				}

                // This function is only called by IE.
                function OnPropertyChange(event)
                {
                    location.href=location.href='textchanged.php:';
                }
				</script>

				<style>
					body.fillpage
					{
						width: 100%; 
						height: 100%; 
						padding: 0 0 0 0; 
						margin: 0 0 0 0; 
						overflow: hidden;
					}

					input.fillpage
					{
						position:absolute;
						top: 0px;
						left: 0px;
						width: 100%; 
						height: 100%; 
						display: inline;
					}					

					textarea.fillpage
					{
						position:absolute;
						top: 0px;
						left: 0px;
						width: 100%; 
						height: 100%; 
						display: inline;
						resize: none;
					}
				</style>
				</head>
				<body contentEditable=""false"" class=""fillpage"" tabindex=""-1""><input autofocus=""autofocus"" type=""text"" class=""fillpage"" id=""textbox"" tabindex=""1"" oninput=""OnInput(event)"" onpropertychange=""OnPropertyChange(event)""/></body></html>";
		
		const string _multilineHtml = @"<textarea class=""fillpage"" rows=""2"" cols=""20"" id=""textbox"" tabindex=""1"" oninput=""OnInput(event)""></textarea>";
        
		#endregion

        #region Constructor
        public HtmlTextBox()
		{
            // TODO: don't use LoadHtml
            _htmlEditor.LoadHtml(_htmlPage);			

			Controls.Add(_htmlEditor);
		}
        #endregion

        #region TextBox API Events
        public new event EventHandler TextChanged;
		#endregion

		#region Properties

		/// <summary>
		/// Returns true if document is initalized enough to start using Html TextBox
		/// </summary>
		protected bool IsTextBoxReady
		{
            get { return (ControlReady && GetTextBoxElement() != null && (!Multiline || IsMultiLineReady)); }
		}

        private bool IsMultiLineReady
        {
            get
            {
                if (!Multiline)
                    throw new ApplicationException("Programmer error: Only used for Multiline text boxes");
    
                GeckoHtmlElement element = GetTextBoxElement();
    
                if (element == null)
                    return false;

                return element.TagName.ToLowerInvariant() == "textarea";                
            }
        }

		#endregion

		#region TextBox API Properties.

		public override string Text
		{
			get
			{
				if (IsTextBoxReady)
				{
					GeckoHtmlElement element = GetTextBoxElement();
                    if (element is GeckoInputElement)
                        return ((GeckoInputElement)element).Value ?? String.Empty;
					if (element != null)
						return element.InnerHtml ?? String.Empty;
				}

				return _cachedText ?? String.Empty;
			}
			set
			{
			    _cachedText = value;
				if (IsTextBoxReady)
				{
                    GeckoHtmlElement element = GetTextBoxElement();
                    if (element is GeckoInputElement)
					{
                        ((GeckoInputElement)element).Value = value;
					}
					else
					{
                        // enforce MaxLength limit
                        if (_cachedMaxLength == null || value.Length <= _cachedMaxLength)
                        {
    						element.TextContent = value;
                        }
                        else
                        {
                            element.TextContent = value.Substring(0, (int)_cachedMaxLength);
                        }
					}
				}				

                // ENHANCE: if Text value is changed before control is ready this will generate two TextChanged events
                // for the last set before control is ready. A enhancement would be to prevent this.
                // Emit a TextChanged event field value is programatically changed.
                if (TextChanged != null)
                    TextChanged(this, EventArgs.Empty);
			}
		}
		public override RightToLeft RightToLeft
		{
			get { return base.RightToLeft; }
			set
			{
				base.RightToLeft = value;

				if (!IsTextBoxReady)
				{
					_cachedRightToLeft = value;
					return;
				}

				// TODO: should use css direction but IHTMLStyle doesn't support it.
				GetTextBoxElement().SetAttribute("DIR", value == RightToLeft.Yes ? "RTL" : "LTR");
			}
		}

		public bool Multiline { get; set; }

		public ScrollBars ScrollBars { get; set; }

		public virtual bool ReadOnly { get; set; }

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (!IsTextBoxReady)
				{
					_cachedFont = value;
					return;
				}

				if (base.Font.Equals(value))
					return;

				base.Font = value;

				GetTextBoxElement().Style.SetPropertyValue("font-size", value.SizeInPoints.ToString() + "pt");
                GetTextBoxElement().Style.SetPropertyValue("font-family", value.Name);

			    if (!Multiline)
                {
                    // Set the input box font size to double the text size.
                    GetTextBoxElement().Style.SetPropertyValue("height", (value.SizeInPoints * 2).ToString() + "pt");
                    SetControlHeightByMeasuringInternalHtmlControlHeight();
                }
			}
		}
		
		public virtual string FontFeatures
		{
			get
			{
				return _cachedFontFeature;
			}
			
			set
			{
				_cachedFontFeature = value;
				
				if (!IsTextBoxReady)
				{					
					return;
				}
				
#if PORT
				GetTextBoxElement().style.SetAttribute("-moz-font-feature-settings", String.Format("\"{0}\"",_cachedFontFeature), 0);
#endif
			}
		}

		public virtual int SelectionLength
		{
			get
			{
				if (!IsTextBoxReady)
					return _cachedSelectionLength ?? 0;

#if PORT
			    IHTMLTxtRange tr = CreateTextRange();
                int start = -tr.moveStart("character", int.MinValue);
                int end = -tr.moveEnd("character", int.MinValue);

                return end - start;
#else
			    return 0;
#endif
			}

			set
			{
				if (!IsTextBoxReady)
				{
					_cachedSelectionLength = value;
					return;
				}

#if PORT
			    IHTMLTxtRange tr = CreateTextRange();
                int startPosition = -tr.moveStart("character", int.MinValue);
                tr.move("character", startPosition);
                tr.moveEnd("character", value);
                tr.@select();
#endif
			}
		}

		public int SelectionStart
		{
			get
			{
				if (!IsTextBoxReady)				
					return _cachedSelectionStart ?? 0;

#if PORT
			    IHTMLTxtRange tr = CreateTextRange();
                return -tr.moveStart("character", int.MinValue);
#endif
			    return 0;
			}

			set
			{
				if (!IsTextBoxReady)
				{
					_cachedSelectionStart = value;
					return;
				}

#if PORT
			    IHTMLTxtRange tr = CreateTextRange();
                tr.moveStart("character", int.MinValue);
                tr.moveStart("character", value);
                tr.select();
#endif
			}
		}
		
		public int MaxLength
		{
			get
			{
                return _cachedMaxLength ?? int.MaxValue;
			}
			set
			{
                _cachedMaxLength = value;
                if (IsTextBoxReady)
                {
#if PORT
                    IHTMLElement element = GetTextBoxElement();
                    if (element is IHTMLInputElement)
                    {
                        ((IHTMLInputElement)element).maxLength = value;
                    }
                    else
                    {
                        element.SetAttribute("maxlength",value,0);
                    }
#endif
                }
                // TODO: truncate Text to new MaxLength (would "Text = Text" suffice?)
			}
		}
		

		#endregion
		
		#region Methods       

        protected override void OnControlReady()
        {
            if (Multiline)
            {
                // Change to a textarea.					
                GetBodyElement().InnerHtml = _multilineHtml;
            }

            if (_cachedText != null)
                Text = _cachedText;

            if (_cachedMaxLength != null)
                MaxLength = (int)_cachedMaxLength;

            if (_cachedSelectionStart != null && _cachedSelectionLength != null)
            {
                Select((int)_cachedSelectionStart, (int)_cachedSelectionLength);
            }
            else if (_cachedSelectionStart != null)
            {
                SelectionStart = (int)_cachedSelectionStart;
            }
            else if (_cachedSelectionLength != null)
            {
                SelectionLength = (int)_cachedSelectionLength;
            }

            if (_cachedSelectStart != null)
            {
                Select((int)_cachedSelectStart, (int)_cachedSelectEnd);
            }

            // For non MultiLine instances Font the font controls the height of the control.
            if (Multiline && _cachedHeight != null)
                SetBodyHeight((int)_cachedHeight);

            if (_cachedWidth != null)
                SetBodyWidth((int)_cachedWidth);

            if (_cachedRightToLeft != null)
                RightToLeft = (RightToLeft)_cachedRightToLeft;

            if (_cachedFont != null)
                Font = _cachedFont;

            if (_cachedFontFeature != null)
                FontFeatures = _cachedFontFeature;

            if (ReadOnly)
                GetTextBoxElement().SetAttribute("readonly", "readonly");

#if PORT
            _htmlEditor.SetEditDesigner(new HtmlTextBoxEditDesigner(this));
#endif

            if (_cachedFocus || Focused || ContainsFocus)
                HandleOnGotFocus();

            _htmlEditor.Navigating += HtmlEditor_BeforeNavigate;

            // Update cached values when form is closed. // TODO: make this more robust.
            FindForm().FormClosing += (sender, args) => _cachedText = Text;
        }		
		
		/// <summary>
		/// Use BeforeNavigate to be informed of changes input text or the textarea content.
		/// </summary>		
		void HtmlEditor_BeforeNavigate(object s, GeckoNavigatingEventArgs e)
		{
			string tURL = e.Uri.AbsoluteUri;
			string args;

			if (ParseUrl(tURL, "textchanged.php", out args))
			{                
                e.Cancel = true;
			    string currentTextValue = Text;
                if (TextChanged != null && _cachedText != currentTextValue)
                {
                    TextChanged(this, EventArgs.Empty);
                    _cachedText = currentTextValue;
                }
			}
		}
		
		// TODO: move this to somewhere in Utilities.
		/// <summary>
		/// Parses a url string looking for a url of the type idString?arguments
		/// </summary>
		/// <param name="targetUrl">The url to parse</param>
		/// <param name="idString">the 'filename' to look for</param>
		/// <param name="arguments">string following the '?' after idString in targetUrl</param>
		/// <returns>true if idString was found</returns>
		private static bool ParseUrl(string targetUrl, string idString, out string arguments)
		{
			arguments = string.Empty;

			if (targetUrl.IndexOf(idString) == -1)
				return false;

			int tM = targetUrl.IndexOf(idString);
			string tString = targetUrl.Substring(tM + idString.Length + 1);
			if (!string.IsNullOrEmpty(tString))
				arguments = tString;

			return true;
		}

		private GeckoHtmlElement GetTextBoxElement()
		{
		    return GetElementById("textbox");
		}


		private void SetBodyWidth(int width)
		{
			GetBodyElement().Style.SetPropertyValue("Width", (String.Format("{0}px", width.ToString())));
		}

		private void SetBodyHeight(int height)
		{
            GetBodyElement().Style.SetPropertyValue("Height", (String.Format("{0}px", height.ToString())));
		}

        private void SetControlHeightByMeasuringInternalHtmlControlHeight()
        {
            SetControlHeightByMeasuringInternalHtmlControlHeight(GetTextBoxElement());
        }

		internal protected virtual void ZoomLarger()
		{					
			SetBodyHeight(this.Height);
			OnKeyDown(new KeyEventArgs(Keys.Oemplus | Keys.Control));
		}

		internal protected virtual void ZoomSmaller()
		{
			// TODO: refactor HtmlScrTextBox to override Zoom* methods rather than listening for keypresses - but for now keep interfaces the same
			OnKeyDown(new KeyEventArgs(Keys.OemMinus | Keys.Control));
		}

		internal protected virtual void ZoomActual()
		{
			// TODO: refactor HtmlScrTextBox to override Zoom* methods rather than listening for keypresses - but for now keep interfaces the same
			OnKeyDown(new KeyEventArgs(Keys.D0 | Keys.Control));
		}
		
		/// <summary>
		/// Peform desired actions following a enter key.
		/// </summary>
		/// <returns>
		/// true if Enter has been handled, returns false otherwise.
		/// </returns>
		internal protected virtual bool EnterPressed()
		{
			if (!Multiline)
			{
				ParentForm.AcceptButton.PerformClick();
				return true;
			}
			
			return false;
		}

		internal protected virtual void EscapePressed()
		{
			ParentForm.CancelButton.PerformClick();
		}

		internal protected virtual void Tab(bool forward)
		{
			ParentForm.SelectNextControl(this, forward, true, true, true);
		}

        internal protected virtual void AltX()
        {
            if (AltXDown != null)
                AltXDown(this, EventArgs.Empty);
        }

        internal protected virtual void ShowContextMenu()
        {
            // TODO: disable menu items which aren't available (at least Undo, Paste)
            var tc = new List<ToolStripMenuItem>();
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Undo"),null,Undo));
            // TODO: tc.Add(CreateToolStripMenuItem("-",null,null));
            int selectionLength = SelectionLength;
            tc.Add(CreateToolStripMenuItem("Cut", null, Cut, selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("Copy", null, Copy, selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("Paste", null, Paste, true));
            tc.Add(CreateToolStripMenuItem("Delete", null, Delete, selectionLength != 0));
            tc.Add(CreateToolStripMenuItem("-", null, null, true));
            tc.Add(CreateToolStripMenuItem("Select All",null,SelectAll, selectionLength < Text.Length));
            // TODO: tc.Add(CreateToolStripMenuItem("-",null,null));
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Right to left Reading order"),null,...));
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Show Unicode control characters"),null,...));
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Insert Unicode control character"),null,...));
            // TODO: tc.Add(CreateToolStripMenuItem("-",null,null));
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Open IME"),null,...));
            // TODO: tc.Add(CreateToolStripMenuItem(Localizer.Str("Reconversion"),null,...));
            DisplaySubMenu(tc);
        }
        
        private void Cut(object sender, EventArgs e)
        {
#if PORT
            _htmlEditor.Cut();
#endif
        }

        private void Copy(object sender, EventArgs e)
        {
#if PORT
            _htmlEditor.Copy();
#endif
        }

        private void Paste(object sender, EventArgs e)
        {
#if PORT
            _htmlEditor.Paste();
#endif
        }

        private void Delete(object sender, EventArgs e)
        {
#if PORT
            _htmlEditor.DeleteSelection();
#endif
        }

        private void SelectAll(object sender, EventArgs e)
        {
            SelectionStart = 0;
            SelectionLength = Text.Length;
        }

        protected void HandleOnGotFocus()
        {            
            GetTextBoxElement().Focus();
        }

		#endregion

		#region TextBox API methods

		protected override void OnGotFocus(EventArgs e)
		{
            if (IsTextBoxReady)
                HandleOnGotFocus();
            else
                _cachedFocus = true;
			
			base.OnGotFocus(e);
		}

        protected override void OnLostFocus(EventArgs e)
        {
            _cachedFocus = false;

            base.OnLostFocus(e);
        }        

		protected override void OnSizeChanged(EventArgs e)
		{			
			base.OnSizeChanged(e);

			if (!IsTextBoxReady)
			{
				_cachedWidth = Width;
				_cachedHeight = Height;
			}			
		}

		public void Select(int start,int length)
		{
			if (!IsTextBoxReady)
			{
				_cachedSelectStart = start;
				_cachedSelectEnd = start + length;
				return;
			}

		    var input = (nsIDOMHTMLInputElement)GetTextBoxElement().DomObject;
            input.SetSelectionStartAttribute(start);
            input.SetSelectionEndAttribute(start + length);
		}

		public void ScrollToCaret()
		{

		}

		public void Clear()
		{
			if (!IsTextBoxReady)
			{
				_cachedText = String.Empty;
				return;
			}

		    GeckoHtmlElement element = GetTextBoxElement();
            if (element is GeckoInputElement)
            {
                GetTextBoxElement().NodeValue = String.Empty;
            }
            else
            {
                element.InnerHtml = String.Empty;
            }
		}

		#endregion
	}   
}
