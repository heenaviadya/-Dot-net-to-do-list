using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using To_do_list.Models;

namespace To_do_list.Areas.Identity.Data;

// Add profile data for application users by adding properties to the To_do_listUser class
public class To_do_listUser : IdentityUser
{
    public List<ToDoList>? List { get; set; }
}

