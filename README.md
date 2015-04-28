# fsharp-kafka-simple
A simple implementation of a Kafka producer and consumer in F#

Spin up Kafka with the following:

```shell
docker run -d -p 2181:2181 -h zookeeper.lacolhost.com --name zookeeper confluent/zookeeper

docker run -d -p 9092:9092 -h kafka.lacolhost.com --name kafka --link zookeeper:zookeeper confluent/kafka
```

Add the following to your /etc/hosts on the machine hosting the fsharp-kafka-producer & fsharp-kafka-consumer:

```shell
<ip-address-of-kafka-host> kafka.lacolhost.com zookeeper.lacolhost.com
```

Now you can stream data into Kafka (from https://github.com/anaerobic/generaterace.git , for instance) using something like:

```shell
docker run --rm generate-race 5000 3 | docker run -i --rm streaming-fsharp | docker run -i --rm --net host fsharp-kafka-producer results http://kafka.lacolhost.com:9092
```

And you can read it back using:

```shell
docker run -i --rm --net host fsharp-kafka-consumer results1 http://kafka.lacolhost.com:9092
```
