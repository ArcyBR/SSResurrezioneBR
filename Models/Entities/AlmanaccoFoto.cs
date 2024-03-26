using System;
using System.Collections.Generic;

namespace SSResurrezioneBR.Models.Entities;

public class AlmanaccoFoto
{
    public long IdAlmanaccoFoto { get; private set; }

    public string PathFotoAlmanaccoFoto { get; private set; } = null!;

    public long IdAlmanacco { get; private set; }

    public virtual Almanacco IdAlmanaccoNavigation { get; private set; } = null!;
}
