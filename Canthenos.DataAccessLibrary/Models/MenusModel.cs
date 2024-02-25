namespace Canthenos.DataAccessLibrary.Models;

public class MenusModel
{
    public int MenuId { get; set; }
    public string? MenuName { get; set; }
    public int DishId { get; set; }
    public string? DishName { get; set; }
    public string? DishPrice { get; set; }
    public string? DishImagePath { get; set; }
    public int DishType { get; set; }
    public string? DishTypeName { get; set; }
    public int DayId { get; set; }
    public string? DayName { get; set; }
}