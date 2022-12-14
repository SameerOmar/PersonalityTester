// -----------------------------------------------------------------------
//  <copyright file="DatabaseService.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonalityTester.Data;
using PersonalityTester.Models;

namespace PersonalityTester.Services
{
    public class DatabaseService : IDatabaseService
    {
        #region

        private readonly AppDbContext _appDbContext;

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseService" /> class.
        /// </summary>
        /// <param name="appDbContext">The application database context.</param>
        public DatabaseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        ///     Gets the top questions specified by the count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public List<Question> GetQuestions(int count)
        {
            var questions = new List<Question>();

            questions.AddRange(_appDbContext.Questions.Include(question => question.Answers).Take(count));

            return questions;
        }
    }
}