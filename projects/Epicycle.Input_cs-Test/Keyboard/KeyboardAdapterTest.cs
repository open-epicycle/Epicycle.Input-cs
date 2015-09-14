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
using System;

namespace Epicycle.Input.Keyboard
{
    [TestFixture]
    public class KeyboardAdapterTest
    {
        private Mock<IKeyboard<int, int>> _sourceKeyboardMock;
        private KeyboardAdapter<string, int, int> _keyboardAdapter;
        private KeyEventArgs<string, int> _keyEvent;

        [SetUp]
        public void SetUp()
        {
            ResetKeyEvent();

            _sourceKeyboardMock = KeyboardTestUtils.CreateKeyboardMock();
            _keyboardAdapter = new KeyboardAdapter<string, int, int>(_sourceKeyboardMock.Object);

            _keyboardAdapter.MapKey("a", 10);
            _keyboardAdapter.MapKey("b", 20);
            _keyboardAdapter.MapKey("c", 30);

            _keyboardAdapter.OnKeyEvent += (sender, eventArgs) => _keyEvent = eventArgs;
        }

        private void ResetKeyEvent()
        {
            _keyEvent = null;
        }

        private void AssertEventAndReset(string expectedKeyId, KeyEventType expectedEventType)
        {
            Assert.That(_keyEvent, Is.Not.Null);
            Assert.That(_keyEvent.KeyId, Is.EqualTo(expectedKeyId));
            Assert.That(_keyEvent.EventType, Is.EqualTo(expectedEventType));
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
            Assert.That(_keyboardAdapter.SourceKeyboard, Is.SameAs(_sourceKeyboardMock.Object));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void MapKey_existing_source_throws_excpetion()
        {
            _keyboardAdapter.MapKey("x", 20);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void MapKey_existing_destination_throws_excpetion()
        {
            _keyboardAdapter.MapKey("b", 100);
        }

        [Test]
        public void GetKeyState_retuns_source_state()
        {
            _sourceKeyboardMock.SetKeyState(20, KeyEventType.Pressed);
            Assert.That(_keyboardAdapter.GetKeyState("b"), Is.EqualTo(KeyEventType.Pressed));
        }

        [Test]
        [ExpectedException(typeof(NoSuchKeyExcpetion))]
        public void GetKeyState_non_mapped_key_throws_exception()
        {
            _sourceKeyboardMock.SetKeyState(20, KeyEventType.Pressed);
            _keyboardAdapter.GetKeyState("x");
        }

        [Test]
        public void IsThisKeyMapped_mapped_key_returns_true()
        {
            Assert.That(_keyboardAdapter.IsThisKeyMapped("b"), Is.True);
        }

        [Test]
        public void IsThisKeyMapped_unmapped_key_returns_false()
        {
            Assert.That(_keyboardAdapter.IsThisKeyMapped("x"), Is.False);
        }

        [Test]
        public void IsSourceKeyMapped_mapped_key_returns_true()
        {
            Assert.That(_keyboardAdapter.IsSourceKeyMapped(20), Is.True);
        }

        [Test]
        public void IsSourceKeyMapped_unmapped_key_returns_false()
        {
            Assert.That(_keyboardAdapter.IsSourceKeyMapped(100), Is.False);
        }

        [Test]
        public void ToThisKeyId_returns_the_mapped_source_key_id()
        {
            Assert.That(_keyboardAdapter.ToThisKeyId(20), Is.EqualTo("b"));
        }

        [Test]
        [ExpectedException(typeof(NoSuchKeyExcpetion))]
        public void ToThisKeyId_unmapped_source_key_id_throws()
        {
            _keyboardAdapter.ToThisKeyId(100);
        }

        [Test]
        public void ToSourceKeyId_returns_the_mapped_key_id()
        {
            Assert.That(_keyboardAdapter.ToSourceKeyId("b"), Is.EqualTo(20));
        }

        [Test]
        [ExpectedException(typeof(NoSuchKeyExcpetion))]
        public void ToSourceKeyId_unmapped_key_id_throws()
        {
            _keyboardAdapter.ToSourceKeyId("x");
        }

        [Test]
        public void OnSourceKeyEvent_key_event_propogates_for_mapped_keys()
        {
            _sourceKeyboardMock.SendKeyEvent(20, KeyEventType.Released);
            AssertEventAndReset("b", KeyEventType.Released);
        }

        [Test]
        public void OnSourceKeyEvent_key_event_doesnt_propogate_for_unmapped_keys()
        {
            _sourceKeyboardMock.SendKeyEvent(100, KeyEventType.Released);
            AssertNoEventAndReset();
        }
    }
}
