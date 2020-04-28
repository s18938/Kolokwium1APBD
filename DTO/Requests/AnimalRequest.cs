using Kolokwium1APBD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1APBD.DTO.Requests
{
    public class AnimalRequest
    {
        [Required(ErrorMessage = "Musisz podać id!")]
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwe!")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Musisz podać typ!")]
        [MaxLength(100)]
        public string AnimalType { get; set; }

        [Required(ErrorMessage = "Musisz podać date!")]
        public DateTime DateOfAdmision { get; set; }

        [Required(ErrorMessage = "Musisz podać wlasciciela!")]
        public int IdOwner { get; set; }

        public List<Procedure> Procedures { get; set; }
    }
}
