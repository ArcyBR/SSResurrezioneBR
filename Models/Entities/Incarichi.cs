using System;
using System.Collections.Generic;

namespace SSResurrezioneBR.Models.Entities;

public class Incarichi
{
    public long Id { get; private set; }

    public string Titolo { get; private set; } = null!;

    public string Cognome { get; private set; } = null!;

    public string Nome { get; private set; } = null!;

    public string Incarico { get; private set; } = null!;

    public string? Email { get; private set; }

    public string? TelefonoFisso { get; private set; }

    public string? TelefonoCellulare { get; private set; }

    public string? Indirizzo { get; private set; }

    public string? NumeroCivico { get; private set; }

    public string? Cap { get; private set; }

    public string? Città { get; private set; }

    public string? LinkDiocesi { get; private set; }
}
