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
    
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.Playerteaminfoes = new HashSet<Playerteaminfo>();
            this.Schedules = new HashSet<Schedule>();
            this.Schedules1 = new HashSet<Schedule>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string semester { get; set; }
        public Nullable<int> isRegistered { get; set; }
        public Nullable<int> no_players { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> sport_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playerteaminfo> Playerteaminfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules1 { get; set; }
        public virtual Sport Sport { get; set; }
        public virtual User User { get; set; }
    }
}