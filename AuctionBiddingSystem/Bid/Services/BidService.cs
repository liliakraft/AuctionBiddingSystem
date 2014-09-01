using System.Collections.Generic;
using AuctionBiddingSystem.Bid.Interaces;
using AuctionBiddingSystem.Bid.Interaces.Repositories;
using AuctionBiddingSystem.Bid.Interaces.Services;

namespace AuctionBiddingSystem.Bid.Services
{
    public class BidService : IBidService   
    {
        private readonly IValidateBid bidValidator;
        private readonly IBidRepository bidRepository;

        public BidService(IValidateBid bidValidator, IBidRepository bidRepository)
        {
            this.bidValidator = bidValidator;
            this.bidRepository = bidRepository;
        }

        public void SubmitBid(AssetBid assetBid)
        {
            if (IsValidBid(assetBid))
            {
                bidRepository.SaveBid(assetBid);
            }
        }

        public IEnumerable<AssetBid> GetBidsForBiddingRound(int biddingRoundId)
        {
            return bidRepository.GetBidsForRound(biddingRoundId);
        }

        private bool IsValidBid(AssetBid assetBid)
        {
            return bidValidator.ValidateBidNotBelowAssetInitialPrice(assetBid);
        }
    }
}