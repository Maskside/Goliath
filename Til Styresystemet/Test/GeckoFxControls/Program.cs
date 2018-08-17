using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Gecko;

namespace GeckoFxControls
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: GetXULRunnerLocation isn't very robust but its ok when hacking..
            Xpcom.Initialize(XULRunnerLocator.GetXULRunnerLocation());

            var form1 = new Form1();
            
            var textBox = new HtmlTextBox();
            textBox.Location = new Point(5, 0);
            textBox.Width = 100;
            textBox.Height = 40;
            textBox.Visible = true;
            var comboBox = new HtmlEditableComboBox();
            comboBox.Location = new Point(5, 45);
            comboBox.Width = 100;
            comboBox.Height = 40;

            
            form1.Controls.Add(textBox);
            form1.Controls.Add(comboBox);
            
            Application.Run(form1);
        }
    }
}
