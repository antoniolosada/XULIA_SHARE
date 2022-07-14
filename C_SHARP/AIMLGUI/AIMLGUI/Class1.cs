using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XULIA
{
    public class MyVerticalProgessBarC : ProgressBar
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = (cp.Style | 4);
                return cp;
            }
        }
    }
}
