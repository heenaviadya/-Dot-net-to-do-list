using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_do_list.Areas.Identity.Data;
using To_do_list.Models;


namespace To_do_list.Data
{
    public class ToDoListDbContext: IdentityDbContext //this is used to inherit the ToDoListDbContext class from DbContet class of EF   
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)

        {

        }
        public DbSet<ToDoList> Tasks { get; set; }// this line is used to create a table named Tasks corresponding to model ToDoList in database
        public DbSet<To_do_listUser> Users  { get; set; }
    }
}
