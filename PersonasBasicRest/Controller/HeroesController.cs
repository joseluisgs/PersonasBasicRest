using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasBasicRest.Database;
using PersonasBasicRest.Logger;
using PersonasBasicRest.Mapper;
using PersonasBasicRest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PersonasBasicRest.Controller;

/**
 * Controlador para las operaciones CRUD de héroes.
 */
[Route("heroes")]
[ApiController]
public class HeroesController : ControllerBase
{
    private readonly HeroDbContext _context; // contexto de la base de datos

    private readonly Serilog.Core.Logger _logger = LoggerUtils<HeroesController>.GetLogger(); // logger

    public HeroesController(HeroDbContext context)
    {
        _context = context;
    }

    // GET: heroes
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all heroes",
        Description = "Returns a list of all heroes available in the database.")]
    [SwaggerResponse(200, "Returns the list of heroes", typeof(IEnumerable<Hero>))]
    public async Task<ActionResult<IEnumerable<Hero>>> GetAll()
    {
        _logger.Debug("Getting all heroes");
        // return await _context.Heroes.ToListAsync();
        // return Ok(await _context.Heroes.ToListAsync());
        return Ok(await _context.Heroes
            .Select(hero => hero.ToModel())
            .ToListAsync()
        );
    }

    // GET: heroes/5
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Retrieve a hero", Description = "Returns a hero with the specified ID.")]
    [SwaggerResponse(200, "Returns the hero", typeof(Hero))]
    [SwaggerResponse(404, "Hero with this ID not found")]
    public async Task<ActionResult<Hero>> GetHero(
        [SwaggerParameter("ID of the hero", Required = true)]
        long id)
    {
        _logger.Debug($"Getting hero with ID: {id}");

        var hero = await _context.Heroes.FindAsync(id);

        // Si es nulo héroe no existe, devuelve un 404 Not Found
        if (hero == null) return NotFound("Hero with this ID " + id + " not found.");

        // Devuelve el héroe encontrado
        _logger.Information($"Hero {hero.Id} retrieved");
        return Ok(hero.ToModel());
    }

    // POST: heroes
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new hero", Description = "Creates a new hero")]
    [SwaggerResponse(201, "Hero created", typeof(Hero))]
    [SwaggerResponse(400, "Invalid hero name")]
    public async Task<ActionResult<Hero>> Post(
        [FromBody] [SwaggerParameter("Hero to create", Required = true)]
        Hero hero
    )
    {
        _logger.Debug("Creating a new hero");

        // Valida el nombre del héroe
        if (string.IsNullOrWhiteSpace(hero.Name)) return BadRequest("Invalid hero name.");

        var entityToSave = hero.ToEntity();
        entityToSave.Id = HeroEntity.NewId;

        await _context.Heroes.AddAsync(entityToSave); // Guarda el héroe en la base de datos
        await _context.SaveChangesAsync(); // Confirma los cambios en la base de datos
        _logger.Information($"Hero {entityToSave.Id} created");

        // Devuelve el héro creado
        return CreatedAtAction(nameof(GetHero), new { id = entityToSave.Id }, entityToSave.ToModel());
    }

    // PUT: heroes/5
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update a hero", Description = "Updates a hero")]
    [SwaggerResponse(200, "Hero updated", typeof(Hero))]
    [SwaggerResponse(404, "Hero with this ID not found")]
    [SwaggerResponse(400, "Invalid hero name")]
    public async Task<ActionResult<Hero>> Put(
        [SwaggerParameter("ID of the hero", Required = true)]
        long id,
        [FromBody] [SwaggerParameter("Hero to update", Required = true)]
        Hero hero
    )
    {
        _logger.Debug($"Updating hero with ID: {id}");

        // Valida el nombre del héroe
        if (string.IsNullOrWhiteSpace(hero.Name)) return BadRequest("Invalid hero name.");

        // Busca el héroe en la base de datos, si no lo encuentra devuelve un 404 Not Found
        var entityToUpdate = await _context.Heroes.FindAsync(id);
        if (entityToUpdate == null) return NotFound("Hero with this ID " + id + " not found.");

        // Copia los valores del héroe recibido al héroe en la base de datos
        // No puedes usar directamente el héroe recibido con el mapper porque necesitamos la referencia del objeto
        entityToUpdate.Name = hero.Name;
        entityToUpdate.IsBad = hero.IsBad;

        // Guarda los cambios en la base de datos
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        _logger.Information($"Hero {entityToUpdate.Id} updated");

        // Devuelve el héroe actualizado
        return Ok(entityToUpdate.ToModel());
    }
}