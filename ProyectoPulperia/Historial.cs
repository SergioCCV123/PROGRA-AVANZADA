//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
