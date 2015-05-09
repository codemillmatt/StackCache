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

				List<QuestionInfo> newQuestions = new List<QuestionInfo>();
				foreach (var question in downloadedQuestions) {
					if (_displayQuestions.Contains (question) == false) {
						_displayQuestions.Insert (0, question);
						newQuestions.Add(question);
					}
				}

				await App.StackDataManager.Database.SaveQuestions (newQuestions);

				if (newQuestions.Count > 0)
					await GrabAnswers (newQuestions);

			} catch (NoInternetException) {
				await HandleException ();
			}
		}

		// 5. Proactively grab the answer for the questions
		protected async Task GrabAnswers (List<QuestionInfo> questions)
		{			
			var questionsIds = new List<int> ();

			foreach (var item in questions) {
				questionsIds.Add (item.QuestionID);
			}

			var soAPI = new StackOverflowService ();

			var lotsOfAnswers = await soAPI.GetAnswersForManyQuestions (questionsIds);

			await App.StackDataManager.Database.SaveAnswers (lotsOfAnswers);
		}

		protected virtual async Task HandleException ()
		{
		}
	}
}


