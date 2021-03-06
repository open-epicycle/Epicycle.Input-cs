﻿// [[[[INFO>
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
    public sealed class ToggleKey<TKeyId, TAdditionalKeyEventData> : IToggleKey<TKeyId, TAdditionalKeyEventData>
    {
        private readonly SimpleKey<TKeyId, TAdditionalKeyEventData> _key;

        public ToggleKey(IKeyboard<TKeyId, TAdditionalKeyEventData> keyboard, TKeyId keyId, bool initialState = false)
        {
            _key = new SimpleKey<TKeyId, TAdditionalKeyEventData>(keyboard, keyId);

            IsToggled = initialState;

            _key.OnKeyPress += OnKeyPress;
        }

        private void OnKeyPress(object sender, SimpleKeyEventArgs<TKeyId, TAdditionalKeyEventData> eventArgs)
        {
            IsToggled = !IsToggled;

            if (OnStateChange != null)
            {
                OnStateChange(this, new ToggleKeyEventArgs<TKeyId, TAdditionalKeyEventData>(_key.KeyId, IsToggled, eventArgs.AdditionalData));
            }
        }

        public IKeyboard<TKeyId, TAdditionalKeyEventData> Keyboard
        {
            get { return _key.Keyboard; }
        }

        public TKeyId KeyId
        {
            get { return _key.KeyId; }
        }

        public bool IsToggled { get; private set; }

        public event EventHandler<ToggleKeyEventArgs<TKeyId, TAdditionalKeyEventData>> OnStateChange;
    }
}
