// -----------------------------------------------------------------------
//  <copyright file="DatabaseServiceUnitTest.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using PersonalityTester.Data;
using PersonalityTester.Models;
using PersonalityTester.Services;

namespace PersonalityTesterUnitTests
{
    [TestClass]
    public class DatabaseServiceUnitTest
    {
        #region

        private const int QuestionsCount = 10;

        private AppDbContext _appDbContext;

        #endregion

        /// <summary>
        ///     Cleanups test pre-required data.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Dispose();
        }

        /// <summary>
        ///     Tests if we get the exact requested questions
        /// </summary>
        [TestMethod]
        public void GetQuestions_ReturnsExactRecordsCount()
        {
            // Arrange
            const int count = 5;
            var databaseService = new DatabaseService(_appDbContext);

            // Act
            var questions = databaseService?.GetQuestions(count);

            // Assert
            Assert.IsNotNull(databaseService);
            Assert.IsNotNull(questions);
            Assert.AreEqual(count, questions.Count);
        }

        /// <summary>
        ///     Tests if we request more records than we have it should
        ///     returns the available records only without thrown an exception
        /// </summary>
        [TestMethod]
        public void GetQuestions_ReturnsMaxRecordsCount()
        {
            // Arrange
            const int count = 15;
            var databaseService = new DatabaseService(_appDbContext);

            try
            {
                // Act
                var questions = databaseService?.GetQuestions(count);

                // Assert
                Assert.IsNotNull(databaseService);
                Assert.IsNotNull(questions);
                Assert.AreEqual(QuestionsCount, questions.Count);
            }
            catch
            {
                Assert.Fail("Exception occurred");
            }
        }

        /// <summary>
        ///     Prepares the test pre-requirements.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ApplicationDatabase")
                .Options;

            _appDbContext = new AppDbContext(options);

            var questionId = 1;
            var answerId = 1;

            for (var i = 0; i < QuestionsCount; i++)
            {
                var question = _appDbContext.Questions.Add(new Question
                {
                    Id = questionId++,
                    Text = $"Question{questionId}"
                });

                for (var y = 0; y < 10; y++)
                {
                    question.Entity.Answers.Add(new Answer
                    {
                        Id = answerId++,
                        Score = 40,
                        Text = $"Answer{answerId}"
                    });
                }
            }

            _appDbContext.SaveChanges();
        }
    }
}