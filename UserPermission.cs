//
// Permission.cs
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
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authorize
{
    /// <summary>
    /// Represents a collection of authorized and unauthorized actions. Typically, an instance of this class
    /// would handle a single user's authorizations.
    /// </summary>
    [JsonObjectAttribute]
    public class UserPermission
    {
        public UserPermission()
        {
            this.AuthorizedActions = new List<Action>();
            this.UnauthorizedActions = new List<Action>();
        }
        [JsonPropertyAttribute]
        private IList<Action> AuthorizedActions { get; set; }

        [JsonPropertyAttribute]
        private IList<Action> UnauthorizedActions{ get; set; }

        public void AddAuthorization(Action action)
        {
            if (this.AuthorizedActions.Contains(action))
            {
                return;
            }

            this.AuthorizedActions.Add(action);
        }

        public void RemoveAuthorization(Action action)
        {
            if (!this.AuthorizedActions.Contains(action))
            {
                return;
            }

            this.AuthorizedActions.Remove(action);
        }

        public void AddUnauthorization(Action action)
        {
            if (this.UnauthorizedActions.Contains(action))
            {
                return;
            }

            this.UnauthorizedActions.Add(action);
        }

        public void RemoveUnauthorization(Action action)
        {
            if (!this.UnauthorizedActions.Contains(action))
            {
                return;
            }

            this.UnauthorizedActions.Remove(action);
        }

        public bool? Can(Action action, object subject)
        {
            // Check for unauthorized actions that apply to this subject.
            bool unauthorizedTypeFound = false;
            foreach (var unauthorizedAction in this.UnauthorizedActions)
            {
                if (unauthorizedAction.IsTypeOf(action))
                {
                    unauthorizedTypeFound = true;
                    if (unauthorizedAction.AppliesTo(subject))
                    {
                        return false;
                    }
                }
            }

            // Check for authorized actions that apply to this subject.
            bool authorizedTypeFound = false;
            foreach (var authorizedAction in this.AuthorizedActions)
            {
                if (authorizedAction.IsTypeOf(action))
                {
                    authorizedTypeFound = true;
                    if (authorizedAction.AppliesTo(subject))
                    {
                        return true;
                    }
                }
            }

            // We have actions specified of the given type but not for that subject matter
            // so we say that we are NOT authorized.
            if (authorizedTypeFound || unauthorizedTypeFound)
            {
                return false;
            }

            // We don't have any guidance on that action 
            // so return null to indicate complete ambiguity.
            return null;
        }
    }
}

