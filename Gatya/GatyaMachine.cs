using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// ガチャガチャマシーン
	/// お金の計算とカード配りをする
	/// </summary>
	public class GatyaMachine
	{
		/// <summary>
		/// トータルでかかったコスト
		/// </summary>
		public uint TotalCost { get; private set; }

		/// <summary>
		/// 1回にかかる値段
		/// </summary>
		public uint Cost { get; private set; }

		/// <summary>
		/// トータルの試行回数
		/// </summary>
		public uint TryCount { get; private set; }

		/// <summary>
		/// ガチャの中にどれだけお金が入っているか
		/// 全てのコストの合計になるもののリセットされない
		/// </summary>
		public uint InMoney { get; private set; }

		/// <summary>
		/// 合計で何回試行されたか
		/// リセットされない
		/// </summary>
		public uint InTryCount { get; private set; }

		/// <summary>
		/// ガチャの環境設定
		/// </summary>
		private GatyaEnvironment env = null;

		/// <summary>
		/// 合計で何枚カードが引かれたか
		/// </summary>
		public uint TotalCardCount { get { return TryCount * env.OutCount; } }

		/// <summary>
		/// 全ての合計で何枚カードが引かれたか
		/// リセットされない
		/// </summary>
		public uint InCardCount { get { return InTryCount * env.OutCount; } }

		/// <summary>
		/// コンプできたかどうか
		/// </summary>
		public bool IsComplete { get { return this.env.Database.IsComplete; } }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="cost">1回にかかるコスト</param>
		/// <param name="env">環境設定</param>
		public GatyaMachine(GatyaEnvironment env, uint cost)
		{
			this.env = env;
			this.InMoney = 0;
			this.InTryCount = 0;
			Init(cost);
		}

		/// <summary>
		/// 試行結果をリセットする
		/// </summary>
		public void Reset()
		{
			Init(Cost);
			this.env.Database.Reset();	// データベースの内容もクリアする
		}

		/// <summary>
		/// 初期化を行う
		/// </summary>
		/// <param name="cost">コスト</param>
		private void Init(uint cost)
		{
			this.Cost = cost;
			this.TotalCost = 0;
			this.TryCount = 0;
		}

		/// <summary>
		/// 試行してみる
		/// </summary>
		/// <param name="env">環境設定</param>
		/// <returns>カードの結果</returns>
		public Card[] Try()
		{
			CountUp();
			return CardFactory.CreateCards(this.env);
		}

		/// <summary>
		/// 各種カウンターを上昇させる
		/// </summary>
		private void CountUp()
		{
			this.TryCount++;
			this.InTryCount++;
			this.TotalCost += this.Cost;
			this.InMoney += this.Cost;
		}

		/// <summary>
		/// コンプするまで自動的に繰り返す
		/// </summary>
		public UserInfomation TryCompletely(bool unreset=false)
		{
			while (!this.env.Database.IsComplete)
			{
				CardFactory.CreateCards(this.env);
				CountUp();
			}
			UserInfomation info = new UserInfomation(this.TryCount, this.TotalCost, this.TotalCardCount);
			if (!unreset) Reset();
			return info;
		}
	}
}
