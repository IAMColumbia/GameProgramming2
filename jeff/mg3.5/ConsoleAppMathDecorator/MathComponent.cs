using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorSample
{
    
    public interface IMathComponent
    {
        int Calculate();
    }

    public abstract class MathComponent : IMathComponent
    {   
        
        
        public virtual int Calculate()
        {
            return 0;
        }

    }

    public class AddOne : MathComponent
    {
        public override int Calculate()
        {
            return base.Calculate() + 1;
        }
    }

    public class AddTwo : MathComponent
    {
        public override int Calculate()
        {
            return base.Calculate() + 2;
        }
    }

    public class AddThree : MathComponent
    {
        public override int Calculate()
        {
            return base.Calculate() + 3;
        }
    }
}
