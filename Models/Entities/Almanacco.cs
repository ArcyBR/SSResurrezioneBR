using System;
using System.Collections.Generic;

namespace SSResurrezioneBR.Models.Entities;

public class Almanacco
{
    public Almanacco (string titoloAlmanacco, string creatoreEventoAlmanacco){
        if (string.IsNullOrWhiteSpace(titoloAlmanacco)){
            throw new ArgumentException("Almanacco deve avere un titolo");
        }
        TitoloAlmanacco = titoloAlmanacco;
        CreatoreEventoAlmanacco = creatoreEventoAlmanacco;
    }
    public long IdAlmanacco { get; private set; }

    public string DescrizioneAlmanacco { get; private set; } = string.Empty!;

    public string TitoloAlmanacco { get; private set; } = string.Empty;
    public DateTime DataEventoAlmanacco { get; private set; } 
    public int? ImageVisibileAlmanacco { get; set; } = 1;
    public string? CreatoreEventoAlmanacco { get; set; }
    public string ? RowVersion { get; private set; }

    public virtual ICollection<AlmanaccoFoto> AlmanaccoFotos { get; private set; } = new List<AlmanaccoFoto>();

    public void ChangeTitle (string newTitoloAlmanacco){
        if(string.IsNullOrWhiteSpace(newTitoloAlmanacco)){
            throw new ArgumentException("Almanacco deve avere un titolo");
        }
        TitoloAlmanacco = newTitoloAlmanacco;
    }
    public void ChangeDescription (string newDesciptionAlmanacco){
        if(string.IsNullOrWhiteSpace(newDesciptionAlmanacco)){
            throw new ArgumentException("Almanacco deve avere una descrizione");
        }
        DescrizioneAlmanacco = newDesciptionAlmanacco;
    }
    public void ChangeEventData (DateTime newEventDataAlmanacco){
        if(string.IsNullOrEmpty(newEventDataAlmanacco.ToString())){
            throw new ArgumentException("Almanacco deve avere una data evento");
        }
        DataEventoAlmanacco = newEventDataAlmanacco;
    }
}