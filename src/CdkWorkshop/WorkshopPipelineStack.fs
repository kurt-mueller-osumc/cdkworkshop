namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.CodeCommit
open Constructs

type WorkshopPipelineStack(scope: Construct, id: string, props: StackProps) as this =
    inherit Stack(scope, id, props)

