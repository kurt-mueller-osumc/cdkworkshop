namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.DynamoDB
open Amazon.CDK.AWS.Lambda
open Constructs

type IHitCounterProps =
    abstract Downstream: IFunction

type HitCounter(scope: Construct, id: string, props: IHitCounterProps) as this =
    inherit Construct(scope, id)

    let table = Table(this, "Hits", TableProps(
        PartitionKey = Attribute(
            Name = "path",
            Type = AttributeType.STRING
        )
    ))

    let handler = Function(this, "HitCounterHandler", FunctionProps(
        Runtime = Runtime.NODEJS_LATEST,
        Handler = "hitcounter.handler",
        Code = Code.FromAsset("lambda"),
        Environment = dict [
            "DOWNSTREAM_FUNCTION_NAME", props.Downstream.FunctionName
            "HITS_TABLE_NAME", table.TableName
        ]
    ))

    do
        table.GrantReadWriteData(handler) |> ignore

        ()

    member _.Handler() = handler