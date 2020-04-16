using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecoratorSample;

namespace UnitTestMathDecorator
{
    [TestClass]
    public class UnitTestMathComponentDecorator
    {

        MathComponentDecoratorNode mc;

        public UnitTestMathComponentDecorator()
        {
            mc = new MathComponentDecoratorNode();
        }

        [TestMethod]
        public void TestMathAddComponentNoDecorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 0;
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd1Decorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 1;
            mc.AddComponent(new MathComponentDecoratorNodeAdd1());
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd2Decorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 2;
            mc.AddComponent(new MathComponentDecoratorNodeAdd2());
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd3Decorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 3;
            mc.AddComponent(new MathComponentDecoratorNodeAdd3());
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd1ThreeTimesDecorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 3;
            mc.AddComponent(new MathComponentDecoratorNodeAdd1());
            mc.AddComponent(new MathComponentDecoratorNodeAdd1());
            mc.AddComponent(new MathComponentDecoratorNodeAdd1());
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd1Add2Add3Decorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 3 + 2 + 1;
            mc.AddComponent(new MathComponentDecoratorNodeAdd1());
            mc.AddComponent(new MathComponentDecoratorNodeAdd2());
            mc.AddComponent(new MathComponentDecoratorNodeAdd3());
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void TestMathAddComponentAdd1Add2Add3ArrayDecorator()
        {
            //Arrange
            int Expected, Result;
            //Act
            Expected = 3 + 2 + 1;
            mc.AddComponent(
                new List<IMathComponentDecoratorNode>()
                {   
                    new MathComponentDecoratorNodeAdd1(),
                    new MathComponentDecoratorNodeAdd2(), 
                    new MathComponentDecoratorNodeAdd3()
                }
            );
            Result = mc.Calculate();
            //Assert
            Assert.AreEqual(Expected, Result);
        }
    }
}
