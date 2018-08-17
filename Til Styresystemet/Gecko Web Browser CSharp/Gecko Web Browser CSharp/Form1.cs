using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;

namespace Gecko_Web_Browser_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Gecko.Xpcom.Initialize("xulrunner");
            Gecko.GeckoPreferences.Default["extensions.blocklist.enabled"] = false;
        }
    }
}
