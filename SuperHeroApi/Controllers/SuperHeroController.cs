using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Models;

namespace SuperHeroApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext context;

    public SuperHeroController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var hero = context.SuperHeroes.FindAsync(id);

        if (hero == null)
            return BadRequest("Hero not found");

        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        context.SuperHeroes.Add(hero);
        context.SaveChangesAsync();

        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<SuperHero>> Update(SuperHero request)
    {
        var hero = await context.SuperHeroes.FindAsync(request.Id);

        if (hero == null)
        {
            return BadRequest("Hero not found");
        }

        hero.Name = request.Name;
        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

        await context.SaveChangesAsync();

        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SuperHero>> Delete(int id)
    {
        var hero = await context.SuperHeroes.FindAsync(id);

        if (hero == null)
        {
            return BadRequest("Hero not found");
        }

        context.SuperHeroes.Remove(hero);
        context.SaveChangesAsync();

        return Ok(await context.SuperHeroes.ToListAsync());
    }
}
