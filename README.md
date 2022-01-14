# observabilityEasyDebug
The application getting pointed PDB files and search matching dlls in destination directory, if find matches trying to copy *.PDB to the dll's location.

The first parameter is a location of *.PDB files
The second parameter is location of root OrionDirectory on loacl or remote machine (if remote write permisions needed)
Example arguments:
C:\git\orion\src\web\src\SolarWinds.IPAM\SolarWinds.IPAM.Web.Common\bin\Debug\net48 \\\REMOTEMACHINE\Orion
