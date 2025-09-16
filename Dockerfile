# ��� 1: ����� ���������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# ����� ���� �-csproj ������ �������
COPY *.csproj ./
RUN dotnet restore

# ����� ��� ���� �������, ����� ������
COPY . ./
RUN dotnet publish -c Release -o out

# ��� 2: ����� Image ����
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "travelAgent.dll"]