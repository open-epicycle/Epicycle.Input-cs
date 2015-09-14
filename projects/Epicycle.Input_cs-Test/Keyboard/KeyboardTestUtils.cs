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

namespace Epicycle.Input.Keyboard
{
    internal static class KeyboardTestUtils
    {
        public static Mock<IKeyboard<int, int>> CreateKeyboardMock()
        {
            return new Mock<IKeyboard<int, int>>(MockBehavior.Strict);
        }

        public static void SendKeyEvent(this Mock<IKeyboard<int, int>> @this, int keyId, KeyEventType eventType)
        {
            @this.Raise(m => m.OnKeyEvent += null, @this.Object, new KeyEventArgs<int, int>(keyId, eventType, 234));
        }

        public static void SetKeyState(this Mock<IKeyboard<int, int>> @this, int keyId, KeyState state)
        {
            @this.Setup(m => m.GetKeyState(keyId)).Returns(state);
        }
    }
}
