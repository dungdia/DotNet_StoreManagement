using System;
using System.Collections.Generic;

namespace DotNet_StoreManagement.Domain.entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
}
