using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDictionary.Models
{
	class WordModel : INotifyPropertyChanged
	{
		public WordModel(){}
		public WordModel(string word, string translate)
		{
			_word = word;
			_translate = translate;
		}


		private string _word;
		private string _translate;

		public DateTime AddingDate { get; set; } = DateTime.Now;


		public string Word
		{
			get { return _word; }
			set 
			{
				if (_word == value)
				{
					return;
				}
				_word = value;
				OnPropertyChanged("Word");
			}
		}


		public string Translate
		{
			get
			{
				return _translate;
			}

			set 
			{
				if (_translate == value)
				{
					return;
				}
				_translate = value;
				OnPropertyChanged("Translate");
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;


		protected virtual void OnPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
