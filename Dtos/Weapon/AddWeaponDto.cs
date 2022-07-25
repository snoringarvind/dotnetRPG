using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetRPG.Dtos.Weapon
{
    public class AddWeaponDto
    {
        public int Damage { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CharacterId { get; set; }
    }
}