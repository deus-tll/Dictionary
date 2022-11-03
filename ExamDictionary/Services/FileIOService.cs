using ExamDictionary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDictionary.Services
{
	class FileIOService
	{
		private readonly string PATH;


		public FileIOService(string path)
		{
			PATH = path;
		}


		public BindingList<WordModel> LoadData()
		{
			bool fileExists = File.Exists(PATH);
			if (!fileExists)
			{
				File.CreateText(PATH).Dispose();
				return new BindingList<WordModel>();
			}

			using(StreamReader reader = File.OpenText(PATH))
			{
				string fileText = reader.ReadToEnd();
				if (fileText == "")
					return new BindingList<WordModel>();

				return JsonConvert.DeserializeObject<BindingList<WordModel>>(fileText);
			}
		}


		public void SaveData(object wordsList)
		{
			using (StreamWriter writer = File.CreateText(PATH))
			{
				string output = JsonConvert.SerializeObject(wordsList);
				writer.WriteLine(output);
			}
		}
	}
}
