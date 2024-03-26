using System;
using System.Collections.Generic;

namespace SSResurrezioneBR.Models.Entities;

public class ImgForCarousel
{
    public long ImageId { get; private set; }

    public string ImagePath { get; private set; } = null!;

    public string ImageLabel { get; private set; } = null!;

    public string ImageContentTitle { get; private set; } = null!;

    public long ImageVisible { get; private set; }

    public string ImageContentDescription { get; private set; } = null!;
}
