using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite.Net;
using SQLite.Net.Async;

namespace StackCache
{
	public class StackDBConnection : SQLiteAsyncConnection
	{
		public StackDBConnection (Func<SQLiteConnectionWithLock> connectionFunction) : base (connectionFunction)
		{
			
		}

		public async Task SetupDatabaseAsync ()
		{			
			await CreateTablesAsync<QuestionInfo, AnswerInfo> ().ConfigureAwait (false);
		}

		public async Task<IList<QuestionInfo>>GetQuestions (DateTime dateToDisplay)
		{
			var topDate = dateToDisplay.AddDays (1);

			return await Table<QuestionInfo> ().Where (qi => 
				qi.InsertDate > dateToDisplay &&
				qi.InsertDate < topDate
			).ToListAsync ().ConfigureAwait (false);			
		}

		public async Task<AnswerInfo>GetAnswerForQuestion (int questionId)
		{
			return await Table<AnswerInfo> ().Where (ai => ai.QuestionID == questionId).FirstOrDefaultAsync ().ConfigureAwait (false);
		}

		public async Task SaveQuestion(QuestionInfo question)
		{
			int questionId = question.QuestionID;

			var dbRecord = await Table<QuestionInfo> ()
				.Where (qi => qi.QuestionID == questionId)
				.FirstOrDefaultAsync ().ConfigureAwait (false);

			if (dbRecord == null) {
				question.InsertDate = DateTime.Now;
				await InsertAsync (question).ConfigureAwait (false);
			} else {
				question.InsertDate = dbRecord.InsertDate;
				await UpdateAsync (question).ConfigureAwait (false);
			}
		}

		public async Task SaveQuestions (IList<QuestionInfo> questions)
		{
			foreach (var item in questions) {
				int questionId = item.QuestionID;

				var dbRecord = await Table<QuestionInfo> ()
					.Where (qi => qi.QuestionID == questionId)
					.FirstOrDefaultAsync ().ConfigureAwait (false);

				if (dbRecord == null) {
					item.InsertDate = DateTime.Now;
					await InsertAsync (item).ConfigureAwait (false);
				} else {
					item.InsertDate = dbRecord.InsertDate;
					await UpdateAsync (item).ConfigureAwait (false);
				}
			}
		}

		public async Task DeleteQuestionsAndAnswers ()
		{
			DateTime cutOff = DateTime.Now.AddDays (-7);

			var oldQuestions = await Table<QuestionInfo> ().Where (qi => qi.InsertDate < cutOff).ToListAsync ().ConfigureAwait (false);

			foreach (var item in oldQuestions) {
				var questionId = item.QuestionID;

				var oldAnswers = await Table<AnswerInfo> ().Where (ai => ai.QuestionID == questionId).ToListAsync ().ConfigureAwait (false);

				foreach (var oa in oldAnswers) {
					var answerId = oa.AnswerID;
					await DeleteAsync<AnswerInfo> (answerId);
				}

				await DeleteAsync<QuestionInfo> (questionId);
			}
		}

		public async Task SaveAnswers (IList<AnswerInfo> answers)
		{
			foreach (var item in answers) {
				int answerId = item.AnswerID;

				var dbRecord = await Table<AnswerInfo> ().Where (ai => ai.AnswerID == answerId).FirstOrDefaultAsync ().ConfigureAwait (false);

				if (dbRecord == null)
					await InsertAsync (item).ConfigureAwait (false);
				else
					await UpdateAsync (item).ConfigureAwait (false);
			}
		}

		public async Task SaveAnswer (AnswerInfo answer)
		{
			int answerId = answer.AnswerID;

			var dbRecord = await Table<AnswerInfo> ()
				.Where (ai => ai.AnswerID == answerId)
				.FirstOrDefaultAsync ().ConfigureAwait (false);

			if (dbRecord == null)
				await InsertAsync (answer).ConfigureAwait (false);
			else
				await UpdateAsync (answer).ConfigureAwait (false);
		}
	}
}

