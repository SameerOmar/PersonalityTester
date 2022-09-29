// -----------------------------------------------------------------------
//  <copyright file="IDatabaseService.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PersonalityTester.Models;

namespace PersonalityTester.Services;

public interface IDatabaseService
{
    /// <summary>
    ///     Gets the top questions specified by the count.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns></returns>
    List<Question> GetQuestions(int count);
}