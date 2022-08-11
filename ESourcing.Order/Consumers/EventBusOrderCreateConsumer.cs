using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands.OrderCreate;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Order.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection persistentConnection, IMediator mediator, IMapper mapper)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Consume()
        {
            //rabbitMQ ile bağlantılı olup olmadığını kontrol ediyor.
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();//connect olmaya zorla
            }

            //Q yu kontrol etme oluşturuluyor
            //Q nun ismi OrderCreateQueue den çekiliyor
            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.OrderCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //EventingBasicConsumer=rabbitmq ile gelen kütüphane. Nesneyi oluşturmayı sağlıyor.Q yu yönetmeyi sağlıyor
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;//gelen eventi işleyeceği bir custom event ayarlıyoruz

            channel.BasicConsume(queue: EventBusConstants.OrderCreateQueue, autoAck: true, consumer: consumer);//consume etmeye başlıyor
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

            if(e.RoutingKey == EventBusConstants.OrderCreateQueue)
            {
                var command = _mapper.Map<OrderCreateCommand>(@event);
                //içinde ekstra değişikliklik yapılması gereken şeyler değiştiriliyor
                command.CreatedAt = DateTime.Now;
                command.TotalPrice = @event.Quantity * @event.Price;
                command.UnitPrice = @event.Price;

                var result = await _mediator.Send(command);//gelen mesajı mediatr üzerinden veritabanına insert yapma
            }
        }

        public void Disconnect()
        {
            _persistentConnection.Dispose();//connection kapatma işlemi
        }
    }
}
