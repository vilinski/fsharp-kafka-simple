# fsharp-kafka-simple
A simple implementation of a Kafka producer and consumer in F#

execute the following to build it in Docker:

```shell
git clone https://github.com/anaerobic/fsharp-kafka-simple.git
cd fsharp-kafka-simple
sudo sh docker-build.sh
```

Spin up Kafka with the following:

```shell
docker run -d -p 2181:2181 -h zookeeper.localhost.com --name zookeeper confluent/zookeeper

docker run -d -p 9092:9092 -h kafka.localhost.com --name kafka --link zookeeper:zookeeper confluent/kafka
```

Add the following to your /etc/hosts on the machine hosting the fsharp-kafka-producer & fsharp-kafka-consumer:

```shell
<ip-address-of-kafka-host> kafka.localhost.com zookeeper.localhost.com
```

Now you can stream data into Kafka (from https://github.com/anaerobic/generaterace.git , for instance) using something like:

```shell
docker run --rm -i generate-race 5000 3 | docker run -i --rm --net host fsharp-kafka-producer reads http://kafka.localhost.com:9092
```

We can stream that data out of Kafka and into another microservice, then stream the results back into Kafka like:

```shell
docker run --rm --net host fsharp-kafka-consumer reads http://kafka.localhost.com:9092 | docker run -i --rm streaming-fsharp | docker run -i --rm --net host fsharp-kafka-producer results http://kafka.localhost.com:9092
```

And you can read it back using:

```shell
docker run -i --rm --net host fsharp-kafka-consumer results http://kafka.localhost.com:9092
```
