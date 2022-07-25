using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetRPG.Dtos.Weapon
{
    public class GetWeaponDto
    {
        public int Damage { get; set; }
        public string name { get; set; } = string.Empty;
    }
}