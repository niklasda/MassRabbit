using System;
using System.Diagnostics;
using BusMessages.Interfaces;
using BusMessages.Requests;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MassTransit;

namespace BusClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);
            PutMessageCommand = new RelayCommand(PutMessage);
        }

        private int _i = 0;
        private string _textResult;
        public string TextResult
        {
            get { return _textResult; }
            set
            {
                if (_textResult != value)
                {
                    _textResult = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand SendMessageCommand { get; set; }
        public RelayCommand PutMessageCommand { get; set; }

        private async void PutMessage()
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));

            busControl.Start();

            try
            {
                await busControl.Publish(new MessageRequest("m_" + _i++));

                Debug.WriteLine("Published: ");
                TextResult = "Published";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception {0}", ex);
            }
            finally
            {
                Debug.WriteLine("Stopping it");
                busControl.Stop();
            }
        }

        private async void SendMessage()
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));

            busControl.Start();

            try
            {
                var serviceAddress = new Uri("rabbitmq://localhost/request_service");
                var simpleClient = busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
                var complexClient = busControl.CreateRequestClient<IComplexRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));

                ISimpleResponse simpleResponse = await simpleClient.Request(new SimpleRequest("sr_" + _i++));
                ISimpleResponse complexResponse = await complexClient.Request(new ComplexRequest("qr_" + _i++));

                Debug.WriteLine("Customer Name1: {0}", simpleResponse.CustomerName);
                Debug.WriteLine("Customer Name2: {0}", complexResponse.CustomerName);
                TextResult = simpleResponse.CustomerName + " - " + complexResponse.CustomerName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception {0}", ex);
            }
            finally
            {
                Debug.WriteLine("Stopping it");
                busControl.Stop();
            }
        }
    }
}