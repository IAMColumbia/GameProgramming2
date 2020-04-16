using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using JSONOjbectMap;

namespace Tests
{
    public class TestScriptGameManagerPlayode
    {
        TestTextReaders tr;
        Pool pool;

        // A Test behaves as an ordinary method
        [Test]
        public void TestScriptGameManagerPlayodeSimplePasses()
        {
            // Use the Assert class to test conditions
            //Arrange
            var textFile = Resources.Load<TextAsset>("JSONMap");
            var testObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/JOSNTestObject"));

            tr = testObject.GetComponent<TestTextReaders>();
            pool = testObject.GetComponent<Pool>();

            var pac = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PacMan"));

            //Act
            pool.CreateObjectPools();
            tr.Manager = new GhostManager(pac);
            tr.Manager.State = GhostManager.GhostManagerState.Loading;
            tr.Manager.Initialize();

            //Assert
            Assert.AreEqual(tr.Manager.jsonGhostParser.Json.ToString(), textFile.ToString());
            Assert.AreEqual(tr.Manager.Ghosts.Count, 6);
            Assert.AreEqual(tr.Manager.Ghosts[0].transform.position.x, 1);
            Assert.AreEqual(tr.Manager.Ghosts[0].transform.position.y, 1);
            Assert.AreEqual(tr.Manager.Ghosts[1].transform.position.x, 2);
            Assert.AreEqual(tr.Manager.Ghosts[1].transform.position.y, 1);
            Assert.AreEqual(tr.Manager.Ghosts[2].transform.position.x, 3);
            Assert.AreEqual(tr.Manager.Ghosts[2].transform.position.y, 1);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestScriptGameManagerPlayodeWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
