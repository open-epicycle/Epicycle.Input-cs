// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Input-cs
// ]]]]

using Moq;
using NUnit.Framework;

namespace Epicycle.Input.Keyboard
{
    [TestFixture]
    public class ToggleKeyTest
    {
        private Mock<IKeyboard<int>> _keyboardMock;
        private int _keyId;
        private ToggleKey<int> _key;
        private ToggleKeyEventArgs<int> _keyEvent;

        [SetUp]
        public void SetUp()
        {
            ResetKeyEvent();

            _keyboardMock = KeyboardTestUtils.CreateKeyboardMock();

            _keyId = 123;
            _key = new ToggleKey<int>(_keyboardMock.Object, _keyId, true);

            _key.OnStateChange += (sender, eventArgs) => _keyEvent = eventArgs;
        }

        private void PressKey()
        {
            _keyboardMock.SendKeyEvent(_keyId, KeyEventType.Pressed);
        }

        private void RepeastKey()
        {
            _keyboardMock.SendKeyEvent(_keyId, KeyEventType.Repeat);
        }

        private void PressWrongKey()
        {
            _keyboardMock.SendKeyEvent(200, KeyEventType.Pressed);
        }

        private void ReleaseKey()
        {
            _keyboardMock.SendKeyEvent(_keyId, KeyEventType.Released);
        }

        private void ResetKeyEvent()
        {
            _keyEvent = null;
        }

        private void AssertEventAndReset(bool expectedState)
        {
            Assert.That(_keyEvent, Is.Not.Null);
            Assert.That(_keyEvent.KeyId, Is.EqualTo(_keyId));
            Assert.That(_keyEvent.NewState, Is.EqualTo(expectedState));
            ResetKeyEvent();
        }

        private void AssertNoEventAndReset()
        {
            Assert.That(_keyEvent, Is.Null);
            ResetKeyEvent();
        }

        [Test]
        public void ctor_sets_properties_correctly()
        {
            Assert.That(_key.Keyboard, Is.SameAs(_keyboardMock.Object));
            Assert.That(_key.KeyId, Is.EqualTo(_keyId));
        }

        [Test]
        public void IsToggled_equal_initial_state_upon_construction()
        {
            Assert.That(_key.IsToggled, Is.True);
        }

        [Test]
        public void IsToggled_flips_state_after_key_press()
        {
            PressKey();
            Assert.That(_key.IsToggled, Is.False);
        }

        [Test]
        public void IsToggled_state_remains_after_key_release()
        {
            PressKey();
            ReleaseKey();
            Assert.That(_key.IsToggled, Is.False);
        }

        [Test]
        public void IsToggled_flips_state_back_after_two_key_press()
        {
            PressKey();
            ReleaseKey();
            PressKey();
            ReleaseKey();
            Assert.That(_key.IsToggled, Is.True);
        }

        [Test]
        public void IsToggled_wrong_key_press_doesnt_affect_state()
        {
            PressWrongKey();
            Assert.That(_key.IsToggled, Is.True);
        }

        [Test]
        public void IsToggled_repeat_key_doesnt_affect_state()
        {
            RepeastKey();
            Assert.That(_key.IsToggled, Is.True);
        }

        [Test]
        public void OnStateChange_key_press_sends_event()
        {
            PressKey();
            AssertEventAndReset(false);
        }

        [Test]
        public void OnStateChange_pressed_key_release_doesn_send_event()
        {
            PressKey();
            ResetKeyEvent();
            ReleaseKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnStateChange_wrong_key_pressed_doesnt_send_event()
        {
            PressWrongKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnStateChange_repeat_key_doesnt_send_event()
        {
            RepeastKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnStateChange_pressed_key_twice_sends_correct_state_in_event()
        {
            PressKey();
            ResetKeyEvent();
            ReleaseKey();
            PressKey();
            AssertEventAndReset(true);
        }
    }
}
