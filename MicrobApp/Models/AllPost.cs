using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MicrobApp.Models
{
    class AllPost
    {
        public List<Post> Posts { get; set; } = new List<Post>();

        public AllPost() { }
    }


}
