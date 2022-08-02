# LacaApp-WPF
Laca app is a helper app to manage and control data of ingredients and recipe for Coffee Shop Laca in Vietnam.
Released App is in the project folder ```./bin/Release/LacaApp.exe```.

# Note
If opening with Visual Studio gives error ```Unable to find signing certificate in the certificate store```:
- Open project folder, open file ```LacaApp.csproj``` with text editor.
- Find and delete line ```<SignManifests>true</SignManifests>```.