using UnityEngine;
using UnityEditor;
using System.Xml;
using Microsoft.Build.Evaluation;
using FSharpUnity;
using System.Collections.Generic;

public class FSharpBuild : EditorWindow
{
    private static string csharpAssemblyLocation;
    private static string csharpProjectLocation;
    private static string fsharpProjectLocation;
    private static string fsharpBuildLocation;

    [SerializeField]
    private static bool buildFSharp = false;

    [MenuItem("FSharp/Build Settings")]
    public static void Init()
    {
        FSharpBuild window = (FSharpBuild)EditorWindow.GetWindow(typeof(FSharpBuild));
        window.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        buildFSharp = GUILayout.Toggle(buildFSharp, "Enable F# build");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Assembly-CSharp.dll: " + csharpAssemblyLocation);
        if (GUILayout.Button("Change Location"))
        {
            csharpAssemblyLocation = EditorUtility.OpenFilePanel("Assembly-CSharp.dll", "", "dll");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Assembly-CSharp.proj: " + csharpProjectLocation);
        if (GUILayout.Button("Change Location"))
        {
            csharpProjectLocation = EditorUtility.OpenFilePanel("Assembly-CSharp.csproj", "", "csproj");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("fsproj Location: " + fsharpProjectLocation);
        if (GUILayout.Button("Change Location"))
        {
            fsharpProjectLocation = EditorUtility.OpenFilePanel("fsproj", "", "fsproj");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Build Destination: " + fsharpBuildLocation);
        if (GUILayout.Button("Change Location"))
        {
            fsharpBuildLocation = EditorUtility.OpenFolderPanel("Build Destination", "", "");
        }
        EditorGUILayout.EndHorizontal();
        if(GUILayout.Button("Build"))
        {
            FSharpUnity.FSharpUnity.updateProject(csharpProjectLocation, fsharpProjectLocation, fsharpBuildLocation, csharpAssemblyLocation);
        }
        EditorGUILayout.EndVertical();
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        if (buildFSharp)
        {
            FSharpUnity.FSharpUnity.updateProject(csharpProjectLocation, fsharpProjectLocation, fsharpBuildLocation, csharpAssemblyLocation);
        }
    }

    //private static void UpdateProject(string csharpProjectLocation, string fsharpProjectLocation, string fsharpBuildLocation, string csharpAssemblyLocation)
    //{
    //    Project csharpProject = new Project(XmlReader.Create(csharpProjectLocation));
    //    Project fsharpProject = new Project(XmlReader.Create(fsharpProjectLocation));

    //    foreach(var x in csharpProject.GetItems("Compile"))
    //    {
    //        x.ItemType = "None";
    //    }

    //    foreach(var x in csharpProject.GetItems("Reference"))
    //    {
    //        x.SetMetadataValue("Private", "False");
    //    }

    //    foreach(var x in csharpProject.GetItems("Compile"))
    //    {
    //        fsharpProject.AddItem(x.ItemType, x.UnevaluatedInclude, (IEnumerable<KeyValuePair<string, string>>)x.Metadata);
    //    }

    //    foreach(var x in csharpProject.GetItems("Reference"))
    //    {
    //        fsharpProject.AddItem(x.ItemType, x.UnevaluatedInclude, (IEnumerable<KeyValuePair<string, string>>)x.Metadata);
    //    }

    //    fsharpProject.SetProperty("OutputPath", fsharpBuildLocation);
    //    fsharpProject.AddItem("Reference", "Assembly-CSharp", new Dictionary<string, string> { { "HintPath", csharpAssemblyLocation }, { "Private", "False" } });
    //    fsharpProject.Save(fsharpProjectLocation);
    //}

}
