using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace StackCache
{
	public class QuestionResponse
	{
		public QuestionResponse ()
		{
		}

		public bool has_more {
			get;
			set;
		}

		//[JsonProperty("items")]
		public IList<QuestionInfoResponse> items {
			get;
			set;
		}

		public int quota_max {
			get;
			set;
		}

		public int quota_remaining {
			get;
			set;
		}
	}

	public class QuestionInfoResponse
	{
		public QuestionInfoResponse ()
		{
			
		}

		public int answer_count {
			get;
			set;
		}

		public OwnerInfoResponse owner {
			get;
			set;
		}

		public string link {
			get;
			set;
		}

		public string[] tags {
			get;
			set;
		}

		public int view_count {
			get;
			set;
		}

		public int creation_date {
			get;
			set;
		}

		public bool is_answered {
			get;
			set;
		}

		//[JsonProperty("title")]
		public string title {
			get;
			set;
		}

		public int last_activity_date {
			get;
			set;
		}

		//[JsonProperty("question_id")]
		public int question_id {
			get;
			set;
		}

		public int score {
			get;
			set;
		}
	}

	public class OwnerInfoResponse
	{
		public OwnerInfoResponse ()
		{
			
		}

		public string link {
			get;
			set;
		}

		public string profile_image {
			get;
			set;
		}

		public string user_id {
			get;
			set;
		}
		public string display_name {
			get;
			set;
		}

		public string reputation {
			get;
			set;
		}

		public string user_type {
			get;
			set;
		}
	}

	public class AnswerResponse
	{
		public bool has_more {
			get;
			set;
		}

		public IList<AnswerInfoResponse> items {
			get;
			set;
		}

		public int quota_max {
			get;
			set;
		}

		public int quota_remaining {
			get;
			set;
		}
	}

	public class AnswerInfoResponse 
	{
		public int score {
			get;
			set;
		}

		public bool is_accepted {
			get;
			set;
		}

		public int creation_date {
			get;
			set;
		}

		public int last_activity_date {
			get;
			set;
		}

		public int question_id {
			get;
			set;
		}

		public int answer_id {
			get;
			set;
		}

		public string body {
			get;
			set;
		}


	}
}

