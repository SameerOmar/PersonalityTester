// -----------------------------------------------------------------------
//  <copyright file="IDatabaseService.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PersonalityTester.Models;

namespace PersonalityTester.Services;

internal interface IDatabaseService
{
    /// <summary>
    ///     Gets the top questions specified by the count.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns></returns>
    List<Question> GetQuestions(int count);
}