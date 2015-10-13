//
// TestPermission.cs
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
using NUnit;
using NUnit.Framework;
using Authorize;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Authorize.Test
{
    public class TestPermission
    {
        public TestPermission()
        {
        }

        [Test]
        public void TestCanCreateAnything()
        {
            UserPermission user = new UserPermission();
            user.AddAuthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
        }

        [Test]
        public void TestCannotCreateAnything()
        {
            UserPermission user = new UserPermission();
            user.AddUnauthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == false);
        }

        [Test]
        public void TestNotCertainIfCanCreate()
        {
            UserPermission user = new UserPermission();
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == null);
        }

        [Test]
        public void UnauthorizedDominatesOverAuthorized()
        {
            UserPermission user = new UserPermission();
            user.AddUnauthorization(Actions.Create);
            user.AddAuthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == false);
        }

        [Test]
        public void CanAuthorizeAgainstAType()
        {
            UserPermission user = new UserPermission();
            Create createAction = new Create();
            createAction.AddSubject(typeof(AddressModel));
            user.AddAuthorization(createAction);
            Assert.IsTrue(createAction.AppliesTo(typeof(AddressModel)));
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
        }

        [Test]
        public void CanAuthorizedAgainstAnInstance()
        {
            UserPermission user = new UserPermission();
            Update updateAction = new Update();
            AddressModel address = new AddressModel();

            updateAction.AddSubject(address);
            user.AddAuthorization(updateAction);
            Assert.IsTrue(updateAction.AppliesTo(address));
            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);
        }

        [Test]
        public void CanAuthorizedAgainstAnInstanceButNotAgainstAnotherInstance()
        {
            UserPermission user = new UserPermission();
            AddressModel address = new AddressModel();
            Create createAction = new Create();
            createAction.AddSubject(address);

            user.AddAuthorization(createAction);
           
            bool? canUpdate = user.Can(Actions.Create, address);
            Assert.IsTrue(canUpdate == true);

            AddressModel secondAddress = new AddressModel();
            canUpdate = user.Can(Actions.Create, secondAddress);
            Assert.IsTrue(canUpdate == false);
        }

        [Test]
        public void AuthorizingAgainstTheTypeGivesAuthorityForAllInstances()
        {
            UserPermission user = new UserPermission();
            Update updateAction = new Update();
            AddressModel address = new AddressModel();

            updateAction.AddSubject(typeof(AddressModel));
            user.AddAuthorization(updateAction);
            Assert.IsTrue(updateAction.AppliesTo(address));
            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);
        }

        [Test]
        public void UnauthorizedAgainstTheTypePreventsAuthorizationForAllInstances()
        {

            // If we are unauthorized against a type,
            // then even if we authorize against an instance
            // we are not authorized
            UserPermission user = new UserPermission();
            Update updateAction = new Update(typeof(AddressModel));
            AddressModel address = new AddressModel();

            user.AddUnauthorization(updateAction);
            Assert.IsTrue(updateAction.AppliesTo(address));
            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == false);

            // now authorize against an instance

            Update authorizeInstanceUpdateAction = new Update();

            authorizeInstanceUpdateAction.AddSubject(address);
            user.AddAuthorization(authorizeInstanceUpdateAction);
            canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == false);
        }

        [Test]
        public void ManagingActionIsUnionOfAllActions()
        {
            // When we have a Manage Action,
            // we can do all actions
            UserPermission user = new UserPermission();
            Manage manageAction = new Manage();
            AddressModel address = new AddressModel();

            user.AddAuthorization(manageAction);
            Assert.IsTrue(manageAction.AppliesTo(address));

            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);
        }

        [Test]
        public void GetAuthorizationsForPersistance()
        {
            UserPermission user = new UserPermission();
            Create createAction = new Create(typeof(AddressModel));
            user.AddAuthorization(createAction);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented 
            };
            string serialized = JsonConvert.SerializeObject(user, settings);
            UserPermission deserializedUser = JsonConvert.DeserializeObject<UserPermission>(serialized, settings);

            // Now make sure deserialized object has same behaviour
            Assert.IsTrue(user.Can(Actions.Create, typeof(AddressModel)) == true);
            Assert.IsTrue(deserializedUser.Can(Actions.Create, typeof(AddressModel)) == true);

        }
    }
}

