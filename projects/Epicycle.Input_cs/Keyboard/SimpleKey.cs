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

using System;

namespace Epicycle.Input.Keyboard
{
    public sealed class SimpleKey<TKeyId> : ISimpleKey<TKeyId>
    {
        public readonly IKeyboard<TKeyId> _keyboard;
        public readonly TKeyId _keyId;

        public SimpleKey(IKeyboard<TKeyId> keyboard, TKeyId keyId)
        {
            _keyboard = keyboard;
            _keyId = keyId;

            keyboard.OnKeyEvent += OnKeyEvent;

            IsPressed = false;
        }

        public event EventHandler<TKeyId> OnKeyPress;

        public IKeyboard<TKeyId> Keyboard
        {
            get { return _keyboard; }
        }

        public TKeyId KeyId
        {
            get { return _keyId; }
        }

        public bool IsPressed { get; private set; }

        private void OnKeyEvent(object sender, KeyEventArgs<TKeyId> eventArgs)
        {
            if (eventArgs.NewState == KeyState.Repeat)
            {
                return;
            }

            if (!eventArgs.KeyId.Equals(KeyId))
            {
                return; // Not our key
            }

            var newState = IsPressed;

            if (eventArgs.NewState == KeyState.Pressed)
            {
                newState = true;
            }
            else if (eventArgs.NewState == KeyState.Released)
            {
                newState = false;
            }

            var shouldSendEvent = !IsPressed && newState;

            IsPressed = newState;

            if (shouldSendEvent && OnKeyPress != null)
            {
                OnKeyPress(this, KeyId);
            }
        }
    }
}
