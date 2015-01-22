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
    public class BidirectionalMovementKeysTest
    {
        private Mock<IKeyboard<int>> _keyboardMock;
        private BidirectionalMovementKeys<int> _bidirectionalMovementKeys;
        private BidirectionalMovement? _keyEvent;

        [SetUp]
        public void SetUp()
        {
            ResetKeyEvent();

            _keyboardMock = KeyboardTestUtils.CreateKeyboardMock();

            _bidirectionalMovementKeys = new BidirectionalMovementKeys<int>(_keyboardMock.Object, 123, 234);

            _bidirectionalMovementKeys.OnStateChange += (sender, eventArgs) => _keyEvent = eventArgs;
        }

        private void PressPositiveKey()
        {
            _keyboardMock.SendKeyEvent(123, KeyState.Pressed);
        }

        private void PressNegativeKey()
        {
            _keyboardMock.SendKeyEvent(234, KeyState.Pressed);
        }

        private void PressWrongKey()
        {
            _keyboardMock.SendKeyEvent(2, KeyState.Pressed);
        }

        private void ReleasePositiveKey()
        {
            _keyboardMock.SendKeyEvent(123, KeyState.Released);
        }

        private void ReleaseNegativeKey()
        {
            _keyboardMock.SendKeyEvent(234, KeyState.Released);
        }

        private void ResetKeyEvent()
        {
            _keyEvent = null;
        }

        private void AssertEventAndReset(BidirectionalMovement expectedState)
        {
            Assert.That(_keyEvent.HasValue, Is.True);
            Assert.That(_keyEvent.Value, Is.EqualTo(expectedState));
            ResetKeyEvent();
        }

        private void AssertNoEventAndReset()
        {
            Assert.That(_keyEvent.HasValue, Is.False);
            ResetKeyEvent();
        }

        [Test]
        public void ctor_sets_properties_correctly()
        {
            Assert.That(_bidirectionalMovementKeys.Keyboard, Is.SameAs(_keyboardMock.Object));
            Assert.That(_bidirectionalMovementKeys.PositiveDirectionKeyId, Is.EqualTo(123));
            Assert.That(_bidirectionalMovementKeys.NegativeDirectionKeyId, Is.EqualTo(234));
        }

        [Test]
        public void MovementDirection_still_upon_construction()
        {
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Still));
        }

        [Test]
        public void MovementDirection_posotive_key_press_sets_to_positive()
        {
            PressPositiveKey();
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Positive));
        }

        [Test]
        public void MovementDirection_negative_key_press_sets_to_negative()
        {
            PressNegativeKey();
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Negatize));
        }

        [Test]
        public void MovementDirection_negative_key_press_followed_by_positive_key_press_remain_negative()
        {
            PressNegativeKey();
            PressPositiveKey();
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Negatize));
        }

        [Test]
        public void MovementDirection_possitive_key_press_and_release_return_to_still()
        {
            PressPositiveKey();
            ReleasePositiveKey();
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Still));
        }

        [Test]
        public void MovementDirection_possitive_key_press_and_negative_key_release_remains_positive()
        {
            PressPositiveKey();
            ReleaseNegativeKey();
            Assert.That(_bidirectionalMovementKeys.MovementDirection, Is.EqualTo(BidirectionalMovement.Positive));
        }

        ////

        [Test]
        public void OnStateChange_posotive_key_press_sends_positive_state()
        {
            PressPositiveKey();
            AssertEventAndReset(BidirectionalMovement.Positive);
        }

        [Test]
        public void OnStateChange_negative_key_press_sends_negative_state()
        {
            PressNegativeKey();
            AssertEventAndReset(BidirectionalMovement.Negatize);
        }

        [Test]
        public void OnStateChange_negative_key_press_followed_by_positive_key_press_doesnt_raise_event()
        {
            PressNegativeKey();
            ResetKeyEvent();
            PressPositiveKey();
            AssertNoEventAndReset();
        }

        [Test]
        public void OnStateChange_possitive_key_press_and_release_return_to_still()
        {
            PressPositiveKey();
            ResetKeyEvent();
            ReleasePositiveKey();
            AssertEventAndReset(BidirectionalMovement.Still);
        }

        [Test]
        public void OnStateChange_possitive_key_press_and_negative_key_release_doesnt_raise_event()
        {
            PressPositiveKey();
            ResetKeyEvent();
            ReleaseNegativeKey();
            AssertNoEventAndReset();
        }
    }
}
