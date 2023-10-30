

using Common.Query;
using Dapper;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByUserId;

public record GetSellerByUserId(long UserId) : IQuery<SellerDto>;