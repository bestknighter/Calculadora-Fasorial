using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GerenteFasores : MonoBehaviour {

	public GameObject content;
	public GameObject fasorPrefab;
	public Text contadorFasores;
	public ScrollRect scroll;
	public RectTransform viewport;
	public RectTransform gaveta;
	[Range(0.1f, 5)]
	public float tempoAnimacao = 1f;
	public AnimationCurve movimentoGaveta;

	private static GerenteFasores m_instance = null;
	private float posFechado;

	public static GerenteFasores instance {
		get {
			return m_instance;
		}
	}

	public void Awake () {
		if (m_instance == null) {
			m_instance = this;
		} else {
			Debug.LogWarning ("Tentativa de inicializar um segundo objeto de classe singleton detectada. Removendo gameObject...");
			Destroy (gameObject);
		}
	}

	public void Start () {
		viewport.sizeDelta = new Vector2 (viewport.sizeDelta.x+1, viewport.sizeDelta.y+1); // Workaraund
		Canvas.ForceUpdateCanvases();
		posFechado = -(gaveta.FindChild ("Panel") as RectTransform).sizeDelta.y;
		gaveta.anchoredPosition = new Vector2 (gaveta.anchoredPosition.x, posFechado);
		Atualizar ();
	}


	public void CriarFasor () {
		GameObject go = GameObject.Instantiate (fasorPrefab, content.transform) as GameObject;
		go.transform.SetSiblingIndex (content.transform.childCount-2);
		Canvas.ForceUpdateCanvases ();
		scroll.verticalNormalizedPosition = 0f;

		go.GetComponentInChildren<Button> ().onClick.AddListener ( delegate() {DestruirFasor (go);} );
		Atualizar ();
	}

	public void DestruirFasor (GameObject go) {
		DestroyImmediate (go);
		Atualizar ();
	}

	public void Atualizar () {
		contadorFasores.text = "Fasores (" + (content.transform.childCount-1) + ")";
		#if DEBUG
		string msg = "Gerente Fasores atualizado.\n";
		FasorHandler[] listFasores = new FasorHandler[content.transform.childCount - 1];
		for (int i = 0; i < content.transform.childCount - 1; i++) {
			FasorHandler fh = content.transform.GetChild (i).GetComponent<FasorHandler> ();
			listFasores[i] = fh;
			msg += string.Format("Fasor {0}: {1} {2}\n", i, fh.fasor, fh.cor);
		}
		GridOverlay.instance.Fasores = listFasores;
		Debug.Log (msg);
		#endif
	}

	public void ToggleGaveta() {
		StartCoroutine ("AnimarGaveta");
	}

	public IEnumerator AnimarGaveta () {
		Vector2 inicio = gaveta.anchoredPosition;
		Vector2 fim = new Vector2 (inicio.x, posFechado - inicio.y);
		for (float tempo = 0; tempo < tempoAnimacao; tempo += Time.deltaTime) {
			float peso = movimentoGaveta.Evaluate (tempo / tempoAnimacao);
			gaveta.anchoredPosition = fim * peso + inicio * (1 - peso);
			yield return null;
		}
	}

}