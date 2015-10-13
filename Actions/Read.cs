﻿//
// Read.cs
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

    /// <summary>
    /// Read action.
    /// </summary>
    public class Read : Action
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Read"/> class.
        /// </summary>
        public Read()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Read"/> class.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public Read(object subject)
            : base(subject)
        {
        }
    }
}