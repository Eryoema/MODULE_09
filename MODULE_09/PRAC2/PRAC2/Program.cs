using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAC2
{
    using System;

    public interface IInternalDeliveryService
    {
        void DeliverOrder(string orderId);
        string GetDeliveryStatus(string orderId);
        decimal CalculateDeliveryCost(string orderId);
    }

    public class InternalDeliveryService : IInternalDeliveryService
    {
        public void DeliverOrder(string orderId)
        {
            Console.WriteLine($"Внутренняя доставка: Заказ {orderId} отправлен.");
        }

        public string GetDeliveryStatus(string orderId)
        {
            return $"Статус заказа {orderId}: Доставляется";
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return 10.0m;
        }
    }

    public class ExternalLogisticsServiceA
    {
        public void ShipItem(int itemId)
        {
            Console.WriteLine($"ExternalLogisticsServiceA: Отправка предмета {itemId}.");
        }

        public string TrackShipment(int shipmentId)
        {
            return $"ExternalLogisticsServiceA: Статус отправки {shipmentId} - в пути.";
        }

        public decimal GetShippingCost(int itemId)
        {
            return 15.0m;
        }
    }

    public class ExternalLogisticsServiceB
    {
        public void SendPackage(string packageInfo)
        {
            Console.WriteLine($"ExternalLogisticsServiceB: Отправка посылки {packageInfo}.");
        }

        public string CheckPackageStatus(string trackingCode)
        {
            return $"ExternalLogisticsServiceB: Статус посылки {trackingCode} - доставляется.";
        }

        public decimal EstimateShippingCost(string packageInfo)
        {
            return 12.5m;
        }
    }

    public class ExternalLogisticsServiceC
    {
        public void DispatchItem(string itemDetails)
        {
            Console.WriteLine($"ExternalLogisticsServiceC: Отправка предмета {itemDetails}.");
        }

        public string RetrieveItemStatus(string itemCode)
        {
            return $"ExternalLogisticsServiceC: Статус предмета {itemCode} - на складе.";
        }

        public decimal CalculateDispatchCost(string itemDetails)
        {
            return 18.0m;
        }
    }

    public class LogisticsAdapterA : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceA _serviceA;

        public LogisticsAdapterA(ExternalLogisticsServiceA serviceA)
        {
            _serviceA = serviceA;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                int itemId = int.Parse(orderId);
                _serviceA.ShipItem(itemId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при доставке заказа {orderId}: {ex.Message}");
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                int shipmentId = int.Parse(orderId);
                return _serviceA.TrackShipment(shipmentId);
            }
            catch (Exception ex)
            {
                return $"Ошибка при отслеживании заказа {orderId}: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            int itemId = int.Parse(orderId);
            return _serviceA.GetShippingCost(itemId);
        }
    }
    
    public class LogisticsAdapterB : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceB _serviceB;

        public LogisticsAdapterB(ExternalLogisticsServiceB serviceB)
        {
            _serviceB = serviceB;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                _serviceB.SendPackage(orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при доставке заказа {orderId}: {ex.Message}");
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                return _serviceB.CheckPackageStatus(orderId);
            }
            catch (Exception ex)
            {
                return $"Ошибка при отслеживании заказа {orderId}: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return _serviceB.EstimateShippingCost(orderId);
        }
    }

    public class LogisticsAdapterC : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceC _serviceC;

        public LogisticsAdapterC(ExternalLogisticsServiceC serviceC)
        {
            _serviceC = serviceC;
        }

        public void DeliverOrder(string orderId)
        {
            try
            {
                _serviceC.DispatchItem(orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при доставке заказа {orderId}: {ex.Message}");
            }
        }

        public string GetDeliveryStatus(string orderId)
        {
            try
            {
                return _serviceC.RetrieveItemStatus(orderId);
            }
            catch (Exception ex)
            {
                return $"Ошибка при отслеживании заказа {orderId}: {ex.Message}";
            }
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return _serviceC.CalculateDispatchCost(orderId);
        }
    }

    public enum DeliveryServiceType
    {
        Internal,
        ExternalA,
        ExternalB,
        ExternalC
    }

    public class DeliveryServiceFactory
    {
        public static IInternalDeliveryService CreateDeliveryService(DeliveryServiceType serviceType)
        {
            switch (serviceType)
            {
                case DeliveryServiceType.Internal:
                    return new InternalDeliveryService();
                case DeliveryServiceType.ExternalA:
                    return new LogisticsAdapterA(new ExternalLogisticsServiceA());
                case DeliveryServiceType.ExternalB:
                    return new LogisticsAdapterB(new ExternalLogisticsServiceB());
                case DeliveryServiceType.ExternalC:
                    return new LogisticsAdapterC(new ExternalLogisticsServiceC());
                default:
                    throw new ArgumentException("Неподдерживаемый тип службы доставки");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            IInternalDeliveryService deliveryService = DeliveryServiceFactory.CreateDeliveryService(DeliveryServiceType.ExternalA);

            string orderId = "12345";
            deliveryService.DeliverOrder(orderId);

            string status = deliveryService.GetDeliveryStatus(orderId);
            Console.WriteLine(status);

            decimal cost = deliveryService.CalculateDeliveryCost(orderId);
            Console.WriteLine($"Стоимость доставки: {cost}");
        }
    }
}
