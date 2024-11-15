namespace CdkWorkshop

open Amazon.CDK
open Amazon.CDK.AWS.SNS
open Amazon.CDK.AWS.SNS.Subscriptions
open Amazon.CDK.AWS.SQS

type CdkWorkshopStack(scope, id, props) as this =
    inherit Stack(scope, id, props)

    let queue = Queue(this, "CdkWorkshopQueue", QueueProps(VisibilityTimeout = Duration.Seconds(300.)))

    let topic = Topic(this, "CdkWorkshopTopic")
    do topic.AddSubscription(SqsSubscription(queue)) |> ignore
