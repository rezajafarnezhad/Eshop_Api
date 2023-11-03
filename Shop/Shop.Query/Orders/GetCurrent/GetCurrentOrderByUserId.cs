using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;

public class GetCurrentOrderByUserId : IQuery<OrderDto>
{
    public GetCurrentOrderByUserId(long userid)
    {
        Userid = userid;
    }

    public long Userid { get;private set; }
}