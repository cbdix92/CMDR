﻿using System;

namespace Decoder
{
    public class FileNotSupported : Exception
    {
        public FileNotSupported()
        {
        }

        public FileNotSupported(string message)
            :base(message)
        {
        }

        public FileNotSupported(string message, Exception inner)
            :base(message, inner)
        {
        }
    }
}
