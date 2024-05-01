using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shiza.Scripts
{
    public interface IChar
    {
        public void MoveCharacter(Vector2 pos, Vector2 scale);
    }
}
