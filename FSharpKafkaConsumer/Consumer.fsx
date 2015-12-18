#r "packages/kafka-net/lib/net45/kafka-net.dll"

open KafkaNet
open KafkaNet.Model
open KafkaNet.Protocol
open System
open System.Text

match fsi.CommandLineArgs with
| [| scriptName; topic; url |] ->
  let router = new BrokerRouter(new KafkaOptions(new Uri(url)))
  let consumer = new KafkaNet.Consumer(new ConsumerOptions(topic, router))
  consumer.SetOffsetPosition(new OffsetPosition(0, 0L))
  for message in consumer.Consume() do
      message.Value
      |> System.Text.Encoding.UTF8.GetString
      |> printfn "%s"
| _ -> failwithf "usage: %s topic routerUrl" __SOURCE_FILE__
