using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DirectoresDocentes.Models.ApplicationModels
{
    public class ExactusModels
    {
    }    
    public class VIEW_capacitacion
    {
        [MaxLength(20)]
        public string identificacion { get; set; }
        [MaxLength(70)]
        public string nombre { get; set; }
        [MaxLength(4)]
        public string tipo_academico { get; set; }
        [MaxLength(40)]
        public string descripcion { get; set; }
        [MaxLength(50)]
        public string CARRERA { get; set; }
        [MaxLength(250)]
        public string INSTITUCION { get; set; }
    }
}