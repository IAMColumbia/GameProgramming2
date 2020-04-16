using System.Collections;
using System.Collections.Generic;
using JSONOjbectMap;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameManagerTests
    {

       
        TestTextReaders tr;
        Pool pool;
        
        // A Test behaves as an ordinary method
        [Test]
        public void GameManagerTestsSimplePasses()
        {
            //Arrange
            

            var textFile = Resources.Load<TextAsset>("JSONMap");

            var testObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/JOSNTestObject"));

            tr = testObject.GetComponent<TestTextReaders>();
            pool = testObject.GetComponent<Pool>();

            var pac = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/PacMan"));

            //Act
            //pool.CreateObjectPools();
            //tr.Manager = new GhostManager(pac);
            //tr.Manager.State = GhostManager.GhostManagerState.Loading;
            //tr.Manager.Initialize();
            

            //Assert
            Assert.AreEqual(pool.MaxPoolSize, 20);


        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GameManagerTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
