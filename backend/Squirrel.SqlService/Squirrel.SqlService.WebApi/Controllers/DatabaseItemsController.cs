using Microsoft.AspNetCore.Mvc;
using Squirrel.Shared.DTO.DatabaseItem;
using Squirrel.SqlService.BLL.Interfaces;

namespace Squirrel.SqlService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DatabaseItemsController : ControllerBase
{
    private readonly IDbItemsRetrievalService _dbItemsRetrieval;
    private readonly ISqlFormatterService _sqlFormatter;

    public DatabaseItemsController(IDbItemsRetrievalService dbItemsRetrieval, ISqlFormatterService sqlFormatter)
    {
        _dbItemsRetrieval = dbItemsRetrieval;
        _sqlFormatter = sqlFormatter;
    }

    [HttpGet]
    public ActionResult<List<DatabaseItem>> GetAllItems()
    {
        return Ok(_dbItemsRetrieval.GetAllItems());
    }

    [HttpGet("formatsql")]
    public ActionResult<string> GetFormattedSql()
    {
        string sql = @"USE [WideWorldImporters]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [Website].[InsertCustomerOrders]
@Orders Website.OrderList READONLY,
@OrderLines Website.OrderLineList READONLY,
@OrdersCreatedByPersonID INT,
@SalespersonPersonID INT
WITH EXECUTE AS OWNER
AS
BEGIN
    SeT NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @OrdersToGenerate AS TABLE
    (
        OrderReference INT PRIMARY KEY,  -- reference from the application
        OrderID INT
    );
    -- allocate the new order numbers
    INSERT @OrdersToGenerate (OrderReference, OrderID)
    SELECT OrderReference, NEXT VALUE FOR Sequences.OrderID
    FROM
    @Orders;
    BeGIN TRY
        BEGIN TRAN;
        INSERT Sales.Orders
            (OrderID, CustomerID, SalespersonPersonID, PickedByPersonID, ContactPersonID, BackorderOrderID, OrderDate,
             ExpectedDeliveryDate, CustomerPurchaseOrderNumber, IsUndersupplyBackordered, Comments, DeliveryInstructions, InternalComments,
             PickingCompletedWhen, LastEditedBy, LastEditedWhen)
        SELECT otg.OrderID, o.CustomerID, @SalespersonPersonID, NULL, o.ContactPersonID, NULL, SYSDATETIME(),
               o.ExpectedDeliveryDate, o.CustomerPurchaseOrderNumber, o.IsUndersupplyBackordered, o.Comments, o.DeliveryInstructions, NULL,
               NULL, @OrdersCreatedByPersonID, SYSDATETIME()
        FROM @OrdersToGenerate AS otg
        InNER JOIN @Orders AS o
        ON otg.OrderReference = o.OrderReference;
        INSERT Sales.OrderLines
            (OrderID, StockItemID, [Description], PackageTypeID, Quantity, UnitPrice,
             TaxRate, PickedQuantity, PickingCompletedWhen, LastEditedBy, LastEditedWhen)
        SELECT otg.OrderID, ol.StockItemID, ol.[Description], si.UnitPackageID, ol.Quantity,
               Website.CalculateCustomerPrice(o.CustomerID, ol.StockItemID, SYSDATETIME()),
               si.TaxRate, 0, NULL, @OrdersCreatedByPersonID, SYSDATETIME()
        FROM @OrdersToGenerate AS otg
        INNER JOIN @OrderLines AS ol
        ON otg.OrderReference = ol.OrderReference
        INNER JOIN @Orders AS o
        ON ol.OrderReference = o.OrderReference
        INNER JOIN Warehouse.StockItems AS si
        ON ol.StockItemID = si.StockItemID;
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        PRINT N'Unable to create the customer orders.';
        THROW;
        RETURN -1;
    END CATCH;
    RETURN 0;
END;
GO
";
        return Ok(_sqlFormatter.GetFormattedSql(sql, Core.DAL.Enums.DbEngine.MsSqlServer));
    }
}
