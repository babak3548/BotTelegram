//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Channel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Channel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int ChannelId { get; set; }
        public byte Star { get; set; }
    
        public virtual Channel Channel { get; set; }
        public virtual User User { get; set; }
    }
}
