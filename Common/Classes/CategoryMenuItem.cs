
using System;
using System.Collections.Generic;

namespace Common.Classes
{
    public class CategoryMenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CategoryMenuItem> ChildItems { get; set; } = new List<CategoryMenuItem>();
    }
}
