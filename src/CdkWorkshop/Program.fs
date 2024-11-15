open Amazon.CDK
open CdkWorkshop

[<EntryPoint>]
let main _ =
    let app = App(null)

    CdkWorkshopStack(app, "CdkWorkshopStack", StackProps()) |> ignore

    app.Synth() |> ignore
    0
