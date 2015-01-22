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
using System.Collections.Generic;

namespace Epicycle.Input.Keyboard
{
    public sealed class KeyboardAdapter<TKeyId, TSourceKeyId> : IKeyboardAdapter<TKeyId, TSourceKeyId>
    {
        private readonly IKeyboard<TSourceKeyId> _sourceKeyboard;
        private readonly Dictionary<TSourceKeyId, TKeyId> _sourceToThisKeyMapping;
        private readonly Dictionary<TKeyId, TSourceKeyId> _thisToSourceKeyMapping;

        public KeyboardAdapter(IKeyboard<TSourceKeyId> sourceKeyboard)
        {
            _sourceKeyboard = sourceKeyboard;

            _sourceToThisKeyMapping = new Dictionary<TSourceKeyId, TKeyId>();
            _thisToSourceKeyMapping = new Dictionary<TKeyId, TSourceKeyId>();

            _sourceKeyboard.OnKeyEvent += OnSourceKeyEvent;
        }

        public event EventHandler<KeyEventArgs<TKeyId>> OnKeyEvent;

        public IKeyboard<TSourceKeyId> SourceKeyboard
        {
            get { return _sourceKeyboard; }
        }

        public void MapKey(TKeyId keyId, TSourceKeyId sourceKeyId)
        {
            if (_thisToSourceKeyMapping.ContainsKey(keyId))
            {
                throw new ArgumentException(string.Format("The key {0} is already mapped!", keyId));
            }

            if (_sourceToThisKeyMapping.ContainsKey(sourceKeyId))
            {
                throw new ArgumentException(string.Format("The source key {0} is already mapped!", sourceKeyId));
            }

            _sourceToThisKeyMapping[sourceKeyId] = keyId;
            _thisToSourceKeyMapping[keyId] = sourceKeyId;
        }

        public KeyEventType GetKeyState(TKeyId keyId)
        {
            return _sourceKeyboard.GetKeyState(ToSourceKeyId(keyId));
        }

        public bool IsThisKeyMapped(TKeyId keyId)
        {
            return _thisToSourceKeyMapping.ContainsKey(keyId);
        }

        public bool IsSourceKeyMapped(TSourceKeyId sourceKeyId)
        {
            return _sourceToThisKeyMapping.ContainsKey(sourceKeyId);
        }

        public TKeyId ToThisKeyId(TSourceKeyId sourceKeyId)
        {
            if (!_sourceToThisKeyMapping.ContainsKey(sourceKeyId))
            {
                throw new NoSuchKeyExcpetion(sourceKeyId.ToString());
            }

            return _sourceToThisKeyMapping[sourceKeyId];
        }

        public TSourceKeyId ToSourceKeyId(TKeyId keyId)
        {
            if (!_thisToSourceKeyMapping.ContainsKey(keyId))
            {
                throw new NoSuchKeyExcpetion(keyId.ToString());
            }

            return _thisToSourceKeyMapping[keyId];
        }

        private void OnSourceKeyEvent(object sender, KeyEventArgs<TSourceKeyId> eventArgs)
        {
            if (OnKeyEvent != null && IsSourceKeyMapped(eventArgs.KeyId))
            {
                var keyId = ToThisKeyId(eventArgs.KeyId);

                OnKeyEvent(this, new KeyEventArgs<TKeyId>(keyId, eventArgs.EventType));
            }
        }
    }
}
