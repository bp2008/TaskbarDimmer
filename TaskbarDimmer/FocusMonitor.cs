using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace TaskbarDimmer
{
	public class FocusMonitor : IDisposable
	{
		/// <summary>
		/// Event raised with the process ID of the focused process whenever focus changes.
		/// </summary>
		public event EventHandler<int> FocusChanged = delegate { };

		AutomationFocusChangedEventHandler focusHandler = null;
		public FocusMonitor()
		{
			focusHandler = OnFocusChanged;
			Automation.AddAutomationFocusChangedEventHandler(focusHandler);
		}
		private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
		{
			try
			{
				AutomationElement focusedElement = sender as AutomationElement;
				if (focusedElement != null)
				{
					int processId = focusedElement.Current.ProcessId;
					FocusChanged(this, processId);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
				}

				try
				{
					Automation.RemoveAutomationFocusChangedEventHandler(focusHandler);
				}
				catch { }
				focusHandler = null;

				// free unmanaged resources (unmanaged objects) and override finalizer
				//  set large fields to null
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}