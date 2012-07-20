using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// ガチャの設定
	/// </summary>
	public class GatyaEnvironment
	{
		/// <summary>
		/// ガチャから出てくる枚数
		/// </summary>
		public uint OutCount { get; private set; }

		/// <summary>
		/// レアの確率
		/// </summary>
		public float RarePercent { get; private set; }

		/// <summary>
		/// 必要コンプ枚数
		/// </summary>
		public uint CompleteCount { get; private set; }

		/// <summary>
		/// 何枚レアカードをコンプしたか
		/// </summary>
		public uint HitCount { get; private set; }

		/// <summary>
		/// 確率変動がかかるコンプ枚数
		/// </summary>
		public uint StartUnhitCount { get; private set; }

		/// <summary>
		/// 確率変動が始まった際のレアがヒットする確率
		/// </summary>
		public float UnhitPercent { get; private set; }

		/// <summary>
		/// カードが何種類存在するか
		/// </summary>
		public uint CardCount { get; private set; }

		/// <summary>
		/// 通常カードの枚数
		/// </summary>
		public uint CommonCount { get { return CardCount - CompleteCount; } }

		/// <summary>
		/// カードのデータベース
		/// </summary>
		public CardDatabase Database { get; private set; }

		/// <summary>
		/// ガチャ本体
		/// </summary>
		/// <param name="card_count">何種類のカードが存在するか</param>
		/// <param name="out_count">出てくる枚数</param>
		/// <param name="rare_percent">レアの確率</param>
		/// <param name="complete_count">コンプがかかる枚数</param>
		/// <param name="start_unhit_count">確率変動がかかるコンプ枚数</param>
		/// <param name="unhit_percent">確率変動した場合のレアがヒットする確率</param>
		public GatyaEnvironment(uint card_count ,uint out_count, float rare_percent, uint complete_count, uint start_unhit_count, float unhit_percent)
		{
			this.CardCount = card_count;
			this.OutCount = out_count;
			this.RarePercent = rare_percent;
			this.CompleteCount = complete_count;
			this.StartUnhitCount = start_unhit_count;
			this.UnhitPercent = unhit_percent;
			this.HitCount = 0;

			// データベースの作成
			this.Database = new CardDatabase(card_count, complete_count);
		}

		/// <summary>
		/// コンプリート枚数を加算する
		/// </summary>
		public void AddHitCount() { this.HitCount++; }
	}
}
