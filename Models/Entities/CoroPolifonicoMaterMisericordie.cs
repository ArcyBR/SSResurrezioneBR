using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SSResurrezioneBR.Models.Entities;

public class CoroPolifonicoMaterMisericordie
{
    public CoroPolifonicoMaterMisericordie(string titoloCoroPolifonicoMaterMisericordie, string creatoreEventoCoroPolifonicoMaterMisericordie)
    {
        if (string.IsNullOrWhiteSpace(titoloCoroPolifonicoMaterMisericordie))
        {
            throw new ArgumentException("CoroPolifonicoMaterMisericordie deve avere un titolo");
        }
        TitoloCoroPolifonicoMaterMisericordie = titoloCoroPolifonicoMaterMisericordie;
        CreatoreEventoCoroPolifonicoMaterMisericordie = creatoreEventoCoroPolifonicoMaterMisericordie;
    }
    public long IdCoroPolifonicoMaterMisericordie { get; private set; }

    public string DescrizioneEventoCoroPolifonicoMaterMisericordie { get; private set; } = string.Empty!;

    public string TitoloCoroPolifonicoMaterMisericordie { get; private set; } = string.Empty;
    public DateTime DataEventoCoroPolifonicoMaterMisericordie { get; set; }
    public int? ImageVisibileCoroPolifonicoMaterMisericordie { get; set; } = 1;
    public string? CreatoreEventoCoroPolifonicoMaterMisericordie { get; set; }

    public virtual ICollection<CoroPolifonicoMaterMisericordieFoto> CoroPolifonicoMaterMisericordieFotos { get; private set; } = new List<CoroPolifonicoMaterMisericordieFoto>();
    public string? RowVersion { get; private set; }
    public void ChangeTitle(string newTitoloEventoCoroPolifonicoMaterMisericordie)
    {
        if (string.IsNullOrWhiteSpace(newTitoloEventoCoroPolifonicoMaterMisericordie))
        {
            throw new ArgumentException("CoroPolifonicoMaterMisericordie deve avere un titolo");
        }
        TitoloCoroPolifonicoMaterMisericordie = newTitoloEventoCoroPolifonicoMaterMisericordie;
    }
    public void ChangeDescription(string newDesciptioneEventoCoroPolifonicoMaterMisericordie)
    {
        if (string.IsNullOrWhiteSpace(newDesciptioneEventoCoroPolifonicoMaterMisericordie))
        {
            throw new ArgumentException("Almanacco deve avere una descrizione");
        }
        DescrizioneEventoCoroPolifonicoMaterMisericordie = newDesciptioneEventoCoroPolifonicoMaterMisericordie;
    }
    public void ChangeEventData(DateTime newDataEventoCoroPolifonicoMaterMisericordie)
    {
        if (string.IsNullOrEmpty(newDataEventoCoroPolifonicoMaterMisericordie.ToString()))
        {
            throw new ArgumentException("Coro Polifonico Mater Misericordie deve avere una data evento");
        }
        DataEventoCoroPolifonicoMaterMisericordie = newDataEventoCoroPolifonicoMaterMisericordie;
    }
}