using Common.Application;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.EditInventory;

namespace Shop.Presentation.Facade.Sellers.Inventories;

public interface ISellerInventoryFacade
{
    Task<OperationResult> AddInventory(AddSellerInventoryCommand command);
    Task<OperationResult> EditInventory(EditSellerInventoryCommand command);
}