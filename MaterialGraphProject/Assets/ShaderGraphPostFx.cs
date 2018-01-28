using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
public sealed class ShaderParameter : ParameterOverride<Shader>
{
}

[System.Serializable]
[PostProcess(typeof(ShaderGraphPostFxRenderer), PostProcessEvent.AfterStack, "Custom/ShaderGraphPostFx")]
public sealed class ShaderGraphPostFx : PostProcessEffectSettings
{
    public ShaderParameter shader = new ShaderParameter { value = null };
}

public sealed class ShaderGraphPostFxRenderer : PostProcessEffectRenderer<ShaderGraphPostFx>
{
    Material _material;

    public override void Release()
    {
        base.Release();

        if (_material != null)
            if (Application.isPlaying)
                Object.Destroy(_material);
            else
                Object.DestroyImmediate(_material);

        _material = null;
    }

    public override void Render(PostProcessRenderContext context)
    {
        if ((Shader)settings.shader != null)
        {
            if (_material == null)
            {
                _material = new Material(settings.shader);
                _material.hideFlags = HideFlags.DontSave;
            }

            context.command.Blit(context.source, context.destination, _material, 0);
        }
        else
        {
            context.command.BlitFullscreenTriangle(context.source, context.destination);
        }
    }
}
