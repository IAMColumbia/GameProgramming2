using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ObserverTestScript
    {

        GameObject pacManPrefab;
        GameObject ghostPrefab;
        GhostSprite ghostSprite;
        Player player;

        public ObserverTestScript()
        {
            pacManPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PacMan"));
            player = pacManPrefab.GetComponent<Player>();
            player.PacMan = new UnityPacMan(pacManPrefab);

            ghostPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Ghost"));
            ghostSprite = ghostPrefab.GetComponent<GhostSprite>();
            ghostSprite.Awake();
            ghostSprite.PacMan = pacManPrefab;
        }

        
        [Test]
        public void TestObserverOringinalState()
        {
            // Use the Assert class to test conditions

            //Arrage
            player.PacMan = new UnityPacMan(pacManPrefab);
            PacManState originalPacState = player.PacMan.State;
            ghostSprite.PacMan = pacManPrefab;
            GhostState oringinalGhostState = ghostSprite.State;
            //Act
            //Nothing

            //Assert
            Assert.AreEqual(originalPacState, PacManState.Still);
            Assert.AreEqual(oringinalGhostState, GhostState.Roving);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestObserverPacToSuperState()
        {
            // Use the Assert class to test conditions

            //Arrage
            ghostSprite.Ghost.Attach(player.PacMan);
            GhostState oringinalGhostState = ghostSprite.State;

            player.PacMan.State = PacManState.Chomping;
            PacManState originalPacState = player.PacMan.State;

            PacManState afterExpectedPacState = PacManState.SuperPacMan;
            GhostState afterExpectedGhostState = GhostState.Evading;

            //Act
            player.PacMan.State = PacManState.SuperPacMan; //should also change ghost state
            PacManState afterPacState = player.PacMan.State;
            GhostState afterGhostState = ghostSprite.Ghost.State;

            //Assert
            Assert.AreEqual(afterExpectedPacState, afterPacState);
            Assert.AreEqual(afterExpectedGhostState, afterGhostState);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ObserverTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions

            //Arrange
            int expected, actual;
            //Act
            expected = 1;
            actual = 1;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ObserverTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
