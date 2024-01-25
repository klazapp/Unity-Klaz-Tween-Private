using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;

namespace com.Klazapp.Utility
{
    //[Todoheader("Should only have 1 version of Color, auto convert to Color32. do same for other variables. eg. Vector4 -> float4")]
    //[TodoHeader("Add other functionalities as required")]
    //[TodoHeader("Check performance on event and action callbacks")]
    //[TodoHeader("Add custom inspector")]
    //[TodoHeader("Add Easing")]
    //[TodoHeader("Add Delay")]
    //[TodoHeader("Revert back to using jobsystem in manager script when tween count more than 10, re-enable readonly attribute")]
    //[TodoHeader("Use Singleton for manager")]
    //[TodoHeader("Create high performance generic job system")]
    //[ScriptHeader("High performance tween manager that utilizes multi-threading and machine code compilation when necessary")]
    public partial class KlazTweenManager : MonoBehaviour
    {
        //Tweens
        private Dictionary<int, IKlazTween> floatTweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> float2Tweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> float3Tweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> float4Tweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> quaternionTweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> colorTweens = new Dictionary<int, IKlazTween>();
        private Dictionary<int, IKlazTween> color32Tweens = new Dictionary<int, IKlazTween>();
        // private List<IKlazTween> floatTweens = new List<IKlazTween>();
        // private List<IKlazTween> float2Tweens = new List<IKlazTween>();
        // private List<IKlazTween> float3Tweens = new List<IKlazTween>();
        // private List<IKlazTween> float4Tweens = new List<IKlazTween>();
        // private List<IKlazTween> quaternionTweens = new List<IKlazTween>();
        // private List<IKlazTween> colorTweens = new List<IKlazTween>();
        // private List<IKlazTween> color32Tweens = new List<IKlazTween>();

        //Native arrays for tweens
        private KlazTweenNativeArrays<float> floatNativeArrays = new KlazTweenNativeArrays<float>();
        private KlazTweenNativeArrays<float2> float2NativeArrays = new KlazTweenNativeArrays<float2>();
        private KlazTweenNativeArrays<float3> float3NativeArrays = new KlazTweenNativeArrays<float3>();
        private KlazTweenNativeArrays<float4> float4NativeArrays = new KlazTweenNativeArrays<float4>();
        private KlazTweenNativeArrays<quaternion> quaternionNativeArrays = new KlazTweenNativeArrays<quaternion>();
        private KlazTweenNativeArrays<Color> colorNativeArrays = new KlazTweenNativeArrays<Color>();
        private KlazTweenNativeArrays<Color32> color32NativeArrays = new KlazTweenNativeArrays<Color32>();

        public bool useJobSystem;
        private int tweenId = 0;

        #region Lifecycle Flow
        private void PostAwake()
        {
            tweenId = 0;

#if KLAZAPP_ENABLE_JOBSYSTEM

#endif
        }
        
        private void OnDestroy()
        {
            DisposeArrays();
        }
        
        private void Update()
        {
#if KLAZAPP_ENABLE_JOBSYSTEM
            var totalTweenCount = GetTotalTweenCount();
            //if(totalTweenCount >= 10)
            if (useJobSystem)
            {
                UpdateJobTweens();
            }
            else
            {
                UpdateRegularTweens();
            }
#else
            UpdateRegularTweens();
#endif

            ProcessCompletedTweens();
        }
        #endregion

        #region Public Access
        public void DoTween<T>(T startValue, T endValue, float duration, Action<T> onUpdate, float delay = 0f, KlazTweenCallback startTweenCallback = null, KlazTweenCallback endTweenCallback = null) where T : struct
        {
            //Get lerp func
            var lerpFunc = GetLerpFuncByType<T>();

            //Create new tween
            var tween = new KlazTween<T>(startValue, endValue, duration, onUpdate, lerpFunc, delay, startTweenCallback, endTweenCallback, tweenId++);
            
            //Add to list
            AddTween(tween);

            //Initialize native arrays
            InitializeNativeArrays();

            //Prepare tweens for job
            PrepareTweenForJob();
        }
        #endregion

        #region Modules
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetTotalTweenCount()
        {
            var floatTweenCount = floatTweens.Count;
            var float3TweenCount = float3Tweens.Count;

            return floatTweenCount + float3TweenCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisposeArrays()
        {
            floatNativeArrays.DisposeNativeArrays();
            float3NativeArrays.DisposeNativeArrays();
        }
        #endregion
    }
}