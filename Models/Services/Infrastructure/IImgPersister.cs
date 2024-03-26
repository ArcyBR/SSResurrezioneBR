using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public interface IImgPersister
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="formFile"></param>
        /// <returns>The Image URL e.g. /IniziativeInCorso/</returns>
        Task<string> SaveImageAsync (IFormFile formFile);
    }
}