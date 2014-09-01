using System.Collections.Generic;
using AuctionBiddingSystem.Auction;
using AuctionBiddingSystem.Auction.Interfaces.Services;
using AuctionBiddingSystem.BiddingRounds;
using AuctionBiddingSystem.BiddingRounds.Interfaces.Services;
using Moq;
using NUnit.Framework;

namespace AuctionBiddingSystemTests
{
    [TestFixture]
    public class AuctionTests
    {
        private const int NumberOfBiddingRounds = 3;
        private Auction auction;
        private Mock<IAuctionService> auctionServiceMock;
        private Mock<IBiddingRoundService> biddingRoundServiceMock;
        private List<BiddingRound> biddingRounds;

        [SetUp]
        public void SetUp()
        {
            biddingRounds = new List<BiddingRound>
                {
                    new BiddingRound{Id =1, State = BiddingRoundState.Finished},
                    new BiddingRound{Id =2, State = BiddingRoundState.InProgress},
                    new BiddingRound{Id = 3, State = BiddingRoundState.NotStarted},
                };
            auctionServiceMock = new Mock<IAuctionService>();
            auctionServiceMock.Setup(i => i.GetNumberOfBiddingRounds()).Returns(NumberOfBiddingRounds);
            biddingRoundServiceMock = new Mock<IBiddingRoundService>();
            biddingRoundServiceMock.Setup(i => i.CreateBiddingRounds(It.IsAny<int>())).Returns(biddingRounds);
        }

        private void CreateAuction()
        {
            auction = new Auction(auctionServiceMock.Object, biddingRoundServiceMock.Object);
        }

        [Test]
        public void Should_call_auction_service_to_get_numer_of_bidding_rounds()
        {
            auctionServiceMock.Setup(i => i.GetNumberOfBiddingRounds()).Returns(It.IsAny<int>());
            CreateAuction();

            auction.SetUpAuction();

            auctionServiceMock.Verify(i => i.GetNumberOfBiddingRounds(), Times.Once);
        }

        [Test]
        public void Should_call_bidding_round_service_to_create_bidding_rounds()
        {
            CreateAuction();

            auction.SetUpAuction();

            biddingRoundServiceMock.Verify(i=>i.CreateBiddingRounds(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Should_take_no_action_when_processing_bidding_round_with_status_not_started()
        {
            biddingRounds = new List<BiddingRound> {new BiddingRound {Id = 1, State = BiddingRoundState.NotStarted}};
            biddingRoundServiceMock.Setup(i => i.CreateBiddingRounds(It.IsAny<int>())).Returns(biddingRounds);

            CreateAuction();

            auction.SetUpAuction();

            auction.ProcessBiddingRounds();
        }
    }
}
            