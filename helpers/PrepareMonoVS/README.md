Mono targeting for Visual Studio
============

Tools for setting up Mono target framework profile for Visual Studio 2013. 

* Prequisites: Visual Studio 2013 (.NET Framework 4.5) and Mono 3.2.3.
* If you installed Mono to other than default location, edit the batch file PrepareMono.bat accordingly.
  * 32-bit Windows: edit MONOLOCATION32 (default: C:\Program Files\Mono-3.2.3 )
  * 64-bit Windows: edit MONOLOCATION64 (default: C:\Program Files (x86)\Mono-3.2.3 )
* Run PrepareMono.bat **as administrator**.
* Optional: Run Visual Studio, and change Target framework (Project properties - Application) of C#-project to "Mono 3.2.3 Profile".