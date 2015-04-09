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

		public QuestionListPage ()
		{
			Title = "Xamarin Questions";

			_displayQuestions = new ObservableCollection<QuestionInfo> ();

			_questionsListView = new ListView {
				ItemsSource = _displayQuestions,
				RowHeight = 40,
				HasUnevenRows = true
			};

			_questionsListView.ItemTemplate = new DataTemplate (typeof(QuestionCell));

			_questionsListView.ItemTapped += async (object sender, ItemTappedEventArgs e) => {
				_questionsListView.SelectedItem = null;

				var questionInfo = e.Item as QuestionInfo;

				var newPage = new QuestionDetailPage(questionInfo.QuestionID);

				await Navigation.PushAsync(newPage);
			};
						
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					_questionsListView
				}
			};

			Task.Run (async () => {
				await LoadQuestions();
			});
		}

		protected async Task LoadQuestions ()
		{
			_displayQuestions.Clear ();

			var questionAPI = new StackOverflowService ();

			var downloadedQuestions = await questionAPI.GetQuestions ();

			foreach (var question in downloadedQuestions) {
				_displayQuestions.Add (question);
			}				
		}
	}
}


