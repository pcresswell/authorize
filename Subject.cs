//
// Subject.cs
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
    using Newtonsoft.Json;

    /// <summary>
    /// A subject. Subjects are targets for actions. They can represent either a Type
    /// or an instance of a Type.
    /// </summary>
    [JsonObjectAttribute]
    public class Subject
    {
        public Subject()
        {
            this.Identity = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Subject"/> class.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public Subject(object subject)
        {
            Type t = subject as Type;
            if (t != null)
            {
                //subject is a Type
                this.Type = (Type)subject;
            }
            else
            {
                this.Type = subject.GetType();
                this.Identity = subject.GetHashCode().ToString();
            }
        }

        /// <summary>
        /// Gets the type that this subject applies against.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyAttribute]
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the instance identity.
        /// </summary>
        /// <value>The identity.</value>
        [JsonPropertyAttribute]
        public object Identity { get; private set; }

        /// <summary>
        /// Does this subject apply against the given subject.
        /// Four possible situations
        /// 1. We are a type and subject is a type - only types must match
        /// 2. We are a type and subject is instance of the type - only types must match
        /// 3. We are an instance and subject is a type - false
        /// 4. We are an instance and subject is an instance - same type and Identity
        /// </summary>
        /// <returns><c>true</c>, if to was appliesed, <c>false</c> otherwise.</returns>
        /// <param name="subject">The subject.</param>
        public bool AppliesTo(object subject)
        {
            // 1. We are a type
            if (this.Identity == null)
            {
                // 2. They are a type
                Type t = subject as Type;
                if (t != null)
                {
                    return subject == this.Type;
                }
                else
                {
                    return subject.GetType().Equals(this.Type);
                }
            }
            else 
            {
                // 3. We are an instance and they are a type.
                Type t = subject as Type;
                if (t != null)
                {
                    return false;
                }
                else
                {
                    // 4. we are an instance and they are as well
                    // Type does not match
                    if (!this.Type.Equals(subject.GetType()))
                    {
                        return false;
                    }
                    else
                    {
                        return this.Identity.Equals(subject.GetHashCode().ToString());
                    }
                }
            }
        }
    }
}