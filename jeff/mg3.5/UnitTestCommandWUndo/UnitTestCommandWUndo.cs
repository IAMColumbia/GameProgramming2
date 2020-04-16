using ConsoleCommand;
using ConsoleCommandWUndo;
using ConsoleCommandWUndo.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCommandWUndo
{
    [TestClass]
    public class UnitTestCommandWUndo
    {
        GameComponent fakeComponentReceiver;
        ConsoleCommandProcessor proc;

        public UnitTestCommandWUndo()
        {
            fakeComponentReceiver = new GameComponent();
            proc = new ConsoleCommandProcessor();
        }

        [TestMethod]
        public void TestCommandProcessorStack()
        {
            //Arrange
            int orignalLocationY = proc.FakeComponentReceiver.Y;
            int orignalLocationX = proc.FakeComponentReceiver.X;
            int expectedMoveAmountVertical = 0;
            int expectedMoveAmountHorizonal = 0;
            int expectedStackSize = 4;
            int finalLocationY;
            int finalLocationX;
            //Act
            proc.ProcessCommand(new MoveUpCommand());
            proc.ProcessCommand(new MoveDownCommand());
            proc.ProcessCommand(new MoveLeftCommand());
            proc.ProcessCommand(new MoveRightCommand());
            finalLocationX = proc.FakeComponentReceiver.X;
            finalLocationY = proc.FakeComponentReceiver.Y;

            //Assert
            Assert.AreEqual(expectedStackSize, proc.Commands.Count);
            Assert.AreEqual(expectedMoveAmountHorizonal + orignalLocationX, finalLocationX);
            Assert.AreEqual(expectedMoveAmountVertical + orignalLocationY, finalLocationY);
        }

        [TestMethod]
        public void TestCommandProcessorStackWUndo()
        {
            //Arrange
            int orignalLocationY = proc.FakeComponentReceiver.Y;
            int orignalLocationX = proc.FakeComponentReceiver.X;
            int expectedMoveAmountVertical = 0;
            int expectedMoveAmountHorizonal = 0;
            int expectedStackSize = 4;
            int finalLocationY;
            int finalLocationX;
            //Act
            proc.ProcessCommand(new MoveUpCommand());
            proc.ProcessCommand(new MoveDownCommand());
            proc.ProcessCommand(new MoveUpCommand().UndoCommand);
            proc.ProcessCommand(new MoveDownCommand().UndoCommand);
            proc.ProcessCommand(new MoveLeftCommand());
            proc.ProcessCommand(new MoveRightCommand());
            proc.ProcessCommand(new MoveLeftCommand().UndoCommand);
            proc.ProcessCommand(new MoveRightCommand().UndoCommand);
            finalLocationX = proc.FakeComponentReceiver.X;
            finalLocationY = proc.FakeComponentReceiver.Y;

            //Assert
            Assert.AreEqual(expectedStackSize, proc.Commands.Count);
            Assert.AreEqual(expectedMoveAmountHorizonal + orignalLocationX, finalLocationX);
            Assert.AreEqual(expectedMoveAmountVertical + orignalLocationY, finalLocationY);
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
