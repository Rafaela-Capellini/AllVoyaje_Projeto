﻿namespace ProjetoAllVoyaje.Models
{
    public class Cliente 
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string AspNetUserId { get; set; }
    }
}
