using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StackCache
{
	public class QuestionListPage : ContentPage
	{
		ListView _questionsListView;
		ObservableCollection<QuestionInfo> _displayQuestions;
		DateTime _dateToDisplay;

		bool _initialDisplay = true;

		public QuestionListPage (DateTime dateToDisplay)
		{
			Title = "Xamarin Questions";
			_dateToDisplay = dateToDisplay;

			_displayQuestions = new ObservableCollection<QuestionInfo> ();

			_questionsListView = new ListView {
				ItemsSource = _displayQuestions,
				RowHeight = 40,
				HasUnevenRows = true
			};

			_questionsListView.ItemTemplate = new DataTemplate (typeof(QuestionCell));

			_questionsListView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
					
				var questionInfo = e.Item as QuestionInfo;
				var newPage = new QuestionDetailPage (questionInfo.QuestionID);						
				await Navigation.PushAsync (newPage);
			};
						
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					_questionsListView
				}
			};
					
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();

			if (_initialDisplay) {
				await LoadQuestions ();
				_initialDisplay = false;
			}
		}

		protected async Task LoadQuestions ()
		{
			_displayQuestions.Clear ();

			// 1. Get rid of anything too old for the cache
			await App.StackDataManager.Database.DeleteQuestionsAndAnswers ();

			// 2. Load up cached questions from the database
			var databaseQuestions = await App.StackDataManager.Database.GetQuestions (_dateToDisplay);

			foreach (var item in databaseQuestions) {
				_displayQuestions.Add (item);
			}

			try {
				// 4. Load up new questions from web
				var questionAPI = new StackOverflowService ();
				var downloadedQuestions = await questionAPI.GetQuestions (_dateToDisplay);

				// 5. See which questions are new from web and only add those to the display and cache
				foreach (var question in downloadedQuestions) {
					if (_displayQuestions.Contains (question) == false) {
						_displayQuestions.Insert (0, question);

						// 6. Save the new question to the cache
						await App.StackDataManager.Database.SaveQuestion(question);
					}
				}
					
			} catch (NoInternetException) {
				await HandleException ();
			}
		}
			
		protected virtual async Task HandleException ()
		{
		}
	}
}


