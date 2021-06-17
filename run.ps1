dotnet new tool-manifest
dotnet tool install dotnet-ef
dotnet tool update dotnet-ef

dotnet build

dotnet ef database update --startup-project src/Services/Auth.Service/Auth.IdentityServer.Oidc.Web --project src/Services/Auth.Service/Auth.IdentityServer.Oidc.Web
dotnet ef database update --startup-project src/Services/Event.Service/Event.GraphQL --project src/Services/Event.Service/Event.Infrastructure
dotnet ef database update --startup-project src/Services/EventRegistration.Service/EventRegistration.GraphQL --project src/Services/EventRegistration.Service/EventRegistration.Infrastructure
dotnet ef database update --startup-project src/Services/Profile.Service/Profile.GraphQL --project src/Services/Profile.Service/Profile.Infrastructure
dotnet ef database update --startup-project src/Services/PushNotofication.Service/PushNotification.SignalR --project src/Services/PushNotofication.Service/PushNotification.Infrastructure

Start-Process cmd -Argument "/C dotnet run --project src/Services/Auth.Service/Auth.IdentityServer.Oidc.Web"
Start-Process cmd -Argument "/C dotnet run --project src/Services/Event.Service/Event.GraphQL"
Start-Process cmd -Argument "/C dotnet run --project src/Services/EventRegistration.Service/EventRegistration.GraphQL"
Start-Process cmd -Argument "/C dotnet run --project src/Services/Profile.Service/Profile.GraphQL"
Start-Process cmd -Argument "/C dotnet run --project src/Services/PushNotofication.Service/PushNotification.SignalR"
Start-Process cmd -Argument "/C dotnet run --project src/Services/WatchDog.Service/WatchDog.Web"
Start-Process cmd -Argument "/C dotnet run --project src/Web/AdminApp.BlazorWasm"
Start-Process cmd -Argument "/C dotnet run --project src/Web/PublicApp.BlazorWasm"

Start-Process "https://localhost:44343"
Start-Process "https://localhost:44338"
Start-Process "https://localhost:44369/hc-ui"

Read-Host -Prompt "Press Enter to exit"