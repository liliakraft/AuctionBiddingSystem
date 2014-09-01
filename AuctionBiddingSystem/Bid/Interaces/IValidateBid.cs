namespace AuctionBiddingSystem.Bid.Interaces
{
    public interface IValidateBid
    {
        bool ValidateBidNotBelowAssetInitialPrice(AssetBid assetBid);
    }
}