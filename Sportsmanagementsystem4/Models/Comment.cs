//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sportsmanagementsystem4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int id { get; set; }
        public string description { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> match_id { get; set; }
    
        public virtual Match Match { get; set; }
        public virtual User User { get; set; }
    }
}
