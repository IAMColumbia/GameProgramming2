using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorSample
{
    public interface IMathComponentDecoratorNode : IMathComponent
    {
        IMathComponentDecoratorNode Child { get; set; }
        void AddComponent(IMathComponentDecoratorNode compenent);
        void RemoveComponent(IMathComponentDecoratorNode compenent);

    }


    public class MathComponentDecoratorNode : IMathComponentDecoratorNode
    {

        public IMathComponentDecoratorNode Child { get; set; }

        protected int solution;

        public MathComponentDecoratorNode()
        {
           
            if(this is MathComponentDecoratorNodeEmpty)
            {
                //leave th child null
            }
            else
            {
                Child = new MathComponentDecoratorNodeEmpty();
            }
           
        }


        public int Calculate()
        {
            //Ask Child to calculate if its not emmpty
            if (this.Child.GetType() != typeof(MathComponentDecoratorNodeEmpty))
            {
                solution = decoratorCalculate() + Child.Calculate();
            }
            else
            {
                solution = decoratorCalculate(); //no child
            }
            return solution; 
        }

        protected virtual int decoratorCalculate()
        {
            return 0;
        }

        public void AddComponent(IMathComponentDecoratorNode compenent)
        {
            //If Child is Empty replace with added coponent
            if (this.Child.GetType() == typeof(MathComponentDecoratorNodeEmpty))
            {
                this.Child = compenent;
            }
            else
            {
                //find empty child
                this.Child.AddComponent(compenent);
            }
        }

        public void AddComponent(List<IMathComponentDecoratorNode> components)
        {
            foreach (var item in components)
            {
                this.AddComponent(item);
            }
            
        }

        public void RemoveComponent(IMathComponentDecoratorNode component)
        {
            this.Child = new MathComponentDecoratorNodeEmpty();
        }

        public int Calculate(int input)
        {
            return solution + 0;
        }
    }

    public class MathComponentDecoratorNodeEmpty : MathComponentDecoratorNode
    {
        public MathComponentDecoratorNodeEmpty()
        {
            this.Child = null; //null object pattern
        }
    }

    public class MathComponentDecoratorNodeAdd1 : MathComponentDecoratorNode
    {
        protected override int decoratorCalculate()
        {
            return base.decoratorCalculate() + 1;
        }
    }

    public class MathComponentDecoratorNodeAdd2 : MathComponentDecoratorNode
    {
        protected override int decoratorCalculate()
        {
            return base.decoratorCalculate() + 2;
        }
    }

    public class MathComponentDecoratorNodeAdd3 : MathComponentDecoratorNode
    {
        protected override int decoratorCalculate()
        {
            return base.decoratorCalculate() + 3;
        }
    }
}
