//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabsAdminASP.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class actividad_computador
    {
        public int id_actividad_computador { get; set; }
        public int id_actividad { get; set; }
        public int id_computador { get; set; }
    
        public virtual actividad actividad { get; set; }
        public virtual computadora computadora { get; set; }
    }
}
