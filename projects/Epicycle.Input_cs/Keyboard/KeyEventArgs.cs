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
    public sealed class KeyEventArgs<TKeyId, TAdditionalKeyEventData> : EventArgs
    {
        private readonly TKeyId _keyId;
        private readonly KeyEventType _eventType;
        private readonly TAdditionalKeyEventData _additionalData;

        public KeyEventArgs(TKeyId keyId, KeyEventType eventType, TAdditionalKeyEventData additionalData)
        {
            _keyId = keyId;
            _eventType = eventType;
            _additionalData = additionalData;
        }

        public TKeyId KeyId { get { return _keyId; } }

        public KeyEventType EventType { get { return _eventType; } }

        public TAdditionalKeyEventData AdditionalData { get { return _additionalData; } }
    }
}
