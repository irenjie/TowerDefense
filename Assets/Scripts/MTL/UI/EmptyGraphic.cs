using UnityEngine;
using UnityEngine.UI;

namespace MUI {
    [UnityEngine.RequireComponent(typeof(CanvasRenderer))]
    public class EmptyGraphic : Graphic {
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
        protected override void OnPopulateMesh(VertexHelper vh) {
            vh.Clear();
        }
    }
}