//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPulperia
{
    using System;
    using System.Collections.Generic;
    
    public partial class Historial
    {
        public int IDHISTORIAL { get; set; }
        public string USERID { get; set; }
        public int IDCOMPRA { get; set; }
    
        public virtual Compra Compra { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
