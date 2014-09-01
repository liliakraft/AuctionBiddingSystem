using System.Collections.Generic;

namespace AuctionBiddingSystem.Bid.Interaces.Repositories
{
    public interface IBidRepository
    {
        void SaveBid(AssetBid assetBid);
        IEnumerable<AssetBid> GetBidsForRound(int biddingRoundId);
    }
}