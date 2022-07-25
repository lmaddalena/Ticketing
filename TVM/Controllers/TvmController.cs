using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TVM.Models;
using TVM.Repository;

namespace TVM.Controllers;

[ApiController]
[Route("[controller]")]
public class TvmController : ControllerBase
{
    private readonly ILogger<TvmController> logger;
    private readonly ITicketRepository ticketRepository;

    public TvmController(ILogger<TvmController> logger, ITicketRepository ticketRepository)
    {
        this.logger = logger;
        this.ticketRepository = ticketRepository;

    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TicketSale>), StatusCodes.Status200OK)]    
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TicketSale>>> Get()
    {
        try
        {
            return this.Ok(await ticketRepository.GetAllTicketSalesAsync());            
        }
        catch (System.Exception ex)
        {
            logger.LogError(0, ex, ex.Message);

            return Problem(
                title:"An error has occurred", 
                detail: ex.Message, 
                statusCode: StatusCodes.Status500InternalServerError);
        }


    }

    // GET 
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TicketSale), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TicketSale>> Get(int id)
    {
        try
        {
            logger.LogInformation(0, "Get TicketSale with id: {0}", id);

            TicketSale ts = await ticketRepository.GetTicketSaleByIdAsync(id);

            if(ts == null)
                return this.NotFound();
            else
                return this.Ok(ts);
            
        }
        catch (System.Exception ex)
        {
            
            logger.LogError(0, ex, ex.Message);

            return Problem(
                title:"An error has occurred", 
                detail: ex.Message, 
                statusCode: StatusCodes.Status500InternalServerError);

        }
    }



    [HttpPost]
    [ProducesResponseType(typeof(TicketSale), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ConflictResult), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TicketSale>> Post([FromBody] AddTicketSale ats)
    {
        try
        {
            logger.LogInformation(0, "Add new sale");
            
            // get the ticket
            Ticket tk = await ticketRepository.GetTicketByIdAsync(ats.TicketId);

            if(tk == null)
                return NotFound();

            // add sale to the repository
            TicketSale ts = await ticketRepository.AddSaleAsync(ats.TicketId, ats.Quantity, tk.Price, DateTime.UtcNow);
            await ticketRepository.SaveAsync();

            logger.LogInformation(0, "Ticket Sale added. ID: ", ts.SaleId);

            // return 
            return CreatedAtAction(nameof(Get), new { SaleId = ts.SaleId}, ts);
            
        }
        catch (System.Exception ex)
        {
            
            logger.LogError(0, ex, ex.Message);

            return Problem(
                title:"An error has occurred", 
                detail: ex.Message, 
                statusCode: StatusCodes.Status500InternalServerError);


        }

    }    

}
