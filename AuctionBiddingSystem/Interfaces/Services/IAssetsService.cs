using System.Collections.Generic;

namespace AuctionBiddingSystem.Interfaces.Services
{
    public interface IAssetsService  
    {
        IEnumerable<Asset> GetInitialAssets();
        IEnumerable<Asset> GetAssetsForBiddingRound(int biddingRoundId);
    }
}