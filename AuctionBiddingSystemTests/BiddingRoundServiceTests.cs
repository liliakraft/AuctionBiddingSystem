using AuctionBiddingSystem.Bid.Interaces;
using AuctionBiddingSystem.Bid.Interaces.Services;
using AuctionBiddingSystem.BiddingRounds.Services;
using AuctionBiddingSystem.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class BiddingRoundServiceTests
    {
        private const int biddingRoundId = 1;
        private Mock<IAssetsService> assetserviceMock;
        private Mock<IBidService> bidServiceMock;
        private BiddingRoundService biddingRoundService;
        private Mock<IValidateSubmittedBids> submittedBidsValidator;
        private Mock<ISetWinningBid> winningBidValidatorMock;

        [SetUp]
        public void SetUp()
        {
            assetserviceMock = new Mock<IAssetsService>();
            bidServiceMock = new Mock<IBidService>();
            submittedBidsValidator = new Mock<IValidateSubmittedBids>();
            winningBidValidatorMock = new Mock<ISetWinningBid>();
        }

        public void CreateBiddingRoundService()
        {
            biddingRoundService = new BiddingRoundService(submittedBidsValidator.Object, winningBidValidatorMock.Object);
        }

        [Test]
        public void Should_validate_submitted_bids_when_assigning_winning_bid()
        {
            CreateBiddingRoundService();

            biddingRoundService.AssignWinningBid(biddingRoundId);

            submittedBidsValidator.Verify(i => i.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Should_call_winning_bid_validator_when_assigning_winning_bid()
        {
            submittedBidsValidator.Setup(i => i.ValidateThatAllBiddersSubmittedABid(It.IsAny<int>())).Returns(true);
            CreateBiddingRoundService();

            biddingRoundService.AssignWinningBid(biddingRoundId);

            winningBidValidatorMock.Verify(i => i.SetWinningBid(It.IsAny<int>()), Times.Once);
        }
    }
}
