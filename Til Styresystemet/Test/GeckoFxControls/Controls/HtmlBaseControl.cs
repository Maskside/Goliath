using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gecko;

namespace GeckoFxControls
{
    public abstract class HtmlBaseControl : ContainerControl
    {
        /// <summary>
        /// The Html control that HtmlBaseControl uses.
        /// </summary>
        internal GeckoWebBrowser _htmlEditor;

        protected HtmlBaseControl()
        {
            CreateHtmlEditorAdaptor();
            ControlReady = false;
            _htmlEditor.DocumentCompleted += HtmlComboBox_ReadyStateChanged;
        }

        protected virtual void CreateHtmlEditorAdaptor()
        {
            _htmlEditor = new GeckoWebBrowser();
            _htmlEditor.Dock = DockStyle.Fill;            
        }

        #region Is Control Ready logic

        /// <summary>
        /// Returns true if the HtmlBased control is ready.
        /// </summary>
        internal protected bool ControlReady { get; private set; }

        private void HtmlComboBox_ReadyStateChanged(object sender, EventArgs readyStateChangedEventArgs)
        {            
            _htmlEditor.DocumentCompleted -= HtmlComboBox_ReadyStateChanged;

            ControlReady = true;
            OnControlReady();
        }

        /// <summary>
        /// Called when html is fully loaded.                
        /// </summary>
        protected abstract void OnControlReady();

        #endregion

        #region Utility methods

        /// <summary>
        /// Returns the effective, Html render that is powering this control.
        /// This is better than check HtmlEditorFactory.IsFireFox(), as that just checks the system default,
        /// and doesn't check if an override/force is in effect.
        /// </summary>
        // TODO: delete me
        protected bool IsFirefox
        {
            get { return true; }
        }

        protected GeckoHtmlElement GetElementById(string id)
        {
            if (_htmlEditor == null)
                return null;

#if PORT
            if (_htmlEditor.HtmlDocument2 == null)
                return null;

            return _htmlEditor.DocumentHTML.GetElementByID(id);
#else
            return _htmlEditor.Document.GetHtmlElementById(id);
#endif
        }

        protected GeckoHtmlElement GetBodyElement()
        {
            if (_htmlEditor == null)
                return null;

#if PORT
            if (_htmlEditor.HtmlDocument2 == null)
                return null;

            return _htmlEditor.HtmlDocument2.GetBody();
#else
            return _htmlEditor.Document.Body;
#endif
        }

        protected int SetControlHeightByMeasuringInternalHtmlControlHeight(GeckoHtmlElement htmlControl)
        {
#if PORT
            var newHeight = htmlControl.offsetHeight + 1;
            Height = newHeight;
            if (Parent is TableLayoutPanel)
            {
                const int paddingValue = 5;
                var tableLayoutPanel = (TableLayoutPanel)Parent;
                int row = tableLayoutPanel.GetRow(this);
                tableLayoutPanel.RowStyles[row] = new RowStyle(SizeType.Absolute, newHeight + paddingValue);
            }

            return newHeight;
#else
            var newHeight = htmlControl.OffsetHeight;
            Height = newHeight;
            return newHeight;
#endif
        }

        #endregion

        #region context menu helper methods

        protected void DisplaySubMenu(List<ToolStripMenuItem> tc)
        {
            ToolStripMenuItem tm;
            ToolStripSeparator ts;

            if ((tc != null) && tc.Count > 0)
            {
                var cMenu2 = new ContextMenuStrip();
                foreach (ToolStripMenuItem tm_loopVariable in tc)
                {
                    tm = tm_loopVariable;
                    if (tm.Text == "-")
                    {
                        ts = new ToolStripSeparator();
                        cMenu2.Items.Add(ts);
                    }
                    else
                    {
                        cMenu2.Items.Add(tm);
                    }
                }

                _htmlEditor.ContextMenuStrip = cMenu2;
                cMenu2.Show(_htmlEditor, _htmlEditor.PointToClient(Control.MousePosition));
            }
        }

        protected ToolStripMenuItem CreateToolStripMenuItem(string text, object tag, EventHandler clickAction, bool enabled)
        {
            return CreateToolStripMenuItem(text, tag, clickAction, enabled, String.Empty);
        }

        protected ToolStripMenuItem CreateToolStripMenuItem(string text, object tag, EventHandler clickAction, bool enabled, string tooltipText)
        {
            var MenuMain = new System.Windows.Forms.MenuStrip();
            var tFont = new Font(MenuMain.Font.Name, MenuMain.Font.Size);
            var tm = new ToolStripMenuItem
            {
                Font = tFont,
                RightToLeft = RightToLeft.No,
                Text = text,
                Tag = tag,
                ToolTipText = tooltipText,
                Enabled = enabled,
            };
            tm.Click += clickAction;

            return tm;
        }

        protected ToolStripMenuItem CreateSeperator()
        {
            return new ToolStripMenuItem { Text = "-" };
        }

        #endregion
    }
}
