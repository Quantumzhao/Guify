path=./bin/Debug/net6.0/
repo=`pwd`

dotnet build &&

dotnet exec \
--runtimeconfig $path/Guify.runtimeconfig.json \
--depsfile $path/Guify.deps.json \
~/.nuget/packages/avalonia/0.10.15/tools/netcoreapp2.0/designer/Avalonia.Designer.HostApp.dll \
--transport file://$repo/Views/MainWindow.axaml \
--method html --html-url http://127.0.0.1:6001 $path/Guify.dll