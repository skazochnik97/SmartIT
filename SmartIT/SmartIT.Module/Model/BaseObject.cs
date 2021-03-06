﻿using System;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace SmartIT.Module.Model
{

    [Table(Name = "BaseObject")]
    public class BaseObject
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "Name"), NotNull]
        [Required]
        public string Name { get; set; }

    }
}
