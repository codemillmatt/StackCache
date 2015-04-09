using System;

using SQLite.Net.Attributes;

namespace StackCache
{
	public class QuestionInfo : IEquatable<QuestionInfo>
	{
		public QuestionInfo ()
		{
		}
			
		[PrimaryKey]
		public int QuestionID {
			get;
			set;
		}

		public string Title {
			get;
			set;
		}

		[Ignore]
		public bool LoadedFromWeb {
			get;
			set;
		}	

		[Ignore]
		public string TitleWithLoadFrom {
			get {
				if (LoadedFromWeb)
					return "W - " + this.Title;
				else
					return "D - " + this.Title;
			}
		}

		public bool Equals(QuestionInfo other)
		{
			return this.QuestionID == other.QuestionID;
		}
	}
}

