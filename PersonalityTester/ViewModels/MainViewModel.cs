// -----------------------------------------------------------------------
//  <copyright file="MainViewModel.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PersonalityTester.Models;
using PersonalityTester.Services;

namespace PersonalityTester.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region

        private readonly IDatabaseService _databaseService;
        private string _actionName = "Next";
        private Question _currentQuestion = new();
        private bool _finished;
        private int _progressValue;
        private List<Question> _questionsList = new();
        private bool _started;

        #endregion

        public MainViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        ///     Gets a value indicating whether can press Next/Finish button.
        /// </summary>
        public bool CanMoveNext => CollectedScores.ContainsKey(CurrentQuestion.Id);

        /// <summary>
        ///     Gets or sets the collected scores.
        /// </summary>
        public Dictionary<int, int> CollectedScores { get; set; } = new();

        /// <summary>
        ///     Gets or sets the current question.
        /// </summary>
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => SetValue(value, ref _currentQuestion, nameof(CurrentQuestion));
        }

        /// <summary>
        ///     Gets the final score.
        /// </summary>
        public double FinalScore => Math.Round(CollectedScores.Sum(s => s.Value) / (double)QuestionsCount, 2);

        /// <summary>
        ///     Gets or sets a value indicating whether this test is finished.
        /// </summary>
        public bool IsFinished
        {
            get => _finished;
            set => SetValue(value, ref _finished, nameof(IsFinished));
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this test is started.
        /// </summary>
        public bool IsStarted
        {
            get => _started;
            set => SetValue(value, ref _started, nameof(IsStarted));
        }

        /// <summary>
        ///     Gets or sets the name of the Next button.
        /// </summary>
        public string NextButtonName
        {
            get => _actionName;
            set => SetValue(value, ref _actionName, nameof(NextButtonName));
        }

        /// <summary>
        ///     Gets the progress maximum value.
        /// </summary>
        public int ProgressMax => QuestionsCount;

        /// <summary>
        ///     Gets or sets the progress value.
        /// </summary>
        public int ProgressValue
        {
            get => _progressValue;
            set => SetValue(value, ref _progressValue, nameof(ProgressValue));
        }

        /// <summary>
        ///     Gets the questions count.
        /// </summary>
        public int QuestionsCount { get; } = 8;

        /// <summary>
        ///     Gets or sets the questions list.
        /// </summary>
        public List<Question> QuestionsList
        {
            get => _questionsList;
            set => SetValue(value, ref _questionsList, nameof(QuestionsList));
        }

        /// <summary>
        ///     Handles the answer click event.
        /// </summary>
        /// <param name="answer">The selected answer.</param>
        public void AnswerSelected(Answer answer)
        {
            if (CollectedScores.ContainsKey(CurrentQuestion.Id))
            {
                CollectedScores[CurrentQuestion.Id] = answer.Score;
            }
            else
            {
                CollectedScores.Add(CurrentQuestion.Id, answer.Score);
            }

            NotifyOfPropertyChange(() => CanMoveNext);
        }

        /// <summary>
        ///     Closes the test window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void Close(Window window)
        {
            window.Close();
        }

        /// <summary>
        ///     Does the Next/Finish action.
        /// </summary>
        public void Next()
        {
            switch (NextButtonName)
            {
                case "Next":
                    var questionIndex = QuestionsList.IndexOf(CurrentQuestion);

                    CurrentQuestion = QuestionsList[++questionIndex];

                    ProgressValue++;

                    if (questionIndex == QuestionsCount - 1)
                    {
                        NextButtonName = "Finish";
                    }

                    NotifyOfPropertyChange(() => CanMoveNext);

                    break;

                case "Finish":
                    IsStarted = false;
                    IsFinished = true;

                    NotifyOfPropertyChange(() => FinalScore);

                    break;
            }
        }

        /// <summary>
        ///     Starts this Test.
        /// </summary>
        public void Start()
        {
            IsStarted = true;
            IsFinished = false;

            QuestionsList = _databaseService.GetQuestions(QuestionsCount);
            CurrentQuestion = QuestionsList.First();
            ProgressValue = 1;

            NotifyOfPropertyChange(() => ProgressMax);
            NotifyOfPropertyChange(() => ProgressValue);
        }
    }
}