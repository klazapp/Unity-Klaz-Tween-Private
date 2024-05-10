using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace com.Klazapp.Utility
{
    internal static class KlazTweenLerp
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Lerp(float start, float end, float progress) => math.lerp(start, end, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float2 Lerp(float2 start, float2 end, float progress) => math.lerp(start, end, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float3 Lerp(float3 start, float3 end, float progress) => math.lerp(start, end, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float4 Lerp(float4 start, float4 end, float progress) => math.lerp(start, end, progress);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double2 Lerp(double2 start, double2 end, float progress) => math.lerp(start, end, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static quaternion Slerp(quaternion start, quaternion end, float progress) =>
            math.slerp(start, end, progress);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Color32 Lerp(Color32 start, Color32 end, float progress)
        {
            var startFloat4 = new float4(start.r / 255f, start.g / 255f, start.b / 255f, start.a / 255f);
            var endFloat4 = new float4(end.r / 255f, end.g / 255f, end.b / 255f, end.a / 255f);

            var lerpResult = math.lerp(startFloat4, endFloat4, progress);

            return new Color32(
                (byte)(lerpResult.x * 255),
                (byte)(lerpResult.y * 255),
                (byte)(lerpResult.z * 255),
                (byte)(lerpResult.w * 255)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Color Lerp(Color start, Color end, float progress)
        {
            var startFloat4 = new float4(start.r, start.g, start.b, start.a);
            var endFloat4 = new float4(end.r, end.g, end.b, end.a);

            var lerpResult = math.lerp(startFloat4, endFloat4, progress);

            return new Color(lerpResult.x, lerpResult.y, lerpResult.z, lerpResult.w);
        }
    }
}