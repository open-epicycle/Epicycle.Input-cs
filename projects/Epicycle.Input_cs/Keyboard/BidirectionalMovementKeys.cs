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
    public sealed class BidirectionalMovementKeys<TKeyId>
    {
        private readonly IKeyboard<TKeyId> _keyboard;
        private readonly TKeyId _positiveDirectionKeyId;
        private readonly TKeyId _negativeDirectionKeyId;

        public BidirectionalMovementKeys(IKeyboard<TKeyId> keyboard, TKeyId positiveDirectionKeyId, TKeyId negativeDirectionKeyId)
        {
            _keyboard = keyboard;
            _positiveDirectionKeyId = positiveDirectionKeyId;
            _negativeDirectionKeyId = negativeDirectionKeyId;

            MovementDirection = BidirectionalMovement.Still;

            keyboard.OnKeyStateChange += OnKeyStateChange;
        }

        public event EventHandler<BidirectionalMovement> OnStateChange;

        public IKeyboard<TKeyId> Keyboard
        {
            get { return _keyboard; }
        }

        public TKeyId PositiveDirectionKeyId
        {
            get { return _positiveDirectionKeyId; }
        }

        public TKeyId NegativeDirectionKeyId
        {
            get { return _negativeDirectionKeyId; }
        }

        public BidirectionalMovement MovementDirection { get; private set; }

        public void OnKeyStateChange(object sender, KeyEventArgs<TKeyId> eventArgs)
        {
            var isPositiveKey = eventArgs.KeyId.Equals(_positiveDirectionKeyId);

            if (!isPositiveKey && !eventArgs.KeyId.Equals(_negativeDirectionKeyId))
            {
                return; // Ignore anything besides our two keys
            }

            var newState = MovementDirection;

            if (eventArgs.NewState == KeyState.Pressed)
            {
                if (MovementDirection == BidirectionalMovement.Still)
                {
                    newState = isPositiveKey ? BidirectionalMovement.Positive : BidirectionalMovement.Negatize;
                }
            }
            else if (eventArgs.NewState == KeyState.Released)
            {
                if (MovementDirection != BidirectionalMovement.Still)
                {
                    var isMovementForward = MovementDirection == BidirectionalMovement.Positive;

                    if (isPositiveKey == isMovementForward)
                    {
                        newState = BidirectionalMovement.Still; // Stop only if the correct direction key was pressed.
                    }
                }
            }

            if(MovementDirection != newState)
            {
                MovementDirection = newState;

                if (OnStateChange != null)
                {
                    OnStateChange(this, newState);
                }
            }
        }
    }
}
