using System.Linq;
using AuctionBiddingSystem.Bid.Interaces;
using AuctionBiddingSystem.Bid.Interaces.Services;
using AuctionBiddingSystem.Interfaces;

namespace AuctionBiddingSystem.Bid
{
    public class WinningBidSetter : ISetWinningBid
    {
        private readonly IBidService bidService;

        public WinningBidSetter(IBidService bidService)
        {
            this.bidService = bidService;
        }

        public void SetWinningBid(int biddingRoundId)
        {
            var bids = bidService.GetBidsForBiddingRound(biddingRoundId);
            bids.OrderByDescending(i => i.BiddingPrice).First().IsWinningBid = true;
        }
    }
}