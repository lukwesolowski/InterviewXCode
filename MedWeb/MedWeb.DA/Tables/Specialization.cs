using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.DA.Tables
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime InsertTime { get; set; }
    }
}