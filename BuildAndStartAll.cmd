dotnet restore "MDC.ApiGateway/MDC.ApiGateway.csproj"
dotnet restore "AuthService/MDC.AuthService.csproj"
dotnet restore "MDC.ValidationService/MDC.ValidationService.csproj"
dotnet restore "MDC.ContributionsService/MDC.ContributionsService.csproj"
dotnet restore "MDC.WebClient/MDC.WebClient.csproj"

dotnet build "MDC.ApiGateway/MDC.ApiGateway.csproj"
dotnet build "AuthService/MDC.AuthService.csproj"
dotnet build "MDC.ValidationService/MDC.ValidationService.csproj"
dotnet build "MDC.ContributionsService/MDC.ContributionsService.csproj"
dotnet build "MDC.WebClient/MDC.WebClient.csproj"

start /d "MDC.ApiGateway"       dotnet watch run MDC.ApiGateway.csproj
start /d "AuthService"       dotnet watch run MDC.AuthService.csproj
start /d "MDC.ValidationService"       dotnet watch run MDC.ValidationService.csproj
start /d "MDC.ContributionsService"       dotnet watch run MDC.ContributionsService.csproj
start /d "MDC.WebClient"       dotnet watch run MDC.WebClient.csproj