// -----------------------------------------------------------------------
//  <copyright file="Question.cs" company="">
//      Author: Sameer Omar
//      Copyright (c) . All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalityTester.Models
{
    public class Question
    {
        /// <summary>
        ///     Gets or sets the answers.
        /// </summary>
        /// <value>
        ///     The answers.
        /// </value>
        public List<Answer> Answers { get; set; } = new();

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Required]
        public string Text { get; set; } = string.Empty;
    }
}