using System.Linq;

namespace SportStoreAutoFac.Data {

    public interface IOrderRepository {

        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
