using System;
using System.Collections.Generic;

namespace SSResurrezioneBR.Models.Entities;

public class CoroPolifonicoMaterMisericordieFoto
{
    public long IdCoroPolifonicoMaterMisericordieFoto { get; private set; }

    public string PathFotoCoroPolifonicoMaterMisericordieFoto { get; private set; } = null!;

    public long IdCoroPolifonicoMaterMisericordie { get; private set; }

    public virtual CoroPolifonicoMaterMisericordie IdCoroPolifonicoMaterMisericordieNavigation { get; private set; } = null!;
}
