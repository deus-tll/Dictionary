using ExamDictionary.Models;
using ExamDictionary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ExamDictionary.Pages
{
	/// <summary>
	/// Interaction logic for PageListOfWords.xaml
	/// </summary>
	public partial class PageListOfWords : Page
	{
		private string PATH;
		private BindingList<WordModel> _wordsList;
		private FileIOService _fileIOService;
		private string _fromLanguage;
		private string _toLanguage;
		private bool _ascDescForWord = false;
		private bool _ascDescForDate = false;

		public PageListOfWords()
		{
			InitializeComponent();
		}


		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			_fromLanguage = PageSelectLanguage._FromLanguage;
			_toLanguage = PageSelectLanguage._ToLanguage;


			PATH = $"{Environment.CurrentDirectory}\\From{_fromLanguage}To{_toLanguage}.Json";


			HeaderOfWord.Header = $"{_fromLanguage} слово";
			HeaderOfTranslations.Header = $"Варіанти перекладу з {_toLanguage}";

			
			_fileIOService = new FileIOService(PATH);


			try
			{
				_wordsList = _fileIOService.LoadData();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				MainWindow.GetWindow(this).Close();
			}


			wordsList.ItemsSource = _wordsList;
			_wordsList.ListChanged += _wordsList_ListChanged;
		}


		private void _wordsList_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemMoved)
			{
				try
				{
					_fileIOService.SaveData(sender);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					MainWindow.GetWindow(this).Close();
				}
			}
		}


		private void SortByDate_Click(object sender, RoutedEventArgs e)
		{
			var l = _wordsList.ToList();

			_ascDescForDate = !_ascDescForDate;

			if (_ascDescForDate)
				l.Sort((s1, s2) => s1.AddingDate.CompareTo(s2.AddingDate));
			else
				l.Sort((s1, s2) => s2.AddingDate.CompareTo(s1.AddingDate));


			ClearAndFill(l);
		}


		private void SortByWord_Click(object sender, RoutedEventArgs e)
		{
			var l = _wordsList.ToList();

			_ascDescForWord = !_ascDescForWord;

			if (_ascDescForWord)
				l.Sort((s1, s2) => s1.Word.ToLower().CompareTo(s2.Word.ToLower()));
			else
				l.Sort((s1, s2) => s2.Word.ToLower().CompareTo(s1.Word.ToLower()));


			ClearAndFill(l);
		}


		private void AddWord_Click(object sender, RoutedEventArgs e)
		{
			DataExchange.Word = null;
			DataExchange.Translate = null;

			var _wordDialogWindow = new WordDialogWindow();
			_wordDialogWindow.SetTextBlock("Введіть слово та його варіанти перекладу");
			_wordDialogWindow.ShowDialog();

			if (DataExchange.Word is null || DataExchange.Translate is null)
				return;

			_wordsList.Add(new WordModel(DataExchange.Word, DataExchange.Translate));
		}


		private void ChangeWord_Click(object sender, RoutedEventArgs e)
		{
			DataExchange.Word = null;
			DataExchange.Translate = null;

			int index;
			if (!IsThereWord(out index))
				return;

			var _wordDialogWindow = new WordDialogWindow();
			_wordDialogWindow.SetTextWord(_wordsList[index].Word);
			_wordDialogWindow.SetTextTranslate(_wordsList[index].Translate);

			_wordDialogWindow.SetTextBlock("Внесіть потрібні корективи");
			_wordDialogWindow.ShowDialog();

			if (DataExchange.Word is null || DataExchange.Translate is null)
				return;

			_wordsList[index].Word = DataExchange.Word;
			_wordsList[index].Translate = DataExchange.Translate;
		}


		private void FindWord_Click(object sender, RoutedEventArgs e)
		{
			int index;
			if (!IsThereWord(out index))
				return;

			var tmp1 = _wordsList[0];
			var tmp2 = _wordsList[index];

			_wordsList[0] = tmp2;
			_wordsList[index] = tmp1;


			List<WordModel> l = _wordsList.ToList();

			ClearAndFill(l);
		}


		private void RemoveWord_Click(object sender, RoutedEventArgs e)
		{
			int index;
			if (!IsThereWord(out index))
				return;

			_wordsList.RemoveAt(index);

			List<WordModel> l = _wordsList.ToList();

			ClearAndFill(l);
		}


		private void RefreshWordField_Click(object sender, RoutedEventArgs e) => Word.Text = "";


		private void ClearAndFill(List<WordModel> l)
		{
			_wordsList.Clear();

			foreach (var item in l)
				_wordsList.Add(item);
		}


		private int FindWordInList()
		{
			var l = _wordsList.ToList<WordModel>();

			return l.FindIndex(w => w.Word == Word.Text);
		}


		private bool IsThereWord(out int index)
		{
			index = -1;

			if (Word.Text is "")
				return false;

			index = FindWordInList();

			if (index == -1)
			{
				MessageBox.Show("Слово не було знайдено у списку.", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
				return false;
			}

			return true;
		}
	}
}
