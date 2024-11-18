namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.Lambda
open Amazon.CDK.AWS.APIGateway
open Constructs
open Cdklabs.DynamoTableViewer

type CdkWorkshopStack(scope: Construct, id: string, props: IStackProps) as this =
    inherit Stack(scope, id, props)

    let hello = Function(this, "HelloHandler", FunctionProps(
        Runtime = Runtime.NODEJS_LATEST,
        Code = Code.FromAsset("lambda"),
        Handler = "hello.handler"
    ))

    let hitCounterProps =
        { new IHitCounterProps with
            member _.Downstream = hello
        }

    let helloWithCounter = HitCounter(this, "HelloHitCounter", hitCounterProps)

    let gateway = LambdaRestApi(this, "Endpoint", LambdaRestApiProps(
        Handler = helloWithCounter.Handler()
    ))

    let tv = TableViewer(this, "ViewHitCounter", TableViewerProps(
        Title = "Hello Hits",
        Table = helloWithCounter.Table
    ))

    do

        ()
