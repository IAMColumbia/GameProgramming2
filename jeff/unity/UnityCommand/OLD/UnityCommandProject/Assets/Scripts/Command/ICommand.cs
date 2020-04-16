using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConsoleCommandWUndo
{
    /// <summary>
    /// ICommand Exceutes on a GameComponent
    /// </summary>
    public interface ICommand
    {
        void Execute(GameObject gc);
    }
}
