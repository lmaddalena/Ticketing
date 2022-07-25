using TVM.Models;
using Microsoft.EntityFrameworkCore;

namespace TVM.Repository;

class TicketRepository : ITicketRepository
{
    private TvmDataContext dataContext;

    public TicketRepository(TvmDataContext dc)
    {
        dataContext = dc;
    }
    public async Task<Ticket> AddTicketAsync(Ticket ticket)
    {
        await dataContext.Tickets.AddAsync(ticket);
        return ticket;
        
    }

    public async Task<TicketSale> AddSaleAsync(string tickeId, int quantity, double unitPrice, DateTime saleDate)
    {
        TicketSale ts = new TicketSale(tickeId, unitPrice, quantity, saleDate);
        await dataContext.TicketSales.AddAsync(ts);

        return ts;

    }

    public void DelTicket(Ticket ticket)
    {
        dataContext.Tickets.Remove(ticket);
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        var tk = from t in dataContext.Tickets
                 orderby t.TicketId
                 select t;
        
        return await tk.ToListAsync();
    }

    public async Task<List<TicketSale>> GetAllTicketSalesAsync()
    {
        var ts = from t in dataContext.TicketSales
                 orderby t.SaleDate
                 select t;
        
        return await ts.ToListAsync();

    }

    public async Task<Ticket> GetTicketByIdAsync(string ticketId)
    {
        var tk = from t in dataContext.Tickets
                  where t.TicketId == ticketId
                  select t;

        #pragma warning disable CS8603
        return await tk.SingleOrDefaultAsync();
        #pragma warning restore CS8603

    }


    public async Task<TicketSale> GetTicketSaleByIdAsync(int id)
    {
        var ts = from t in dataContext.TicketSales
                  where t.SaleId == id
                  select t;

        #pragma warning disable CS8603
        return await ts.SingleOrDefaultAsync();
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