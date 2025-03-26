ECD XML Test – Documentation & Instructions
===========================================


🖥️ System Requirements
-----------------------
- .NET SDK 8.0 or later installed
  Download: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

- Windows OS
- Visual Studio OR Visual Studio Code with C# extension


📁 Setup Instructions
---------------------
1. Create XML folder:
   Go to your system’s Documents folder:
     C:\Users\<your_username>\Documents
   Create a new folder named:
     xml_repo

2. Add XML files:
   Copy your ECD .xml files into that xml_repo folder.

🚀 How to Run the Program
--------------------------
Option A: Using Command Line (dotnet run)
-----------------------------------------
1. Open PowerShell or Command Prompt.
2. Navigate to the project directory:
     cd path\to\TEC_TEST\ECD_Handler
3. Run the program:
     dotnet run

Option B: Using Visual Studio
-----------------------------
1. Open tec_test.sln in Visual Studio.
2. Right-click ECD_Handler > Set as Startup Project.
3. Press Ctrl + F5 to run without debugging.

✅ Output
---------
- Console displays the total monto_total in each XML file under liquidacion num_liq="0".
- A results.txt file is generated in the same folder, containing lines like:
     File: ECD_20240325.xml - Total monto_total: $12,345.67

⚙️ Configuration
----------------
By default, the program reads XML files from:
  C:\Users\<your_username>\Documents\xml_repo
