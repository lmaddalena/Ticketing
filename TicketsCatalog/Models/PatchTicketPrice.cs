using System.ComponentModel.DataAnnotations;

namespace TicketsCatalog.Models;

public class PatchTicketPrice
{
    [Required]
    [MaxLength(5)]
    public string TicketId { get; set; }
    [Required]
    [Range(0,20)]
    public double NewPrice { get; set; }


    public PatchTicketPrice(string ticketId, double newPrice)
    {
        this.TicketId = ticketId;
        this.NewPrice = newPrice;
    }
}