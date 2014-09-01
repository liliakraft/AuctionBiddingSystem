using System.Collections.Generic;
using AuctionBiddingSystem.Bid.Interaces;

namespace AuctionBiddingSystem.BiddingRounds.Services
{
    public class BiddingRoundService : Interfaces.Services.IBiddingRoundService
    {
        private readonly IValidateSubmittedBids submittedBidsValidator;
        private readonly ISetWinningBid winningBidValidator;
        private BiddingRound biddingRound;

        public BiddingRoundService(IValidateSubmittedBids submittedBidsValidator, ISetWinningBid setWinningBid)
        {
            this.submittedBidsValidator = submittedBidsValidator;
            this.winningBidValidator = setWinningBid;
        }

        public IEnumerable<BiddingRound> CreateBiddingRounds(int numberOfBiddingRounds)
        {
            var biddingRounds = new List<BiddingRound>();
            //TODO create BidRoundCreator and push down this logic
            for (int biddingRoundId = 1; biddingRoundId <= numberOfBiddingRounds; biddingRoundId++)
            {
                biddingRounds.Add(new BiddingRound
                    {
                        Id = biddingRoundId
                    });
            }
            return biddingRounds;
        }

        private bool AllBidsSubmitted(int biddingRoundId)
        {
            return submittedBidsValidator.ValidateThatAllBiddersSubmittedABid(biddingRoundId);
        }

        public void AssignWinningBid(int biddingRoundId)
        {
            if (AllBidsSubmitted(biddingRoundId))
            {
                winningBidValidator.SetWinningBid(biddingRoundId);
            }
        }
    }
}