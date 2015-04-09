using System;
using System.ComponentModel;

namespace StackCache
{
	public class AnswerInfo : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		int answerId, questionid;
		string answerBody;

		public AnswerInfo ()
		{
		}

		public int AnswerID {
			get { return answerId; }
			set {
				if (answerId != value) {
					answerId = value;
					OnPropertyChanged ("AnswerID");
				}
			}
		}

		public int QuestionID {
			get { return questionid; }
			set{
				if (questionid != value) {
					questionid = value;
					OnPropertyChanged ("QuestionID");
				}
			}
		}

		public string AnswerBody {
			get { return answerBody; }
			set{
				if(answerBody != value) {
					answerBody = value;
					OnPropertyChanged ("AnswerBody");
				}
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}

