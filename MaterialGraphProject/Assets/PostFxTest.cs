using System;

namespace UnityEngine.Rendering.PostProcessing
{
    [Serializable]
    [PostProcess(typeof(PostFxTestRenderer), PostProcessEvent.AfterStack, "graphs/PostFxTest")]
    public class PostFxTest : PostProcessEffectSettings
    {
        
    }

    public class PostFxTestRenderer : PostProcessEffectRenderer<PostFxTest>
    {
        Shader _shader;

        public override void Init()
        {
            if (_shader == null)
                _shader = Shader.Find("Hidden/graphs/PostFxTest");
        }

        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(_shader);
            
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}