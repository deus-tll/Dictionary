using ExamDictionary.Services;
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
using System.Windows.Shapes;

namespace ExamDictionary
{
	/// <summary>
	/// Interaction logic for WindowAdding.xaml
	/// </summary>
	public partial class WordDialogWindow : Window
	{
		public WordDialogWindow()
		{
			InitializeComponent();
		}

		public void SetTextBlock(string textBlock)
		{
			TEXT_BLOCK.Text = textBlock;
		}

		public void SetTextWord(string word)
		{
			Word.Text = word;
		}

		public void SetTextTranslate(string translate)
		{
			Translate.Text = translate;
		}


		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			if (Word.Text is "" || Translate.Text is "")
			{
				MessageBox.Show("Обидва поля повинні бути заповнені!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			DataExchange.Word = Word.Text;
			DataExchange.Translate = Translate.Text;

			Word.Text = "";
			Translate.Text = "";

			Close();
		}
	}
}
