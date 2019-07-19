dotnet publish -c Release -r linux-arm

tar -vczf compressed\publictransport-release.tar.gz -C BMS.Web.PublicTransport\bin\Release\netcoreapp2.1\linux-arm\publish .

@echo off
echo Lets do: scp compressed\publictransport-release.tar.gz pi@192.168.0.137:PublicTransport

