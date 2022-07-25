using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TicketsCatalog.EventBus;
using TicketsCatalog.Models;
using TicketsCatalog.Repository;

namespace TicketsCatalog.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ILogger<TicketsController> logger;
    private readonly ITicketRepository ticketRepository;
    private readonly IEventPublisher eventPublisher;

    public TicketsController(ILogger<TicketsController> logger, ITicketRepository ticketRepository, IEventPublisher eventPublisher)
    {
        this.logger = logger;
        this.ticketRepository = ticketRepository;
        this.eventPublisher = eventPublisher;

    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Ticket>), StatusCodes.Status200OK)]    
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Ticket>>> Get()
    {
        try
        {
            return this.Ok(await ticketRepository.GetAllAsync());            
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
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Ticket>> Get(string id)
    {
        try
        {
            logger.LogInformation(0, "Get Ticket with id: {0}", id);

            var ticket = await ticketRepository.GetByIdAsync(id);

            if(ticket == null)
                return this.NotFound();
            else
                return this.Ok(ticket);
            
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


    /// <summary>
    /// Creates a Ticket.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /
    ///     {
    ///        "ticketId": "001",
    ///        "price": 1.30,
    ///        "validFrom": "2022-07-20T14:32:56.399Z"
    ///     }
    ///
    /// </remarks>
    /// <param name="t"></param>
    /// <returns>A newly created Ticket</returns>
    /// <response code="201">Returns the newly created Ticket</response>
    /// <response code="409">If the TicketId already exists</response> 
    /// <response code="400">Bad Request</response> 
    [HttpPost]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ConflictResult), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Ticket>> Post([FromBody]Ticket t)
    {
        try
        {
            logger.LogInformation(0, "Add new ticket");
            
            // check if already exists            
            if(await ticketRepository.GetByIdAsync(t.TicketId) != null)
                return Conflict($"Ticket {t.TicketId} already exists");

            // add ticket to the repository
            var data = ticketRepository.AddAsync(t);
            await ticketRepository.SaveAsync();

            logger.LogInformation(0, "Ticket added. ID: ", t.TicketId);

            eventPublisher.PublishToMessageQueue("ticket.add", JsonConvert.SerializeObject(t));

            // return 
            return CreatedAtAction(nameof(Get), new { TicketID = t.TicketId}, t);
            
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

    // PATCH 
    [HttpPatch]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Ticket>> PatchTicket([FromBody] PatchTicketPrice patchTicket)
    {

        try
        {
            logger.LogInformation(0, "Patch ticket with id: {0}, new ticket price: {1}", patchTicket.TicketId, patchTicket.NewPrice);

            var tk = await ticketRepository.GetByIdAsync(patchTicket.TicketId);

            if(tk == null)
                return this.NotFound();
            else
            {
                tk.Price = patchTicket.NewPrice;
                await ticketRepository.SaveAsync();

                eventPublisher.PublishToMessageQueue("ticket.patch", JsonConvert.SerializeObject(tk));

                return this.Ok(tk);
            }
            
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
