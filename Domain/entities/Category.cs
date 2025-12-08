using System;
using System.Collections.Generic;

namespace DotNet_StoreManagement.Domain.entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;
}
