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
    
    public partial class CV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CV()
        {
            this.CVXAbilities = new HashSet<CVXAbility>();
            this.CVXCertificates = new HashSet<CVXCertificate>();
            this.CVXExperiences = new HashSet<CVXExperience>();
            this.CVXTrainings = new HashSet<CVXTraining>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CandidateId { get; set; }
    
        public virtual Candidate Candidate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CVXAbility> CVXAbilities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CVXCertificate> CVXCertificates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CVXExperience> CVXExperiences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CVXTraining> CVXTrainings { get; set; }
    }
}
