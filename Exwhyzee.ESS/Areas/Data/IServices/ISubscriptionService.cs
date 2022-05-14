using Exwhyzee.ESS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exwhyzee.ESS.Areas.Data.IServices
{
    public interface ISubscriptionService
    {
        Task Create(Subscription item);
        //Task DepositCreate(Subscription item);
        Task OnlinePay(Subscription item, int? id);

        Task BankDeposit(Subscription item, int? id);
        Task<Subscription> Get(int? id);

        Task Delete(int? id);
        Task<List<Subscription>> AllSubscription();

        Task Edit(Subscription item);
        //Task ApproveOnline(Subscription item);
        //Task ApproveDeposit(Subscription item);

        Task<List<Subscription>> GetUserSubscription();
    }
}
