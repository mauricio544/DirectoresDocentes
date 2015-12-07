using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace DirectoresDocentes.Models.BachilleratoModels
{
    public class BachilleratoModels
    {
    }

    public class GetPeriodo
    {
        public int CODIGO { get; set; }
        public string NOMBRE { get; set; }
    }

    public class PERSONA_FACULTADES
    {
        public int CANTIDAD { get; set; }
        public int FACULTAD { get; set; }
    }

    [MetadataType(typeof (FACULTADES_ESCUELAS))]
    [Table("FACULTADES_ESCUELAS", Schema = "ACADEMICO")]
    public class FACULTADES_ESCUELAS
    {
        public string CAPITALIZER
        {
            get
            {
                var name = NOMBRE;
                return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            }
        }

        [Key]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }
        public int AREA_ACADE_CODIGO { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_CREACION { get; set; }

        public string RESOLUCION { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }
    }

    [MetadataType(typeof (PROGRAMAS_ESTUDIOS))]
    [Table("PROGRAMAS_ESTUDIOS", Schema = "ACADEMICO")]
    public class PROGRAMAS_ESTUDIOS
    {
        public string CAPITALIZER
        {
            get
            {
                var name = NOMBRE;
                return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            }
        }

        [Key]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }
        public string RESOLUCION { get; set; }

        [ForeignKey("FACULTADES_ESCUELAS")]
        public int FACUL_ESCU_CODIGO { get; set; }

        public virtual FACULTADES_ESCUELAS FACULTADES_ESCUELAS { get; set; }
        public int TIP_ESTUDI_CODIGO { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_CREACION { get; set; }
    }

    [MetadataType(typeof (PLANES_ESTUDIOS))]
    [Table("PLANES_ESTUDIOS", Schema = "ACADEMICO")]
    public class PLANES_ESTUDIOS
    {
        [Key, Column(Order = 0)]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("PROGRAMAS_ESTUDIOS")]
        public int PROG_ESTUD_CODIGO { get; set; }

        public virtual PROGRAMAS_ESTUDIOS PROGRAMAS_ESTUDIOS { get; set; }
        public int? CREDITOS { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_CREACION { get; set; }

        public string ACTUAL { get; set; }
        public string VIGENCIA { get; set; }
        public int SEMESTRES { get; set; }
        public string OBSERVACIONES { get; set; }
        public string MINOR { get; set; }
    }

    [MetadataType(typeof (PROGRAMAS_ESTUDIOS_PERSONAS))]
    [Table("PROGRAMAS_ESTUDIOS_PERSONAS", Schema = "ACADEMICO")]
    public class PROGRAMAS_ESTUDIOS_PERSONAS
    {
        [Key, Column(Order = 0)]
        [ForeignKey("PROGRAMAS_ESTUDIOS")]
        public int PROG_ESTUD_CODIGO { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("PERSONA_ROLES")]
        public int PERSONA_RESPONSABLE { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("PERSONA_ROLES")]
        public int PERSONA_ROL { get; set; }

        public virtual PERSONA_ROLES PERSONA_ROLES { get; set; }
        public virtual PROGRAMAS_ESTUDIOS PROGRAMAS_ESTUDIOS { get; set; }
        public string DIRECTOR { get; set; }
    }

    [MetadataType(typeof (PERSONAS))]
    [Table("PERSONAS", Schema = "ACADEMICO")]
    public class PERSONAS
    {
        public string NOMBRE_COMPLETO
        {
            get { return APELLIDO_PATERNO + " " + APELLIDO_MATERNO + ", " + NOMBRES; }
        }

        public string APELLIDOS
        {
            get { return APELLIDO_PATERNO + " " + APELLIDO_MATERNO; }
        }

        public string GENERO
        {
            get { return SEXO != "F" ? "Masculino" : "Femenino"; }
        }

        public string EDAD
        {
            get
            {
                if (FECHA_NACIMIENTO != null)
                {
                    var now = DateTime.Today;
                    var current = FECHA_NACIMIENTO.Value;
                    var age = now.Year - current.Year;
                    if (now < current.AddYears(age)) age--;
                    return age.ToString();
                }
                return "null";
            }
        }

        [Key]
        public int CODIGO { get; set; }

        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASENIA { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? FECHA_NACIMIENTO { get; set; }

        public int? LUGAR_NACIMIENTO { get; set; }
        public string ESTADO_CIVIL { get; set; }
        public string SEXO { get; set; }
        public string DIRECCION { get; set; }
        public int? LUGAR_DIRECCION { get; set; }
        public string TELEFONO_CASA { get; set; }
        public string TELEFONO_CELU { get; set; }
        public string CORREO_UCSP { get; set; }
        public string CORREO_PERSONAL { get; set; }
        public int? COLEGIO { get; set; }
        public byte[] FOTO { get; set; }
        public string CONTRASENIA2 { get; set; }
    }

    [MetadataType(typeof (ROLES))]
    [Table("ROLES", Schema = "ACADEMICO")]
    public class ROLES
    {
        [Key]
        public int CODIGO { get; set; }

        public int ROLES_CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string ABREVIACION { get; set; }
    }

    [MetadataType(typeof (PERSONA_ROLES))]
    [Table("PERSONA_ROLES", Schema = "ACADEMICO")]
    public class PERSONA_ROLES
    {
        [Key, Column(Order = 1)]
        [ForeignKey("ROLES")]
        public int ROLES_CODIGO { get; set; }

        [Key, Column(Order = 0)]
        [ForeignKey("PERSONAS")]
        public int PERSONAS_CODIGO { get; set; }

        public virtual ROLES ROLES { get; set; }
        public virtual PERSONAS PERSONAS { get; set; }
        public string HABILITADO { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? FECHA_DESHABILITA { get; set; }
    }

    [MetadataType(typeof (TIPOS_ALUMNO_ESTADO))]
    public class TIPOS_ALUMNO_ESTADO
    {
        [Key]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }
    }

    [MetadataType(typeof (ALUMNOS_FICHAS))]
    [Table("ALUMNOS_FICHAS", Schema = "ACADEMICO")]
    public class ALUMNOS_FICHAS
    {
        [Key]
        public int CODIGO { get; set; }

        public string CODIGO_ACAD { get; set; }

        [ForeignKey("PERSONA_ROLES"), Column(Order = 0)]
        public int ALUMNO_PERSONA { get; set; }

        [ForeignKey("PERSONA_ROLES"), Column(Order = 1)]
        public int? ALUMNO_ROL { get; set; }

        public virtual PERSONA_ROLES PERSONA_ROLES { get; set; }

        [ForeignKey("PLANES_ESTUDIOS"), Column(Order = 3)]
        public int? PROG_ESTUD { get; set; }

        [ForeignKey("PLANES_ESTUDIOS"), Column(Order = 2)]
        public int? PLAN_ESTUD { get; set; }

        public virtual PLANES_ESTUDIOS PLANES_ESTUDIOS { get; set; }
        public int? POSTULANTE_CODIGO { get; set; }
        public int? ESTADO { get; set; }
        public int? RANK_EGRESO { get; set; }
        public int? RANK_PROMOCION { get; set; }
        public int? SEMESTRE { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Fecha no válida.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? FECHA_CREACION { get; set; }

        public int? TMP_PERIOD_ING { get; set; }
        public int? TMP_PERIOD_EGR { get; set; }
    }

    [MetadataType(typeof (PROFESORES_FICHAS))]
    public class PROFESORES_FICHAS
    {
        [Key]
        public int CODIGO { get; set; }

        public int PERIODOS_CODIGO { get; set; }

        [ForeignKey("PERSONA_ROLES"), Column(Order = 0)]
        public int PROFESOR_PERSONA { get; set; }

        [ForeignKey("PERSONA_ROLES"), Column(Order = 1)]
        public int PROFESOR_ROL { get; set; }

        public virtual PERSONA_ROLES PERSONA_ROLES { get; set; }
        public int TIP_CONTRA_CODIGO { get; set; }
        [MaxLength(20)]
        public string NRO_CONTRATO { get; set; }
        public int TIP_PROFES_CODIGO { get; set; }
        public int HORAS_MENSUAL { get; set; }
        [MaxLength(1)]
        public string DEDICACION { get; set; }
        public int? TMP_CODIGO { get; set; }
        [MaxLength(4)]
        public string PALM_CLAVE { get; set; }
    }

    [MetadataType(typeof (PERIODOS))]
    [Table("PERIODOS", Schema = "ACADEMICO")]
    public class PERIODOS
    {
        [Key]
        public int CODIGO { get; set; }

        public string NOMBRE { get; set; }
        public string TIPO { get; set; }
        public int ANIO { get; set; }
        public int SEMESTRE { get; set; }
        public string ESTADO { get; set; }
        public string POLITICAS { get; set; }
        public int? ANTECEDE { get; set; }
    }
}