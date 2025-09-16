# שלב 1: בניית האפליקציה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# העתקת קובץ ה-csproj והורדת התלויות
COPY *.csproj ./
RUN dotnet restore

# העתקת שאר קבצי הפרויקט, בנייה ופרסום
COPY . ./
RUN dotnet publish -c Release -o out

# שלב 2: יצירת Image סופי
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "travelAgent.dll"]