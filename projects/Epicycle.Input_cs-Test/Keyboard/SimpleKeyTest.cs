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
    public class SimpleKeyTest
    {
        private Mock<IKeyboard<int, int>> _keyboardMock;
        private int _keyId;
        private int _additionalData;
        private SimpleKey<int, int> _key;
        private SimpleKeyEventArgs<int, int> _keyEvent;

        [SetUp]
        public void SetUp()
        {
            ResetKeyEvent();

            _keyboardMock = KeyboardTestUtils.CreateKeyboardMock();

            _keyId = 123;
            _additionalData = 234;
            _key = new SimpleKey<int, int>(_keyboardMock.Object, _keyId);

            _key.OnKeyPress += (sender, keyId) => _keyEvent = keyId;
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

        private void ReleaseWrongKey()
        {
            _keyboardMock.SendKeyEvent(200, KeyEventType.Released);
        }

        private void ResetKeyEvent()
        {
            _keyEvent = null;
        }

        private void AssertEventAndReset()
        {
            Assert.That(_keyEvent, Is.Not.Null);
            Assert.That(_keyEvent.KeyId, Is.EqualTo(_keyId));
            Assert.That(_keyEvent.AdditionalData, Is.EqualTo(_additionalData));
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
        public void IsPressed_false_upon_construction()
        {
            Assert.That(_key.IsPressed, Is.False);
        }

        [Test]
        public void IsPressed_pressed_key_sets_to_true()
        {
            PressKey();
            Assert.That(_key.IsPressed, Is.True);
        }

        [Test]
        public void IsPressed_pressed_wrong_key_doesnt_affect()
        {
            PressWrongKey();
            Assert.That(_key.IsPressed, Is.False);

            PressKey();
            ReleaseWrongKey();
            Assert.That(_key.IsPressed, Is.True);
        }

        [Test]
        public void IsPressed_repeat_key_doesnt_affect()
        {
            RepeastKey();
            Assert.That(_key.IsPressed, Is.False);

            PressKey();
            ReleaseWrongKey();
            Assert.That(_key.IsPressed, Is.True);
        }

        [Test]
        public void IsPressed_released_unpressed_key_sets_to_false()
        {
            ReleaseKey();
            Assert.That(_key.IsPressed, Is.False);
        }

        [Test]
        public void IsPressed_pressed_already_pressed_key_remains_true()
        {
            PressKey();
            PressKey();
            Assert.That(_key.IsPressed, Is.True);
        }

        [Test]
        public void IsPressed_released_pressed_key_sets_to_false()
        {
            PressKey();
            ReleaseKey();
            Assert.That(_key.IsPressed, Is.False);
        }

        [Test]
        public void OnKeyPress_pressed_key_sends_event()
        {
            PressKey();
            AssertEventAndReset();
        }

        [Test]
        public void OnKeyPress_pressed_wrong_key_doesnt_send_event()
        {
            PressWrongKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnKeyPress_repeat_key_doesnt_send_event()
        {
            RepeastKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnKeyPress_repeat_pressed_key_sends_only_one_event()
        {
            PressKey();
            AssertEventAndReset();
            PressKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnKeyPress_released_unpressed_key_doesnt_send_event()
        {
            ReleaseKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnKeyPress_released_pressed_key_doesnt_send_event()
        {
            PressKey();
            AssertEventAndReset();
            ReleaseKey();
            AssertNoEventAndReset();
        }
    }
}
