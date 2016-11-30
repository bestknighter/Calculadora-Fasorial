namespace MathCore {

	public struct Complexo {
		
		/// <summary>
		/// Parte real.
		/// </summary>
		public readonly double Real;
		/// <summary>
		/// Parte imaginária
		/// </summary>
		public readonly double Imaginario;
		/// <summary>
		/// Inicializa um número do tipo <see cref="Complexo"/>.
		/// </summary>
		/// <param name="re">Parte real.</param>
		/// <param name="im">Parte Imaginária.</param>
		public Complexo (double re, double im) {
			Real = re;
			Imaginario = im;
		}

		/// <summary>
		/// Retorna uma string que representa o objeto.
		/// </summary>
		/// <returns>A string que representa o objeto.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString () {
			string resp;
			if (Real != 0 && Imaginario != 0) {
				resp = (Imaginario>0) ? (Real + "+j" + Imaginario) : (Real + "-j" + (-1*Imaginario));

			} else if (Real != 0) {
				resp = "" + Real;

			} else if (Imaginario != 0) {
				resp = (Imaginario>0) ? ("j" + Imaginario) : ("-j" + (-1*Imaginario));

			} else {
				resp = "0";
			}
			return resp;
		}

	}

}
