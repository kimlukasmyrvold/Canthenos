namespace Canthenos.DataAccessLibrary.Models;

public class SnacksModel
{
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public string? ProductTypeName { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public string? Price { get; set; }
    public string? AvailableFromTime { get; set; }
    public string? AvailableToTime { get; set; }
    public string? AvailableFromDate { get; set; }
    public string? AvailableToDate { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}