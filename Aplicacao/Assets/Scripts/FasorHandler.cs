using UnityEngine;
using UnityEngine.UI;
using System;
using MathCore;

[RequireComponent(typeof(Image))]
public class FasorHandler : MonoBehaviour {

	public Color cor;

	[SerializeField]
	private Fasor f = new Fasor (0, 0, 0);
	private Image image;

	public void Awake () {
		cor = UnityEngine.Random.ColorHSV (0f, 1f, 0.5f, 1f, 0.5f, 1f);
		image = GetComponent<Image> ();
		image.color = cor;
	}

	public string Amplitude {
		set {
			if (!string.IsNullOrEmpty (value)) {
				f = new Fasor (double.Parse (value), f.FrequenciaAngular, f.FaseRadianos);
				if (GerenteFasores.instance != null) {
					GerenteFasores.instance.Atualizar ();
				}
			}
		}
	}

	public string Frequencia {
		set {
			if (!string.IsNullOrEmpty (value)) {
				f = new Fasor (f.Amplitude, double.Parse (value), f.FaseRadianos);
				if (GerenteFasores.instance != null) {
					GerenteFasores.instance.Atualizar ();
				}
			}
		}
	}

	public string FaseGraus {
		set {
			if (!string.IsNullOrEmpty (value)) {
				f = new Fasor (f.Amplitude, f.FrequenciaAngular, double.Parse (value)/(180d/Math.PI));
				if (GerenteFasores.instance != null) {
					GerenteFasores.instance.Atualizar ();
				}
			}
		}
	}

	public Fasor fasor {
		get { return f; }
	}
}
