using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// 集めたカードの登録を行う
	/// </summary>
	public class CardDatabase
	{
		/// <summary>
		/// コモンのコレクション
		/// </summary>
		public uint[] Common { get; private set; }

		/// <summary>
		/// レアカードのコレクション
		/// </summary>
		public uint[] Rare { get; private set; }

		/// <summary>
		/// カードの全種類の枚数
		/// </summary>
		public int CardCount { get { return Common.Length + Rare.Length; } }

		/// <summary>
		/// コンプリートできているかどうか
		/// UnHitCountが0枚であればコンプしている
		/// </summary>
		public bool IsComplete { get { return UnHitCount == 0 ? true : false; } }

		/// <summary>
		/// コンプしてない枚数を調べる
		/// </summary>
		public uint UnHitCount
		{
			get
			{
				uint result = 0;
				for (int i = 0; i < Rare.Length; i++) 
					if (Rare[i] == 0) result++;
				return result;
			}
		}

		/// <summary>
		/// コンプした枚数を調べる
		/// </summary>
		public uint HitCount
		{
			get
			{
				uint result = 0;
				for (int i = 0; i < Rare.Length; i++)
					if (Rare[i] > 0) result++;
				return result;
			}
		}

		/// <summary>
		/// 既にヒットしたレアカードのインデックスを取得する
		/// </summary>
		public uint[] HittedRareIndices
		{
			get
			{
				List<uint> hit = new List<uint>();
				for (uint i = 0; i < Rare.Length; i++)
					if (Rare[i] > 0) hit.Add(i);
				return hit.ToArray();
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="card_count">カードの種類</param>
		/// <param name="complete_count">コンプリートに必要な数</param>
		public CardDatabase(uint card_count, uint complete_count)
		{
			Reset(card_count, complete_count);
		}

		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="card_count">カードの種類</param>
		/// <param name="complete_count">コンプリートに必要な数</param>
		private void Reset(uint card_count, uint complete_count)
		{
			this.Common = new uint[card_count - complete_count];
			this.Rare = new uint[complete_count];
		}

		/// <summary>
		/// リセットする
		/// </summary>
		public void Reset()
		{
			this.Common = new uint[this.Common.Length];
			this.Rare = new uint[this.Rare.Length];
		}

		/// <summary>
		/// コンプしたカードの登録を行う
		/// </summary>
		/// <param name="cards"></param>
		public void EntryCards(Card[] cards)
		{
			for (uint i = 0; i < cards.Length; i++)
			{
				switch (cards[i].Rarerity)
				{
					case CardRank.Common:
						Common[cards[i].Number]++;
						break;

					case CardRank.Rare:
						Rare[cards[i].Number]++;
						break;
				}
			}
		}
	}
}
