using TVM.Models;

namespace TVM.Repository;

public interface ITicketRepository
{
    Task<Ticket> AddTicketAsync(Ticket ticket);
    Task<Ticket> GetTicketByIdAsync(string ticketId);
    void DelTicket(Ticket ticket);
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<TicketSale> AddSaleAsync(string tickeId, int quantity, double unitPrice, DateTime saleDate);
    void Save();
    Task<int> SaveAsync();
    Task<TicketSale> GetTicketSaleByIdAsync(int id);
    Task<List<TicketSale>> GetAllTicketSalesAsync();
}