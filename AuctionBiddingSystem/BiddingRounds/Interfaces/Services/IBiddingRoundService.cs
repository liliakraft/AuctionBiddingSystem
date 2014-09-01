using System.Collections.Generic;

namespace AuctionBiddingSystem.BiddingRounds.Interfaces.Services
{
    public interface IBiddingRoundService       
    {
        IEnumerable<BiddingRound> CreateBiddingRounds(int numberOfBiddingRounds);
    }
}