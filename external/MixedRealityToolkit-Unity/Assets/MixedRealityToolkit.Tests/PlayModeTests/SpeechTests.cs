// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#if !WINDOWS_UWP
// When the .NET scripting backend is enabled and C# projects are built
// The assembly that this file is part of is still built for the player,
// even though the assembly itself is marked as a test assembly (this is not
// expected because test assemblies should not be included in player builds).
// Because the .NET backend is deprecated in 2018 and removed in 2019 and this
// issue will likely persist for 2018, this issue is worked around by wrapping all
// play mode tests in this check.

using Microsoft.MixedReality.Toolkit.Diagnostics;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Microsoft.MixedReality.Toolkit.Tests
{
    public class SpeechTests
    {
        [SetUp]
        public void Setup()
        {
            PlayModeTestUtilities.Setup();
        }

        [TearDown]
        public void TearDown()
        {
            PlayModeTestUtilities.TearDown();
        }

        [UnityTest]
        public IEnumerator TestToggleProfilerCommand()
        {
            // Confirm that the diagnostics system is enabled.
            IMixedRealityDiagnosticsSystem diagnosticsSystem = null;
            MixedRealityServiceRegistry.TryGetService<IMixedRealityDiagnosticsSystem>(out diagnosticsSystem);
            Assert.IsNotNull(diagnosticsSystem, "The diagnostics system is not enabled in the scene.");
            yield return null;

            // This test uses the input system to simulate speech commands.
            IMixedRealityInputSystem inputSystem = null;
            MixedRealityServiceRegistry.TryGetService<IMixedRealityInputSystem>(out inputSystem);
            Assert.IsNotNull(inputSystem, "The input system is not enabled in the scene.");
            yield return null;

            // Verify that the VisualProfiler is enabled.
            Assert.IsTrue(diagnosticsSystem.ShowProfiler, "The VisualProfiler is not active.");
            yield return null;

            int frameDelay = 10;

            // Toggle the profiler visualization off.
            var gazeInputSource = inputSystem.DetectedInputSources.Where(x => x.SourceName.Equals("Gaze")).First();
            inputSystem.RaiseSpeechCommandRecognized(
                gazeInputSource, 
                RecognitionConfidenceLevel.High, 
                new TimeSpan(), 
                DateTime.Now, 
                new SpeechCommands("toggle profiler", KeyCode.Alpha9, MixedRealityInputAction.None));
            // It may take a few frames before the event is handled and the system responds to the state change.
            for (int i = 0; i < frameDelay; i++) { yield return null; }

            // Verify that the VisualProfiler is disabled.
            Assert.IsFalse(diagnosticsSystem.ShowProfiler, "The VisualProfiler is active (should be inactive).");
            yield return null;
        }
    }
}
#endif
