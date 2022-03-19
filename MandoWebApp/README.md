# Application setup history

* Create template application with `dotnet new angular -o MandoWebApp -au Individual -uld` while having dotnet 6.0 cli installed
* Prepare for code scaffolding by installing nuget package `Microsoft.VisualStudio.Web.CodeGeneration.Design@6.0.2`
    * This package is not compatible with dotnet 6.0.3, so downgrade everything to `6.0.2`
* Scaffold Identity so we can make changes to Registration flow based on [documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-6.0&tabs=visual-studio#scaffold-identity-into-an-mvc-project-with-authorization)
* Apply DB migration with `Update-Database`
* Run `npm install` from an elevated commadn prompt from inside the `MandoWebApp/ClientApp` (this may be done automatically if you run VS as administrator 🤔)
* Start application