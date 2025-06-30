using System.Collections;
using Cars.Car;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayMode
{
    public class CarViewTests
    {
        private GameObject _carGo;
        private CarView _carView;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _carGo = new GameObject("Car");
            _carGo.tag = "Untagged";
            _carGo.AddComponent<BoxCollider2D>();

            var rb = _carGo.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic; // Important: prevent it from falling or moving

            _carView = _carGo.AddComponent<CarView>();

            yield return null;
        }


        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_carGo != null)
                Object.Destroy(_carGo);
            yield return null;
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter2D_DestructionZone_InvokesEvent()
        {
            var wasCalled = false;
            _carView.OnEnteredDestructionZone += () => wasCalled = true;

            var zone = new GameObject("DestructionZone");
            zone.tag = "DestructionZone";
            zone.AddComponent<BoxCollider2D>().isTrigger = true;
            zone.transform.position = _carGo.transform.position;
            
            // Wait for physics to run
            yield return new WaitForFixedUpdate();

            Physics2D.Simulate(Time.fixedDeltaTime);

            Assert.IsTrue(wasCalled);
            Object.Destroy(zone);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter2D_Player_InvokesEvent()
        {
            var wasCalled = false;
            _carView.OnPlayerCollision += () => wasCalled = true;

            var player = new GameObject("Player");
            player.tag = "Player";
            player.AddComponent<BoxCollider2D>().isTrigger = true;
            player.transform.position = _carGo.transform.position;
            
            // Wait for physics to run
            yield return new WaitForFixedUpdate();

            Physics2D.Simulate(Time.fixedDeltaTime);

            Assert.IsTrue(wasCalled);
            Object.Destroy(player);
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter2D_OtherTag_DoesNotInvoke()
        {
            var anyCalled = false;
            _carView.OnEnteredDestructionZone += () => anyCalled = true;
            _carView.OnPlayerCollision += () => anyCalled = true;

            var other = new GameObject("Other");
            other.tag = "Untagged";
            other.AddComponent<BoxCollider2D>().isTrigger = true;
            other.transform.position = _carGo.transform.position;
            
            // Wait for physics to run
            yield return new WaitForFixedUpdate();

            Physics2D.Simulate(Time.fixedDeltaTime);

            Assert.IsFalse(anyCalled);
            Object.Destroy(other);
        }

        [UnityTest]
        public IEnumerator Destroy_RemovesGameObject()
        {
            _carView.Destroy();
            yield return null;

            Assert.IsTrue(_carView == null || _carView.gameObject == null);
        }
    }
}
