using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Basketball
{
    public partial class frmMain : Form
    {
        private Random m_random = new Random();
        private Rectangle m_basketryBox = new Rectangle(0, 0, 50, 50);
        private Rectangle m_basketballBox=new Rectangle(0,0,30,30);

        private void ResetPosition()
        {
            m_basketryBox.X = m_random.Next(picBoard.Width - m_basketryBox.Width);
            m_basketryBox.Y = m_random.Next(picBoard.Height - m_basketryBox.Height);

            do
            {
                m_basketballBox.X = m_random.Next(picBoard.Width - m_basketballBox.Width);
                m_basketballBox.Y = m_random.Next(picBoard.Height - m_basketballBox.Height);
            } while (m_basketballBox.IntersectsWith(m_basketryBox)); 
        }


        public frmMain()
        {
            InitializeComponent();

            ResetPosition();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetPosition();
            lblResult.Text = string.Empty;
            picBoard.Refresh();
        }

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Basketball.Properties.Resources.basketry, m_basketryBox);
            e.Graphics.DrawImage(Basketball.Properties.Resources.basketball, m_basketballBox);
        }

        private bool m_dragging=false;
        private Point m_prePosition = new Point();

        private void picBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                m_basketballBox.Contains(e.Location))
            {
                m_dragging = true;
                m_prePosition = e.Location;
            }
        }

        private void picBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_dragging)
            {
                m_basketballBox.X += e.Location.X - m_prePosition.X;
                m_basketballBox.Y += e.Location.Y - m_prePosition.Y;

                m_prePosition = e.Location;

                picBoard.Refresh();
            }
        }

        private void picBoard_MouseUp(object sender, MouseEventArgs e)
        {
            m_dragging = false;

            if (m_basketryBox.Contains(m_basketballBox))
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "太棒了";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "可惜了";
            }
        }
    }
}
