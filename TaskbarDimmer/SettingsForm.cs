using BPUtil;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarDimmer
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();

			this.Text += " " + Globals.AssemblyVersion;

			try
			{
				cbStartAutomatically.Checked = CheckAutomaticStartup();
			}
			catch (Exception)
			{
				MessageBox.Show("Insufficient permission to access Task Scheduler.");
			}
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			taskbarEditor1.SetIndex(0);
			taskbarEditor2.SetIndex(1);
			taskbarEditor3.SetIndex(2);
			taskbarEditor4.SetIndex(3);
			taskbarEditor5.SetIndex(4);
			taskbarEditor6.SetIndex(5);
		}

		private void cbStartAutomatically_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				bool isStartingAutomatically = CheckAutomaticStartup();
				if (cbStartAutomatically.Checked != isStartingAutomatically)
				{
					ProcessRunnerOptions opt = new ProcessRunnerOptions();
					opt.RunAsAdministrator = true;
					opt.workingDirectory = Directory.GetCurrentDirectory();
					if (cbStartAutomatically.Checked)
					{
						int exitCode = ProcessRunner.RunProcessAndWait(Application.ExecutablePath, "start_automatically_enable", out string std, out string err, opt);
						if (exitCode != 2)
							MessageBox.Show("Operation Failed");
					}
					else
					{
						int exitCode = ProcessRunner.RunProcessAndWait(Application.ExecutablePath, "start_automatically_disable", out string std, out string err, opt);
						if (exitCode != 2)
							MessageBox.Show("Operation Failed");
					}
					cbStartAutomatically.Checked = CheckAutomaticStartup();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private const string TaskName = "TaskbarDimmer Automatic Startup";
		public static void CreateStartupTask()
		{
			if (CheckAutomaticStartup())
				DeleteStartupTask();
			using (TaskService ts = new TaskService())
			{
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = "Start the TaskbarDimmer tray application.";
				td.Triggers.Add(new LogonTrigger());
				td.Actions.Add(new ExecAction(Globals.ApplicationDirectoryBase + Globals.ExecutableNameWithExtension, null, Globals.ApplicationRoot));
				td.Principal.RunLevel = TaskRunLevel.LUA;
				td.Settings.AllowDemandStart = true;
				td.Settings.DisallowStartIfOnBatteries = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
				td.Settings.Hidden = false;
				td.Settings.RestartCount = 1440;
				td.Settings.RestartInterval = TimeSpan.FromMinutes(1);
				td.Settings.RunOnlyIfIdle = false;
				td.Settings.RunOnlyIfNetworkAvailable = false;
				td.Settings.StartWhenAvailable = true;
				td.Settings.StopIfGoingOnBatteries = false;
				td.Settings.Volatile = false;

				ts.RootFolder.RegisterTaskDefinition(TaskName, td);
			}
		}
		public static void DeleteStartupTask()
		{
			using (TaskService ts = new TaskService())
			{
				if (ts.RootFolder.Tasks.Any(t => t.Name == TaskName))
					ts.RootFolder.DeleteTask(TaskName);
			}
		}
		/// <summary>
		/// Returns true if the program is configured to start automatically.
		/// </summary>
		/// <returns></returns>
		private static bool CheckAutomaticStartup()
		{
			using (TaskService ts = new TaskService())
			{
				return ts.RootFolder.Tasks.Any(t => t.Name == TaskName);
			}
		}
	}
}
