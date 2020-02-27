using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        // field to store the logged in user's id
        private readonly Guid _userId;

        // constructor stores user id when the NoteService is initialized
        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            // creates a new note entity to add to the database using the given information
            var entity = new Note()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };

            // creates a disposable use of the db context
            using (var ctx = new ApplicationDbContext())
            {
                // add the new entity to the database
                ctx.Notes.Add(entity);
                // return true if the new note gets added, and false if it does not (reason: should only change 1 row, therefore returning 1)
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            // creates a disposable (temporary) use of the db context
            using (var ctx = new ApplicationDbContext())
            {
                // makes an IQuery object to hold all of the notes found
                var query =
                    // using that disposable context
                    ctx
                        // go into the notes
                        .Notes
                        // filter them keeping only those that belong to the user id
                        .Where(e => e.OwnerId == _userId)
                        // puts a copy of each item into the IQuery object
                        .Select(e => new NoteListItem { NoteId = e.NoteId, Title = e.Title, CreatedUtc = e.CreatedUtc });

                // returns all of the notes found that belong to the user in the form of an array
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            // create another temporary use of the application database context
            using (var ctx = new ApplicationDbContext())
            {
                // new entity to hold information we find
                var entity =
                    // go into the database context
                    ctx
                        // look at the ntes
                        .Notes
                        // find a single item where the note id is the id provided and it belongs to the user who is logged in
                        .Single(e => e.NoteId == id && e.OwnerId == _userId);

                // return a new note detail with the information found in the entity we got
                return new NoteDetail
                {
                    NoteId = entity.NoteId,
                    Title = entity.Title,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }
    }
}
