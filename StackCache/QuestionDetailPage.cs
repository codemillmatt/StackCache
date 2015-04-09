using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StackCache
{
	public class QuestionDetailPage : ContentPage
	{
		WebView _theFullAnswer;
		AnswerInfo _theAnswer;
		Label _label;

		public QuestionDetailPage (int questionId)
		{									
			_theAnswer = new AnswerInfo ();

			Title = "Possible Answer";
				
			HtmlWebViewSource webSource = new HtmlWebViewSource ();

			_label = new Label ();
			_label.BindingContext = _theAnswer;
			_label.SetBinding (Label.TextProperty, "AnswerBody");

			webSource.BindingContext = _theAnswer;
			webSource.SetBinding (HtmlWebViewSource.HtmlProperty, "AnswerBody");
		
			_theFullAnswer = new WebView { 
				Source = webSource,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			View toAdd;

			if (Device.OS == TargetPlatform.Android)
				toAdd = _label;
			else
				toAdd = _theFullAnswer;

			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					toAdd
				}
			};
					
			Task.Run (async () => {
				await LoadAnswers (questionId);
			});

		}

		protected async Task LoadAnswers (int questionId)
		{				
			var answerAPI = new StackOverflowService ();
					
			var downloadedAnswer = await answerAPI.GetAnswerForQuestion (questionId);

			if (downloadedAnswer != null) {				
				_theAnswer.AnswerID = downloadedAnswer.AnswerID;
				_theAnswer.QuestionID = downloadedAnswer.QuestionID;
				_theAnswer.AnswerBody = downloadedAnswer.AnswerBody;
			} else {
				_theAnswer.AnswerBody = "No answer found";
			}
				
		}
	}
}


