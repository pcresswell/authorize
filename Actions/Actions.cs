// Actions.cs
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
    /// Static class for Actions.
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// The Create action.
        /// </summary>
        public static readonly Create Create = new Create();

        /// <summary>
        /// The Read action.
        /// </summary>
        public static readonly Read Read = new Read();

        /// <summary>
        /// The update action.
        /// </summary>
        public static readonly Update Update = new Update();

        /// <summary>
        /// The Delete action.
        /// </summary>
        public static readonly Delete Delete = new Delete();

        /// <summary>
        /// The Share action.
        /// </summary>
        public static readonly Share Share = new Share();

        /// <summary>
        /// The Manage action.
        /// </summary>
        public static readonly Manage Manage = new Manage();

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorize.Actions"/> class.
        /// </summary>
        private Actions()
        {
        }
    }
}