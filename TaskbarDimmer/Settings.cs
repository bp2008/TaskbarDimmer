using BPUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskbarDimmer
{
	public class Settings : SerializableObjectJson
	{
		public List<TaskbarSettings> Taskbars = new List<TaskbarSettings>();

		public void Initialize()
		{
			List<TaskbarSettings> list = new List<TaskbarSettings>();
			if (Taskbars == null)
				Taskbars = new List<TaskbarSettings>();
			if (Taskbars.Count == 0)
			{
				TaskbarSettings s = new TaskbarSettings();
				s.Position = TaskbarPosition.Bottom;
				Taskbars.Add(s);
			}
		}

		protected override SerializableObjectJson DeserializeFromJson(string str)
		{
			return JsonConvert.DeserializeObject<Settings>(str);
		}

		protected override string SerializeToJson(object obj)
		{
			return JsonConvert.SerializeObject(obj, Formatting.Indented);
		}
	}
	public class TaskbarSettings
	{
		/// <summary>
		/// Lightness from 1 to 100.
		/// </summary>
		public int Lightness = 30;
		/// <summary>
		/// Size in pixels.
		/// </summary>
		public int Size = 30;
		/// <summary>
		/// The edge of the screen where the taskbar is located.
		/// </summary>
		[JsonConverter(typeof(StringEnumConverter))]
		public TaskbarPosition Position = TaskbarPosition.None;
	}

	public enum TaskbarPosition
	{
		None,
		Bottom,
		Left,
		Right,
		Top
	}
}
