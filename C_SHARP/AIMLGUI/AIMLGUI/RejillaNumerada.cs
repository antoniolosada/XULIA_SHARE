using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Runtime.InteropServices;

namespace XULIA
{

    public partial class RejillaNumerada : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public struct sAutomationElement
        {
            public int ID;
            public int IdPadre;
            public AutomationElement el;
        }

        public RejillaNumerada()
        {
            InitializeComponent();
        }
        void FindTreeAutomationElments(int IdPadre, ref int Contador, int nivel, AutomationElement elementWindowElement, List<sAutomationElement> lst, int profundidad)
        {
            if (elementWindowElement == null)
                throw new ArgumentException();

            if ((elementWindowElement.Current.ClassName != "ScrollBar")
                && (elementWindowElement.Current.ClassName != "")
                )
            {
                if (elementWindowElement.Current.ClassName.Substring(0, 1) != "_")
                {
                    // Find all children that match the specified conditions.

                    AutomationElementCollection elementCollection =
                            elementWindowElement.FindAll(TreeScope.Children, Condition.TrueCondition);

                    //List<AutomationElement> elementCollection = GetAllChildren(elementWindowElement);


                    foreach (AutomationElement el1 in elementCollection)
                    {
                        sAutomationElement se = new sAutomationElement();
                        se.ID = Contador++;
                        se.IdPadre = IdPadre;
                        se.el = el1;
                        lst.Add(se);
                        if (nivel <= profundidad)
                            FindTreeAutomationElments(se.ID, ref Contador, nivel + 1, el1, lst, profundidad);
                    }
                }
            }
        }
        public void Pintar()
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush drawBrushYellow = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            AutomationElement el = AutomationElement.FromHandle(GetForegroundWindow());
            int Contador = 0;
            List<sAutomationElement> lst = new List<sAutomationElement>();
            FindTreeAutomationElments(0, ref Contador, 0, el, lst, 7);

            foreach (sAutomationElement el1 in lst)
            {
                System.Windows.Point clickablePoint;
                try  
                {
                    clickablePoint = el1.el.GetClickablePoint();

                    formGraphics.FillRectangle(drawBrushYellow, new Rectangle((int)(clickablePoint.X), (int)(clickablePoint.X), 30, 30));
                    formGraphics.DrawString(el1.ID.ToString(), drawFont, drawBrush, (int)(clickablePoint.X), (int)(clickablePoint.Y), drawFormat);
                }
                catch (Exception ex) {
                    clickablePoint = new System.Windows.Point(0, 0);
                }
            }

            this.Show();

        }
    }
}
