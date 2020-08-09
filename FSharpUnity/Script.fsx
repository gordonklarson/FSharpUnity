
#I @"D:\work\FSharpUnity\packages\Microsoft.Build\lib\net472\"
#I @"D:\work\FSharpUnity\packages\Microsoft.Build.Framework\lib\net472\"
#I @"D:\work\FSharpUnity\packages\Microsoft.Build.Engine\lib\net472\"
#r "Microsoft.Build.Engine.dll"
#r "Microsoft.Build.Framework.dll"
#r "Microsoft.Build.dll"
open Microsoft.Build.Evaluation 
open System.Collections.Generic
open System.IO

let csharpProjLoc =  @"D:\work\personal\Spectacle/Press Your Luck\Assembly-CSharp.csproj"
let fsharpProjLoc = @"D:\work\personal\Spectacle\Press Your Luck\Source\PressYourLuck\PressYourLuck.fsproj"
let assemblyLoc = @"D:\work\personal\Spectacle\Press Your Luck\Library\ScriptAssemblies\Assembly-CSharp.dll"
let buildLoc = @"D:\work\personal\Spectacle\Press Your Luck\Assets\FSharpScripts\"

let updateProject (csharpProjectLocation : string) (fsharpProjectLocation : string) fsharpBuildLocation csharpAssemblyLocation =
            let projectCollection = new ProjectCollection()
            let csharpProj = Project(csharpProjectLocation, null, null, projectCollection)
            let fsharpProj = Project(fsharpProjectLocation, null, null, projectCollection)

            
            csharpProj.GetItems "Reference"
            |>Seq.toList
            |>List.iter(fun (x : ProjectItem) -> x.SetMetadataValue("Private", "False")|>ignore)

            csharpProj.GetItems "Compile"
            |>Seq.toArray
            |>Array.iter(fun x -> 
                if fsharpProj.GetItems "None"
                   |>Seq.exists(fun i -> i.UnevaluatedInclude = x.UnevaluatedInclude)
                   |>not
                then
                    fsharpProj.AddItem("None", x.UnevaluatedInclude)
                    |>ignore)

            csharpProj.GetItems "Reference"
            |>Seq.toArray
            |>Array.iter(fun x -> 
                match fsharpProj.GetItems "Reference"
                      |>Seq.tryFind(fun r -> r.UnevaluatedInclude = x.UnevaluatedInclude) with
                |Some r when r.GetMetadataValue("HintPath") = x.GetMetadataValue("HintPath") ->
                    ()
                |Some r ->
                    fsharpProj.RemoveItem r                
                    |>ignore
                    let metaData =
                        x.Metadata
                        |>Seq.map(fun m -> new KeyValuePair<string, string>(m.Name, m.UnevaluatedValue))
                    fsharpProj.AddItem(x.ItemType, x.UnevaluatedInclude, metaData)
                    |>ignore
                |None ->           
                    let metaData =
                        x.Metadata
                        |>Seq.map(fun m -> new KeyValuePair<string, string>(m.Name, m.UnevaluatedValue))
                    fsharpProj.AddItem(x.ItemType, x.UnevaluatedInclude, metaData)
                    |>ignore
                )

            fsharpProj.SetProperty("OutputPath", fsharpBuildLocation)
            |>ignore

            match fsharpProj.GetItems "Reference"
                  |>Seq.tryFind(fun r -> r.UnevaluatedInclude = "Assembly-CSharp") with
            |Some r when r.GetMetadataValue("HintPath") = csharpAssemblyLocation ->
                ()
            |Some r ->
                fsharpProj.RemoveItem r                
                |>ignore
                fsharpProj.AddItem("Reference", "Assembly-CSharp", [new KeyValuePair<string, string>("HintPath", csharpAssemblyLocation); new KeyValuePair<string, string>("Private", "False")])
                |>ignore
            |None ->           
                fsharpProj.AddItem("Reference", "Assembly-CSharp", [new KeyValuePair<string, string>("HintPath", csharpAssemblyLocation); new KeyValuePair<string, string>("Private", "False")])
                |>ignore

            fsharpProj.Save()
            // match fsharpProj.IsDirty with
            // |true -> 
            //     fsharpProj.Build()
            //     |>ignore
            // |false ->
            //     ()
            projectCollection.UnloadAllProjects()
            projectCollection.Dispose()


updateProject csharpProjLoc fsharpProjLoc buildLoc assemblyLoc

