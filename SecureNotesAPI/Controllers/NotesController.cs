using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureNotesAPI.Data;
using SecureNotesAPI.Models;

namespace SecureNotesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(
            AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirst("UserId")!.Value);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var notes =
                _context.Notes
                .Where(x =>
                x.UserId == GetUserId())
                .ToList();

            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Add(Note note)
        {
            note.UserId = GetUserId();

            _context.Notes.Add(note);

            _context.SaveChanges();

            return Ok(new
            {
                message =
                "Note added successfully.",
                noteId = note.Id
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            Note updated)
        {
            var note =
                _context.Notes.FirstOrDefault(
                x => x.Id == id &&
                x.UserId == GetUserId());

            if (note == null)
                return NotFound();

            note.Title = updated.Title;
            note.Content = updated.Content;

            _context.SaveChanges();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note =
                _context.Notes.FirstOrDefault(
                x => x.Id == id &&
                x.UserId == GetUserId());

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);

            _context.SaveChanges();

            return Ok(
                "Deleted Successfully");
        }
    }
}