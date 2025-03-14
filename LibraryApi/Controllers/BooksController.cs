﻿using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        LibraryContext libraryContext;

        public BooksController(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            libraryContext.Books.Add(book);
            await libraryContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await libraryContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [HttpGet]
        public async Task<ActionResult<Book>> GetAllBooks()
        {
            var books = await libraryContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            var book = await libraryContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            libraryContext.Books.Remove(book);
            await libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest("Nincs ilyen könyv");
            }

            var existingBook = await libraryContext.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

           
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.PublishedYear = updatedBook.PublishedYear;
            existingBook.Genre = updatedBook.Genre;
            existingBook.Price = updatedBook.Price;

           
            await libraryContext.SaveChangesAsync();

            return NoContent();
        }



    }
}
