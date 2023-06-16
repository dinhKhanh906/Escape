using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TintModelScene
{
    public class ButtonHighlight : BaseButton
    {
        public Image targetHighlight;
        public Color colorNonHighLight;
        public Color colorHighlight;
        protected override void Awake()
        {
            base.Awake();
            targetHighlight.color = colorNonHighLight;
        }
        public virtual void Highlight()
        {
            targetHighlight.color = colorHighlight;
        }
        public virtual void UnHighlight()
        {
            targetHighlight.color = colorNonHighLight;
        }
    }
}