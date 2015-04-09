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

		public async Task<IList<QuestionInfo>>GetQuestions ()
		{
			return await Table<QuestionInfo> ().ToListAsync ().ConfigureAwait (false);
		}

		public async Task<AnswerInfo>GetAnswerForQuestion (int questionId)
		{
			return await Table<AnswerInfo> ().Where (ai => ai.QuestionID == questionId).FirstOrDefaultAsync ().ConfigureAwait (false);
		}

		public async Task SaveQuestions (IList<QuestionInfo> questions)
		{
			foreach (var item in questions) {
				int questionId = item.QuestionID;

				var dbRecord = await Table<QuestionInfo> ()
			.Where (qi => qi.QuestionID == questionId)
			.FirstOrDefaultAsync ().ConfigureAwait (false);

				if (dbRecord == null)
					await InsertAsync (item).ConfigureAwait (false);
				else
					await UpdateAsync (item).ConfigureAwait (false);
			}
		}

		public async Task SaveAnswers(IList<AnswerInfo> answers)
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

