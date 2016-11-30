using UnityEngine;
using UnityEngine.UI;
using System;
using MathCore;

public class Calculador : MonoBehaviour {

	public FasorHandler a, b;
	public Fasor c;
	public InputField amp, fase;

	private delegate Fasor Operacao(Fasor a, Fasor b);
	private Operacao operacao;


	void Start() {
		operacao = Somar;
	}

	// Update is called once per frame
	void Update () {
		c = operacao (a.fasor, b.fasor);
		amp.text = c.Amplitude.ToString();
		fase.text = (c.FaseRadianos*(180d/Math.PI)).ToString();
	}

	public void MudarOperacao (int op) {
		switch (op) {
		case 0:
			operacao = Somar;
			break;
		case 1:
			operacao = Subtrair;
			break;
		case 2:
			operacao = Multiplicar;
			break;
		case 3:
			operacao = Dividir;
			break;
		case 4:
			operacao = Integrar_A;
			break;
		case 5:
			operacao = Derivar_A;
			break;
		case 6:
			operacao = Integrar_B;
			break;
		case 7:
			operacao = Derivar_B;
			break;
		}
	}

	Fasor Somar (Fasor a, Fasor b) {
		return a + b;
	}

	Fasor Subtrair (Fasor a, Fasor b) {
		return a - b;
	}

	Fasor Multiplicar (Fasor a, Fasor b) {
		return a * b;
	}

	Fasor Dividir (Fasor a, Fasor b) {
		return a / b;
	}

	Fasor Integrar_A (Fasor a, Fasor b) {
		return a.Integrado;
	}

	Fasor Derivar_A (Fasor a, Fasor b) {
		return a.Derivado;
	}

	Fasor Integrar_B (Fasor a, Fasor b) {
		return b.Integrado;
	}

	Fasor Derivar_B (Fasor a, Fasor b) {
		return b.Derivado;
	}
}
