using System;
using UnityEngine;

namespace MathCore {

	public struct Fasor {

		public readonly double Amplitude;
		public readonly double FrequenciaAngular;
		public readonly double FaseRadianos;

		/// <summary>
		/// Inicializa um <see cref="Fasor"/>.
		/// </summary>
		/// <param name="amplitude">Amplitude.</param>
		/// <param name="frequenciaRadS">Frequência em radianos/segundo.</param>
		/// <param name="faseRad">Fase em radianos.</param>
		public Fasor (double amplitude, double frequenciaRadS, double faseRad) {
			if (amplitude < 0) {
				amplitude = -amplitude;
				faseRad += Math.PI;
			}
			faseRad %= 2 * Math.PI;

			Amplitude = amplitude;
			FrequenciaAngular = frequenciaRadS;
			FaseRadianos = (faseRad >= 0 ? faseRad : 2 * Math.PI + faseRad);
		}

		/// <summary>
		/// Representação complexa do fasor no tempo especificado
		/// </summary>
		/// <param name="tempo">Tempo a analisar.</param>
		/// <returns>Um complexo com a representação temporal do fasor.
		public Complexo Retangular (double tempo = 0) {
			return new Complexo (Amplitude*Math.Cos(FrequenciaAngular*tempo+FaseRadianos), Amplitude*Math.Sin(FrequenciaAngular*tempo+FaseRadianos));
		}

		/// <summary>
		/// Representação cossenoide do fasor.
		/// </summary>
		/// <returns>Uma string com a representação cossenoide.</returns>
		public string RepresentacaoCossenoide () {
			if (Amplitude == 0) {
				return "0";
			}

			string resp = Amplitude+"*cos(";

			// Monta de forma bonita a string do termo do cosseno.
			// Ou seja, não mostra termos nulos e oculta termos iguais a 1 em multiplicações
			if (FrequenciaAngular != 0 && FaseRadianos != 0) {
				if (FrequenciaAngular == 1) {
					resp += "t+" + FaseRadianos;
				} else {
					resp += FrequenciaAngular + "*t+" + FaseRadianos;
				}
			
			} else if (FrequenciaAngular == 1) {
				resp += "t";

			} else if (FrequenciaAngular != 0) {
				resp += FrequenciaAngular + "*t";

			} else if (FaseRadianos != 0) {
				resp += FaseRadianos;

			} else {
				resp += "0";
			}

			return resp+")";
		}

		/// <summary>
		/// Representação retangular do fasor.
		/// </summary>
		/// <param name="tempo">Tempo a analisar.</param>
		/// <returns>Uma string com a representação retangular.</returns>
		public string RepresentacaoRetangular (double tempo = 0) {
			return Retangular(tempo).ToString();
		}

		/// <summary>
		/// Representação exponencial do fasor.
		/// </summary>
		/// <returns>Uma string com a representação exponencial.</returns>
		public string RepresentacaoExponencial () {
			return Amplitude.ToString() + "e^i(" + FrequenciaAngular + "t+" + FaseRadianos + ")";
		}

		/// <summary>
		/// Representação angular do fasor.
		/// </summary>
		/// <returns>Uma string com a representação angular.</returns>
		/// <filterpriority>2</filterpriority>
		public string RepresentacaoAngular() {
			return Amplitude.ToString() + "<" + FaseRadianos*(180d/Math.PI) + "º";
		}

		/// <summary>
		/// Retorna uma string que representa o objeto.
		/// </summary>
		/// <returns>A string que representa o objeto.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString () {
			return string.Format ("[Fasor: Amplitude={0}, Frequencia={1}, FaseRadianos={2}]", Amplitude, FrequenciaAngular, FaseRadianos);
		}

		public static Fasor operator + (Fasor lhs, Fasor rhs){
			double aux1 = lhs.Amplitude*Math.Cos (lhs.FaseRadianos) + rhs.Amplitude*Math.Cos (rhs.FaseRadianos);
			double aux2 = lhs.Amplitude*Math.Sin (lhs.FaseRadianos) + rhs.Amplitude*Math.Sin (rhs.FaseRadianos);

			double amp = Math.Sqrt (Math.Pow (aux1, 2) + Math.Pow (aux2, 2));
			double fase = Math.Atan2 (aux2, aux1);

			return new Fasor (amp, lhs.FrequenciaAngular, fase);
		}

		public static Fasor operator - (Fasor lhs) {
			return new Fasor (lhs.Amplitude, lhs.FrequenciaAngular, lhs.FaseRadianos + Math.PI);
		}

		public static Fasor operator - (Fasor lhs, Fasor rhs) {
			return lhs + (-rhs);
		}

		public static Fasor operator * (Fasor lhs, Fasor rhs) {
			return new Fasor (lhs.Amplitude * rhs.Amplitude, lhs.FrequenciaAngular, lhs.FaseRadianos + rhs.FaseRadianos);
		}

		/// <summary>
		/// Obtêm o fasor inverso (1/fasor).
		/// </summary>
		/// <value>O fasor inverso.</value>
		public Fasor Inverso {
			get { return new Fasor (1 / Amplitude, FrequenciaAngular, 2 * Math.PI - FaseRadianos); }
		}

		public static Fasor operator / (Fasor lhs, Fasor rhs) {
			return lhs * rhs.Inverso;
		}

		/// <summary>
		/// Obtêm o fasor com a parte imaginária conjugada.
		/// </summary>
		/// <value>O fasor conjugado.</value>
		public Fasor Conjugado {
			get { return new Fasor (Amplitude, FrequenciaAngular, 2 * Math.PI - FaseRadianos); }
		}

		/// <summary>
		/// Obtêm o fasor resultante da derivação.
		/// </summary>
		/// <value>O fasor derivado.</value>
		public Fasor Derivado {
			get { return new Fasor (Amplitude * FrequenciaAngular, FrequenciaAngular, FaseRadianos + (Math.PI / 2d)); }
		}

		/// <summary>
		/// Obtêm o fasor resultante da integração.
		/// </summary>
		/// <value>O fasor integrado.</value>
		public Fasor Integrado {
			get { return new Fasor (Amplitude / FrequenciaAngular, FrequenciaAngular, FaseRadianos - (Math.PI / 2d)); }
		}

	}

}
