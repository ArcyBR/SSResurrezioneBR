﻿using System;

namespace SSResurrezioneBR.Models.Exceptions.Infrastructure
{
    public class ImagePersistenceException : Exception
    {
        public ImagePersistenceException(Exception innerException) : base ("Couldn't persist the image", innerException)
        { 
        }
    }
}
