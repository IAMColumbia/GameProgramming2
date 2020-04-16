using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGCommand
{
    /// <summary>
    /// Interface to hold all the methods executed by Commands on GameComponents
    /// </summary>
    public interface ICommandComponent
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

    }

    /// <summary>
    /// Base Methods fot Stratedgy Pattern if classes can inherit from CommandComponent
    /// </summary>
    class CommandComponent : ICommandComponent
    {
        public  virtual void MoveDown()
        {
            throw new NotImplementedException();
        }

        public virtual void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public virtual void MoveRight()
        {
            throw new NotImplementedException();
        }

        public virtual void MoveUp()
        {
            throw new NotImplementedException();
        }
    }
}
