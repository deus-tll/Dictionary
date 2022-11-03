using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ExamDictionary.Pages
{
	/// <summary>
	/// Interaction logic for PageSelectLanguage.xaml
	/// </summary>
	public partial class PageSelectLanguage : Page
	{
		private List<string> _languages;
		public static string _FromLanguage { get; protected set; }
		public static string _ToLanguage { get; protected set; }
		private Frame _frame;

		public PageSelectLanguage(Frame frame)
		{
			InitializeComponent();

			_languages = new List<string>()
			{
				"English",
				"Ukrainian",
				"Polish",
				"Japanese",
				"Latvian",
				"Spanish",
				"Danish",
				"Estonian",
				"Korean",
				"Finnish",
			};

			_frame = frame;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			FromLanguage.ItemsSource = _languages;
			ToLanguage.ItemsSource = _languages;
		}


		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			if (_FromLanguage == null || _ToLanguage == null )
			{
				MessageBox.Show("Виберіть дві мови!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}
			else if (_FromLanguage == _ToLanguage)
			{
				MessageBox.Show("Не може бути дві однакових мови!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			
			_frame.Content = new PageListOfWords();
		}


		private void FromLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (FromLanguage.SelectedItem is string listBoxItem)
			{
				_FromLanguage = listBoxItem;
			}
		}


		private void ToLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ToLanguage.SelectedItem is string listBoxItem)
			{
				_ToLanguage = listBoxItem;
			}
		}
	}
}
