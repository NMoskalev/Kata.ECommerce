# Kata.ECommerce

#### Prerequisites:

- .NET Core 3.1

#### Requirements:

| Product Code | Unit Price |Special Offer |
|--------------|------------|--------------|
|   Apples     |   £0.50	|              | 
|   Bananas	   |   £0.70    |2 for £1.00   |
|   Oranges	   |   £0.45    |3 for £0.90   |

The checkout accepts the items in any order, so that if we scan a banana, an apple and then another banana

example, “Bananas are £0.70 each or 2 for £1.00”.


#### Implementation:

Discount are stored in `DiscountInMemoryRepository.cs`.

Example of usages can be found in `Program.cs` file.

Discount calculation logic is in `BaseItemsDiscountType.cs`.


#### TBD:

 - unit-tests
