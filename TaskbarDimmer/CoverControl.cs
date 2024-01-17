using BPUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarDimmer
{
	public partial class CoverControl : UserControl
	{
		public CoverControl()
		{
			//SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
			InitializeComponent();
			//SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Logger.Info("CoverControl_OnPaint");
		}

		private void CoverControl_Paint(object sender, PaintEventArgs e)
		{

			Logger.Info("CoverControl_Paint");
		}
	}
}
