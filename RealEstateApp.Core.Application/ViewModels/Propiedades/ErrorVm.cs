using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class ErrorVm
    {
        public bool hasError;

        public bool HasError
        {
            set
            {
                hasError = true;
            }
        }

        public string Error 
        {
            get
            {
                return " <ul> " +
                            "<li> Tipos de Propiedades </li>" +
                            "<li> Tipos de Ventas </li>" +
                            "<li> Tipos de Mejoras </li>" +
                            " <br/>" +
                            "<p> No han sido creadas en el sistema,Favor contactar al administrador </p> " +
                        "</ul>";
            }
        
        }

    }
}
