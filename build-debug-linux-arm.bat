dotnet publish -c Debug -r linux-arm

tar -vczf compressed\publictransport-debug.tar.gz -C BMS.Web.PublicTransport\bin\Debug\netcoreapp2.1\linux-arm\publish .

@echo off
echo Lets do: scp compressed\publictransport-debug.tar.gz pi@192.168.0.137:PublicTransport
