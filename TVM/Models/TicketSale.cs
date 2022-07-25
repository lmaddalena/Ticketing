using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVM.Models;

public class TicketSale
{
    [Key]
    [Required]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }
    [Required]
    [MaxLength(5)]
    public string TicketId { get; set; }
    [Required]
    [Range(0,10)]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    [Required]
    public DateTime SaleDate { get; set; }

    public TicketSale(string ticketId, double unitPrice, int quantity, DateTime saleDate)
    {
        this.TicketId = ticketId;
        this.UnitPrice = unitPrice;
        this.Quantity = quantity;
        this.SaleDate = saleDate;
    }
}
