//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace masterpeace2
{
    using System;
    using System.Collections.Generic;
    
    public partial class feedback
    {
        public int ID { get; set; }
        public string userId { get; set; }
        public string text { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
