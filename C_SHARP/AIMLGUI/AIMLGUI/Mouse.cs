using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AIMLGUI
{
    public class Mouse
    {
        //Control del ratón ***************************************************************************************************************************************
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x20;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x40;
        private const uint MOUSEEVENTF_MOVE = 0x1;
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        //Control del ratón ***************************************************************************************************************************************

        public void sendMouseMiddleClick(Point p)
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }
        public void sendMouseRightclick(Point p)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void sendMouseDoubleClick(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);

            Thread.Sleep(150);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }
        public void sendMouseClick(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void sendMouseRightDoubleClick(Point p)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);

            Thread.Sleep(150);

            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void sendMouseDown(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void sendMouseUp(Point p)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }
        public void sendMouseCenterDown(Point p)
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void sendMouseCenterUp(Point p)
        {
            mouse_event(MOUSEEVENTF_MIDDLEUP, (uint)p.X, (uint)p.Y, 0, (UIntPtr)0);
        }

        public void MouseMove(Point p)
        {
            Cursor.Position = p;
        }

    }
}
