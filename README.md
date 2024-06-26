Features
Automatically updates F# projects with all necessary references for integration into a Unity project.
Supports seamless integration of F# scripts and libraries into Unity.
Provides a GUI for easy configuration and building within the Unity Editor.
Getting Started
Prerequisites
Unity Editor installed on your machine.
F# development environment set up.
.NET Framework (as targeted by your Unity project).
Installation
Clone the FSharpUnity repository to your local machine.
Build the FSharpUnity solution in the FSharpUnity subdirectory.
Usage
Create a new F# Class Library project for your Unity project.
Launch the FSharpUnity executable.
In the "fsproj Location" field, point to the .fsproj file created in the first step.
In the "Project Location" field, input the location of your Unity project.
Click the Update button. The .fsproj file will now be updated with the proper references, and a DLL will be built in the Assets\Plugins\FSharp folder.
To use the compiled DLL, navigate to it in the Unity editor in the Assets\Plugins\FSharp directory. There you will see the individual scripts compiled to the DLL as sub-objects of the DLL.
Use the compiled scripts as you would any other script file in Unity.
