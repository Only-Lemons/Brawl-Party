using UnityEngine.UI;

namespace Fanatee.Unity
{
	/// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
	/// Useful for providing a raycast target without actually drawing anything.
	/// From https://trello.com/c/1HhrHDcP/112-sometimes-you-need-a-ui-raycast-target-but-dont-need-the-overdraw-of-an-image-this-is-where-you-would-need-a-raycasttarget-scrip
	/// In turn, from https://answers.unity.com/questions/1091618/ui-panel-without-image-component-as-raycast-target.html
	public class RaycastTarget : Graphic
	{
		public override void SetMaterialDirty()
		{
			return;
		}

		public override void SetVerticesDirty()
		{
			return;
		}

		/// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			return;
		}
	}
}
