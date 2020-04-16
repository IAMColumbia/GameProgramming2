using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecoratorSample;

namespace UnitTestMathDecorator
{
    [TestClass]
    public class UnitTestMathDecorator
    {
        MathDecorator md;
        IMathComponent a1;
        IMathComponent a2;
        IMathComponent a3;

        public UnitTestMathDecorator()
        {
            md = new MathDecorator();
            a1 = new AddOne();
            a2 = new AddTwo();
            a3 = new AddThree();

        }


        [TestMethod]
        public void TestMathAddComponent()
        {
            //Arrange
            int addOneResult, addTwoResult, addThreeResult;
            int addOneExpected, addTwoExpected, addThreeExpected;
            addOneExpected = 1;
            addTwoExpected = 2;
            addThreeExpected = 3;
            //Act
            md = new MathDecorator();
            md.AddComponent(a1);
            addOneResult = md.Calculate();
            md = new MathDecorator();  //reset
            md.AddComponent(a2);
            addTwoResult = md.Calculate();
            md = new MathDecorator();
            md.AddComponent(a3);
            addThreeResult = md.Calculate();

            //Assert
            Assert.AreEqual(addOneExpected, addOneResult);
            Assert.AreEqual(addTwoExpected, addTwoResult);
            Assert.AreEqual(addThreeExpected, addThreeResult);
        }

        [TestMethod]
        public void TestMathAddRemoveComponent()
        {
            //Arrange
            int addOneResult, addTwoResult, addThreeResult;
            int addOneExpected, addTwoExpected, addThreeExpected;
            addOneExpected = 1;
            addTwoExpected = 2;
            addThreeExpected = 3;
            //Act
            md = new MathDecorator();
            md.AddComponent(a1);
            md.AddComponent(a1);
            md.RemoveComponent(a1);
            addOneResult = md.Calculate();
            md = new MathDecorator();
            md.AddComponent(a1);
            md.RemoveComponent(a1);
            md.AddComponent(a2);
            addTwoResult = md.Calculate();
            md = new MathDecorator();
            md.AddComponent(a1);
            md.AddComponent(a2);
            md.AddComponent(a3);
            md.RemoveComponent(a1);
            md.RemoveComponent(a2);
            addThreeResult = md.Calculate();

            //Assert
            Assert.AreEqual(addOneExpected, addOneResult);
            Assert.AreEqual(addTwoExpected, addTwoResult);
            Assert.AreEqual(addThreeExpected, addThreeResult);
        }

        [TestMethod]
        public void TestMathAddMultipleComponent()
        {
            //Arrange
            int addAllResult;
            int addAllExpected;
            
            addAllExpected = 1+2+3;
            //Act
            md = new MathDecorator();
            md.AddComponent(a1);
            md.AddComponent(a2);
            md.AddComponent(a3);
            addAllResult = md.Calculate();

            //Assert
            Assert.AreEqual(addAllExpected, addAllResult);
        }
    }
}
