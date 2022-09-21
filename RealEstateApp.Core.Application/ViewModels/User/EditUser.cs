using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class EditUser
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un telefono")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Url { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        public bool HasError { get; set; }
        public string Error { get; set; }

    }
}
