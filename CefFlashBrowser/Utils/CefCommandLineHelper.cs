using CefFlashBrowser.Models;
using System;
using System.Collections.Generic;

namespace CefFlashBrowser.Utils
{
    public static class CefCommandLineHelper
    {
        public static void ApplyGpuArguments(IDictionary<string, string> args, Settings settings)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.DisableGpuAcceleration)
            {
                args["disable-gpu"] = "1";
                args["disable-gpu-compositing"] = "1";
                args["disable-gpu-vsync"] = "1";
            }

            if (settings.DisableDirectComposition)
            {
                args["disable-direct-composition"] = "1";
            }
        }
    }
}
