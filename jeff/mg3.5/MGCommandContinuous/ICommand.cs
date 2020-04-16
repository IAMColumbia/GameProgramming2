using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleCommandWUndo
{
    /// <summary>
    /// ICommand Exceutes on a GameComponent
    /// </summary>
    public interface ICommand
    {
        void Execute(GameComponent gc);
    }
}
