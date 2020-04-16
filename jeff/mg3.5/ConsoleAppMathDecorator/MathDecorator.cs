using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorSample
{
    public class MathDecorator : IMathComponent
    {
        List<IMathComponent> Maths; //UK 

        int solution;

        public MathDecorator()
        {
            Maths = new List<IMathComponent>();
        }

        public int decoratorCalculate(int input)
        {
            foreach (var item in Maths)
            {
                solution += item.Calculate();
            }
            return solution;
        }

        public int Calculate()
        {
            return this.decoratorCalculate(solution);
        }

        public void AddComponent(IMathComponent compenent)
        {
            this.Maths.Add(compenent);
        }

        public void RemoveComponent(IMathComponent component)
        {
            this.Maths.Remove(component);
        }
    }
}
