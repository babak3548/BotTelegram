//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PublicChatBot
{
    using System;
    using System.Collections.Generic;
    
    public partial class content
    {
        public int id { get; set; }
        public string fk_bot { get; set; }
        public int fk_user { get; set; }
        public string jsonMessage { get; set; }
        public int messageId { get; set; }
        public int ratePlus { get; set; }
        public byte isBroadcast { get; set; }
        public System.DateTime date { get; set; }
        public int rateMinus { get; set; }
    
        public virtual Bot Bot { get; set; }
        public virtual User User { get; set; }
    }
}
