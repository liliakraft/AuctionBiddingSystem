using System.Collections.Generic;
using AuctionBiddingSystem.Auction.Interfaces;
using AuctionBiddingSystem.Auction.Interfaces.Services;
using AuctionBiddingSystem.BiddingRounds;
using AuctionBiddingSystem.BiddingRounds.Interfaces.Services;

namespace AuctionBiddingSystem.Auction
{
    public class Auction : IAuction
    {
        private readonly IAuctionService auctionService;
        private readonly IBiddingRoundService biddingRoundService;
        private IEnumerable<BiddingRound> biddingRounds; 
        private int numberOfBiddingRounds;

        public Auction(IAuctionService auctionService, IBiddingRoundService biddingRoundService)
        {
            this.auctionService = auctionService;
            this.biddingRoundService = biddingRoundService;
        }

        public void SetUpAuction()
        {
            numberOfBiddingRounds = auctionService.GetNumberOfBiddingRounds();

            biddingRounds = biddingRoundService.CreateBiddingRounds(numberOfBiddingRounds);
        }

        public void ProcessBiddingRounds()
        {
            foreach (var biddingRound in biddingRounds)
            {
                switch (biddingRound.State)
                {
                    case BiddingRoundState.NotStarted:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}