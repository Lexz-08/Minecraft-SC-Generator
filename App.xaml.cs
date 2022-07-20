using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Minecraft_SC_Generator
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
		}

		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
		{
			if (e.Name.Contains("resources"))
				return e.RequestingAssembly;

			using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Minecraft_SC_Generator.WinTitleBitmaps.Wpf.dll"))
			{
				byte[] buffer = new byte[s.Length];
				s.Read(buffer, 0, buffer.Length);

				return Assembly.Load(buffer);
			}
		}
	}
}
