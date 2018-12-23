using System;
using System.Collections;
using System.Reflection;
using Microsoft.Office.Interop.Word;

namespace WordSpellChecker 
{
	class Class1 
	{
		[STAThread]
		static void Main(string[] args) 
		{
			// текст дл€ проверки
			string sentence = "ѕроверка орфографии слуво с ошибкай.";

			// делим текст на слова
			string[] words = sentence.Split();

			// класс дл€ проверки слов
			SpellChecker checker = new SpellChecker();

			// провер€ем каждое слово
			foreach (string word in words) 
			{
				// проверка
				string[] suggestions = checker.Suggest(word);

				// правильно написано
				if (suggestions == null) 
				{
					Console.WriteLine("\"{0}\" написано верно", word);
				} 
				else 
				{
					// ошибка - предлагаем варианты
					Console.WriteLine("\"{0}\" написано не верно! ¬арианты:", word);
					foreach (string suggestion in suggestions)
						Console.WriteLine("\t" + suggestion);
				}
				Console.WriteLine();
			}
			checker = null;

			Console.ReadLine();
		}
	}
    
	//  ласс дл€ проверки слов
	class SpellChecker 
	{
		private ApplicationClass application;

		public SpellChecker() 
		{
			object template = "normal.dot";
			object newtemplate = false;
			object doctype = WdNewDocumentType.wdNewBlankDocument;
			object visible = false;

			// объект MS word
			this.application = new ApplicationClass();
			this.application.DisplayAlerts = WdAlertLevel.wdAlertsNone;
			this.application.Visible = false;
			this.application.Options.SuggestSpellingCorrections = true;

			// создаем новый документ
			Document document = this.application.Documents.Add(ref template, ref newtemplate, ref doctype, ref visible);
			document.Activate();
		}

		~SpellChecker() 
		{
			// завершить word, ничего не сохран€€
			object savenochanges = WdSaveOptions.wdDoNotSaveChanges;
			object nothing = Missing.Value;

			if (this.application != null)
				this.application.Quit(ref savenochanges, ref nothing, ref nothing);
			this.application = null;
		}

		/// <summary>
		/// ѕроверка слова
		/// </summary>
		public string[] Suggest(string word) 
		{
			object nothing = Missing.Value;

			// ѕровер€ем
			bool spelledright = this.application.CheckSpelling(word, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			if (spelledright) 
				return null;

			// получаем список слов дл€ замены
			SpellingSuggestions suggestions = this.application.GetSpellingSuggestions(word, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			// сохран€ем список слов
			ArrayList words = new ArrayList();
			foreach (SpellingSuggestion suggestion in suggestions)
				words.Add(suggestion.Name);

			suggestions = null;

			// возвращаем массив слов
			return (string[]) words.ToArray(typeof (string));
		}
	}
}

