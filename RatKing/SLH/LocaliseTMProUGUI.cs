using UnityEngine;

namespace RatKing.Base {

	public class LocaliseTMProUGUI : MonoBehaviour, ILocaliseComponent {
		[SerializeField] string key = "";
		[SerializeField] int index = 0;
		[SerializeField] bool convertSpecial = true;
		[SerializeField] bool includeChildren = false;
		//
		TMPro.TextMeshProUGUI[] texts;

		void OnValidate() {
			if (index < 0) { index = 0; }
		}

		//

		void Awake() {
			if (texts == null) { Initialise(); }
		}

		void OnDestroy() {
			if (texts != null) { Localisation.Unregister(this); }
		}

		//

		void Initialise() {
			texts = includeChildren ? GetComponentsInChildren<TMPro.TextMeshProUGUI>(true) : GetComponents<TMPro.TextMeshProUGUI>();
			if (texts.Length == 0) { Debug.LogWarning("Trying to localise empty ui [" + key + "]"); Destroy(this); return; }
			Localisation.Register(this);
		}

		// implement ILocaliseComponent

		public void Localise() {
			if (texts == null) { Initialise(); return; }
			var text = index == 0 ? Localisation.Do(key, convertSpecial) : Localisation.Do(key, index, convertSpecial);
			foreach (var t in texts) { t.text = text; }
		}
	}

}