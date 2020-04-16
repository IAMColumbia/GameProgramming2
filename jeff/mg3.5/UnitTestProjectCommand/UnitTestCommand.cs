using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleCommand;
using ConsoleCommand.Commands;

namespace UnitTestProjectCommand
{
    [TestClass]
    public class UnitTestCommand
    {

        GameComponent fakeComponentReceiver;

        public UnitTestCommand()
        {
            fakeComponentReceiver = new GameComponent();
        }

        [TestMethod]
        public void TestCommandMoveUp()
        {
            //Arrange
            int orignalLocationY = fakeComponentReceiver.Y;
            Command moveUP = new MoveUpCommand();
            int finalLocationY;
            int expectedMoveAmount = 1;
            //Act
            moveUP.Execute(fakeComponentReceiver);
            finalLocationY = fakeComponentReceiver.Y;
            //Assert
            Assert.AreEqual(finalLocationY, orignalLocationY + expectedMoveAmount);

        }

        [TestMethod]
        public void TestCommandMoveDown()
        {
            //Arrange
            int orignalLocationY = fakeComponentReceiver.Y;
            Command moveDown = new MoveDownCommand();
            int finalLocationY;
            int expectedMoveAmount = -1;
            //Act
            moveDown.Execute(fakeComponentReceiver);
            finalLocationY = fakeComponentReceiver.Y;
            //Assert
            Assert.AreEqual(finalLocationY, orignalLocationY + expectedMoveAmount);

        }

        [TestMethod]
        public void TestCommandMoveLeft()
        {
            //Arrange
            int orignalLocationX = fakeComponentReceiver.X;
            Command moveLeft = new MoveLeftCommand();
            int finalLocationX;
            int expectedMoveAmount = -1;
            //Act
            moveLeft.Execute(fakeComponentReceiver);
            finalLocationX = fakeComponentReceiver.X;
            //Assert
            Assert.AreEqual(finalLocationX, orignalLocationX + expectedMoveAmount);

        }

        [TestMethod]
        public void TestCommandMoveRight()
        {
            //Arrange
            int orignalLocationX = fakeComponentReceiver.X;
            Command moveRight = new MoveRightCommand();
            int finalLocationX;
            int expectedMoveAmount = 1;
            //Act
            moveRight.Execute(fakeComponentReceiver);
            finalLocationX = fakeComponentReceiver.X;
            //Assert
            Assert.AreEqual(finalLocationX, orignalLocationX + expectedMoveAmount);

        }
    }
}
