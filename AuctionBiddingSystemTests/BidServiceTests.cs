using AuctionBiddingSystem.Bid;
using AuctionBiddingSystem.Bid.Interaces;
using AuctionBiddingSystem.Bid.Interaces.Repositories;
using AuctionBiddingSystem.Bid.Services;
using Moq;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class BidServiceTests
    {
        private bool bidIsValid;
        private Mock<IValidateBid> bidValidatorMock;
        private Mock<IBidRepository> bidRepositoryMock;
        private BidService bidService;

        [SetUp]
        public void SetUp()
        {
            bidValidatorMock = new Mock<IValidateBid>();
            bidRepositoryMock = new Mock<IBidRepository>();
        }

        public void CreateBidService()
        {
            bidValidatorMock.Setup(i => i.ValidateBidNotBelowAssetInitialPrice(It.IsAny<AssetBid>())).Returns(bidIsValid);
            bidService = new BidService(bidValidatorMock.Object, bidRepositoryMock.Object);            
        }

        [Test]
        public void Should_call_bid_validator_when_submitting_a_bid()
        {
            CreateBidService();

            bidService.SubmitBid(new AssetBid());

            bidValidatorMock.Verify(i => i.ValidateBidNotBelowAssetInitialPrice(It.IsAny<AssetBid>()), Times.Once);
        }

        [Test]
        public void Should_call_bid_repository_when_submitting_a_valid_bid()
        {
            bidIsValid = true;
            CreateBidService();

            bidService.SubmitBid(new AssetBid());

            bidRepositoryMock.Verify(i => i.SaveBid(It.IsAny<AssetBid>()), Times.Once);
        }

        [Test]
        public void Should_call_bid_repository_when_getting_bids_for_round()
        {
            const int biddingRoundId = 1;
            CreateBidService();

            bidService.GetBidsForBiddingRound(biddingRoundId);

            bidRepositoryMock.Verify(i =>i.GetBidsForRound(It.IsAny<int>()), Times.Once);
        }
    }
}
