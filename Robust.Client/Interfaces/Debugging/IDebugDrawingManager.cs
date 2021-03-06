﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Shared.Timing;

namespace Robust.Client.Interfaces.Debugging
{
    public interface IDebugDrawingManager
    {
        bool DebugDrawRays { get; set; }
        TimeSpan DebugRayLifetime { get; set; }
        void Initialize();
    }
}
