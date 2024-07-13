
# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 80

# Stage 2: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExcursionBooking/ExcursionBooking.csproj", "ExcursionBooking/"]
RUN dotnet restore "./ExcursionBooking/ExcursionBooking.csproj"
COPY ./ExcursionBooking ./ExcursionBooking
WORKDIR "/src/ExcursionBooking"
RUN dotnet build "./ExcursionBooking.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExcursionBooking.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final Stage with Runtime Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcursionBooking.dll"]