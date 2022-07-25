using TicketsCatalog.Models;

namespace TicketsCatalog.Repository;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket);
    Task<Ticket> GetByIdAsync(string ticketId);
    void DelTicket(Ticket ticket);
    Task<List<Ticket>> GetAllAsync();
    void Save();
    Task<int> SaveAsync();
}