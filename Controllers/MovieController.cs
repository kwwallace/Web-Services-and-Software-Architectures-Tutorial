using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using MovieApi.Services;


namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;

    private IMovieService _service;


    public MovieController(ILogger<MovieController> logger, IMovieService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        IEnumerable<Movie> list = _service.GetMovies();
        if(list != null)
            return Ok(list);
        else 
            return BadRequest();
    }

    [HttpGet("{name}", Name = "GetMovie")]
    public IActionResult GetMovieByName(string name)
    {
        Movie obj = _service.GetMovieByName(name);
        if(obj != null){
            return Ok(obj);
        }
        return BadRequest();
    }

    [HttpGet("year/")]
    public IActionResult GetMovieByYear(int year){
        Movie obj = _service.GetMoviesByYear(year);
        if(obj != null){
            return Ok(obj);
        }
        return BadRequest();
    }

    [HttpPost]
    public IActionResult CreateMovie(Movie m){
        _service.CreateMovie(m);

        //add some code to dertermine if successfull
        return CreatedAtRoute("GetMovie", new { name = m.Name}, m);
    }

    [HttpPut("{name}")]
    public IActionResult UpdateMovie(string name, Movie movieIn){
        _service.UpdateMovie(name, movieIn);
        return NoContent();
    }

    [HttpDelete("{name}")]
    public IActionResult DeleteMovie(string name){
        _service.DeleteMovie(name);
        return NoContent();
    }
}
