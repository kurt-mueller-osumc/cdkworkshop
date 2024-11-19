namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.Pipelines
open Constructs

type WorkshopPipelineStack(scope: Construct, id: string, props: StackProps) as this =
    inherit Stack(scope, id, props)

    let connSourceOptions = ConnectionSourceOptions(
        ConnectionArn = "arn:aws:codeconnections:us-east-1:452283499101:connection/95189adc-e57b-4ccf-ac4e-004e473735d2"
    )
    let source = CodePipelineSource.Connection(repoString = "kurt-mueller-osumc/cdkworkshop", branch = "main", props = connSourceOptions)

    let pipeline = CodePipeline(this, "Pipeline", CodePipelineProps(
        PipelineName = "WorkshopPipeline",
        Synth = CodeBuildStep( "SynthStep",
            CodeBuildStepProps(
                Input = source,
                // Commands = [| "npm ci"; "npm run build"; "npx cdk synth" |]
                Commands = [| "npx cdk synth" |]
            )
        )
    ))

    do
        ()