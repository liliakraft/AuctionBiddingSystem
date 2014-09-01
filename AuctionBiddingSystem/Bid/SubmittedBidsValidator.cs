using System.Linq;
using AuctionBiddingSystem.Bid.Interaces;
using AuctionBiddingSystem.Bid.Interaces.Services;
using AuctionBiddingSystem.Interfaces.Services;

namespace AuctionBiddingSystem.Bid
{
    public class SubmittedBidsValidator : IValidateSubmittedBids
    {
        private readonly IBidderService bidderService;
        private readonly IBidService bidService;

        public SubmittedBidsValidator(IBidderService bidderService, IBidService bidService)
        {
            this.bidderService = bidderService;
            this.bidService = bidService;
        }

        public bool ValidateThatAllBiddersSubmittedABid(int biddingRoundId)
        {
            var bidders = bidderService.GetBidders();
            var assetBids = bidService.GetBidsForBiddingRound(biddingRoundId);
            return bidders.All(a => assetBids.Any(b => b.Bidder.Name == a.Name));
        }
    }
}