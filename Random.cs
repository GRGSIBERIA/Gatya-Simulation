using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GatyaSimulation
{
	/// <summary>
	/// ランダムな値を生成する
	/// </summary>
	public class Random
	{
		/// <summary>
		/// 最大長がlengthの値からランダムに数値を引き出す
		/// </summary>
		/// <param name="length">配列の長さ</param>
		/// <returns>ランダムなインデックス</returns>
		public static uint RandomIndex(uint length)
		{
			// 端数切捨て
			return (uint)(RandomFloat() * length);
		}

		/// <summary>
		/// 0.0 ～ 1.0の間で単精度浮動小数のランダムな値を返す
		/// </summary>
		/// <returns>0.0 ～ 1.0までの実数</returns>
		public static float RandomFloat()
		{
			System.Random rnd = GetRandomHash();
			return (float)rnd.NextDouble();
		}

		/// <summary>
		/// ハッシュ値を使ったランダムの取得
		/// </summary>
		/// <returns>Random</returns>
		private static System.Random GetRandomHash()
		{
			byte[] seed = new byte[4];

			// ハッシュ関数をランダムシードに使う
			RNGCryptoServiceProvider crypt = new RNGCryptoServiceProvider();
			crypt.GetBytes(seed);
			return new System.Random(BitConverter.ToInt32(seed, 0));
		}

		/// <summary>
		/// 最小値～1.0の間で単精度浮動小数のランダムな値を返す
		/// </summary>
		/// <param name="min">最小値</param>
		/// <returns>最小値～1.0までの実数</returns>
		public static float RandomMinClampFloat(float min)
		{
			System.Random rnd = GetRandomHash();
			long value = rnd.Next(0, int.MaxValue);
			return (1 - min) / value + min;
		}
	}
}
