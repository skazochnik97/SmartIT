using System;
using LinqToDB.Mapping;

namespace SmartIT.Module.Model
{

    [Table(Name = "BaseObject")]
    public class BaseObject
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "Name"), NotNull]
        public string Name { get; set; }

    }
}
