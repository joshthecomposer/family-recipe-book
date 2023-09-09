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
      "LocalConnection":"Server=localhost;Database=project_r_db;Uid=root;Pwd=root;"
    },
    "AppSecrets" :{
		"JWTSecret": "<find this value in 1Password Dev Tools Vault>"
	}
}
```  