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
    
    public partial class Bridge_BotUser
    {
        public string fk_bot { get; set; }
        public int fk_user { get; set; }
        public byte role { get; set; }
        public int rate { get; set; }
        public int last_contentId { get; set; }
        public int id { get; set; }
    
        public virtual Bot Bot { get; set; }
        public virtual User User { get; set; }
    }
}