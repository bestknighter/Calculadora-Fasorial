using System;
using MathCore;
using UnityEditor;
using NUnit.Framework;

namespace Tests {

	[TestFixture]
	public class FasorTest {

		static double epsilon = 1e-15d;

		Fasor[] fasores = new Fasor[] {
			new Fasor (1, Math.PI, 1),
			new Fasor (-1, Math.PI, 1),
			new Fasor (1, Math.PI, -1),
			new Fasor (1, Math.PI, 2*Math.PI+1),
			new Fasor (3, Math.PI, 0),
			new Fasor (2, Math.PI, Math.PI),
			new Fasor (0, 1, 1),
			new Fasor (1, 0, 1),
			new Fasor (1, 1, 0),
			new Fasor (1, 0, 0),
			new Fasor (0, 1, 0),
			new Fasor (0, 0, 1),
			new Fasor (0, 0, 0),
			new Fasor (1, 1, 1)
		};

		public FasorTest () {
		}

		[Test]
		public void CreateTest () {
			Assert.AreEqual (1, fasores[0].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[0].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[0].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[1].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[1].FrequenciaAngular, epsilon);
			Assert.AreEqual (Math.PI+1, fasores[1].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[2].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[2].FrequenciaAngular, epsilon);
			Assert.AreEqual (2*Math.PI-1, fasores[2].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[3].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[3].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[3].FaseRadianos, epsilon);

			Assert.AreEqual (3, fasores[4].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[4].FrequenciaAngular, epsilon);
			Assert.AreEqual (0, fasores[4].FaseRadianos, epsilon);

			Assert.AreEqual (2, fasores[5].Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[5].FrequenciaAngular, epsilon);
			Assert.AreEqual (Math.PI, fasores[5].FaseRadianos, epsilon);

			Assert.AreEqual (0, fasores[6].Amplitude, epsilon);
			Assert.AreEqual (1, fasores[6].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[6].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[7].Amplitude, epsilon);
			Assert.AreEqual (0, fasores[7].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[7].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[8].Amplitude, epsilon);
			Assert.AreEqual (1, fasores[8].FrequenciaAngular, epsilon);
			Assert.AreEqual (0, fasores[8].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[9].Amplitude, epsilon);
			Assert.AreEqual (0, fasores[9].FrequenciaAngular, epsilon);
			Assert.AreEqual (0, fasores[9].FaseRadianos, epsilon);

			Assert.AreEqual (0, fasores[10].Amplitude, epsilon);
			Assert.AreEqual (1, fasores[10].FrequenciaAngular, epsilon);
			Assert.AreEqual (0, fasores[10].FaseRadianos, epsilon);

			Assert.AreEqual (0, fasores[11].Amplitude, epsilon);
			Assert.AreEqual (0, fasores[11].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[11].FaseRadianos, epsilon);

			Assert.AreEqual (0, fasores[12].Amplitude, epsilon);
			Assert.AreEqual (0, fasores[12].FrequenciaAngular, epsilon);
			Assert.AreEqual (0, fasores[12].FaseRadianos, epsilon);

			Assert.AreEqual (1, fasores[13].Amplitude, epsilon);
			Assert.AreEqual (1, fasores[13].FrequenciaAngular, epsilon);
			Assert.AreEqual (1, fasores[13].FaseRadianos, epsilon);
		}

		[Test]
		public void RetangularTest () {
			Assert.AreEqual (3d, fasores[4].Retangular().Real, epsilon);
			Assert.AreEqual (0d, fasores[4].Retangular().Imaginario, epsilon);

			Assert.AreEqual (-2d, fasores[5].Retangular().Real, epsilon);
			Assert.AreEqual (0d, fasores[5].Retangular().Imaginario, epsilon);

			Assert.AreEqual (1d, fasores[9].Retangular().Real, epsilon);
			Assert.AreEqual (0d, fasores[9].Retangular().Imaginario, epsilon);
		}

		[Test]
		public void SomaTest() {
			Assert.AreEqual (2d, (fasores[0]+fasores[0]).Amplitude, epsilon);
			Assert.AreEqual (1d, (fasores[0]+fasores[0]).FaseRadianos, epsilon);

			Assert.AreEqual (1d, (fasores[4]+fasores[5]).Amplitude, epsilon);
			Assert.AreEqual (0d, (fasores[4]+fasores[5]).FaseRadianos, epsilon);
		}

		[Test]
		public void SubtracaoTest() {
			Assert.AreEqual (2d, (fasores[0]-fasores[1]).Amplitude, epsilon);
			Assert.AreEqual (1d, (fasores[0]-fasores[1]).FaseRadianos, epsilon);

			Assert.AreEqual (5d, (fasores[4]-fasores[5]).Amplitude, epsilon);
			Assert.AreEqual (0d, (fasores[4]-fasores[5]).FaseRadianos, epsilon);
		}

		[Test]
		public void MultTest() {
			Assert.AreEqual (6d, (fasores[4]*fasores[5]).Amplitude, epsilon);
			Assert.AreEqual (Math.PI, (fasores[4]*fasores[5]).FaseRadianos, epsilon);
		}

		[Test]
		public void DivTest() {
			Assert.AreEqual (1.5d, (fasores[4]/fasores[5]).Amplitude, epsilon);
			Assert.AreEqual (Math.PI, (fasores[4]/fasores[5]).FaseRadianos, epsilon);
		}

		[Test]
		public void ConjTest() {
			Assert.AreEqual (2d, fasores[5].Conjugado.Amplitude, epsilon);
			Assert.AreEqual (Math.PI, fasores[5].Conjugado.FaseRadianos, epsilon);
		}

		[Test]
		public void DerivTest() {
			Assert.AreEqual (2*Math.PI, fasores[5].Derivado.Amplitude, epsilon);
			Assert.AreEqual (3*Math.PI/2, fasores[5].Derivado.FaseRadianos, epsilon);
		}

		[Test]
		public void IntegTest() {
			Assert.AreEqual (2/Math.PI, fasores[5].Integrado.Amplitude, epsilon);
			Assert.AreEqual (Math.PI/2, fasores[5].Integrado.FaseRadianos, epsilon);
		}
	}

}
