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
    
    public partial class computadora
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public computadora()
        {
            this.actividad_computador = new HashSet<actividad_computador>();
        }
    
        public int id_computadora { get; set; }
        public string nombre { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string SO { get; set; }
        public Nullable<int> id_laboratorio { get; set; }
        public string Mac { get; set; }
        public string ip { get; set; }
        public string descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actividad_computador> actividad_computador { get; set; }
        public virtual laboratorio laboratorio { get; set; }
    }
}
