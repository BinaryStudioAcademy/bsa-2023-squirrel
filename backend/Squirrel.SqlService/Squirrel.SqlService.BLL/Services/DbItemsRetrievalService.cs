using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.Shared.Enums;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.BLL.Services;

public class DbItemsRetrievalService : IDbItemsRetrievalService
{
    public ICollection<DatabaseItem> GetAllItems()
    {
        var mockedItems = new List<DatabaseItem>
            {
                new DatabaseItem("Employee", DatabaseItemType.Table),
                new DatabaseItem("GetEmployeeByID", DatabaseItemType.StoredProcedure),
                new DatabaseItem("CalculateSalary", DatabaseItemType.Function),
                new DatabaseItem("SalesView", DatabaseItemType.View),
                new DatabaseItem("Customer", DatabaseItemType.Table),
                new DatabaseItem("InsertOrder", DatabaseItemType.StoredProcedure),
                new DatabaseItem("GetTotalRevenue", DatabaseItemType.Function),
                new DatabaseItem("ProductView", DatabaseItemType.View),
                new DatabaseItem("Order", DatabaseItemType.Table),
                new DatabaseItem("DeleteCustomer", DatabaseItemType.StoredProcedure),
                new DatabaseItem("GetProductCount", DatabaseItemType.Function),
                new DatabaseItem("OrderDetailsView", DatabaseItemType.View),
                new DatabaseItem("Supplier", DatabaseItemType.Table),
                new DatabaseItem("UpdateOrderStatus", DatabaseItemType.StoredProcedure),
                new DatabaseItem("GetSupplierByID", DatabaseItemType.Function),
                new DatabaseItem("SupplierProductsView", DatabaseItemType.View),
                new DatabaseItem("Category", DatabaseItemType.Table),
                new DatabaseItem("CreateCategory", DatabaseItemType.StoredProcedure),
                new DatabaseItem("GetCategoryByName", DatabaseItemType.Function),
                new DatabaseItem("CategoryProductsView", DatabaseItemType.View)
                // Add more items as needed.
            };

        return mockedItems;
    }
}
