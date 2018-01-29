using UnityEngine;
using UnityEditor.Graphing;

namespace UnityEditor.ShaderGraph
{
    [Title("Input", "Geometry", "Post Process UV")]
    public class PostProcessUVNode : AbstractMaterialNode, IGeneratesBodyCode, IMayRequireMeshUV
    {
        public const int OutputSlotId = 0;
        private const string kOutputSlotName = "Out";

        public PostProcessUVNode()
        {
            name = "Post Process UV";
            UpdateNodeAfterDeserialization();
        }

        public override bool hasPreview
        {
            get { return false; }
        }

        public override void UpdateNodeAfterDeserialization()
        {
            AddSlot(new Vector2MaterialSlot(OutputSlotId, kOutputSlotName, kOutputSlotName, SlotType.Output, Vector2.zero));
            RemoveSlotsNameNotMatching(new[] { OutputSlotId });
        }

        public void GenerateNodeCode(ShaderGenerator visitor, GenerationMode generationMode)
        {
			string texcoord = generationMode == GenerationMode.ForReals ? "texcoord" : UVChannel.UV0.GetUVName();
            visitor.AddShaderChunk(string.Format("{0}2 {1} = IN.{2};", precision, GetVariableNameForSlot(OutputSlotId), texcoord), true);
        }

        public bool RequiresMeshUV(UVChannel channel)
        {
            return channel == UVChannel.UV0;
        }
    }
}
