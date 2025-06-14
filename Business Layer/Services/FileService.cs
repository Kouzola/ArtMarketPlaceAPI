﻿using Business_Layer.Exceptions;
using Domain_Layer.Interfaces.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class FileService(IWebHostEnvironment environment) : IFileService
    {
        private readonly IWebHostEnvironment _environment = environment;
        public void DeleteImageFileAsync(string fileName)
        {
            var contentPath = _environment.ContentRootPath;
            var fileToDelete = Path.Combine(contentPath,"Images",fileName);
            Console.WriteLine(fileToDelete);
            if(!File.Exists(fileToDelete)) throw new NotFoundException("File Not found!");
            File.Delete(fileToDelete);
        }

        public async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            //Dossier où on va stocker les images
            var contentPath = _environment.ContentRootPath;
            var saveFileDir = Path.Combine(contentPath,"Images");
            if(!Directory.Exists(saveFileDir)) Directory.CreateDirectory(saveFileDir);

            //Generer un nom de fichier généré ici
            var generatedFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(imageFile.FileName);
            var generatedFileNamePath = Path.Combine(saveFileDir,generatedFileName);
            //Upload file
            using (var stream = File.Create(generatedFileNamePath))
            {
                await imageFile.CopyToAsync(stream);
            }     
            return generatedFileName;
        }
    }
}
