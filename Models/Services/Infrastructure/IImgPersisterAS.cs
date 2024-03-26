using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public interface IImgPersisterAS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="formFile"></param>
        Task<string> SaveImageAsync (IFormFile formFile);
    }
}