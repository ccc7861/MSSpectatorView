// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.


namespace Microsoft.MixedReality.Toolkit.Tests.Services
{
    internal class TestExtensionService2 : BaseTestExtensionService, ITestExtensionService2
    {
        public TestExtensionService2(
            IMixedRealityServiceRegistrar registrar, 
            string name, 
            uint priority, 
            BaseMixedRealityProfile profile) : base(registrar, name, priority, profile) { }
    }
}