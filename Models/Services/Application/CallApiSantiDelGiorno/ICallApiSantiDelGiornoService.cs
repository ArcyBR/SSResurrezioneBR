using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.ViewModel;

namespace SSResurrezioneBR.Models.Services.Application.CallApiSantiDelGiorno
{
    public interface ICallApiSantiDelGiornoService
    {
        Task<string> GetSantiDelGiornoAsync();
    }
}