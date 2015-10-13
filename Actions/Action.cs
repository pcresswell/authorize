// Action.cs
//
// Author:
//       peter <pcresswell@gmail.com>
//
// Copyright (c) 2015 peter
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace Authorize
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Base Action class. Manages the relationship between the action and a list
    /// of subjects. Subjects are target objects that this action is applicable for.
    /// Subjects may be a Type or an instance of a class. If they are a Type, then 
    /// the action applies against all instances of that type. If it is an instance, 
    /// then it applies only against that instance.
    /// An action may apply against multiple subjects.
    /// </summary>
    [JsonObjectAttribute]
    public abstract class Action
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Action"/> class.
        /// </summary>
        public Action()
        {
            this.Subjects = new List<Subject>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Action"/> class.
        /// </summary>
        /// <param name="subject">The subject for which the action applies against.</param>
        public Action(object subject)
            : this()
        {
            this.AddSubject(subject);
        }

        /// <summary>
        /// Gets or sets a List of subjects against which this action applies.
        /// </summary>
        /// <value>The subjects.</value>
        [JsonPropertyAttribute]
        private IList<Subject> Subjects { get; set; }

        /// <summary>
        /// Determines whether this instance is type of the specified action.
        /// A test to see if this action is the same as another action. Remember
        /// that they don't have to have the same type to be of the same type. 
        /// Consider the "Manage" action which covers ALL actions. 
        /// </summary>
        /// <returns><c>true</c> if this instance is same as the specified action; otherwise, <c>false</c>.</returns>
        /// <param name="action">The action</param>
        public virtual bool IsTypeOf(Action action)
        {
            return this.GetType() == action.GetType();
        }

        /// <summary>
        /// Add a subject.
        /// </summary>
        /// <param name="subject">The subject</param>
        public void AddSubject(object subject)
        {
            Subject newSubject = new Subject(subject);
            this.Subjects.Add(newSubject);
        }

        /// <summary>
        /// Test to see if this action applies against the given subject.
        /// </summary>
        /// <returns><c>true</c>, if it applies, <c>false</c> otherwise.</returns>
        /// <param name="subject">The subject</param>
        public bool AppliesTo(object subject)
        {
            if (this.Subjects.Count == 0)
            {
                return true;
            }

            foreach (var s in this.Subjects)
            {
                if (s.AppliesTo(subject))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Authorize.Action"/>.
        /// An action equals another action if the type is the same.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Authorize.Action"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Authorize.Action"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.GetType().Equals(obj.GetType());
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Authorize.Action"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
    }
}