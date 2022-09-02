
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace ZSerializer {

[System.Serializable]
public sealed class AudioSourceZSerializer : ZSerializer.Internal.ZSerializer {
    public System.Single volume;
    public System.Single pitch;
    public System.Single time;
    public System.Int32 timeSamples;
    public UnityEngine.AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup outputAudioMixerGroup;
    public UnityEngine.GamepadSpeakerOutputType gamepadSpeakerOutputType;
    public System.Boolean loop;
    public System.Boolean ignoreListenerVolume;
    public System.Boolean playOnAwake;
    public System.Boolean ignoreListenerPause;
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Single panStereo;
    public System.Single spatialBlend;
    public System.Boolean spatialize;
    public System.Boolean spatializePostEffects;
    public System.Single reverbZoneMix;
    public System.Boolean bypassEffects;
    public System.Boolean bypassListenerEffects;
    public System.Boolean bypassReverbZones;
    public System.Single dopplerLevel;
    public System.Single spread;
    public System.Int32 priority;
    public System.Boolean mute;
    public System.Single minDistance;
    public System.Single maxDistance;
    public UnityEngine.AudioRolloffMode rolloffMode;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioSourceZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.AudioSource;
        volume = instance.volume;
        pitch = instance.pitch;
        time = instance.time;
        timeSamples = instance.timeSamples;
        clip = instance.clip;
        outputAudioMixerGroup = instance.outputAudioMixerGroup;
        gamepadSpeakerOutputType = instance.gamepadSpeakerOutputType;
        loop = instance.loop;
        ignoreListenerVolume = instance.ignoreListenerVolume;
        playOnAwake = instance.playOnAwake;
        ignoreListenerPause = instance.ignoreListenerPause;
        velocityUpdateMode = instance.velocityUpdateMode;
        panStereo = instance.panStereo;
        spatialBlend = instance.spatialBlend;
        spatialize = instance.spatialize;
        spatializePostEffects = instance.spatializePostEffects;
        reverbZoneMix = instance.reverbZoneMix;
        bypassEffects = instance.bypassEffects;
        bypassListenerEffects = instance.bypassListenerEffects;
        bypassReverbZones = instance.bypassReverbZones;
        dopplerLevel = instance.dopplerLevel;
        spread = instance.spread;
        priority = instance.priority;
        mute = instance.mute;
        minDistance = instance.minDistance;
        maxDistance = instance.maxDistance;
        rolloffMode = instance.rolloffMode;
        enabled = instance.enabled;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.AudioSource))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.AudioSource)component;
        instance.volume = volume;
        instance.pitch = pitch;
        instance.time = time;
        instance.timeSamples = timeSamples;
        instance.clip = clip;
        instance.outputAudioMixerGroup = outputAudioMixerGroup;
        instance.gamepadSpeakerOutputType = gamepadSpeakerOutputType;
        instance.loop = loop;
        instance.ignoreListenerVolume = ignoreListenerVolume;
        instance.playOnAwake = playOnAwake;
        instance.ignoreListenerPause = ignoreListenerPause;
        instance.velocityUpdateMode = velocityUpdateMode;
        instance.panStereo = panStereo;
        instance.spatialBlend = spatialBlend;
        instance.spatialize = spatialize;
        instance.spatializePostEffects = spatializePostEffects;
        instance.reverbZoneMix = reverbZoneMix;
        instance.bypassEffects = bypassEffects;
        instance.bypassListenerEffects = bypassListenerEffects;
        instance.bypassReverbZones = bypassReverbZones;
        instance.dopplerLevel = dopplerLevel;
        instance.spread = spread;
        instance.priority = priority;
        instance.mute = mute;
        instance.minDistance = minDistance;
        instance.maxDistance = maxDistance;
        instance.rolloffMode = rolloffMode;
        instance.enabled = enabled;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.AudioSource))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class TransformZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector3 position;
    public UnityEngine.Vector3 localPosition;
    public UnityEngine.Vector3 eulerAngles;
    public UnityEngine.Vector3 localEulerAngles;
    public UnityEngine.Vector3 right;
    public UnityEngine.Vector3 up;
    public UnityEngine.Vector3 forward;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.Quaternion localRotation;
    public UnityEngine.Vector3 localScale;
    public UnityEngine.Transform parent;
    public System.Boolean hasChanged;
    public System.Int32 hierarchyCapacity;
    public UnityEngine.HideFlags hideFlags;
    public TransformZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.Transform;
        position = instance.position;
        localPosition = instance.localPosition;
        eulerAngles = instance.eulerAngles;
        localEulerAngles = instance.localEulerAngles;
        right = instance.right;
        up = instance.up;
        forward = instance.forward;
        rotation = instance.rotation;
        localRotation = instance.localRotation;
        localScale = instance.localScale;
        parent = instance.parent;
        hasChanged = instance.hasChanged;
        hierarchyCapacity = instance.hierarchyCapacity;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Transform))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.Transform)component;
        instance.position = position;
        instance.localPosition = localPosition;
        instance.eulerAngles = eulerAngles;
        instance.localEulerAngles = localEulerAngles;
        instance.right = right;
        instance.up = up;
        instance.forward = forward;
        instance.rotation = rotation;
        instance.localRotation = localRotation;
        instance.localScale = localScale;
        instance.parent = parent;
        instance.hasChanged = hasChanged;
        instance.hierarchyCapacity = hierarchyCapacity;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Transform))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class SpriteRendererZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Sprite sprite;
    public UnityEngine.SpriteDrawMode drawMode;
    public UnityEngine.Vector2 size;
    public System.Single adaptiveModeThreshold;
    public UnityEngine.SpriteTileMode tileMode;
    public UnityEngine.Color color;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public System.Boolean flipX;
    public System.Boolean flipY;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public UnityEngine.Bounds bounds;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public System.Boolean staticShadowCaster;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public SpriteRendererZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.SpriteRenderer;
        sprite = instance.sprite;
        drawMode = instance.drawMode;
        size = instance.size;
        adaptiveModeThreshold = instance.adaptiveModeThreshold;
        tileMode = instance.tileMode;
        color = instance.color;
        maskInteraction = instance.maskInteraction;
        flipX = instance.flipX;
        flipY = instance.flipY;
        spriteSortPoint = instance.spriteSortPoint;
        bounds = instance.bounds;
        localBounds = instance.localBounds;
        enabled = instance.enabled;
        shadowCastingMode = instance.shadowCastingMode;
        receiveShadows = instance.receiveShadows;
        forceRenderingOff = instance.forceRenderingOff;
        staticShadowCaster = instance.staticShadowCaster;
        motionVectorGenerationMode = instance.motionVectorGenerationMode;
        lightProbeUsage = instance.lightProbeUsage;
        reflectionProbeUsage = instance.reflectionProbeUsage;
        renderingLayerMask = instance.renderingLayerMask;
        rendererPriority = instance.rendererPriority;
        rayTracingMode = instance.rayTracingMode;
        sortingLayerName = instance.sortingLayerName;
        sortingLayerID = instance.sortingLayerID;
        sortingOrder = instance.sortingOrder;
        allowOcclusionWhenDynamic = instance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = instance.lightProbeProxyVolumeOverride;
        probeAnchor = instance.probeAnchor;
        lightmapIndex = instance.lightmapIndex;
        realtimeLightmapIndex = instance.realtimeLightmapIndex;
        lightmapScaleOffset = instance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = instance.realtimeLightmapScaleOffset;
        sharedMaterials = instance.sharedMaterials;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteRenderer))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.SpriteRenderer)component;
        instance.sprite = sprite;
        instance.drawMode = drawMode;
        instance.size = size;
        instance.adaptiveModeThreshold = adaptiveModeThreshold;
        instance.tileMode = tileMode;
        instance.color = color;
        instance.maskInteraction = maskInteraction;
        instance.flipX = flipX;
        instance.flipY = flipY;
        instance.spriteSortPoint = spriteSortPoint;
        instance.bounds = bounds;
        instance.localBounds = localBounds;
        instance.enabled = enabled;
        instance.shadowCastingMode = shadowCastingMode;
        instance.receiveShadows = receiveShadows;
        instance.forceRenderingOff = forceRenderingOff;
        instance.staticShadowCaster = staticShadowCaster;
        instance.motionVectorGenerationMode = motionVectorGenerationMode;
        instance.lightProbeUsage = lightProbeUsage;
        instance.reflectionProbeUsage = reflectionProbeUsage;
        instance.renderingLayerMask = renderingLayerMask;
        instance.rendererPriority = rendererPriority;
        instance.rayTracingMode = rayTracingMode;
        instance.sortingLayerName = sortingLayerName;
        instance.sortingLayerID = sortingLayerID;
        instance.sortingOrder = sortingOrder;
        instance.allowOcclusionWhenDynamic = allowOcclusionWhenDynamic;
        instance.lightProbeProxyVolumeOverride = lightProbeProxyVolumeOverride;
        instance.probeAnchor = probeAnchor;
        instance.lightmapIndex = lightmapIndex;
        instance.realtimeLightmapIndex = realtimeLightmapIndex;
        instance.lightmapScaleOffset = lightmapScaleOffset;
        instance.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
        instance.sharedMaterials = sharedMaterials;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteRenderer))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class BoxCollider2DZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector2 size;
    public System.Single edgeRadius;
    public System.Boolean autoTiling;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public BoxCollider2DZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.BoxCollider2D;
        size = instance.size;
        edgeRadius = instance.edgeRadius;
        autoTiling = instance.autoTiling;
        density = instance.density;
        isTrigger = instance.isTrigger;
        usedByEffector = instance.usedByEffector;
        usedByComposite = instance.usedByComposite;
        offset = instance.offset;
        enabled = instance.enabled;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.BoxCollider2D))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.BoxCollider2D)component;
        instance.size = size;
        instance.edgeRadius = edgeRadius;
        instance.autoTiling = autoTiling;
        instance.density = density;
        instance.isTrigger = isTrigger;
        instance.usedByEffector = usedByEffector;
        instance.usedByComposite = usedByComposite;
        instance.offset = offset;
        instance.enabled = enabled;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.BoxCollider2D))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class SpriteMaskZSerializer : ZSerializer.Internal.ZSerializer {
    public System.Int32 frontSortingLayerID;
    public System.Int32 frontSortingOrder;
    public System.Int32 backSortingLayerID;
    public System.Int32 backSortingOrder;
    public System.Single alphaCutoff;
    public UnityEngine.Sprite sprite;
    public System.Boolean isCustomRangeActive;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public UnityEngine.Bounds bounds;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public System.Boolean staticShadowCaster;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public SpriteMaskZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.SpriteMask;
        frontSortingLayerID = instance.frontSortingLayerID;
        frontSortingOrder = instance.frontSortingOrder;
        backSortingLayerID = instance.backSortingLayerID;
        backSortingOrder = instance.backSortingOrder;
        alphaCutoff = instance.alphaCutoff;
        sprite = instance.sprite;
        isCustomRangeActive = instance.isCustomRangeActive;
        spriteSortPoint = instance.spriteSortPoint;
        bounds = instance.bounds;
        localBounds = instance.localBounds;
        enabled = instance.enabled;
        shadowCastingMode = instance.shadowCastingMode;
        receiveShadows = instance.receiveShadows;
        forceRenderingOff = instance.forceRenderingOff;
        staticShadowCaster = instance.staticShadowCaster;
        motionVectorGenerationMode = instance.motionVectorGenerationMode;
        lightProbeUsage = instance.lightProbeUsage;
        reflectionProbeUsage = instance.reflectionProbeUsage;
        renderingLayerMask = instance.renderingLayerMask;
        rendererPriority = instance.rendererPriority;
        rayTracingMode = instance.rayTracingMode;
        sortingLayerName = instance.sortingLayerName;
        sortingLayerID = instance.sortingLayerID;
        sortingOrder = instance.sortingOrder;
        allowOcclusionWhenDynamic = instance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = instance.lightProbeProxyVolumeOverride;
        probeAnchor = instance.probeAnchor;
        lightmapIndex = instance.lightmapIndex;
        realtimeLightmapIndex = instance.realtimeLightmapIndex;
        lightmapScaleOffset = instance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = instance.realtimeLightmapScaleOffset;
        sharedMaterials = instance.sharedMaterials;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteMask))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.SpriteMask)component;
        instance.frontSortingLayerID = frontSortingLayerID;
        instance.frontSortingOrder = frontSortingOrder;
        instance.backSortingLayerID = backSortingLayerID;
        instance.backSortingOrder = backSortingOrder;
        instance.alphaCutoff = alphaCutoff;
        instance.sprite = sprite;
        instance.isCustomRangeActive = isCustomRangeActive;
        instance.spriteSortPoint = spriteSortPoint;
        instance.bounds = bounds;
        instance.localBounds = localBounds;
        instance.enabled = enabled;
        instance.shadowCastingMode = shadowCastingMode;
        instance.receiveShadows = receiveShadows;
        instance.forceRenderingOff = forceRenderingOff;
        instance.staticShadowCaster = staticShadowCaster;
        instance.motionVectorGenerationMode = motionVectorGenerationMode;
        instance.lightProbeUsage = lightProbeUsage;
        instance.reflectionProbeUsage = reflectionProbeUsage;
        instance.renderingLayerMask = renderingLayerMask;
        instance.rendererPriority = rendererPriority;
        instance.rayTracingMode = rayTracingMode;
        instance.sortingLayerName = sortingLayerName;
        instance.sortingLayerID = sortingLayerID;
        instance.sortingOrder = sortingOrder;
        instance.allowOcclusionWhenDynamic = allowOcclusionWhenDynamic;
        instance.lightProbeProxyVolumeOverride = lightProbeProxyVolumeOverride;
        instance.probeAnchor = probeAnchor;
        instance.lightmapIndex = lightmapIndex;
        instance.realtimeLightmapIndex = realtimeLightmapIndex;
        instance.lightmapScaleOffset = lightmapScaleOffset;
        instance.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
        instance.sharedMaterials = sharedMaterials;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteMask))?.OnDeserialize?.Invoke(this, instance);
    }
}
}