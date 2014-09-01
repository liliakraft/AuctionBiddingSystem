using AuctionBiddingSystem.Bid.Interaces;

namespace AuctionBiddingSystem.Bid
{
    public class BidValidator : IValidateBid
    {
        public bool ValidateBidNotBelowAssetInitialPrice(AssetBid assetBid)
        {
            return !(assetBid.BiddingPrice < assetBid.Asset.Price);
        }
    }
}