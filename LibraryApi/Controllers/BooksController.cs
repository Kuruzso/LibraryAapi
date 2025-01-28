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
    }
}
