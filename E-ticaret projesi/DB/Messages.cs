//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_ticaret_projesi.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Messages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Messages()
        {
            this.MessageReplies = new HashSet<MessageReplies>();
        }
    
        public System.Guid Id { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public string AddedDate { get; set; }
        public string ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageReplies> MessageReplies { get; set; }
    }
}
