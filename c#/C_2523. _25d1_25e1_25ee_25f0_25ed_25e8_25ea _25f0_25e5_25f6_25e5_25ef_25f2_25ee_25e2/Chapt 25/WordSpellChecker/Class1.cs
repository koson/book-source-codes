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
			// ����� ��� ��������
			string sentence = "�������� ���������� ����� � �������.";

			// ����� ����� �� �����
			string[] words = sentence.Split();

			// ����� ��� �������� ����
			SpellChecker checker = new SpellChecker();

			// ��������� ������ �����
			foreach (string word in words) 
			{
				// ��������
				string[] suggestions = checker.Suggest(word);

				// ��������� ��������
				if (suggestions == null) 
				{
					Console.WriteLine("\"{0}\" �������� �����", word);
				} 
				else 
				{
					// ������ - ���������� ��������
					Console.WriteLine("\"{0}\" �������� �� �����! ��������:", word);
					foreach (string suggestion in suggestions)
						Console.WriteLine("\t" + suggestion);
				}
				Console.WriteLine();
			}
			checker = null;

			Console.ReadLine();
		}
	}
    
	// ����� ��� �������� ����
	class SpellChecker 
	{
		private ApplicationClass application;

		public SpellChecker() 
		{
			object template = "normal.dot";
			object newtemplate = false;
			object doctype = WdNewDocumentType.wdNewBlankDocument;
			object visible = false;

			// ������ MS word
			this.application = new ApplicationClass();
			this.application.DisplayAlerts = WdAlertLevel.wdAlertsNone;
			this.application.Visible = false;
			this.application.Options.SuggestSpellingCorrections = true;

			// ������� ����� ��������
			Document document = this.application.Documents.Add(ref template, ref newtemplate, ref doctype, ref visible);
			document.Activate();
		}

		~SpellChecker() 
		{
			// ��������� word, ������ �� ��������
			object savenochanges = WdSaveOptions.wdDoNotSaveChanges;
			object nothing = Missing.Value;

			if (this.application != null)
				this.application.Quit(ref savenochanges, ref nothing, ref nothing);
			this.application = null;
		}

		/// <summary>
		/// �������� �����
		/// </summary>
		public string[] Suggest(string word) 
		{
			object nothing = Missing.Value;

			// ���������
			bool spelledright = this.application.CheckSpelling(word, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			if (spelledright) 
				return null;

			// �������� ������ ���� ��� ������
			SpellingSuggestions suggestions = this.application.GetSpellingSuggestions(word, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);

			// ��������� ������ ����
			ArrayList words = new ArrayList();
			foreach (SpellingSuggestion suggestion in suggestions)
				words.Add(suggestion.Name);

			suggestions = null;

			// ���������� ������ ����
			return (string[]) words.ToArray(typeof (string));
		}
	}
}

