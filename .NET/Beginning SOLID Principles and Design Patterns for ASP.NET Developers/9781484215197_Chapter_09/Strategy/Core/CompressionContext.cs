﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.Entity;

using System.IO;
using System.IO.Compression;


namespace Strategy.Core
{
    public class CompressionContext
    {
        private ICompressionAlgorithm strategy;

        public CompressionContext(ICompressionAlgorithm strategy)
        {
            this.strategy = strategy;
        }

        public void Compress(string source, string destination)
        {
            strategy.Compress(source, destination);
        }
    }

}
