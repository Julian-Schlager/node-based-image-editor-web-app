using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DTO;
//https://jasonwatmore.com/post/2021/12/20/net-6-basic-authentication-tutorial-with-example-api

using System.ComponentModel.DataAnnotations;

public class AuthenticateModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
