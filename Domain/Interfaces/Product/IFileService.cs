using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Product
{
    public interface IFileService
    {
        Task<string> SaveImageFileAsync(IFormFile fileName, string[] fileExtension);
        Task DeleteImageFileAsync(string fileName);
    }
}
