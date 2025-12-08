using System;
using System.Collections.Generic;

namespace DotNet_StoreManagement.Domain.entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
