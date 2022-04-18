# Application setup history

* Create template application with `dotnet new angular -o MandoWebApp -au Individual -uld` while having dotnet 6.0 cli installed
* Prepare for code scaffolding by installing nuget package `Microsoft.VisualStudio.Web.CodeGeneration.Design@6.0.2`
    * This package is not compatible with dotnet 6.0.3, so downgrade everything to `6.0.2`
* Scaffold Identity so we can make changes to Registration flow based on [documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-6.0&tabs=visual-studio#scaffold-identity-into-an-mvc-project-with-authorization)
* Run `npm install` from an elevated commadn prompt from inside the `MandoWebApp/ClientApp` (this may be done automatically if you run VS as administrator 🤔)
* Start application

## MariaDB setup

* Download [MariaDB Server 10.6.](https://mariadb.org/download/?t=mariadb&p=mariadb&r=10.6.7) (That's the latest version supported by [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql#supported-database-servers-and-versions))
* [Install it](https://mariadb.com/kb/en/installing-mariadb-msi-packages-on-windows/) with a passwordless root user.
* Start the DB. (On default installation, the Windows service starts automatically.)
* Apply migrations with `Update-Database` or `dotnet ef database update`
* Connect to the DB with HeidiSQL - it's installed by default with MariaDB Server.
