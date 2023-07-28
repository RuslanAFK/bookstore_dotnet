using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IBookFilesService
    {
        Task AddAsync(Book book, IFormFile file);
        Task RemoveAsync(Book book);
        void CheckIfFilePermitted(IFormFile file);
    }
}
