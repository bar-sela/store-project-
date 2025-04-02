using System;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) :DbContext(options)
{
    /*
 Even though StoreContext is already connected to the database, it does not automatically know which tables 
 it should interact with—that's where DbSet<Product> comes in.

    */
    public required DbSet<Product> Product { get; set; } 
}
