# appsettings.json Documentation

## Logging
- **LogLevel**: Specifies the logging levels for different components.
  - **Default**: Defines the default logging level for the application. In this case, it is set to `Information`.
  - **Microsoft.Hosting.Lifetime**: Configures the logging level specifically for the `Microsoft.Hosting.Lifetime` component. This is also set to `Information`.

## WorkerServiceSettings
- **FilePath**: Specifies the full file path for the executable `VbUserAccountTypeChecker.exe`. This path is used by the worker service to locate and execute the specified file.
  - **Example**: `C:\\Users\\Owner\\Desktop\\ServiceTest\\ExampleExecutable\\VbUserAccountTypeChecker.exe`
  
### Additional Considerations
- Ensure that the specified file path is accessible and points to a valid executable.
