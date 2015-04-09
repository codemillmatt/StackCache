using System;
using Xamarin.Forms;

namespace StackCache
{
	public class QuestionCell : ViewCell
	{
		Label title;
		StackLayout layout;

		public QuestionCell ()
		{
			title = new Label { YAlign = TextAlignment.Center };
			title.SetBinding (Label.TextProperty, new Binding("TitleWithLoadFrom"));

			layout = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (0,0,0,0),
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { title }
			};
					
													
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var questionInfo = (QuestionInfo)BindingContext;

			if (questionInfo.Title.Length > 150) {
				this.Height = 200;
			}
			else if (questionInfo.Title.Length > 100)
				this.Height = 100;
			else if (questionInfo.Title.Length > 50)
				this.Height = 75;
			else
				this.Height = 40;
		}
	}
}

