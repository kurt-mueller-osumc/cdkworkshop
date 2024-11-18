namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.Lambda
open Amazon.CDK.AWS.APIGateway
open Constructs

type CdkWorkshopStack(scope: Construct, id: string, props: IStackProps) as this =
    inherit Stack(scope, id, props)

    let hello = Function(this, "HelloHandler", FunctionProps(
        Runtime = Runtime.NODEJS_LATEST,
        Code = Code.FromAsset("lambda"),
        Handler = "hello.handler"
    ))

    let gateway = LambdaRestApi(this, "Endpoint", LambdaRestApiProps(
        Handler = hello
    ))

    let hitCounterProps =
        { new IHitCounterProps with
            member _.Downstream = hello
        }

    let helloWithCounter = HitCounter(this, "HelloHitCounter", hitCounterProps)

    do

        ()
