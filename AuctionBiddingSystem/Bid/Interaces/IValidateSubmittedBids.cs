namespace AuctionBiddingSystem.Bid.Interaces
{
    public interface IValidateSubmittedBids 
    {
        bool ValidateThatAllBiddersSubmittedABid(int biddingRoundId);
    }
}