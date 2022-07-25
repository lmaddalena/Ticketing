using System.ComponentModel.DataAnnotations;

namespace TicketsCatalog.Models;

public class Ticket
{
    [Key]
    [Required]
    [MaxLength(5)]
    public string TicketId { get; set; }
    [Required]
    [Range(0,20)]
    public double Price { get; set; }
    [Required]
    public DateTime ValidFrom { get; set; }

    public Ticket(string ticketId, double price, DateTime validFrom)
    {
        this.TicketId = ticketId;
        this.Price = price;
        this.ValidFrom = validFrom;
    }
}
