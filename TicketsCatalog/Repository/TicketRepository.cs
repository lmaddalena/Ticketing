using TicketsCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketsCatalog.Repository;

class TicketRepository : ITicketRepository
{
    private TicketsCatalogDataContext dataContext;

    public TicketRepository(TicketsCatalogDataContext dc)
    {
        dataContext = dc;
    }
    public async Task<Ticket> AddAsync(Ticket ticket)
    {
        await dataContext.Tickets.AddAsync(ticket);
        return ticket;
        
    }

    public void DelTicket(Ticket ticket)
    {
        dataContext.Tickets.Remove(ticket);
    }

    public async Task<List<Ticket>> GetAllAsync()
    {
        var tk = from t in dataContext.Tickets
                 orderby t.TicketId
                 select t;
        
        return await tk.ToListAsync();
    }

    public async Task<Ticket> GetByIdAsync(string ticketId)
    {
        var tk = from t in dataContext.Tickets
                  where t.TicketId == ticketId
                  select t;

        #pragma warning disable CS8603
        return await tk.SingleOrDefaultAsync();
        #pragma warning restore CS8603

    }

    // save changes
    public void Save()
    {
        dataContext.SaveChanges();
    }

    // save changes asynchronly
    public async Task<int> SaveAsync()
    {

        // EF Core does not support multiple parallel operations being run on the same context instance. 
        // You should always wait for an operation to complete before beginning the next operation. 
        // This is typically done by using the await keyword on each asynchronous operation.

        return await dataContext.SaveChangesAsync();
    }

}