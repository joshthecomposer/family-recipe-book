## Notes  
#### appsettings.Development.json Structure  
```JSON
{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "ConnectionStrings": {
      "LocalConnection":"Server=localhost;Database=family_recipe_db;Uid=root;Pwd=root;"
    },
    "Jwt": {
        "Issuer": "Local",
        "Audience": "Local",
        "SecretKey": "<Stored on 1Password>"
    }
}
```  