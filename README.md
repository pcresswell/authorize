# Authorize
Simple authorization library. NOT Authorize.net.

This is a simple library. Goals are:
- Minimal dependencies.
- Flexible.
- Reasonably simple.

## By default, we are uncertain about everything. 
If you do not specify anything, then we are uncertain about authority. Normally, you would ban authorization. But we leave that up to your interpretation. Usually null will be interpretted as false but again, that' s up to you.
```
UserPermission user = new UserPermission();
bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));

Assert.IsNull(canCreate);
```
            
## User can do anything
```
UserPermission user = new UserPermission();
user.AddAuthorization(Actions.Manage);

bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
Assert.IsTrue(canCreate == true);
canCreate = user.Can(Actions.Delete, typeof(AddressModel));
Assert.IsTrue(canCreate == true);
```

## User can create anything
```
UserPermission user = new UserPermission();
user.AddAuthorization(Actions.Create);

bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
Assert.IsTrue(canCreate == true);
```

## User cannot create anything
```
UserPermission user = new UserPermission();
user.AddUnauthorization(Actions.Create);

bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
Assert.IsTrue(canCreate == false);
```

## User can edit only one particular instance of an object but not any instance of that type.
```
UserPermission user = new UserPermission();
AddressModel address = new AddressModel();
Update updateAction = new Update(address);
user.AddAuthorization(updateAction);

bool? canUpdate = user.Can(Actions.Update, address);
Assert.IsTrue(canUpdate == true);

// Also cannot update any other instance.
AddressModel differentAddress = new AddressModel();
canUpdate = user.Can(Actions.Update, differentAddress);
Assert.IsTrue(canUpdate == false);

// Nor can they now update any of that type.
canUpdate = user.Can(Actions.Update, typeof(AddressModel));
Assert.IsTrue(canUpdate == false);
```

## Other details
As you would expect, unauthorized actions dominate over unauthorized. The permissions can be serialized to a database.
```
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
```
## Things that would be nice
At the moment, there's not really a good way to build an interface on top of this lib since subjects are shoved to objects. You would probably have to build a bunch of type checking to reverse a collection of actions and their subjects out. Also nice to put this into a production project as at this point it's still just a fun little toy. 
