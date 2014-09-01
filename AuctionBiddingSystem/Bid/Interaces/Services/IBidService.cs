using System.Collections.Generic;

namespace AuctionBiddingSystem.Bid.Interaces.Services
{
    public interface IBidService
    {
        void SubmitBid(AssetBid assetBid);
        IEnumerable<AssetBid> GetBidsForBiddingRound(int biddingRoundId);
    }
}