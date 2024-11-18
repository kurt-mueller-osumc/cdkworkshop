namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.Lambda
open Amazon.CDK.AWS.SNS
open Amazon.CDK.AWS.SNS.Subscriptions
open Amazon.CDK.AWS.SQS
open Constructs

type CdkWorkshopStack(scope: Construct, id: string, props: IStackProps) as this =
    inherit Stack(scope, id, props)

    let hello = Function(this, "HelloHandler", FunctionProps(
        Runtime = Runtime.NODEJS_LATEST,
        Code = Code.FromAsset("lambda"),
        Handler = "hello.handler"
    ))

    do
        // subscribe the queue to receive any messages published to the topic
        // topic.AddSubscription(subscription) |> ignore

        ()
