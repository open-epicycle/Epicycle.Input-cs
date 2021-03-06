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

namespace Epicycle.Input.Controllers
{
    public class ControllerChangeEventArgs<TControllerId, TControllerValue> : EventArgs
    {
        private readonly TControllerId _controllerId;
        private readonly TControllerValue _value;

        public ControllerChangeEventArgs(TControllerId controllerId, TControllerValue value)
        {
            _controllerId = controllerId;
            _value = value;
        }

        public TControllerId ControllerId { get { return _controllerId; } }

        public TControllerValue Value { get { return _value; } }
    }
}
