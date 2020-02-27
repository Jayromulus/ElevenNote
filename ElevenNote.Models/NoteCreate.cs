using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteCreate
    {
        [Required]
        // if the Title is less than 2 characters, this will throw an error
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        // if the Title is more than 100 characters, this will throw an error
        [MaxLength(100, ErrorMessage = "There are way too many characters here.")]
        public string Title { get; set; }

        // max number of characters in content is 8000
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
