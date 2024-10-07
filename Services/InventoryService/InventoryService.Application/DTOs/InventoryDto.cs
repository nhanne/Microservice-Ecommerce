﻿namespace InventoryService.Application.DTOs;

public class InventoryDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
}