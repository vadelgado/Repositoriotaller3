using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int Id_usuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Ciudad { get; set; }
        public Nullable<int> Edad { get; set; }
    }
}