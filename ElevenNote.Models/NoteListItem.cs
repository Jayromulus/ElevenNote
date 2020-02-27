using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteListItem
    {
        public int NoteId { get; set; }
     
        public string Title { get; set; }

        
        // this will make the name displayed on the UI "Created" but the column will still be refered to as CreatedUtc
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
