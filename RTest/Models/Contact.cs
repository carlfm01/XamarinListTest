using System;

namespace RTest.Models
{
    public class Contact
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; } 
    }
}