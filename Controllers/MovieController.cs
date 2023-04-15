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
    private static readonly List<Movie> movies = new List<Movie>(10)
    {
        new Movie {Name = "Citizen Smith", Genre = "Drama", Year = 1941},
        new Movie {Name = "The Warlock of Oz", Genre = "Fantasy", Year = 1939},
        new Movie {Name = "The GrandMother", Genre = "Crime", Year = 1972}
    };

    private readonly ILogger<MovieController> _logger;

    private IMovieService _service;


    public MovieController(ILogger<MovieController> logger, IMovieService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]//(Name = "GetMovie")]
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
        foreach (Movie m in movies){
            if (m.Name.Equals(name)){
                return Ok(m);
            }
        }
        return BadRequest();
    }

    [HttpGet("year/")]
    public IActionResult GetMovieByYear(int year){
        foreach (Movie m in movies){
            if (m.Year==year)
                return Ok(m);
        };
        return BadRequest();
    }

    [HttpPost]
    public IActionResult CreateMovie(Movie m){
        try {
            movies.Add(m);
            return CreatedAtRoute("GetMovie", new {name = m.Name}, m);
        }catch (Exception e) {
            return StatusCode(500);
        }
        
    }

    [HttpPut("{name}")]
    public IActionResult UpdateMovie(string name, Movie movieIn){
        try {
            //movies.Add(m);
            foreach (Movie m in movies){
                if(m.Name.Equals(name)){
                    m.Name = movieIn.Name;
                    m.Genre = movieIn.Genre;
                    m.Year = movieIn.Year;
                    return NoContent();
                }
            }
            return BadRequest();
        }catch (Exception e) {
            return StatusCode(500);
        }
    }

    [HttpDelete("{name}")]
    public IActionResult DeleteMovie(string name){
        try {
            //movies.Add(m);
            foreach (Movie m in movies){
                if(m.Name.Equals(name)){
                    movies.Remove(m);
                    return NoContent();
                }
            }
            return BadRequest();
        }catch (Exception e) {
            return StatusCode(500);
        }
    }
}
