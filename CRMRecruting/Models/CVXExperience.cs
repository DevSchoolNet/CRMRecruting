//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMRecruting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CVXExperience
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public int ExperienceId { get; set; }
    
        public virtual CV CV { get; set; }
        public virtual Experience Experience { get; set; }
    }
}
