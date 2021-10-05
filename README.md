# dotnet-dashboard
dotnet 5 | asp.net mvc | Identity: Individual User Accounts | ef | bootstrap 5 | datatables | chartjs

## Requisitos:
dotnet: 
https://dotnet.microsoft.com/(https://dotnet.microsoft.com/)  
sql server: 
https://www.microsoft.com/es-mx/sql-server/(https://www.microsoft.com/es-mx/sql-server/)  
vs code: 
https://code.visualstudio.com/(https://code.visualstudio.com/)  
libman: 
https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli(https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli)  
identity: 
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers(https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers)

## DataBase:
```
CREATE TABLE [Producto](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
    [Nombre] [nvarchar](128) NOT NULL,
    [Descripcion] [nvarchar](512) NOT NULL,
    [Tipo] [smallint] NOT NULL,
    [Precio] [float] NOT NULL,
    [Cantidad] [int] NOT NULL,
    [Foto] [varchar](max) NULL
);
CREATE TABLE [Paciente](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
    [Nombre] [nvarchar](256) NOT NULL,
    [Edad] [int] NOT NULL,
    [Direccion] [nvarchar](512) NOT NULL,
    [Telefono] [nvarchar](128) NOT NULL,
    [Correo] [nvarchar](256) NOT NULL,
    [Expediente] [varchar](max) NULL,
    [Foto] [varchar](max) NULL
);
CREATE TABLE [Cita](
    [Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
    [PacienteId] [uniqueidentifier] NOT NULL,
    [Estatus] [smallint] NOT NULL,
    [Fecha] [datetime] NOT NULL,
    FOREIGN KEY([PacienteId]) REFERENCES [Paciente] ([Id])
);
```

## Arquitecte:
```
.
├───src
│   ├───Application
│   │   └───Services
│   ├───Domain
│   │   ├───Entities
│   │   └───Enums
│   ├───Infrastructure
│   │   ├───Data
│   │   └───Notifications
│   └───Presentation.WebApp
│       ├───Controllers
│       ├───Models
│       ├───Views
│       │   ├───Productos
│       │   ├───Pacientes
│       │   ├───Citas
│       │   ├───Doctores
│       │   ├───Pagos
│       │   └───Shared
│       └───wwwroot
│           ├───css
│           ├───images
│           ├───js
│           └───lib
└───test
    ├───Application.Test
    ├───Domain.Test
    ├───Infrastructure.Test
    └───Presentation.WebApp.Test
         
```
### #.\src
#### #.\src\Domain
```
dotnet new classlib -o .\src\Domain -f net5.0
rm .\src\Domain\Class1.cs
mkdir .\src\Domain\Entities
echo 'namespace Domain { }' > .\src\Domain\Entities\Producto.cs
echo 'namespace Domain { }' > .\src\Domain\Entities\Paciente.cs
echo 'namespace Domain { }' > .\src\Domain\Entities\Cita.cs
echo 'namespace Domain { }' > .\src\Domain\Entities\Doctor.cs
echo 'namespace Domain { }' > .\src\Domain\Entities\Pago.cs
```
#### #.\src\Application
```
dotnet new classlib -o .\src\Application -f net5.0

rm .\src\Application\Class1.cs
mkdir .\src\Application\Services
echo 'namespace Application { }' > .\src\Application\Services\FileConverterService.cs

dotnet add .\src\Application package System.Drawing.Common

dotnet add .\src\Application reference .\src\Domain
```
#### #.\src\Infrastructure
```
dotnet new classlib -o .\src\Infrastructure -f net5.0

rm .\src\Infrastructure\Class1.cs
mkdir .\src\Infrastructure\Notifications
mkdir .\src\Infrastructure\Data
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Notifications\SmtpClientEmailService.cs
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Data\ProductosDbContext.cs
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Data\PacientesDbContext.cs
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Data\CitasDbContext.cs
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Data\DoctoresDbContext.cs
echo 'namespace Infrastructure { }' > .\src\Infrastructure\Data\PagosDbContext.cs

dotnet add .\src\Infrastructure package System.Data.SqlClient

dotnet add .\src\Infrastructure reference .\src\Application
```
#### #.\src\Presentation
```
dotnet new mvc -o .\src\Presentation.WebApp -au Individual -f net5.0

mkdir .\src\Presentation.WebApp\Views\Productos
mkdir .\src\Presentation.WebApp\Views\Pacientes
mkdir .\src\Presentation.WebApp\Views\Citas
mkdir .\src\Presentation.WebApp\Views\Doctores
mkdir .\src\Presentation.WebApp\Views\Pagos
mkdir .\src\Presentation.WebApp\wwwroot\images
echo '<svg></svg>' > .\src\Presentation.WebApp\wwwroot\images\placeholder.svg
echo '<svg></svg>' > .\src\Presentation.WebApp\wwwroot\images\user.svg
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\ProductosController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\PacientesController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\CitasController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\DoctoresController.cs
echo 'namespace Presentation.WebApp.Controllers { }' > .\src\Presentation.WebApp\Controllers\PagosController.cs
echo '@{ ViewData["Title"] = "Productos"; }' >  .\src\Presentation.WebApp\Views\Productos\Index.cshtml
echo '@{ ViewData["Title"] = "Productos > Crear"; }' >  .\src\Presentation.WebApp\Views\Productos\Create.cshtml
echo '@{ ViewData["Title"] = "Productos > Detalle"; }' >  .\src\Presentation.WebApp\Views\Productos\Details.cshtml
echo '@{ ViewData["Title"] = "Productos > Editar"; }' >  .\src\Presentation.WebApp\Views\Productos\Edit.cshtml
echo '@{ ViewData["Title"] = "Pacientes"; }' >  .\src\Presentation.WebApp\Views\Pacientes\Index.cshtml
echo '@{ ViewData["Title"] = "Pacientes > Crear"; }' >  .\src\Presentation.WebApp\Views\Pacientes\Create.cshtml
echo '@{ ViewData["Title"] = "Pacientes > Detalle"; }' >  .\src\Presentation.WebApp\Views\Pacientes\Details.cshtml
echo '@{ ViewData["Title"] = "Pacientes > Editar"; }' >  .\src\Presentation.WebApp\Views\Pacientes\Edit.cshtml
echo '@{ ViewData["Title"] = "Citas"; }' >  .\src\Presentation.WebApp\Views\Citas\Index.cshtml
echo '@{ ViewData["Title"] = "Citas > Crear"; }' >  .\src\Presentation.WebApp\Views\Citas\Create.cshtml
echo '@{ ViewData["Title"] = "Citas > Detalle"; }' >  .\src\Presentation.WebApp\Views\Citas\Details.cshtml
echo '@{ ViewData["Title"] = "Citas > Editar"; }' >  .\src\Presentation.WebApp\Views\Citas\Edit.cshtml
echo '@{ ViewData["Title"] = "Doctores"; }' >  .\src\Presentation.WebApp\Views\Doctores\Index.cshtml
echo '@{ ViewData["Title"] = "Doctores > Crear"; }' >  .\src\Presentation.WebApp\Views\Doctores\Create.cshtml
echo '@{ ViewData["Title"] = "Doctores > Detalle"; }' >  .\src\Presentation.WebApp\Views\Doctores\Details.cshtml
echo '@{ ViewData["Title"] = "Doctores > Editar"; }' >  .\src\Presentation.WebApp\Views\Doctores\Edit.cshtml
echo '@{ ViewData["Title"] = "Pagos"; }' >  .\src\Presentation.WebApp\Views\Pagos\Index.cshtml
echo '@{ ViewData["Title"] = "Pagos > Crear"; }' >  .\src\Presentation.WebApp\Views\Pagos\Create.cshtml
echo '@{ ViewData["Title"] = "Pagos > Detalle"; }' >  .\src\Presentation.WebApp\Views\Pagos\Details.cshtml
echo '@{ ViewData["Title"] = "Pagos > Editar"; }' >  .\src\Presentation.WebApp\Views\Pagos\Edit.cshtml
echo '{"version": "1.0","defaultProvider": "cdnjs","libraries": []}' > .\src\Presentation.WebApp\libman.json

dotnet remove .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Sqlite
dotnet add .\src\Presentation.WebApp package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Design
dotnet add .\src\Presentation.WebApp package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add .\src\Presentation.WebApp package Microsoft.AspNetCore.Identity.UI
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.SqlServer
dotnet add .\src\Presentation.WebApp package Microsoft.EntityFrameworkCore.Tools

dotnet add .\src\Presentation.WebApp reference .\src\Application
dotnet add .\src\Presentation.WebApp reference .\src\Infrastructure
```
```
dotnet aspnet-codegenerator identity -p .\src\Presentation.WebApp -dc Presentation.WebApp.IdentityDbContext --files "Account.Login;Account.Logout;Account.Register;Account.Logout;Account.AccessDenied;Account.Manage._Layout;Account.Manage._ManageNav;Account.Manage._StatusMessage;Account.Manage.ChangePassword;Account.Manage.Index;Account.Manage.SetPassword;"

# IdentityHostingStartup.cs
services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(
        context.Configuration.GetConnectionString("IdentityDbContextConnection")));

services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<IdentityDbContext>();
```


### #.\test
#### #.\test\Domain
```
dotnet new xunit -o .\test\Domain.Test
rm .\test\Domain.Test\UnitTest1.cs
dotnet add .\test\Domain.Test reference .\src\Domain
```
#### #.\test\Application
```
dotnet new xunit -o .\test\Application.Test
rm .\test\Application.Test\UnitTest1.cs
dotnet add .\test\Application.Test reference .\src\Application
```
#### #.\test\Infrastructure
```
dotnet new xunit -o .\test\Infrastructure.Test
rm .\test\Infrastructure.Test\UnitTest1.cs
dotnet add .\test\Infrastructure.Test reference .\src\Infrastructure
```
#### #.\test\Presentation
```
dotnet new xunit -o .\test\Presentation.WebApp.Test
rm .\test\Presentation.WebApp.Test\UnitTest1.cs
echo 'using Xunit; namespace Presentation.WebApp.Test { public class RoutingTest { } }' > .\test\Presentation.WebApp.Test\RoutingTest.cs
dotnet add .\test\Presentation.WebApp.Test reference .\src\Presentation.WebApp
```
```
dotnet new sln
dotnet sln add (ls -r .\**\*.csproj)
```

## #Librerias del cliente Web
### #libman.json
```
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "jquery@3.6.0",
      "destination": "wwwroot/lib/jquery/"
    },
    {
      "library": "jquery-validate@1.19.3",
      "destination": "wwwroot/lib/jquery-validation/"
    },
    {
      "library": "jquery-validation-unobtrusive@3.2.12",
      "destination": "wwwroot/lib/jquery-validation-unobtrusive/"
    },
    {
      "provider": "jsdelivr",
      "library": "bootstrap@5.1.1",
      "destination": "wwwroot/lib/bootstrap/"
    },
    {
      "library": "font-awesome@5.15.4",
      "destination": "wwwroot/lib/font-awesome/"
    },
    {
      "library": "datatables@1.10.21",
      "destination": "wwwroot/lib/datatables/"
    },
    {
      "library": "Chart.js@3.5.1",
      "destination": "wwwroot/lib/chartjs/"
    }
  ]
}
```

```
libman init -p cdnjs
```