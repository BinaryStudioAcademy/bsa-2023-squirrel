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
                new("Employee", DatabaseItemType.Table, "dbo"),
                new("GetEmployeeByID", DatabaseItemType.StoredProcedure, "dbo"),
                new("CalculateSalary", DatabaseItemType.Function, "dbo"),
                new("SalesView", DatabaseItemType.View, "dbo"),
                new("Customer", DatabaseItemType.Table, "dbo"),
                new("InsertOrder", DatabaseItemType.StoredProcedure, "dbo"),
                new("GetTotalRevenue", DatabaseItemType.Function, "dbo"),
                new("ProductView", DatabaseItemType.View, "dbo"),
                new("Order", DatabaseItemType.Table, "dbo"),
                new("DeleteCustomer", DatabaseItemType.StoredProcedure, "dbo"),
                new("GetProductCount", DatabaseItemType.Function, "dbo"),
                new("OrderDetailsView", DatabaseItemType.View, "dbo"),
                new("Supplier", DatabaseItemType.Table, "dbo"),
                new("UpdateOrderStatus", DatabaseItemType.StoredProcedure, "dbo"),
                new("GetSupplierByID", DatabaseItemType.Function, "dbo"),
                new("SupplierProductsView", DatabaseItemType.View, "dbo"),
                new("Category", DatabaseItemType.Table, "dbo"),
                new("CreateCategory", DatabaseItemType.StoredProcedure, "dbo"),
                new("GetCategoryByName", DatabaseItemType.Function, "dbo"),
                new("CategoryProductsView", DatabaseItemType.View, "dbo"),
                // Add more items as needed.
            };

        return mockedItems;
    }
}
