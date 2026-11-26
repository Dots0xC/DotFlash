using CefFlashBrowser.Models;
using CefFlashBrowser.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CefFlashBrowser.Tests
{
    [TestClass]
    public class CefCommandLineHelperTests
    {
        [TestMethod]
        public void ApplyGpuArguments_DefaultSettings_AddsNoFlags()
        {
            var args = new Dictionary<string, string>();

            CefCommandLineHelper.ApplyGpuArguments(args, Settings.Default);

            Assert.AreEqual(0, args.Count);
        }

        [TestMethod]
        public void ApplyGpuArguments_DisableDirectCompositionOnly_SetsDirectCompositionFlag()
        {
            var args = new Dictionary<string, string>();
            var settings = new Settings { DisableDirectComposition = true };

            CefCommandLineHelper.ApplyGpuArguments(args, settings);

            Assert.AreEqual(1, args.Count);
            Assert.AreEqual("1", args["disable-direct-composition"]);
        }

        [TestMethod]
        public void ApplyGpuArguments_DisableGpuAccelerationOnly_SetsGpuFlags()
        {
            var args = new Dictionary<string, string>();
            var settings = new Settings { DisableGpuAcceleration = true };

            CefCommandLineHelper.ApplyGpuArguments(args, settings);

            Assert.AreEqual(3, args.Count);
            Assert.AreEqual("1", args["disable-gpu"]);
            Assert.AreEqual("1", args["disable-gpu-compositing"]);
            Assert.AreEqual("1", args["disable-gpu-vsync"]);
        }

        [TestMethod]
        public void ApplyGpuArguments_BothEnabled_SetsAllFlags()
        {
            var args = new Dictionary<string, string>();
            var settings = new Settings
            {
                DisableGpuAcceleration = true,
                DisableDirectComposition = true
            };

            CefCommandLineHelper.ApplyGpuArguments(args, settings);

            Assert.AreEqual(4, args.Count);
            Assert.AreEqual("1", args["disable-gpu"]);
            Assert.AreEqual("1", args["disable-gpu-compositing"]);
            Assert.AreEqual("1", args["disable-gpu-vsync"]);
            Assert.AreEqual("1", args["disable-direct-composition"]);
        }

        [TestMethod]
        public void ApplyGpuArguments_NullArgs_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => CefCommandLineHelper.ApplyGpuArguments(null, Settings.Default));
        }

        [TestMethod]
        public void ApplyGpuArguments_NullSettings_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => CefCommandLineHelper.ApplyGpuArguments(new Dictionary<string, string>(), null));
        }
    }
}
