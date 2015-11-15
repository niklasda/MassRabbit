using System;
using System.Windows;
using BusMessages;
using MassTransit;

namespace BusClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            // send a message

            IBusControl busControl = CreateBus();
            busControl.Start();

            try
            {
                IRequestClient<ISimpleRequest, ISimpleResponse> client = CreateRequestClient(busControl);

                ISimpleResponse response = await client.Request(new SimpleRequest("Niklas"));

                Console.WriteLine("Customer Name: {0}", response.CusomerName);
                textBlock.Text = response.CusomerName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception!!! OMG!!! {0}", ex);
            }
            finally
            {
                busControl.Stop();
            }
        }

        private static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));
        }

        private static IRequestClient<ISimpleRequest, ISimpleResponse> CreateRequestClient(IBusControl busControl)
        {
            var serviceAddress = new Uri("rabbitmq://localhost/request_service");
            IRequestClient<ISimpleRequest, ISimpleResponse> client =
                busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));

            return client;
        }
    }
}
