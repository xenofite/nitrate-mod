﻿using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace Nitrate.Content.Optimizations.ParticleRendering;

/// <summary>
///     Instance data for a dust particle. Contains a world transformation
///     matrix, subdivision data, and the color used to render the particle.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct DustInstance
{
    public SimdMatrix World { get; set; }

    public Vector4 InstanceUv { get; set; }

    public Vector4 InstanceColor { get; set; }

    public DustInstance(SimdMatrix world, Vector4 instanceUv, Vector4 instanceColor)
    {
        World = world;
        InstanceUv = instanceUv;
        InstanceColor = instanceColor;
    }
}