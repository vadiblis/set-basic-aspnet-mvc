using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace set_basic_aspnet_mvc.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        //useremail who added feedback
        public string UserEmail { get; set; }

        //information written in feedback
        public string Info { get; set; }
    }
}