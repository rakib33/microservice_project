using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using EF.Core.Repository.Interface.Repository;
namespace Ordering.Application.Contacts.Persistence
{
    public interface IOrderRepository : ICommonRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
