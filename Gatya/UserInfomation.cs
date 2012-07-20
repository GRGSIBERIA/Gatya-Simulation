using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// 1ユーザがコンプ試行した情報
	/// </summary>
	public class UserInfomation
	{
		/// <summary>
		/// コンプまでに試行した回数
		/// </summary>
		public uint TryCount { get; private set; }

		/// <summary>
		/// コンプまでにかかった金額
		/// </summary>
		public uint TotalCost { get; private set; }

		/// <summary>
		/// コンプしたら余ったカードの枚数
		/// </summary>
		public uint TotalCards { get; private set; }

		/// <summary>
		/// デフォルトコンストラクタは禁止
		/// </summary>
		private UserInfomation() { }

		/// <summary>
		/// カードをコンプしたときのユーザ情報
		/// </summary>
		/// <param name="try_count">試行回数</param>
		/// <param name="total_cost">合計でかかったお金</param>
		/// <param name="total_cards">合計のカード枚数</param>
		public UserInfomation(uint try_count, uint total_cost, uint total_cards)
		{
			this.TryCount = try_count;
			this.TotalCost = total_cost;
			this.TotalCards = total_cards;
		}

		public void Dump()
		{
			Console.WriteLine("{0}回挑戦しました", TryCount);
			Console.WriteLine("コンプするのに{0}円かかりました", TotalCost);
			Console.WriteLine("この人が持っているカードは{0}枚です", TotalCards);
			Console.WriteLine("--------------");
		}
	}
}
