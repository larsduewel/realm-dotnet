﻿////////////////////////////////////////////////////////////////////////////
//
// Copyright 2016 Realm Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using Realms.Native;

namespace Realms
{
    internal static class RealmResultsNativeHelper
    {
        internal interface Interface
        {
            void NotifyCallbacks(ResultsHandle.CollectionChangeSet? changes, NativeException? exception);
        }

        [NativeCallback(typeof(ResultsHandle.NotificationCallback))]
        internal static void NotificationCallback(IntPtr managedResultsHandle, IntPtr changes, IntPtr exception)
        {
            var results = (Interface)GCHandle.FromIntPtr(managedResultsHandle).Target;
            results.NotifyCallbacks(new PtrTo<ResultsHandle.CollectionChangeSet>(changes).Value, new PtrTo<NativeException>(exception).Value);
        }
    }
}