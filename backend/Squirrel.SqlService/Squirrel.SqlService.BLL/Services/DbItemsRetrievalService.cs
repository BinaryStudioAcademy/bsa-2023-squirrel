using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public sealed class DbItemsRetrievalService : IDbItemsRetrievalService
{
    public ICollection<DatabaseItem> GetAllItems()
    {
        var mockedItems = new List<DatabaseItem>
            {
                new("Employee", DatabaseItemType.Table),
                new("GetEmployeeByID", DatabaseItemType.StoredProcedure),
                new("CalculateSalary", DatabaseItemType.Function),
                new("SalesView", DatabaseItemType.View),
                new("Customer", DatabaseItemType.Table),
                new("InsertOrder", DatabaseItemType.StoredProcedure),
                new("GetTotalRevenue", DatabaseItemType.Function),
                new("ProductView", DatabaseItemType.View),
                new("Order", DatabaseItemType.Table),
                new("DeleteCustomer", DatabaseItemType.StoredProcedure),
                new("GetProductCount", DatabaseItemType.Function),
                new("OrderDetailsView", DatabaseItemType.View),
                new("Supplier", DatabaseItemType.Table),
                new("UpdateOrderStatus", DatabaseItemType.StoredProcedure),
                new("GetSupplierByID", DatabaseItemType.Function),
                new("SupplierProductsView", DatabaseItemType.View),
                new("Category", DatabaseItemType.Table),
                new("CreateCategory", DatabaseItemType.StoredProcedure),
                new("GetCategoryByName", DatabaseItemType.Function),
                new("CategoryProductsView", DatabaseItemType.View)
                // Add more items as needed.
            };

        return mockedItems;
    }
}
