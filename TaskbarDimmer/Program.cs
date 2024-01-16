using BPUtil.Forms;
using BPUtil.NativeWin;
using BPUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskbarDimmer.Properties;
using System.Runtime;

namespace TaskbarDimmer
{
	internal static class Program
	{
		private static TrayIconApplicationContext context;
		private static DimTaskbar dimmer;
		public static Settings Settings = new Settings();
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			try
			{
				Directory.CreateDirectory(Globals.WritableDirectoryBase);
				Directory.CreateDirectory(Globals.WritableDirectoryBase + "Logs/");
				Globals.OverrideErrorFilePath(() => Globals.WritableDirectoryBase + "Logs/" + Globals.AssemblyName + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month.ToString().PadLeft(2, '0') + ".txt");
				Environment.CurrentDirectory = Globals.WritableDirectoryBase;

				if (args.Length == 1)
				{
					try
					{
						if (args[0] == "start_automatically_enable")
						{
							SettingsForm.CreateStartupTask();
							return 2;
						}
						else if (args[0] == "start_automatically_disable")
						{
							SettingsForm.DeleteStartupTask();
							return 2;
						}
					}
					catch (Exception ex)
					{
						Console.Error.Write(ex.ToString());
						return 1;
					}
				}
				if (!SingleInstance.Start())
					return 0;

				Settings = new Settings();
				Settings.Load();
				Settings.Initialize();
				Settings.SaveIfNoExist();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				dimmer = new DimTaskbar();

				context = new TrayIconApplicationContext(Properties.Resources.favicon, Globals.AssemblyTitle + " " + Globals.AssemblyVersion, Context_CreateContextMenu, Context_DoubleClick);
				Application.Run(context);
				return 0;
			}
			finally
			{
				try
				{
					SingleInstance.Stop();
				}
				catch { }
				try
				{
					dimmer.Dispose();
				}
				catch { }
				try
				{
					context?.Dispose();
				}
				catch { }
			}
		}
		private static bool Context_CreateContextMenu(TrayIconApplicationContext context)
		{
			context.AddToolStripMenuItem("&Configure " + Globals.AssemblyTitle, Context_Configure, Properties.Resources.settings64);
			context.AddToolStripSeparator();
			context.AddToolStripMenuItem("E&xit " + Globals.AssemblyTitle, (sender, e) => { context.ExitThread(); }, Properties.Resources.close64);
			return true;
		}
		private static void Context_DoubleClick()
		{
			OpenConfigurationForm();
		}
		private static void Context_Configure(object sender, EventArgs e)
		{
			OpenConfigurationForm();
		}
		#region Configuration Form
		static SettingsForm sf = null;
		private static void OpenConfigurationForm()
		{
			if (sf != null)
				sf.Activate();
			else
			{
				sf = new SettingsForm();
				sf.FormClosed += Mf_FormClosed;
				sf.Show();
				sf.SetLocationNearMouse();
			}
		}

		private static void Mf_FormClosed(object sender, FormClosedEventArgs e)
		{
			sf = null;
		}
		#endregion
	}
}
