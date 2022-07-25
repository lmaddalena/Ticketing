using System.ComponentModel.DataAnnotations;

namespace TVM.Models;

public class AddTicketSale
{
    [Required]
    [MaxLength(5)]
    public string TicketId { get; set; }
    [Required]
    [Range(0,10)]
    public int Quantity { get; set; }


    public AddTicketSale(string ticketId, int quantity)
    {
        this.TicketId = ticketId;
        this.Quantity = quantity;

    }
}
