using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatyaSimulation.Gatya
{
	/// <summary>
	/// カード1枚1枚を表す
	/// </summary>
	public class Card
	{
		/// <summary>
		/// カードのレアリティ
		/// </summary>
		public CardRank Rarerity { get; private set; }

		/// <summary>
		/// カード番号
		/// </summary>
		public uint Number { get; private set; }

		/// <summary>
		/// カードを表す
		/// </summary>
		/// <param name="number">カード番号</param>
		/// <param name="rarerity">レアリティ</param>
		public Card(uint number, CardRank rarerity)
		{
			this.Number = number;
			this.Rarerity = rarerity;
		}
	}
}
