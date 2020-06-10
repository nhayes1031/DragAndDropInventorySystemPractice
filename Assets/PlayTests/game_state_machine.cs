using a_player;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }

        [TearDown]
        public void teardown()
        {
            GameObject.Destroy(Object.FindObjectOfType<GameStateMachine>());
        }

        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);

            PlayButton.LevelToLoad = "Level1";
            yield return null;

            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator switches_to_play_state_when_loading_level_is_completed()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);

            PlayButton.LevelToLoad = "Level1";
            yield return null;

            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);

            yield return new WaitUntil(() => stateMachine.CurrentStateType == typeof(Play));

            Assert.AreEqual(typeof(Play), stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exist()
        {
            var firstGameStateMachine = new GameObject("FirstStateMachine");
            firstGameStateMachine.AddComponent<GameStateMachine>();
            var secondGameStateMachine = new GameObject("SecondGameStateMachine");
            secondGameStateMachine.AddComponent<GameStateMachine>();

            yield return null;

            Assert.IsTrue(secondGameStateMachine == null);
            Assert.IsNotNull(firstGameStateMachine);
        }

        [UnityTest]
        public IEnumerator switches_from_play_to_pause_when_pause_button_pressed()
        {
            yield return Helpers.LoadMenuScene();
            var stateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            PlayButton.LevelToLoad = "Level1";

            yield return new WaitUntil(() => stateMachine.CurrentStateType == typeof(Play));

            // hit pause
            PlayerInput.Instance.PausedPressed.Returns(true);
            yield return null;

            Assert.AreEqual(typeof(Pause), stateMachine.CurrentStateType);
        }

    }
}
