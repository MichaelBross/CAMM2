﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;

namespace Domain.Assemblies
{
    public class Assembly : Item
    {
        public string Rev { get; set; }
        public IList<AssemblyComponent> AssemblyComponents { get; set; }
    }
}
