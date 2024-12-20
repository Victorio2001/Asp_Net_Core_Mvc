﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NomDuProjet.Data;
using System;
using System.Linq;
using NomDuProjet.Models;

namespace NomDuProjet.Models.SeedData;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new NomDuProjetContext(serviceProvider.GetRequiredService<DbContextOptions<NomDuProjetContext>>()))
        {
            //? check si y'à des films en bdd
            if (context.Movie.Any())
            {
                return;
            }
            context.Movie.AddRange(
                new Movie()
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "8"
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "5"
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "1"
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "10"
                }
            );
            context.SaveChanges();
        }
    }
}