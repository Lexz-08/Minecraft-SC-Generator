using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Minecraft_SC_Generator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Close.Content = new Image
			{
				Source = Bitmaps.Instance[32, Colors.Black, Colors.White, BitmapType.Close],
				Stretch = Stretch.None,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};
			Minimize.Content = new Image
			{
				Source = Bitmaps.Instance[32, Colors.Gray, Colors.White, BitmapType.Minimize],
				Stretch = Stretch.None,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};

			foreach (string color in colors)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = color;
				item.Cursor = Cursors.Hand;
				item.Focusable = false;

				Line1_Color.Items.Add(item);
			}


			foreach (string color in colors)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = color;
				item.Cursor = Cursors.Hand;
				item.Focusable = false;

				Line2_Color.Items.Add(item);
			}


			foreach (string color in colors)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = color;
				item.Cursor = Cursors.Hand;
				item.Focusable = false;

				Line3_Color.Items.Add(item);
			}


			foreach (string color in colors)
			{
				ComboBoxItem item = new ComboBoxItem();
				item.Content = color;
				item.Cursor = Cursors.Hand;
				item.Focusable = false;

				Line4_Color.Items.Add(item);
			}

			Line1_Color.Text = "White";
			Line2_Color.Text = "White";
			Line3_Color.Text = "White";
			Line4_Color.Text = "White";

			GenerateCommand_Sign(this, null);
		}

		private static readonly string[] colors =
		{
			"Black",
			"Dark Blue",
			"Dark Green",
			"Dark Aqua",
			"Dark Red",
			"Dark Purple",
			"Gold",
			"Gray",
			"Dark Gray",
			"Blue",
			"Green",
			"Aqua",
			"Red",
			"Light Purple",
			"Yellow",
			"White",
		};

		private string[] slots = new string[26];

		[DllImport("user32.dll")]
		private static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		private void DragWindow(object sender, MouseButtonEventArgs e)
		{
			ReleaseCapture();
			SendMessage(new WindowInteropHelper(this).Handle, 161, 2, 0);
		}

		private void CloseWindow(object sender, MouseButtonEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void MinimizeWindow(object sender, MouseButtonEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private string GetSignJSON(string LineText, string LineColor, bool? Bold, bool? Italic, bool? Strikethrough, bool? Underline, bool? Obfuscated, string ClickCommand = "")
		{
			if (string.IsNullOrEmpty(LineText))
				return "{ \"text\": \"\" }";

			string json = "{ \"text\": \"" + LineText + "\", \"color\": \"" + LineColor + "\"";

			if (Bold == true) json += ", \"bold\": \"true\"";
			if (Italic == true) json += ", \"italic\": \"true\"";
			if (Strikethrough == true) json += ", \"strikethrough\": \"true\"";
			if (Underline == true) json += ", \"underlined\": \"true\"";
			if (Obfuscated == true) json += ", \"obfuscated\": \"true\"";

			if (!string.IsNullOrEmpty(ClickCommand))
				json += ", \"clickEvent\": { \"action\": \"run_command\", \"value\": \"" + ClickCommand + "\" }";

			json += " }";

			return json;
		}

		private void GenerateCommand_Sign(object sender, MouseButtonEventArgs e)
		{
			string line1 = GetSignJSON(
				Line1.Text, Line1_Color.Text.Replace(' ', '_').ToLower(),
				Line1_Bold.IsChecked, Line1_Italic.IsChecked, Line1_Strikethrough.IsChecked, Line1_Underline.IsChecked, Line1_Obfuscated.IsChecked,
				Line1_Command.Text);
			string line2 = GetSignJSON(
				Line2.Text, Line2_Color.Text.Replace(' ', '_').ToLower(),
				Line2_Bold.IsChecked, Line2_Italic.IsChecked, Line2_Strikethrough.IsChecked, Line2_Underline.IsChecked, Line2_Obfuscated.IsChecked,
				Line2_Command.Text);
			string line3 = GetSignJSON(
				Line3.Text, Line3_Color.Text.Replace(' ', '_').ToLower(),
				Line3_Bold.IsChecked, Line3_Italic.IsChecked, Line3_Strikethrough.IsChecked, Line3_Underline.IsChecked, Line3_Obfuscated.IsChecked,
				Line3_Command.Text);
			string line4 = GetSignJSON(
				Line4.Text, Line4_Color.Text.Replace(' ', '_').ToLower(),
				Line4_Bold.IsChecked, Line4_Italic.IsChecked, Line4_Strikethrough.IsChecked, Line4_Underline.IsChecked, Line4_Obfuscated.IsChecked,
				Line4_Command.Text);

			if (SetblockSign.IsChecked == true)
				SignCommand.Text = "/setblock ~ ~ ~ oak_sign{ Text1:'" + line1 + "', Text2:'" + line2 + "', Text3:'" + line3 + "', Text4:'" + line4 + "' }";
			else if (SetblockSign.IsChecked == false)
				SignCommand.Text = "/give @p oak_sign{ BlockEntityTag: { Text1:'" + line1 + "', Text2:'" + line2 + "', Text3:'" + line3 + "', Text4:'" + line4 + "' } }";
		}
	}
}
