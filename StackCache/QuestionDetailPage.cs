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

		public QuestionDetailPage (int questionId)
		{									
			_theAnswer = new AnswerInfo ();

			Title = "Possible Answer";

			Label loadedFromLabel = new Label {  
				MinimumHeightRequest = 50, 
				XAlign = TextAlignment.Center
			};
			loadedFromLabel.BindingContext = _theAnswer;
			loadedFromLabel.SetBinding (Label.TextProperty, new Binding ("LoadedFromText"));
				
			HtmlWebViewSource webSource = new HtmlWebViewSource ();

			webSource.BindingContext = _theAnswer;
			webSource.SetBinding (HtmlWebViewSource.HtmlProperty, new Binding("AnswerBody"));
		
			_theFullAnswer = new WebView { 
				Source = webSource,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
					
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					loadedFromLabel, _theFullAnswer
				}
			};
					
			Task.Run (async () => {
				await LoadAnswers (questionId);
			});

		}

protected async Task LoadAnswers (int questionId)
{	
	AnswerInfo currentAnswer = null;

	// 1. Load from the database
	currentAnswer = await App.StackDataManager.Database.GetAnswerForQuestion (questionId);

	if (currentAnswer != null) {
		_theAnswer.AnswerID = currentAnswer.AnswerID;
		_theAnswer.QuestionID = currentAnswer.QuestionID;
		_theAnswer.AnswerBody = currentAnswer.AnswerBody;
	} else {
		// 2. No database record... Load answer from the web			
		var answerAPI = new StackOverflowService ();

		var downloadedAnswer = await answerAPI.GetAnswerForQuestion (questionId);

		if (downloadedAnswer != null) {				
			_theAnswer.AnswerID = downloadedAnswer.AnswerID;
			_theAnswer.QuestionID = downloadedAnswer.QuestionID;
			_theAnswer.AnswerBody = downloadedAnswer.AnswerBody;

			// 3. Save the answer for next time
			await App.StackDataManager.Database.SaveAnswer (_theAnswer);

		} else {					
			_theAnswer.AnswerBody = "No answer found";
		}
	}
}
	}
}


